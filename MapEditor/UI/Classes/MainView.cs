using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapEditor.Core.Helpers;
using MapEditor.UI;
using MapEditor.Core.Controls;

namespace MapEditor.UI
{
    /// <summary>
    /// Main UI for entire project
    /// </summary>
    public class MainView : Form, IMainView
    {
        #region Fields

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem mapToolStripMenuItem;
        private ToolStripMenuItem layerToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem closeToolStripMenuItem;
        private ToolStripMenuItem closeAllToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem undoToolStripMenuItem;
        private ToolStripMenuItem redoToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem cutToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ToolStripMenuItem showGridToolStripMenuItem;
        private ToolStripMenuItem addLayerToolStripMenuItem;
        private ToolStripMenuItem removeLayerToolStripMenuItem;
        private ToolStripMenuItem duplicateLayerToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem raiseLayerToolStripMenuItem;
        private ToolStripMenuItem lowerLayerToolStripMenuItem;
        private ToolStrip toolStrip1;
        private Core.Controls.ClosableTabControl closableTabControl1;
        private ToolStripMenuItem selectToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton2;
        private ToolStripButton toolStripButton3;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripButton toolStripButton4;
        private ToolStripButton toolStripButton5;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripButton toolStripButton6;
        private ToolStripButton toolStripButton7;
        private ToolStripButton toolStripButton8;
        private ToolStripSeparator toolStripSeparator8;
        private ToolStripButton toolStripButton9;
        private ToolStripButton toolStripButton10;
        private ToolStripButton toolStripButton12; 
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        public event EventHandler FileNew;
        public event EventHandler FileExit;

        public event EventHandler EditCopy;
        public event EventHandler EditCut;
        public event EventHandler EditDraw;
        public event EventHandler EditPaste;
        public event EventHandler EditRemove;
        public event EventHandler EditSelect;

        public event EventHandler LayerAdd;
        public event EventHandler LayerRemove;
        public event EventHandler LayerClone;
        public event EventHandler LayerRaise;
        public event EventHandler LayerLower;
        public event EventHandler LayerVisibility;

        public event EventHandler MapOffset;
        public event EventHandler MapResize;

        public event EventHandler TileResize;

        public event EventHandler Undo;
        public event EventHandler Redo;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current view with focus
        /// </summary>
        public IXnaRenderView GetCurrentView
        {
            get
            {
                return (IXnaRenderView)closableTabControl1.SelectedTab.Controls[0];
            }
        }

        #endregion

        #region Initialize

