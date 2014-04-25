using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using MapEditor.Core.Controls;
using MapEditor.Core.EventsArgs;

namespace MapEditor.UI
{
    /// <summary>
    /// Represents main window for main functionality of program
    /// </summary>
    public class MainView : Form, IMainView, IMessageFilter
    {
        #region Fields

        [DllImport("user32.dll")]
        private static extern IntPtr WindowFromPoint(System.Drawing.Point pt);

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem closeToolStripMenuItem;
        private ToolStripMenuItem closeAllToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem undoToolStripMenuItem;
        private ToolStripMenuItem redoToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem cutToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ToolStripMenuItem selectToolStripMenuItem;
        private ToolStripMenuItem fillToolStripMenuItem;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem zoomInToolStripMenuItem;
        private ToolStripMenuItem zoomOutToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator9;
        private ToolStripMenuItem tilesetToolStripMenuItem;
        private ToolStripMenuItem layersToolStripMenuItem;
        private ToolStripMenuItem minimapToolStripMenuItem;
        private ToolStripMenuItem mapToolStripMenuItem;
        private ToolStripMenuItem resizeMapToolStripMenuItem;
        private ToolStripMenuItem resizeTilesToolStripMenuItem;
        private ToolStripMenuItem offsetMapToolStripMenuItem;
        private ToolStripMenuItem layerToolStripMenuItem;
        private ToolStripMenuItem addLayerToolStripMenuItem;
        private ToolStripMenuItem removeLayerToolStripMenuItem;
        private ToolStripMenuItem duplicateLayerToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem raiseLayerToolStripMenuItem;
        private ToolStripMenuItem lowerLayerToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton2;
        private ToolStripButton toolStripButton3;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripButton toolStripButton4;
        private ToolStripButton toolStripButton5;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripButton toolStripButton10;
        private ToolStripButton toolStripButton9;
        private ToolStripSeparator toolStripSeparator8;
        private ToolStripButton toolStripButton6;
        private ToolStripButton toolStripButton7;
        private ToolStripButton toolStripButton8;
        private ToolStripButton toolStripButton12;
        private ToolStripSeparator toolStripSeparator10;
        private ToolStripMenuItem toggleVisibilityToolStripMenuItem;
        private ToolStripMenuItem renameLayerToolStripMenuItem;
        private TabControl tabControl1; 
       
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #endregion

        #region Properties

        /// <summary>
        /// Occurs when New... button is clicked
        /// </summary>
        public event Action OnFileNew;

        /// <summary>
        /// Occurs when Open... button is clicked
        /// </summary>
        public event Action OnFileOpen;

        /// <summary>
        /// Occurs when Save button is clicked
        /// </summary>
        public event Action OnFileSave;

        /// <summary>
        /// Occurs when Save As button is clicked
        /// </summary>
        public event Action OnFileSaveAs;

        /// <summary>
        /// Occurs when Close button is clicked
        /// </summary>
        public event Action OnFileClose;

        /// <summary>
        /// Occurs when Close All button is clicked
        /// </summary>
        public event Action OnFileCloseAll;

        /// <summary>
        /// Occurs when Exit button is clicked
        /// </summary>
        public event Action OnFileExit;

        /// <summary>
        /// Occurs when Undo button is clicked
        /// </summary>
        public event Action OnEditUndo;

        /// <summary>
        /// Occurs when Redo button is clicked
        /// </summary>
        public event Action OnEditRedo;

        /// <summary>
        /// Occurs when Copy button is clicked
        /// </summary>
        public event Action OnEditCopy;

        /// <summary>
        /// Occurs when Cut button is clicked
        /// </summary>
        public event Action OnEditCut;

        /// <summary>
        /// Occurs when Draw button is clicked
        /// </summary>
        public event Action OnEditDraw;

        /// <summary>
        /// Occurs when Erase button is clicked
        /// </summary>
        public event Action OnEditErase;

        /// <summary>
        /// Occurs when Select button is clicked
        /// </summary>
        public event Action OnEditSelect;

        /// <summary>
        /// Occurs when Fill button is clicked
        /// </summary>
        public event Action OnEditFill;

        /// <summary>
        /// Occurs when Grid button is clicked
        /// </summary>
        public event Action OnViewGrid;

        /// <summary>
        /// Occurs when Tileset button is clicked
        /// </summary>
        public event Action<bool> OnViewTileset;

