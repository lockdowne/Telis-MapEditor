using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapEditor.Core;
using MapEditor.Core.Commands;
using MapEditor.Core.Controls;
using MapEditor.Core.PaintTools;
using MapEditor.UI;
using MapEditor.Models;

namespace MapEditor.Presenters
{
    public class MainRenderPresenter : IMainRenderPresenter
    {
        #region Fields

        private readonly IXnaRenderView view;

        private Camera camera;

        private Vector2 cameraPosition;
        private Vector2 currentMousePosition;
        private Vector2 previousMousePosition;

        public Texture2D Pixel { get; set; }     

        private CommandManager commandManager;

        private int layerCounter;

        private bool isMouseLeftPressed;
        private bool isMouseMiddlePressed;

        #endregion

        #region Properties

        public event Action MapChanged;

        public int[,] TileBrushValues { get; set; }

        public List<int[,]> Clipboard;

        public int LayerIndex {  get; set; }
        public int TilesetIndex { get; set; }
        
        public List<Tileset> Tilesets { get; set; }
        public List<Layer> Layers { get; set; }

        public Vector2? BeginSelectionBox { get; set; }
        public Vector2? EndSelectionBox { get; set; }        

        private IPaintTool[] paintTools;

        private PaintTool currentPaintTool;

        public Rectangle SelectionBox
        {
            get
            {
                if (BeginSelectionBox == null || EndSelectionBox == null)
                    return Rectangle.Empty;

                return new Rectangle((int)Math.Min(BeginSelectionBox.Value.X, EndSelectionBox.Value.X),
                   (int)Math.Min(BeginSelectionBox.Value.Y, EndSelectionBox.Value.Y),
                   (int)Math.Abs(BeginSelectionBox.Value.X - EndSelectionBox.Value.X),
                   (int)Math.Abs(BeginSelectionBox.Value.Y - EndSelectionBox.Value.Y));
            }
        }

        public int MapWidth
        {
            get
            {
                if (Layers.Count <= 0)
                    return 0;

                return Layers.First().MapWidth;
            }
        }

        public int MapHeight
        {
            get
            {
                if (Layers.Count <= 0)
                    return 0;

                return Layers.First().MapHeight;
            }
        }

        public int TileWidth
        {
            get
            {
                if (Tilesets.Count <= 0)
                    return 0;

                return Tilesets.First().TileWidth;
            }
        }

        public int TileHeight
        {
            get
            {
                if (Tilesets.Count <= 0)
                    return 0;

                return Tilesets.First().TileHeight;
            }
        }

        #endregion

        #region Initialize

        public MainRenderPresenter(IXnaRenderView view)
        {
            this.view = view;

            view.OnDraw += new Action<SpriteBatch>(view_OnDraw);
            view.OnInitialize += new Action(view_OnInitialize);
            view.OnXnaDown += new MouseEventHandler(view_OnXnaDown);
            view.OnXnaUp += new MouseEventHandler(view_OnXnaUp);
            view.OnXnaMove += new MouseEventHandler(view_OnXnaMove);
            view.OnXnaWheel += new MouseEventHandler(view_OnXnaWheel);

            Tilesets = new List<Tileset>();
            Layers = new List<Layer>();

            // Move to initialize method
            camera = new Camera()
            {
                Zoom = 1f,
            };

            Clipboard = new List<int[,]>();

            using (MemoryStream memoryStream = new MemoryStream())
            {
                System.Drawing.Bitmap bitmap = MapEditor.Properties.Resources.Pixel;

                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);

                memoryStream.Seek(0, SeekOrigin.Begin);

                Pixel = Texture2D.FromStream(view.GetGraphicsDevice, memoryStream);
            }

            commandManager = new CommandManager();

            paintTools = new IPaintTool[] { new DrawPaintTool(this), new ErasePaintTool(this), new SelectPaintTool(this) };
        }

      
       

        #endregion
        
        #region Events

