using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapEditor.Core.Commands;
using MapEditor.Core.EventsArgs;
using MapEditor.Core.PaintTools;
using MapEditor.UI;

namespace MapEditor.Models
{
    [XmlRoot("Map")]
    public class Map
    {
        #region Fields
        
        private Vector2 cameraPosition;
        private Vector2 currentMousePosition;
        private Vector2 previousMousePosition;

        private Texture2D pixel;

        private CommandManager commandManager;

        private bool isMouseLeftPressed;
        private bool isMouseMiddlePressed;

        private IPaintTool[] paintTools;

        private PaintTool paintTool;

        #endregion

        #region Properties

        /// <summary>
        /// Occurs when Camera has been changed
        /// </summary>
        public event CameraEventHandler OnCameraChanged;

        /// <summary>
        /// Occurs when layer index is changed
        /// </summary>
        public event LayerEventHandler OnLayerChanged;

        /// <summary>
        /// Occurs when when map is changed
        /// </summary>
        public event Action OnMapChanged;

        /// <summary>
        /// Get or set the camera
        /// </summary>
        [XmlIgnore()]
        public Camera Camera { get; set; }
        
        /// <summary>
        /// Gets or sets the tile brush values
        /// </summary>
        [XmlIgnore()]
        public int[,] TileBrushValues { get; set; }

        /// <summary>
        /// List of copied tile brush values
        /// </summary>
        [XmlIgnore()]
        public List<int[,]> Clipboard = new List<int[,]>();

        /// <summary>
        /// Gets or sets current layer index
        /// </summary>
        [XmlIgnore()]
        public int LayerIndex { get; set; }

        /// <summary>
        /// Gets current tileset
        /// </summary>
        public Tileset Tileset { get; set; }

        public List<Layer> Layers = new List<Layer>();

        [XmlIgnore()]
        public Vector2? SelectionBoxA { get; set; }
        [XmlIgnore()]
        public Vector2? SelectionBoxB { get; set; }

        [XmlIgnore()]
        public Rectangle SelectionBox
        {
            get
            {
                if (SelectionBoxA == null || SelectionBoxB == null)
                    return Rectangle.Empty;

                return new Rectangle((int)Math.Min(SelectionBoxA.Value.X, SelectionBoxB.Value.X),
                   (int)Math.Min(SelectionBoxA.Value.Y, SelectionBoxB.Value.Y),
                   (int)Math.Abs(SelectionBoxA.Value.X - SelectionBoxB.Value.X),
                   (int)Math.Abs(SelectionBoxA.Value.Y - SelectionBoxB.Value.Y));
            }
        }

        public int MapWidth
        {
            get
            {
                if (Layers.Count <= 0)
                    return 0;
                return Layers.First().LayerWidth;
            }
        }

        public int MapHeight
        {
            get
            {
                if (Layers.Count <= 0)
                    return 0;
                return Layers.First().LayerHeight;
            }
        }

        public int TileWidth
        {
            get { return Tileset.TileWidth; }
        }

        public int TileHeight
        {
            get { return Tileset.TileHeight; }
        }

        [XmlIgnore()]
        public Layer CurrentLayer
        {
            get 
            {
                if (LayerIndex < 0)
                    return null;
                return Layers[LayerIndex];
            }
        }

        public string MapName { get; set; }

        #endregion

        #region Initialize
 
        public void InitializeMap(GraphicsDevice graphicsDevice, string tilesetPath, int tileWidth, int tileHeight, string mapName)
        {
            Camera = new Camera() { Zoom = 1f };

            using (FileStream fileStream = new FileStream(tilesetPath, FileMode.Open))
            {
                Tileset = new Tileset() { Texture = Texture2D.FromStream(graphicsDevice, fileStream), TexturePath = tilesetPath, TileWidth = tileWidth, TileHeight = tileHeight };
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                System.Drawing.Bitmap bitmap = MapEditor.Properties.Resources.Pixel;

                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);

                memoryStream.Seek(0, SeekOrigin.Begin);

                pixel = Texture2D.FromStream(graphicsDevice, memoryStream);
            }

            commandManager = new CommandManager();

            paintTools = new IPaintTool[] { new DrawPaintTool(this), new ErasePaintTool(this), new SelectPaintTool(this), new FillPaintTool(this) };

            MapName = mapName;
        }

        #endregion

        #region Methods