        /// <summary>
        /// Occurs when Layers button is clicked
        /// </summary>
        public event Action<bool> OnViewLayers;

        /// <summary>
        /// Occurs when Minimap button is clicked
        /// </summary>
        public event Action<bool> OnViewMinimap;

        /// <summary>
        /// Occurs when ZoomIn button is clicked
        /// </summary>
        public event Action OnZoomIn;

        /// <summary>
        /// Occurs when ZoomOut button is clicked
        /// </summary>
        public event Action OnZoomOut;

        /// <summary>
        /// Occurs when Add Layer button is clicked
        /// </summary>
        public event Action OnLayerAdd;

        /// <summary>
        /// Occurs when Remove Layer button is clicked
        /// </summary>
        public event Action OnLayerRemove;

        /// <summary>
        /// Occurs when Duplicate Layer button is clicked
        /// </summary>
        public event Action OnLayerDuplicate;

        /// <summary>
        /// Occurs when Raise Layer button is clicked
        /// </summary>
        public event Action OnLayerRaise;

        /// <summary>
        /// Occurs when Lower Layer button is clicked
        /// </summary>
        public event Action OnLayerLower;

        /// <summary>
        /// Occurs when Toggle Visibility button is clicked
        /// </summary>
        public event Action OnLayerVisibility;

        /// <summary>
        /// Occurs when Layer Remove button is clicked
        /// </summary>
        public event Action OnLayerRename;

        /// <summary>
        /// Occurs when Offset button is clicked
        /// </summary>
        public event Action OnMapOffset;

        /// <summary>
        /// Occurs when Resize Tile button is clicked
        /// </summary>
        public event Action OnMapResizeTile;

        /// <summary>
        /// Occurs when Resize Map button is clicked
        /// </summary>
        public event Action OnMapResizeMap;

        /// <summary>
        /// Occurs when window is about to close
        /// </summary>
        public event FormClosingEventHandler ViewClosing;

        /// <summary>
        /// Occurs when selected tab is changed
        /// </summary>
        public event Action<string> OnSelectedTabChanged;

        /// <summary>
        /// Occurs when selected tab is closed
        /// </summary>
        public event ControlEventHandler OnTabClosed;
        
        /// <summary>
        /// Intercept all window mouse wheel events
        /// Source: http://social.msdn.microsoft.com/Forums/windows/en-US/eb922ed2-1036-41ca-bd15-49daed7b637c/outlookstyle-wheel-mouse-behavior?forum=winforms
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == 0x20a)
            {
                System.Drawing.Point pos = new System.Drawing.Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16);

                IntPtr hWnd = WindowFromPoint(pos);