        void view_OnXnaWheel(object sender, MouseEventArgs e)
        {
            // Position => wheel up => zoom in
            // Negative => wheel down => zoom out
            float zoom = camera.Zoom;

            if (e.Delta > 0)
                zoom += 0.075f;
            else if (e.Delta < 0)
                zoom -= 0.075f;

            camera.Zoom = MathHelper.Clamp(zoom, 0.5f, 2f);



        }

        void view_OnXnaMove(object sender, MouseEventArgs e)
        {
            paintTools[(int)currentPaintTool].OnMouseMove(sender, e);


            if (isMouseMiddlePressed)
            {
                currentMousePosition = InvertCameraMatrix(e.Location);

                Vector2 difference = currentMousePosition - previousMousePosition;
                
                cameraPosition += -difference;
                //cameraPosition.Normalize();

                Console.WriteLine(cameraPosition.ToString());

                ScrollCamera(cameraPosition);
            }
        }

        void view_OnXnaUp(object sender, MouseEventArgs e)
        {
            paintTools[(int)currentPaintTool].OnMouseUp(sender, e);

            if (isMouseLeftPressed)
            {
                isMouseLeftPressed = false;

                if (MapChanged != null)
                    MapChanged();
            }

            if (isMouseMiddlePressed)
            {
                isMouseMiddlePressed = false;
            }
        }
       
        void view_OnXnaDown(object sender, MouseEventArgs e)
        {
            paintTools[(int)currentPaintTool].OnMouseDown(sender, e);

            switch (e.Button)
            {
                case MouseButtons.Left:
                    isMouseLeftPressed = true;
                    break;
                case MouseButtons.Right:
                    SetPaintTool(PaintTool.Select);
                    break;             
                case MouseButtons.Middle:
                    isMouseMiddlePressed = true;
                    previousMousePosition = InvertCameraMatrix(e.Location);
                    break;
            }            
        }

        void view_OnInitialize()
        {
            // TODO: FIX
        }

        void view_OnDraw(SpriteBatch spriteBatch)
        {
            view.GetGraphicsDevice.Clear(Color.DimGray);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, camera.CameraTransformation);

            int tileWidth = 0;
            int tileHeight = 0;
            int mapWidth = 0;
            int mapHeight = 0;

            if (Tilesets.Count > 0)
            {
                tileWidth = Tilesets.First().TileWidth;
                tileHeight = Tilesets.First().TileHeight;
            }

            if (Layers.Count > 0)
            {
                mapWidth = Layers.First().MapWidth;
                mapHeight = Layers.First().MapHeight;
            }

            if (tileWidth <= 0 || tileHeight <= 0)
                return;

            // Draw only viewport
            int left = (int)Math.Floor(camera.Position.X / tileWidth);
            int right = tileWidth + left + spriteBatch.GraphicsDevice.Viewport.Width / tileWidth;
            right = Math.Min(right, mapWidth);

            int top = (int)Math.Floor(camera.Position.Y / tileHeight);
            int bottom = tileHeight + top + spriteBatch.GraphicsDevice.Viewport.Height / tileHeight;
            bottom = Math.Min(bottom, mapHeight);

            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    Tilesets.ForEach(tileset =>
                        {
                            Layers.ForEach(layer =>
                                {
                                    if (layer.IsVisible)
                                    {
                                        if (tileset.Texture != null)
                                        {
                                            int tileID = layer.Rows[y].Columns[x].TileID;

                                            if (tileID > -1)
                                            {
                                                spriteBatch.Draw(tileset.Texture,
                                                    new Rectangle((x * tileset.TileWidth),
                                                        (y * tileset.TileHeight),
                                                        tileset.TileWidth,
                                                        tileset.TileHeight),
                                                        tileset.GetSourceRectangle(tileID),
                                                        Color.White);
                                            }
                                        }
                                    }
                                });
                        });