        public MainView()
        {
            InitializeComponent();
        }
        
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
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.duplicateLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.raiseLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lowerLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton9 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton10 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton12 = new System.Windows.Forms.ToolStripButton();
            this.closableTabControl1 = new MapEditor.Core.Controls.ClosableTabControl();
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
            this.menuStrip1.Size = new System.Drawing.Size(373, 24);
            this.menuStrip1.TabIndex = 0;
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
            this.newToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.newToolStripMenuItem.Text = "New...";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = global::MapEditor.Properties.Resources.folder_3_32;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.openToolStripMenuItem.Text = "Open...";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(120, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = global::MapEditor.Properties.Resources.save_32;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Image = global::MapEditor.Properties.Resources.save_as_32;
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(120, 6);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Image = global::MapEditor.Properties.Resources.x_mark_32;
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.closeToolStripMenuItem.Text = "Close";
            // 
            // closeAllToolStripMenuItem
            // 
            this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
            this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.closeAllToolStripMenuItem.Text = "Close All";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::MapEditor.Properties.Resources.exit_2_32;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.exitToolStripMenuItem.Text = "Exit";
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
            this.selectToolStripMenuItem});
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
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showGridToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // showGridToolStripMenuItem
            // 
            this.showGridToolStripMenuItem.Name = "showGridToolStripMenuItem";
            this.showGridToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.showGridToolStripMenuItem.Text = "Show Grid";
            // 
            // mapToolStripMenuItem
            // 
            this.mapToolStripMenuItem.Name = "mapToolStripMenuItem";
            this.mapToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.mapToolStripMenuItem.Text = "Map";
            // 
            // layerToolStripMenuItem
            // 
            this.layerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addLayerToolStripMenuItem,
            this.removeLayerToolStripMenuItem,
            this.duplicateLayerToolStripMenuItem,
            this.toolStripSeparator4,
            this.raiseLayerToolStripMenuItem,
            this.lowerLayerToolStripMenuItem});
            this.layerToolStripMenuItem.Name = "layerToolStripMenuItem";
            this.layerToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.layerToolStripMenuItem.Text = "Layer";
            // 
            // addLayerToolStripMenuItem
            // 
            this.addLayerToolStripMenuItem.Image = global::MapEditor.Properties.Resources.plus_32;
            this.addLayerToolStripMenuItem.Name = "addLayerToolStripMenuItem";
            this.addLayerToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.addLayerToolStripMenuItem.Text = "Add Layer";
            this.addLayerToolStripMenuItem.Click += new System.EventHandler(this.addLayerToolStripMenuItem_Click);
            // 
            // removeLayerToolStripMenuItem
            // 
            this.removeLayerToolStripMenuItem.Image = global::MapEditor.Properties.Resources.x_mark_32;
            this.removeLayerToolStripMenuItem.Name = "removeLayerToolStripMenuItem";
            this.removeLayerToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.removeLayerToolStripMenuItem.Text = "Remove Layer";
            this.removeLayerToolStripMenuItem.Click += new System.EventHandler(this.removeLayerToolStripMenuItem_Click);
            // 
            // duplicateLayerToolStripMenuItem
            // 
            this.duplicateLayerToolStripMenuItem.Image = global::MapEditor.Properties.Resources.copy_32;
            this.duplicateLayerToolStripMenuItem.Name = "duplicateLayerToolStripMenuItem";
            this.duplicateLayerToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.duplicateLayerToolStripMenuItem.Text = "Duplicate Layer";
            this.duplicateLayerToolStripMenuItem.Click += new System.EventHandler(this.duplicateLayerToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(152, 6);
            // 
            // raiseLayerToolStripMenuItem
            // 
            this.raiseLayerToolStripMenuItem.Image = global::MapEditor.Properties.Resources.arrow_141_32;
            this.raiseLayerToolStripMenuItem.Name = "raiseLayerToolStripMenuItem";
            this.raiseLayerToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.raiseLayerToolStripMenuItem.Text = "Raise Layer";
            this.raiseLayerToolStripMenuItem.Click += new System.EventHandler(this.raiseLayerToolStripMenuItem_Click);
            // 
            // lowerLayerToolStripMenuItem
            // 
            this.lowerLayerToolStripMenuItem.Image = global::MapEditor.Properties.Resources.arrow_142_32;
            this.lowerLayerToolStripMenuItem.Name = "lowerLayerToolStripMenuItem";
            this.lowerLayerToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.lowerLayerToolStripMenuItem.Text = "Lower Layer";
            this.lowerLayerToolStripMenuItem.Click += new System.EventHandler(this.lowerLayerToolStripMenuItem_Click);
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
            this.toolStripButton6,
            this.toolStripButton7,
            this.toolStripButton8,
            this.toolStripButton12,
            this.toolStripSeparator8,
            this.toolStripButton10,
            this.toolStripButton9});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(373, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
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
            // toolStripButton12
            // 
            this.toolStripButton12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton12.Image = global::MapEditor.Properties.Resources.paint_bucket_32;
            this.toolStripButton12.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton12.Name = "toolStripButton12";
            this.toolStripButton12.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton12.Text = "toolStripButton12";
            this.toolStripButton12.ToolTipText = "Paint";
            this.toolStripButton12.Click += new System.EventHandler(this.toolStripButton12_Click);
            // 
            // closableTabControl1
            // 
            this.closableTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.closableTabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.closableTabControl1.ItemSize = new System.Drawing.Size(90, 0);
            this.closableTabControl1.Location = new System.Drawing.Point(0, 49);
            this.closableTabControl1.Name = "closableTabControl1";
            this.closableTabControl1.SelectedIndex = 0;
            this.closableTabControl1.ShowToolTips = true;
            this.closableTabControl1.Size = new System.Drawing.Size(373, 285);
            this.closableTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.closableTabControl1.TabIndex = 2;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 334);
            this.Controls.Add(this.closableTabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainView";
            this.Text = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        #region Events

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FileNew != null)
                FileNew(sender, e);
        }

        private void addLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LayerAdd != null)
                LayerAdd(sender, e);
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Undo != null)
                Undo(sender, e);
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EditCut != null)
                EditCut(sender, e);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EditCopy != null)
                EditCopy(sender, e);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EditPaste != null)
                EditPaste(sender, e);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EditRemove != null)
                EditRemove(sender, e);
        }

        private void removeLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LayerRemove != null)
                LayerRemove(sender, e);
        }

        private void duplicateLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LayerClone != null)
                LayerClone(sender, e);
        }

        private void raiseLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LayerRaise != null)
                LayerRaise(sender, e);
        }

        private void lowerLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LayerLower != null)
                LayerLower(sender, e);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (EditDraw != null)
                EditDraw(sender, e);
        }

        private void selectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EditSelect != null)
                EditSelect(sender, e);
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Redo != null)
                Redo(sender, e);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {            
            if (FileNew != null)
                FileNew(sender, e);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            // Open file
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            // save file
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (Undo != null)
                Undo(sender, e);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (Redo != null)
                Redo(sender, e);
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            // Draw
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            //erase
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            //select
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            //paint bucket
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            //cut
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            //copy
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

        public void AddView(string name, IXnaRenderView renderer)
        {
            Control view = (Control)renderer;
            view.Name = name;
            view.Dock = DockStyle.Fill;

            TabPage tab = new TabPage(name);
            tab.Name = name;
            tab.Controls.Add(view);

            closableTabControl1.TabPages.Add(tab);
        }

        #endregion
    }
}
