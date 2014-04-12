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

        #endregion

        #region Properties

        public int[,] TileBrushValues { get; set; }

        public List<int[,]> Clipboard;

        public int LayerIndex {  get; set; }
        public int TilesetIndex { get; set; }

        /*public int MapWidth { get; private set; }
        public int MapHeight { get; private set; }
        public int TileWidth { get; private set; }
        public int TileHeight { get; private set; }*/

        public List<Tileset> Tilesets = new List<Tileset>();
        public List<Layer> Layers = new List<Layer>();

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

        void view_OnXnaMove(object sender, MouseEventArgs e)
        {
            paintTools[(int)currentPaintTool].OnMouseMove(sender, e);
        }

        void view_OnXnaUp(object sender, MouseEventArgs e)
        {
            paintTools[(int)currentPaintTool].OnMouseUp(sender, e);
        }
       
        void view_OnXnaDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    SetPaintTool(PaintTool.Select);
                    break;
                    
            }

            paintTools[(int)currentPaintTool].OnMouseDown(sender, e);
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
                tileWidth = Tilesets.FirstOrDefault().TileWidth;
                tileHeight = Tilesets.FirstOrDefault().TileHeight;
            }

            if (Layers.Count > 0)
            {
                mapWidth = Layers.FirstOrDefault().MapWidth;
                mapHeight = Layers.FirstOrDefault().MapHeight;
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

            spriteBatch.End();
        }

        #endregion

        #region Methods

        public void ScrollCamera(Vector2 vector)
        {
            // Calculate the side edges of the screen.
            float marginWidth = camera.ViewportWidth * 0.5f; // * 0.5f
            float marginLeft = camera.Position.X + marginWidth;
            float marginRight = camera.Position.X + camera.ViewportWidth - marginWidth;

            // Calculate the top and bottom edges of the screen.
            float marginHeight = camera.ViewportHeight * 0.5f; // * 0.5f
            float marginTop = camera.Position.Y + marginHeight;
            float marginBottom = camera.Position.Y + camera.ViewportHeight - marginHeight;

            // Calculate how far to scroll when the vector is near the edges of the screen.
            float cameraMovementX = 0;
            float cameraMovementY = 0;

            if (vector.X < marginLeft)
                cameraMovementX = vector.X - marginLeft;
            else if (vector.X > marginRight)
                cameraMovementX = vector.X - marginRight;

            if (vector.Y < marginTop)
                cameraMovementY = vector.Y - marginTop;
            else if (vector.Y > marginBottom)
                cameraMovementY = vector.Y - marginBottom;

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
                mapWidth = Layers.FirstOrDefault().MapWidth;
                mapHeight = Layers.FirstOrDefault().MapHeight;
            }

            // Update the camera position, but prevent scrolling off the ends of the level.
            float maxCameraPositionX = tileWidth * mapWidth - camera.ViewportWidth;
            float maxCameraPositionY = tileHeight * mapHeight - camera.ViewportHeight;

            camera.Position = new Vector2(MathHelper.Clamp(camera.Position.X + cameraMovementX, 0, maxCameraPositionX),
                 MathHelper.Clamp(camera.Position.Y + cameraMovementY, 0, maxCameraPositionY));
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
        public void InitializeMap(string texturePath, int tileWidth, int tileHeight, int mapWidth, int mapHeight, CheckedListBox checkedListBox)
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

            Layers.Add(new Layer(mapWidth, mapHeight));

            checkedListBox.Items.Add("Layer" + checkedListBox.Items.Count, true);
            //checkedListBox.SelectedIndex++;
        }

        public void RemoveTileset()
        {

        }

        public void AddTileset(string texturePath, int tileWidth, int tileHeight)
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
                tileWidth = Tilesets.FirstOrDefault().TileWidth;
                tileHeight = Tilesets.FirstOrDefault().TileHeight;
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
                tileWidth = Tilesets.FirstOrDefault().TileWidth;
                tileHeight = Tilesets.FirstOrDefault().TileHeight;
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
        public void AddLayer(CheckedListBox checkedListBox, int mapWidth, int mapHeight)
        {
            commandManager.ExecuteLayerAddCommand(Layers, mapWidth, mapHeight, checkedListBox);
        }

        /// <summary>
        /// Remove layer from map
        /// </summary>
        /// <param name="checkedListBox"></param>
        public void RemoveLayer(CheckedListBox checkedListBox)
        {
            commandManager.ExecuteLayerRemoveCommand(Layers, LayerIndex, checkedListBox);
        }

        /// <summary>
        /// Duplicate layer 
        /// </summary>
        public void CloneLayer(CheckedListBox checkedListBox)
        {
            commandManager.ExecuteLayerClone(Layers, LayerIndex, checkedListBox);
        }

        /// <summary>
        /// Move layer one index up
        /// </summary>
        /// <param name="checkedListBox"></param>
        public void RaiseLayer(CheckedListBox checkedListBox)
        {
            if (LayerIndex <= 0)
                return;

            commandManager.ExecuteLayerRaise(Layers, LayerIndex, checkedListBox);
        }

        /// <summary>
        /// Move layer one index down
        /// </summary>
        /// <param name="checkedListBox"></param>
        public void LowerLayer(CheckedListBox checkedListBox)
        {
            if (LayerIndex >= Layers.Count - 1)
                return;

            commandManager.ExecuteLayerLower(Layers, LayerIndex, checkedListBox);
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
        public void ResizeTiles(int tileWidth, int tileHeight, NumericUpDownEx tileWidthNumeric, NumericUpDownEx tileHeightNumeric)
        {
            commandManager.ExecuteTileResize(Tilesets, tileWidth, tileHeight, tileWidthNumeric, tileHeightNumeric);
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