        public void MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Delta > 0)
                ZoomIn();
            else if (e.Delta < 0)
                ZoomOut();

            if (OnCameraChanged != null)
                OnCameraChanged(new CameraEventArgs(new Camera() { Position = Camera.Position, Zoom = Camera.Zoom, }));
        }

        public void MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            paintTools[(int)paintTool].MouseDown(sender, e);

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                isMouseLeftPressed = true;
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (paintTool != PaintTool.Erase)
                    SetPaintTool(PaintTool.Select);
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Middle)
            {
                isMouseMiddlePressed = true;
                previousMousePosition = InvertCameraMatrix(e.Location);

                if(OnCameraChanged != null)
                    OnCameraChanged(new CameraEventArgs(new Camera() { Position = Camera.Position, Zoom = Camera.Zoom, }));
            }
        }

        public void MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            paintTools[(int)paintTool].MouseUp(sender, e);

            if (isMouseLeftPressed)
            {
                isMouseLeftPressed = false;

                if (OnLayerChanged != null)
                    OnLayerChanged(new LayerEventArgs(LayerIndex));
            }

            if (isMouseMiddlePressed)
                isMouseMiddlePressed = false;
        }

        public void MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            paintTools[(int)paintTool].MouseMove(sender, e);

            if (isMouseMiddlePressed)
            {
                currentMousePosition = InvertCameraMatrix(e.Location);

                Vector2 difference = currentMousePosition - previousMousePosition;

                cameraPosition += -difference;

                if (cameraPosition.X < 0)
                    cameraPosition.X = 0;
                if (cameraPosition.Y < 0)
                    cameraPosition.Y = 0;
                if (cameraPosition.X >= MapWidth * TileWidth)
                    cameraPosition.X = MapWidth * TileWidth;
                if (cameraPosition.Y >= MapHeight * TileHeight)
                    cameraPosition.Y = MapHeight * TileHeight;

                ScrollCamera(cameraPosition);

                if(OnCameraChanged != null)
                    OnCameraChanged(new CameraEventArgs(new Camera() { Position = Camera.Position, Zoom = Camera.Zoom, }));
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.GraphicsDevice.Clear(Color.DimGray);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, Camera.CameraTransformation);

            if (TileWidth <= 0 || TileHeight <= 0)
                return;

            // Draw only tiles within viewport
            int left = (int)Math.Floor(Camera.Position.X / TileWidth);
            int right = (int)(TileWidth + left + spriteBatch.GraphicsDevice.Viewport.Width / TileWidth / Camera.Zoom);
            right = Math.Min(right, MapWidth);

            int top = (int)Math.Floor(Camera.Position.Y / TileHeight);
            int bottom = (int)(TileHeight + top + spriteBatch.GraphicsDevice.Viewport.Height / TileHeight / Camera.Zoom);
            bottom = Math.Min(bottom, MapHeight);

            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    Layers.ForEach(layer =>
                        {
                            if (layer.IsVisible)
                            {
                                if (Tileset.Texture != null)
                                {
                                    int tileID = layer.Rows[y].Columns[x].TileID;

                                    if (tileID > -1)
                                    {
                                        spriteBatch.Draw(Tileset.Texture,
                                            new Rectangle((x * Tileset.TileWidth),
                                                (y * Tileset.TileHeight),
                                                Tileset.TileWidth,
                                                Tileset.TileHeight),
                                                Tileset.GetSourceRectangle(tileID),
                                                Color.White);
                                    }

                                }
                            }
                        });
                }
            }

            // Draw current paint tool
            if (paintTools[(int)paintTool] != null)
                paintTools[(int)paintTool].Draw(spriteBatch);

            // Draw selection box
            if (!SelectionBox.IsEmpty)
                DrawingTool.DrawRectangle(spriteBatch, pixel, SelectionBox, Color.White, 2);

            spriteBatch.End();
        }

        private void ScrollCamera(Vector2 position)
        {
            Camera.Position = new Vector2(MathHelper.Clamp(position.X, 0, MapWidth * TileWidth),
            (MathHelper.Clamp(position.Y, 0, MapHeight * TileHeight)));
        }

        public void ClearSelectionBox()
        {
            SelectionBoxA = null;
            SelectionBoxB = null;
        }

        public void SetPaintTool(PaintTool paintTool)
        {
            switch (paintTool)
            {
                case PaintTool.Draw:
                    paintTool = PaintTool.Draw;
                    ClearSelectionBox();
                    break;
                case PaintTool.Erase:
                    paintTool = PaintTool.Erase;
                    RemoveTiles();
                    break;
                case PaintTool.Fill:
                    paintTool = PaintTool.Fill;
                    ClearSelectionBox();
                    break;
                case PaintTool.Select:
                    paintTool = PaintTool.Select;
                    break;
            }
            this.paintTool = paintTool;
        }

        public void CopySelection()
        {
            if (paintTool == PaintTool.Select)
            {
                if (!SelectionBox.IsEmpty)
                {
                    commandManager.ExecuteEditCopyCommand(CurrentLayer, SelectionBox, TileWidth, TileHeight, Clipboard);

                    TileBrushValues = Clipboard.FirstOrDefault();

                    Clipboard.Clear();

                    SetPaintTool(PaintTool.Draw);

                    ClearSelectionBox();

                    if (OnMapChanged != null)
                        OnMapChanged();
                }
            }
        }

        /// <summary>
        /// Cut selected tiles
        /// </summary>
        public void CutSelection()
        {
            if (paintTool == PaintTool.Select)
            {
                if (!SelectionBox.IsEmpty)
                {
                    if (CurrentLayer == null)
                        return;

                    commandManager.ExecuteEditCutCommand(CurrentLayer, SelectionBox, TileWidth, TileHeight, Clipboard);

                    TileBrushValues = Clipboard.FirstOrDefault();

                    Clipboard.Clear();

                    SetPaintTool(PaintTool.Draw);

                    ClearSelectionBox();

                    if (OnMapChanged != null)
                        OnMapChanged();
                }
            }
        }

        public void Fill(TileBrush tileBrush, int target)
        {
            if (paintTool == PaintTool.Fill)
            {
                if (TileBrushValues != null)
                {
                    if (CurrentLayer == null)
                        return;

                    commandManager.ExecuteEditFillCommand(tileBrush, CurrentLayer, target);

                    if (OnMapChanged != null)
                        OnMapChanged();
                }
            }
        }

        /// <summary>
        /// Add layer to map
        /// </summary>
        /// <param name="checkedListBox"></param>
        public void AddLayer(int layerWidth, int layerHeight)
        {
            commandManager.ExecuteLayerAddCommand(Layers, "Layer", layerWidth, layerHeight);

            LayerIndex = Layers.Count - 1;

            if (OnLayerChanged != null)
                OnLayerChanged(new LayerEventArgs(LayerIndex));
        }

        /// <summary>
        /// Remove layer from map
        /// </summary>
        /// <param name="checkedListBox"></param>
        public void RemoveLayer()
        {
            if (CurrentLayer == null)
                return;

            commandManager.ExecuteLayerRemoveCommand(Layers, LayerIndex);

            LayerIndex = -1;

            if (OnLayerChanged != null)
                OnLayerChanged(new LayerEventArgs(LayerIndex));
        }

        /// <summary>
        /// Duplicate layer 
        /// </summary>
        public void CloneLayer()
        {
            if (CurrentLayer == null)
                return;

            commandManager.ExecuteLayerClone(Layers, LayerIndex);

            LayerIndex = Layers.Count - 1;

            if (OnLayerChanged != null)
                OnLayerChanged(new LayerEventArgs(LayerIndex));
        }

        /// <summary>
        /// Move layer one index up
        /// </summary>
        /// <param name="checkedListBox"></param>
        public void RaiseLayer()
        {
            if (CurrentLayer == null)
                return;

            if (LayerIndex <= 0)
                return;


            commandManager.ExecuteLayerRaise(Layers, LayerIndex);

            LayerIndex = (int)MathHelper.Clamp(LayerIndex -1, 0, Layers.Count - 1);

            if (OnLayerChanged != null)
                OnLayerChanged(new LayerEventArgs(LayerIndex));
        }

        /// <summary>
        /// Move layer one index down
        /// </summary>
        /// <param name="checkedListBox"></param>
        public void LowerLayer()
        {
            if (LayerIndex < 0)
                return;

            if (LayerIndex >= Layers.Count - 1)
                return;

            if (CurrentLayer == null)
                return;          

            commandManager.ExecuteLayerLower(Layers, LayerIndex);
            
            LayerIndex = (int)MathHelper.Clamp(LayerIndex + 1, 0, Layers.Count - 1);

            if (OnLayerChanged != null)
                OnLayerChanged(new LayerEventArgs(LayerIndex));
        }

        /// <summary>
        /// Set layers visibility
        /// </summary>
        /// <param name="isVisible"></param>
        public void SetLayerVisibility()
        {
            if (CurrentLayer == null)
                return;

            commandManager.ExecuteLayerVisibility(CurrentLayer);

            if (OnLayerChanged != null)
                OnLayerChanged(new LayerEventArgs(LayerIndex));
        }

        /// <summary>
        /// Sets current layers name
        /// </summary>
        /// <param name="name"></param>
        public void RenameLayer(string name)
        {
            if (CurrentLayer == null)
                return;

            commandManager.ExecuteRenameLayer(CurrentLayer, name);

            if (OnLayerChanged != null)
                OnLayerChanged(new LayerEventArgs(LayerIndex));
        }

        /// <summary>
        /// Draw temporary tilebrushes into map
        /// </summary>
        /// <param name="tileBrushes"></param>
        public void DrawTileBrushes(TileBrushCollection tileBrushes)
        {
            if (CurrentLayer == null)
                return;

            commandManager.ExecuteEditDrawCommand(CurrentLayer, tileBrushes.TileBrushes);

            if (OnMapChanged != null)
                OnMapChanged();
        }

        /// <summary>
        /// Remove selected tiles from map
        /// </summary>
        /// <param name="tileBrushes"></param>
        public void RemoveTiles()
        {
            if (CurrentLayer == null)
                return;

            commandManager.ExecuteEditRemoveCommand(CurrentLayer, SelectionBox, TileWidth, TileHeight);

            if (OnMapChanged != null)
                OnMapChanged();
        }

        /// <summary>
        /// Offset map in tile measurement
        /// </summary>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        public void OffsetMap(int offsetX, int offsetY)
        {
            if (Layers.Count <= 0)
                return;

            commandManager.ExecuteMapOffset(Layers, offsetX, offsetY);

            if (OnMapChanged != null)
                OnMapChanged();
        }

        /// <summary>
        /// Resize map in tile measurement
        /// </summary>
        /// <param name="mapWidth"></param>
        /// <param name="mapHeight"></param>
        public void ResizeMap(int mapWidth, int mapHeight)
        {
            if (Layers.Count <= 0)
                return;

            commandManager.ExecuteMapResize(Layers, mapWidth, mapHeight);

            if (OnMapChanged != null)
                OnMapChanged();
        }

        /// <summary>
        /// Resize tiles in map and tilesets
        /// </summary>
        /// <param name="tileWidth"></param>
        /// <param name="tileHeight"></param>
        public void ResizeTiles(int tileWidth, int tileHeight)
        {            
            commandManager.ExecuteTileResize(Tileset, tileWidth, tileHeight);

            if (OnMapChanged != null)
                OnMapChanged();
        }

        /// <summary>
        /// Undo previous action
        /// </summary>
        public void Undo()
        {
            commandManager.Undo();

            LayerIndex = -1;

            if (OnLayerChanged != null)
                OnLayerChanged(new LayerEventArgs(LayerIndex));

            if (OnMapChanged != null)
                OnMapChanged();
        }

        /// <summary>
        /// Redo previous action
        /// </summary>
        public void Redo()
        {
            commandManager.Redo();

            LayerIndex = -1;

            if (OnLayerChanged != null)
                OnLayerChanged(new LayerEventArgs(LayerIndex));

            if (OnMapChanged != null)
                OnMapChanged();
        }

        /// <summary>
        /// Clears undo/redo stacks
        /// </summary>
        public void ClearUndoRedo()
        {
            commandManager.ClearAll();
        }

        /// <summary>
        /// Zoom into map by 10%
        /// </summary>
        public void ZoomIn()
        {
            float zoom = Camera.Zoom;
            zoom += 0.1f;

            Camera.Zoom = MathHelper.Clamp(zoom, 0.5f, 2f);

            if (OnMapChanged != null)
                OnMapChanged();
        }

        /// <summary>
        /// Zoom out of map by 10%
        /// </summary>
        public void ZoomOut()
        {
            float zoom = Camera.Zoom;
            zoom -= 0.1f;

            Camera.Zoom = MathHelper.Clamp(zoom, 0.5f, 2f);

            if (OnMapChanged != null)
                OnMapChanged();
        }

        /// <summary>
        /// Rounds off position to fit with the map
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Vector2 SnapToGrid(Vector2 position)
        {
            if (TileWidth <= 0)
                return Vector2.Zero;
            if (TileHeight <= 0)
                return Vector2.Zero;

            int x = (int)position.X / TileWidth;
            int y = (int)position.Y / TileHeight;

            return new Vector2(x * TileWidth, y * TileHeight);
        }

        /// <summary>
        /// Changes set of coordinates on map to pixel length
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Vector2 CoordinateToPixels(int x, int y)
        {
            return new Vector2(x * TileWidth, y * TileHeight);
        }

        /// <summary>
        /// Changes position to set of coordnates on map
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Vector2 PixelsToCoordinate(Vector2 position)
        {
            if (TileWidth <= 0)
                return Vector2.Zero;
            if (TileHeight <= 0)
                return Vector2.Zero;

            return new Vector2((int)position.X / TileWidth, (int)position.Y / TileHeight);
        }

        /// <summary>
        /// Transforms position to adjust itself to scrollable maps
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public Vector2 InvertCameraMatrix(System.Drawing.Point point)
        {
            return Vector2.Transform(new Vector2(point.X, point.Y), Matrix.Invert(Camera.CameraTransformation));
        }


        
        #endregion
    }
}