                    /* Draw grid
                    DrawingTool.DrawRectangle(spriteBatch, pixel, new Rectangle((x * 32),
                                                        (y * 32),
                                                        32,
                                                        32), Color.White, 1);*/
                    
                }
            }

            if (paintTools[(int)currentPaintTool] != null)
                paintTools[(int)currentPaintTool].Draw(spriteBatch);

            // This should only be called reactively 
            /*if (MapChanged != null)
                MapChanged();*/

            spriteBatch.End();
        }

        #endregion

        #region Methods
  
     

        private void ScrollCamera(Vector2 position)
        {
            int tileWidth = Tilesets.First().TileWidth;
            int tileHeight = Tilesets.First().TileHeight;

            int mapWidth = Layers.First().MapWidth;
            int mapHeight = Layers.First().MapHeight;


            /*camera.Position = new Vector2(MathHelper.Clamp((int)(camera.Position.X + position.X), 0, tileWidth * mapWidth - view.GetGraphicsDevice.Viewport.Width),
              (MathHelper.Clamp((int)(camera.Position.Y + position.Y), 0, tileHeight * mapHeight - view.GetGraphicsDevice.Viewport.Height)));
             * */
            camera.Position = new Vector2(MathHelper.Clamp((int)( position.X), 0, tileWidth * mapWidth - view.GetGraphicsDevice.Viewport.Width),
              (MathHelper.Clamp((int)(position.Y), 0, tileHeight * mapHeight - view.GetGraphicsDevice.Viewport.Height)));

        }


        /// <summary>
        /// Sets current paint tool
        /// </summary>
        /// <param name="paintTool"></param>
        public void SetPaintTool(PaintTool paintTool)
        {
            this.currentPaintTool = paintTool;
        }

        /// <summary>
        /// Sets up map with data to initialize
        /// </summary>
        /// <param name="texturePath"></param>
        /// <param name="tileWidth"></param>
        /// <param name="tileHeight"></param>
        /// <param name="mapWidth"></param>
        /// <param name="mapHeight"></param>
        /// <param name="checkedListBox"></param>
        public void InitializeMap(string texturePath, int tileWidth, int tileHeight, int mapWidth, int mapHeight)
        {
            Texture2D texture;

            using (FileStream fileStream = new FileStream(texturePath, FileMode.Open))
            {
                texture = Texture2D.FromStream(view.GetGraphicsDevice, fileStream);
            }

            // TODO: Ensure only one instance of texture is instantiated
            Tilesets.Add(new Tileset()
                {
                    Texture = texture,
                    TexturePath = texturePath,
                    TileWidth = tileWidth,
                    TileHeight = tileHeight,
                });

            layerCounter = 1;

            Layers.Add(new Layer("Layer " + layerCounter.ToString(), mapWidth, mapHeight));
        }

        public void RemoveTileset()
        {

        }

        /// <summary>
        /// Sets the selection box to empty
        /// </summary>
        private void ClearSelectionBox()
        {
            BeginSelectionBox = null;
            EndSelectionBox = null;
        }

        /// <summary>
        /// Copy selected tiles
        /// </summary>
        public void CopySelection()
        {
            int tileWidth = 0;
            int tileHeight = 0;

            if (Tilesets.Count > 0)
            {
                tileWidth = Tilesets.First().TileWidth;
                tileHeight = Tilesets.First().TileHeight;
            }

            if (currentPaintTool == PaintTool.Select)
            {
                if (!SelectionBox.IsEmpty)
                {
                    commandManager.ExecuteEditCopyCommand(Layers[LayerIndex], SelectionBox, tileWidth, tileHeight, Clipboard);

                    TileBrushValues = Clipboard.FirstOrDefault();

                    Clipboard.Clear();

                    SetPaintTool(PaintTool.Draw);

                    ClearSelectionBox();
                }
            }
        }


        /// <summary>
        /// Cut selected tiles
        /// </summary>
        public void CutSelection()
        {
            int tileWidth = 0;
            int tileHeight = 0;

            if (Tilesets.Count > 0)
            {
                tileWidth = Tilesets.First().TileWidth;
                tileHeight = Tilesets.First().TileHeight;
            }

            if (currentPaintTool == PaintTool.Select)
            {
                if (!SelectionBox.IsEmpty)
                {
                    commandManager.ExecuteEditCutCommand(Layers[LayerIndex], SelectionBox, tileWidth, tileHeight, Clipboard);

                    TileBrushValues = Clipboard.FirstOrDefault();

                    Clipboard.Clear();

                    SetPaintTool(PaintTool.Draw);

                    ClearSelectionBox();                                       
                }
            }
        }

        /// <summary>
        /// Add layer to map
        /// </summary>
        /// <param name="checkedListBox"></param>
        public void AddLayer(int mapWidth, int mapHeight)
        {
            layerCounter++;

            commandManager.ExecuteLayerAddCommand(Layers, "Layer " + layerCounter.ToString(), mapWidth, mapHeight);
        }

        /// <summary>
        /// Remove layer from map
        /// </summary>
        /// <param name="checkedListBox"></param>
        public void RemoveLayer()
        {
            commandManager.ExecuteLayerRemoveCommand(Layers, LayerIndex);
        }

        /// <summary>
        /// Duplicate layer 
        /// </summary>
        public void CloneLayer()
        {
            commandManager.ExecuteLayerClone(Layers, LayerIndex);
        }

        /// <summary>
        /// Move layer one index up
        /// </summary>
        /// <param name="checkedListBox"></param>
        public void RaiseLayer()
        {
            if (LayerIndex <= 0)
                return;

            commandManager.ExecuteLayerRaise(Layers, LayerIndex);
        }

        /// <summary>
        /// Move layer one index down
        /// </summary>
        /// <param name="checkedListBox"></param>
        public void LowerLayer()
        {
            if (LayerIndex >= Layers.Count - 1)
                return;

            commandManager.ExecuteLayerLower(Layers, LayerIndex);
        }

        /// <summary>
        /// Set layers visibility
        /// </summary>
        /// <param name="isVisible"></param>
        public void SetLayerVisibility(bool isVisible)
        {
            if (Layers.Count <= 0)
                return;

            commandManager.ExecuteLayerVisibility(Layers[LayerIndex], isVisible);
        }

        /// <summary>
        /// Draw temporary tilebrushes into map
        /// </summary>
        /// <param name="tileBrushes"></param>
        public void DrawTileBrushes(TileBrushCollection tileBrushes)
        {
            if (Layers.Count <= 0)
                return;

            // TODO: Check if layer is selected

            commandManager.ExecuteEditDrawCommand(Layers[LayerIndex], tileBrushes.TileBrushes); 
        } 
        
        /// <summary>
        /// Remove selected tiles from map
        /// </summary>
        /// <param name="tileBrushes"></param>
        public void RemoveTiles(TileBrushCollection tileBrushes)
        {
            if (Layers.Count <= 0)
                return;

            commandManager.ExecuteEditRemoveCommand(Layers[LayerIndex], tileBrushes.TileBrushes);
        }

        /// <summary>
        /// Offset map in tile measurement
        /// </summary>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        public void OffsetMap(int offsetX, int offsetY)
        {
            commandManager.ExecuteMapOffset(Layers, offsetX, offsetY);
        }

        /// <summary>
        /// Resize map in tile measurement
        /// </summary>
        /// <param name="mapWidth"></param>
        /// <param name="mapHeight"></param>
        public void ResizeMap(int mapWidth, int mapHeight, NumericUpDownEx mapWidthNumeric, NumericUpDownEx mapHeightNumeric)
        {
            commandManager.ExecuteMapResize(Layers, mapWidth, mapHeight, mapWidthNumeric, mapHeightNumeric);
        }

        /// <summary>
        /// Resize tiles in map and tilesets
        /// </summary>
        /// <param name="tileWidth"></param>
        /// <param name="tileHeight"></param>
        public void ResizeTiles(int tileWidth, int tileHeight, NumericUpDownEx tileWidthNumeric, NumericUpDownEx tileHeightNumeric, List<Action<int, int>> setTileDimensions)
        {
            commandManager.ExecuteTileResize(Tilesets, tileWidth, tileHeight, tileWidthNumeric, tileHeightNumeric, setTileDimensions);
        }

        /// <summary>
        /// Undo previous action
        /// </summary>
        public void Undo()
        {
            commandManager.Undo();
        }

        /// <summary>
        /// Redo previous action
        /// </summary>
        public void Redo()
        {
            commandManager.Redo();
        }

        public void AddTileset(string texturePath, int tileWidth, int tileHeight, Action<string, int, int> createTileset, Action<string> removeTileset)
        {
            commandManager.ExecuteMapAddTileset(Tilesets, texturePath, tileWidth, tileHeight, view.GetGraphicsDevice, createTileset, removeTileset);
        }

        /// <summary>
        /// Rounds off position to fit with the map
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Vector2 SnapToGrid(Vector2 position)
        {
            int tileWidth = 0;
            int tileHeight = 0;
            int mapWidth = 0;
            int mapHeight = 0;

            if (Tilesets.Count > 0)
            {
                tileWidth = Tilesets.FirstOrDefault().TileWidth;
                tileHeight = Tilesets.FirstOrDefault().TileHeight;
            }

            if (Layers.Count > 0)
            {
                mapWidth = Tilesets.FirstOrDefault().TileWidth;
                mapHeight = Tilesets.FirstOrDefault().TileHeight;
            }

            if (tileWidth <= 0)
                return Vector2.Zero;
            if (tileHeight <= 0)
                return Vector2.Zero;

            int x = (int)position.X / tileWidth;
            int y = (int)position.Y / tileHeight;

            return new Vector2(x * tileWidth, y * tileHeight);
        }

        /// <summary>
        /// Changes set of coordinates on map to pixel length
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Vector2 CoordinateToPixels(int x, int y)
        {
            int tileWidth = 0;
            int tileHeight = 0;
            int mapWidth = 0;
            int mapHeight = 0;

            if (Tilesets.Count > 0)
            {
                tileWidth = Tilesets.FirstOrDefault().TileWidth;
                tileHeight = Tilesets.FirstOrDefault().TileHeight;
            }

            if (Layers.Count > 0)
            {
                mapWidth = Tilesets.FirstOrDefault().TileWidth;
                mapHeight = Tilesets.FirstOrDefault().TileHeight;
            }

            return new Vector2(x * tileWidth, y * tileHeight);
        }

        /// <summary>
        /// Changes position to set of coordnates on map
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Vector2 PixelsToCoordinate(Vector2 position)
        {
            int tileWidth = 0;
            int tileHeight = 0;
            int mapWidth = 0;
            int mapHeight = 0;

            if (Tilesets.Count > 0)
            {
                tileWidth = Tilesets.FirstOrDefault().TileWidth;
                tileHeight = Tilesets.FirstOrDefault().TileHeight;
            }

            if (Layers.Count > 0)
            {
                mapWidth = Tilesets.FirstOrDefault().TileWidth;
                mapHeight = Tilesets.FirstOrDefault().TileHeight;
            }

            if (tileWidth <= 0)
                return Vector2.Zero;
            if (tileHeight <= 0)
                return Vector2.Zero;

            return new Vector2((int)position.X / tileWidth, (int)position.Y / tileHeight);
        }

        /// <summary>
        /// Transforms position to adjust itself to scrollable maps
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public Vector2 InvertCameraMatrix(System.Drawing.Point point)
        {
            return Vector2.Transform(new Vector2(point.X, point.Y), Matrix.Invert(camera.CameraTransformation));
        }

        #endregion
    }
}