                if (hWnd != IntPtr.Zero && hWnd != m.HWnd && Control.FromHandle(hWnd) != null)
                {
                    SendMessage(hWnd, m.Msg, m.WParam, m.LParam);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets selected tab with xna renderer
        /// </summary>
        public XnaRender SelectedXnaTab
        {
            get
            {
                if (tabControl1.TabPages.Count <= 0)
                    return null;
                return (XnaRender)tabControl1.SelectedTab.Controls[0];
            }
        }

        /// <summary>
        /// Gets selected tab name
        /// </summary>
        public string SelectedTabName
        {
            get
            {
                if (tabControl1.TabPages.Count <= 0)
                    return string.Empty;
                return tabControl1.SelectedTab.Name;
            }
        }

        /// <summary>
        ///  Gets width of control
        /// </summary>
        public int ViewportWidth
        {
            get { return Width; }            
        }

        /// <summary>
        /// Gets height of control
        /// </summary>
        public int ViewportHeight
        {
            get { return Height; }
        }

        #endregion

        #region Initialize

        public MainView()
        {
            InitializeComponent();

            this.FormClosing += (sender, e) =>
                {
                    e.Cancel = false;
                };

            WindowState = FormWindowState.Maximized;

            Application.AddMessageFilter(this);
        }     

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fillToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.tilesetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minimapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resizeMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resizeTilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.offsetMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.raiseLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lowerLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.duplicateLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleVisibilityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton10 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton9 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton12 = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.mapToolStripMenuItem,
            this.layerToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(384, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator2,
            this.closeToolStripMenuItem,
            this.closeAllToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = global::MapEditor.Properties.Resources.add_file_32;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newToolStripMenuItem.Text = "New...";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = global::MapEditor.Properties.Resources.folder_3_32;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = global::MapEditor.Properties.Resources.save_32;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Image = global::MapEditor.Properties.Resources.save_as_32;
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Image = global::MapEditor.Properties.Resources.x_mark_32;
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // closeAllToolStripMenuItem
            // 
            this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
            this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.closeAllToolStripMenuItem.Text = "Close All";
            this.closeAllToolStripMenuItem.Click += new System.EventHandler(this.closeAllToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::MapEditor.Properties.Resources.exit_2_32;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator3,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.toolStripSeparator5,
            this.toolStripMenuItem1,
            this.deleteToolStripMenuItem,
            this.selectToolStripMenuItem,
            this.fillToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Image = global::MapEditor.Properties.Resources.undo_4_32;
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Image = global::MapEditor.Properties.Resources.redo_4_32;
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(102, 6);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Image = global::MapEditor.Properties.Resources.cut_32;
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = global::MapEditor.Properties.Resources.copy_32;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(102, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = global::MapEditor.Properties.Resources.pencil_32;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(105, 22);
            this.toolStripMenuItem1.Text = "Draw";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::MapEditor.Properties.Resources.erase_32;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.deleteToolStripMenuItem.Text = "Erase";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // selectToolStripMenuItem
            // 
            this.selectToolStripMenuItem.Image = global::MapEditor.Properties.Resources.square_dashed_32;
            this.selectToolStripMenuItem.Name = "selectToolStripMenuItem";
            this.selectToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.selectToolStripMenuItem.Text = "Select";
            this.selectToolStripMenuItem.Click += new System.EventHandler(this.selectToolStripMenuItem_Click);
            // 
            // fillToolStripMenuItem
            // 
            this.fillToolStripMenuItem.Image = global::MapEditor.Properties.Resources.paint_bucket_32;
            this.fillToolStripMenuItem.Name = "fillToolStripMenuItem";
            this.fillToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.fillToolStripMenuItem.Text = "Fill";
            this.fillToolStripMenuItem.Click += new System.EventHandler(this.fillToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomInToolStripMenuItem,
            this.zoomOutToolStripMenuItem,
            this.toolStripSeparator9,
            this.tilesetToolStripMenuItem,
            this.layersToolStripMenuItem,
            this.minimapToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // zoomInToolStripMenuItem
            // 
            this.zoomInToolStripMenuItem.Image = global::MapEditor.Properties.Resources.zoom_in_32;
            this.zoomInToolStripMenuItem.Name = "zoomInToolStripMenuItem";
            this.zoomInToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.zoomInToolStripMenuItem.Text = "Zoom In";
            this.zoomInToolStripMenuItem.Click += new System.EventHandler(this.zoomInToolStripMenuItem_Click);
            // 
            // zoomOutToolStripMenuItem
            // 
            this.zoomOutToolStripMenuItem.Image = global::MapEditor.Properties.Resources.zoom_out_32;
            this.zoomOutToolStripMenuItem.Name = "zoomOutToolStripMenuItem";
            this.zoomOutToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.zoomOutToolStripMenuItem.Text = "Zoom Out";
            this.zoomOutToolStripMenuItem.Click += new System.EventHandler(this.zoomOutToolStripMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(126, 6);
            // 
            // tilesetToolStripMenuItem
            // 
            this.tilesetToolStripMenuItem.Checked = true;
            this.tilesetToolStripMenuItem.CheckOnClick = true;
            this.tilesetToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tilesetToolStripMenuItem.Name = "tilesetToolStripMenuItem";
            this.tilesetToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.tilesetToolStripMenuItem.Text = "Tileset";
            this.tilesetToolStripMenuItem.Click += new System.EventHandler(this.tilesetToolStripMenuItem_Click);
            // 
            // layersToolStripMenuItem
            // 
            this.layersToolStripMenuItem.Checked = true;
            this.layersToolStripMenuItem.CheckOnClick = true;
            this.layersToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.layersToolStripMenuItem.Name = "layersToolStripMenuItem";
            this.layersToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.layersToolStripMenuItem.Text = "Layers";
            this.layersToolStripMenuItem.Click += new System.EventHandler(this.layersToolStripMenuItem_Click);
            // 
            // minimapToolStripMenuItem
            // 
            this.minimapToolStripMenuItem.Checked = true;
            this.minimapToolStripMenuItem.CheckOnClick = true;
            this.minimapToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.minimapToolStripMenuItem.Name = "minimapToolStripMenuItem";
            this.minimapToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.minimapToolStripMenuItem.Text = "Minimap";
            this.minimapToolStripMenuItem.Click += new System.EventHandler(this.minimapToolStripMenuItem_Click);
            // 
            // mapToolStripMenuItem
            // 
            this.mapToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resizeMapToolStripMenuItem,
            this.resizeTilesToolStripMenuItem,
            this.offsetMapToolStripMenuItem});
            this.mapToolStripMenuItem.Name = "mapToolStripMenuItem";
            this.mapToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.mapToolStripMenuItem.Text = "Map";
            // 
            // resizeMapToolStripMenuItem
            // 
            this.resizeMapToolStripMenuItem.Name = "resizeMapToolStripMenuItem";
            this.resizeMapToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.resizeMapToolStripMenuItem.Text = "Resize Map";
            this.resizeMapToolStripMenuItem.Click += new System.EventHandler(this.resizeMapToolStripMenuItem_Click);
            // 
            // resizeTilesToolStripMenuItem
            // 
            this.resizeTilesToolStripMenuItem.Name = "resizeTilesToolStripMenuItem";
            this.resizeTilesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.resizeTilesToolStripMenuItem.Text = "Resize Tiles";
            this.resizeTilesToolStripMenuItem.Click += new System.EventHandler(this.resizeTilesToolStripMenuItem_Click);
            // 
            // offsetMapToolStripMenuItem
            // 
            this.offsetMapToolStripMenuItem.Name = "offsetMapToolStripMenuItem";
            this.offsetMapToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.offsetMapToolStripMenuItem.Text = "Offset Map";
            this.offsetMapToolStripMenuItem.Click += new System.EventHandler(this.offsetMapToolStripMenuItem_Click);
            // 
            // layerToolStripMenuItem
            // 
            this.layerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addLayerToolStripMenuItem,
            this.removeLayerToolStripMenuItem,
            this.toolStripSeparator4,
            this.raiseLayerToolStripMenuItem,
            this.lowerLayerToolStripMenuItem,
            this.toolStripSeparator10,
            this.duplicateLayerToolStripMenuItem,
            this.toggleVisibilityToolStripMenuItem,
            this.renameLayerToolStripMenuItem});
            this.layerToolStripMenuItem.Name = "layerToolStripMenuItem";
            this.layerToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.layerToolStripMenuItem.Text = "Layer";
            // 
            // addLayerToolStripMenuItem
            // 
            this.addLayerToolStripMenuItem.Image = global::MapEditor.Properties.Resources.plus_32;
            this.addLayerToolStripMenuItem.Name = "addLayerToolStripMenuItem";
            this.addLayerToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.addLayerToolStripMenuItem.Text = "Add Layer";
            this.addLayerToolStripMenuItem.Click += new System.EventHandler(this.addLayerToolStripMenuItem_Click);
            // 
            // removeLayerToolStripMenuItem
            // 
            this.removeLayerToolStripMenuItem.Image = global::MapEditor.Properties.Resources.x_mark_32;
            this.removeLayerToolStripMenuItem.Name = "removeLayerToolStripMenuItem";
            this.removeLayerToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.removeLayerToolStripMenuItem.Text = "Remove Layer";
            this.removeLayerToolStripMenuItem.Click += new System.EventHandler(this.removeLayerToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(155, 6);
            // 
            // raiseLayerToolStripMenuItem
            // 
            this.raiseLayerToolStripMenuItem.Image = global::MapEditor.Properties.Resources.arrow_141_32;
            this.raiseLayerToolStripMenuItem.Name = "raiseLayerToolStripMenuItem";
            this.raiseLayerToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.raiseLayerToolStripMenuItem.Text = "Raise Layer";
            this.raiseLayerToolStripMenuItem.Click += new System.EventHandler(this.raiseLayerToolStripMenuItem_Click);
            // 
            // lowerLayerToolStripMenuItem
            // 
            this.lowerLayerToolStripMenuItem.Image = global::MapEditor.Properties.Resources.arrow_142_32;
            this.lowerLayerToolStripMenuItem.Name = "lowerLayerToolStripMenuItem";
            this.lowerLayerToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.lowerLayerToolStripMenuItem.Text = "Lower Layer";
            this.lowerLayerToolStripMenuItem.Click += new System.EventHandler(this.lowerLayerToolStripMenuItem_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(155, 6);
            // 
            // duplicateLayerToolStripMenuItem
            // 
            this.duplicateLayerToolStripMenuItem.Image = global::MapEditor.Properties.Resources.copy_32;
            this.duplicateLayerToolStripMenuItem.Name = "duplicateLayerToolStripMenuItem";
            this.duplicateLayerToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.duplicateLayerToolStripMenuItem.Text = "Duplicate Layer";
            this.duplicateLayerToolStripMenuItem.Click += new System.EventHandler(this.duplicateLayerToolStripMenuItem_Click);
            // 
            // toggleVisibilityToolStripMenuItem
            // 
            this.toggleVisibilityToolStripMenuItem.Image = global::MapEditor.Properties.Resources.visible_32;
            this.toggleVisibilityToolStripMenuItem.Name = "toggleVisibilityToolStripMenuItem";
            this.toggleVisibilityToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.toggleVisibilityToolStripMenuItem.Text = "Toggle Visibility";
            this.toggleVisibilityToolStripMenuItem.Click += new System.EventHandler(this.toggleVisibilityToolStripMenuItem_Click);
            // 
            // renameLayerToolStripMenuItem
            // 
            this.renameLayerToolStripMenuItem.Image = global::MapEditor.Properties.Resources.rename_32;
            this.renameLayerToolStripMenuItem.Name = "renameLayerToolStripMenuItem";
            this.renameLayerToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.renameLayerToolStripMenuItem.Text = "Rename Layer";
            this.renameLayerToolStripMenuItem.Click += new System.EventHandler(this.renameLayerToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripSeparator6,
            this.toolStripButton4,
            this.toolStripButton5,
            this.toolStripSeparator7,
            this.toolStripButton10,
            this.toolStripButton9,
            this.toolStripSeparator8,
            this.toolStripButton6,
            this.toolStripButton7,
            this.toolStripButton8,
            this.toolStripButton12});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(384, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::MapEditor.Properties.Resources.add_file_32;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.ToolTipText = "New";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::MapEditor.Properties.Resources.folder_3_32;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "toolStripButton2";
            this.toolStripButton2.ToolTipText = "Open";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::MapEditor.Properties.Resources.save_32;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "toolStripButton3";
            this.toolStripButton3.ToolTipText = "Save";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = global::MapEditor.Properties.Resources.undo_4_32;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "toolStripButton4";
            this.toolStripButton4.ToolTipText = "Undo";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = global::MapEditor.Properties.Resources.redo_4_32;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton5.Text = "toolStripButton5";
            this.toolStripButton5.ToolTipText = "Redo";
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton10
            // 
            this.toolStripButton10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton10.Image = global::MapEditor.Properties.Resources.cut_32;
            this.toolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton10.Name = "toolStripButton10";
            this.toolStripButton10.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton10.Text = "toolStripButton10";
            this.toolStripButton10.ToolTipText = "Cut";
            this.toolStripButton10.Click += new System.EventHandler(this.toolStripButton10_Click);
            // 
            // toolStripButton9
            // 
            this.toolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton9.Image = global::MapEditor.Properties.Resources.copy_32;
            this.toolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton9.Name = "toolStripButton9";
            this.toolStripButton9.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton9.Text = "toolStripButton9";
            this.toolStripButton9.ToolTipText = "Copy";
            this.toolStripButton9.Click += new System.EventHandler(this.toolStripButton9_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton6.Image = global::MapEditor.Properties.Resources.pencil_32;
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton6.Text = "toolStripButton6";
            this.toolStripButton6.ToolTipText = "Draw";
            this.toolStripButton6.Click += new System.EventHandler(this.toolStripButton6_Click);
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton7.Image = global::MapEditor.Properties.Resources.erase_32;
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton7.Text = "toolStripButton7";
            this.toolStripButton7.ToolTipText = "Erase";
            this.toolStripButton7.Click += new System.EventHandler(this.toolStripButton7_Click);
            // 
            // toolStripButton8
            // 
            this.toolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton8.Image = global::MapEditor.Properties.Resources.square_dashed_32;
            this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton8.Name = "toolStripButton8";
            this.toolStripButton8.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton8.Text = "toolStripButton8";
            this.toolStripButton8.ToolTipText = "Select";
            this.toolStripButton8.Click += new System.EventHandler(this.toolStripButton8_Click);
            // 
            // toolStripButton12
            // 
            this.toolStripButton12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton12.Image = global::MapEditor.Properties.Resources.paint_bucket_32;
            this.toolStripButton12.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton12.Name = "toolStripButton12";
            this.toolStripButton12.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton12.Text = "toolStripButton12";
            this.toolStripButton12.ToolTipText = "Fill";
            this.toolStripButton12.Click += new System.EventHandler(this.toolStripButton12_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 49);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(384, 312);
            this.tabControl1.TabIndex = 3;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "MainView";
            this.Text = "Telis";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        #endregion

        #region Windows Form Designer generated events

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OnFileNew != null)
                OnFileNew();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OnFileOpen != null)
                OnFileOpen();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OnFileSave != null)
                OnFileSave();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OnFileSaveAs != null)
                OnFileSaveAs();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OnFileClose != null)
                OnFileClose();
        }

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OnFileCloseAll != null)
                OnFileCloseAll();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OnFileExit != null)
                OnFileExit();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OnEditUndo != null)
                OnEditUndo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OnEditRedo != null)
                OnEditRedo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OnEditCut != null)
                OnEditCut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OnEditCopy != null)
                OnEditCopy();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (OnEditDraw != null)
                OnEditDraw();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OnEditErase != null)
                OnEditErase();
        }

        private void selectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OnEditSelect != null)
                OnEditSelect();
        }

        private void fillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OnEditFill != null)
                OnEditFill();
        }

        private void showGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OnViewGrid != null)
                OnViewGrid();
        }

        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OnZoomIn != null)
                OnZoomIn();
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OnZoomOut != null)
                OnZoomOut();
        }

        private void tilesetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OnViewTileset != null)
            {
                OnViewTileset(tilesetToolStripMenuItem.Checked);
            }
        }

        private void layersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OnViewLayers != null)
                OnViewLayers(layersToolStripMenuItem.Checked);
        }

        private void minimapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OnViewMinimap != null)
                OnViewMinimap(minimapToolStripMenuItem.Checked);
        }

        private void resizeMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OnMapResizeMap != null)
                OnMapResizeMap();
        }

        private void resizeTilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OnMapResizeTile != null)
                OnMapResizeTile();
        }

        private void offsetMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OnMapOffset != null)
                OnMapOffset();
        }

        private void addLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OnLayerAdd != null)
                OnLayerAdd();
        }

        private void removeLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OnLayerRemove != null)
                OnLayerRemove();
        }

        private void duplicateLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OnLayerDuplicate != null)
                OnLayerDuplicate();
        }

        private void raiseLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OnLayerRaise != null)
                OnLayerRaise();
        }

        private void lowerLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OnLayerLower != null)
                OnLayerLower();
        }

        private void toggleVisibilityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OnLayerVisibility != null)
                OnLayerVisibility();
        }

        private void renameLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OnLayerRename != null)
                OnLayerRename();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (OnFileNew != null)
                OnFileNew();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (OnFileOpen != null)
                OnFileOpen();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (OnFileSave != null)
                OnFileSave();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (OnEditUndo != null)
                OnEditUndo();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (OnEditRedo != null)
                OnEditRedo();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            if (OnEditCut != null)
                OnEditCut();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            if (OnEditCopy != null)
                OnEditCopy();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (OnEditDraw != null)
                OnEditDraw();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (OnEditErase != null)
                OnEditErase();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (OnEditSelect != null)
                OnEditSelect();
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            if (OnEditFill != null)
                OnEditFill();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OnSelectedTabChanged != null)
                OnSelectedTabChanged(SelectedTabName);
        }


        private void tilePropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //tile properties
        }


        #endregion

        #region Methods

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Add new tab with single xna render control
        /// </summary>
        /// <param name="name"></param>
        public void AddTab(string name)
        {
            XnaRender xna = new XnaRender();
            xna.Dock = DockStyle.Fill;

            TabPage tab = new TabPage(name);
            tab.Name = name;
            tab.Text = name;

            tab.Controls.Add(xna);

            tabControl1.TabPages.Add(tab);

            tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;            
        }

        public void RemoveTab(string name)
        {
            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                if (tabControl1.TabPages[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    tabControl1.TabPages.RemoveAt(i);
                    break;
                }
            }
        }

        public void RemoveAllTabs()
        {
            tabControl1.TabPages.Clear();
        }

        /// <summary>
        /// Displays a message box
        /// </summary>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        /// <param name="buttons"></param>
        /// <param name="icon"></param>
        public void DisplayMessage(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            MessageBox.Show(text, caption, buttons, icon);
        }
        #endregion       

       
    }
}
