using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapEditor.Core.EventsArgs;

namespace MapEditor.UI
{
    /// <summary>
    /// Represents a window that displays and enables interaction with map layers
    /// </summary>
    public class LayerView : Form, ILayerView
    {
        #region Fields

        private ToolStrip toolStrip1;
        private ToolStripButton toolStripAdd;
        private ToolStripButton toolStripRemove;
        private ToolStripButton toolStripDuplicate;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton toolStripMoveUp;
        private ToolStripButton toolStripMoveDown;
        private ListBox listBox;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton toolStripVisibility;
        private ToolStripButton toolStripRename;       

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #endregion

        #region Properties

        /// <summary>
        /// Occurs when MoveLayerDown button is clicked
        /// </summary>
        public event LayerEventHandler OnMoveLayerDown;

        /// <summary>
        /// Occurs when MoveLayerUp button is clicked
        /// </summary>
        public event LayerEventHandler OnMoveLayerUp;

        /// <summary>
        /// Occurs when RemoveLayer button is clicked
        /// </summary>
        public event LayerEventHandler OnRemoveLayer;

        /// <summary>
        /// Occurs when AddLayer button is clicked
        /// </summary>
        public event LayerEventHandler OnAddLayer;

        /// <summary>
        /// Occurs when DuplicateLayer button is clicked
        /// </summary>
        public event LayerEventHandler OnDuplicateLayer;

        /// <summary>
        /// Occurs when selected LayerVisibility button is clicked
        /// </summary>
        public event LayerEventHandler OnLayerVisibilityChanged;

        /// <summary>
        /// Occurs when RenameLayer button is clicked
        /// </summary>
        public event LayerEventHandler OnRenameLayer;

        /// <summary>
        /// Occurs when listbox index has changed
        /// </summary>
        public event LayerEventHandler OnLayerIndexChanged;

        /// <summary>
        /// Sets the index of the listBox control
        /// </summary>
        public int SelectedIndex
        {
            set { listBox.SelectedIndex = value; }
        }

        /// <summary>
        /// Gets the current selected index of the listBox control
        /// </summary>
        private int CurrentSelectedIndex
        {
            get { return listBox.SelectedIndex; }
        }

        #endregion

        #region Initialize

        public LayerView()
        {
            InitializeComponent();

            // Prevent close button from closing window and hide it instead
            this.FormClosing += (sender, e) =>
                {
                    this.Hide();
                    e.Cancel = true;
                };
        }  

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripRemove = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMoveUp = new System.Windows.Forms.ToolStripButton();
            this.toolStripMoveDown = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDuplicate = new System.Windows.Forms.ToolStripButton();
            this.toolStripVisibility = new System.Windows.Forms.ToolStripButton();
            this.toolStripRename = new System.Windows.Forms.ToolStripButton();
            this.listBox = new System.Windows.Forms.ListBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripAdd,
            this.toolStripRemove,
            this.toolStripSeparator1,
            this.toolStripMoveUp,
            this.toolStripMoveDown,
            this.toolStripSeparator2,
            this.toolStripDuplicate,
            this.toolStripVisibility,
            this.toolStripRename});
            this.toolStrip1.Location = new System.Drawing.Point(0, 143);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(196, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripAdd
            // 
            this.toolStripAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripAdd.Image = global::MapEditor.Properties.Resources.plus_32;
            this.toolStripAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripAdd.Name = "toolStripAdd";
            this.toolStripAdd.Size = new System.Drawing.Size(23, 22);
            this.toolStripAdd.ToolTipText = "Add Layer";
            this.toolStripAdd.Click += new System.EventHandler(this.toolStripAdd_Click);
            // 
            // toolStripRemove
            // 
            this.toolStripRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripRemove.Image = global::MapEditor.Properties.Resources.x_mark_32;
            this.toolStripRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripRemove.Name = "toolStripRemove";
            this.toolStripRemove.Size = new System.Drawing.Size(23, 22);
            this.toolStripRemove.Text = "toolStripRemove";
            this.toolStripRemove.ToolTipText = "Remove Layer";
            this.toolStripRemove.Click += new System.EventHandler(this.toolStripRemove_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripMoveUp
            // 
            this.toolStripMoveUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripMoveUp.Image = global::MapEditor.Properties.Resources.arrow_141_32;
            this.toolStripMoveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMoveUp.Name = "toolStripMoveUp";
            this.toolStripMoveUp.Size = new System.Drawing.Size(23, 22);
            this.toolStripMoveUp.Text = "toolStripMoveUp";
            this.toolStripMoveUp.ToolTipText = "Move Layer Up";
            this.toolStripMoveUp.Click += new System.EventHandler(this.toolStripMoveUp_Click);
            // 
            // toolStripMoveDown
            // 
            this.toolStripMoveDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripMoveDown.Image = global::MapEditor.Properties.Resources.arrow_142_32;
            this.toolStripMoveDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMoveDown.Name = "toolStripMoveDown";
            this.toolStripMoveDown.Size = new System.Drawing.Size(23, 22);
            this.toolStripMoveDown.Text = "toolStripMoveDown";
            this.toolStripMoveDown.ToolTipText = "Move Layer Down";
            this.toolStripMoveDown.Click += new System.EventHandler(this.toolStripMoveDown_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripDuplicate
            // 
            this.toolStripDuplicate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDuplicate.Image = global::MapEditor.Properties.Resources.copy_32;
            this.toolStripDuplicate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDuplicate.Name = "toolStripDuplicate";
            this.toolStripDuplicate.Size = new System.Drawing.Size(23, 22);
            this.toolStripDuplicate.Text = "toolStripButton1";
            this.toolStripDuplicate.ToolTipText = "Duplicate Layer";
            this.toolStripDuplicate.Click += new System.EventHandler(this.toolStripDuplicate_Click);
            // 
            // toolStripVisibility
            // 
            this.toolStripVisibility.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripVisibility.Image = global::MapEditor.Properties.Resources.visible_32;
            this.toolStripVisibility.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripVisibility.Name = "toolStripVisibility";
            this.toolStripVisibility.Size = new System.Drawing.Size(23, 22);
            this.toolStripVisibility.Text = "Toggle Visibility";
            this.toolStripVisibility.Click += new System.EventHandler(this.toolStripVisibility_Click);
            // 
            // toolStripRename
            // 
            this.toolStripRename.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripRename.Image = global::MapEditor.Properties.Resources.rename_32;
            this.toolStripRename.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripRename.Name = "toolStripRename";
            this.toolStripRename.Size = new System.Drawing.Size(23, 22);
            this.toolStripRename.Text = "Rename";
            this.toolStripRename.ToolTipText = "Rename";
            this.toolStripRename.Click += new System.EventHandler(this.toolStripRename_Click);
            // 
            // listBox
            // 
            this.listBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox.FormattingEnabled = true;
            this.listBox.Location = new System.Drawing.Point(0, 0);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(196, 143);
            this.listBox.TabIndex = 2;
            this.listBox.SelectedIndexChanged += new System.EventHandler(this.listBox_SelectedIndexChanged);
            // 
            // LayerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(196, 168);
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "LayerView";
            this.Text = "LayerView";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        
        #endregion

        #endregion

        #region Windows Form Designer generated events

        private void toolStripAdd_Click(object sender, EventArgs e)
        {
            if (OnAddLayer != null)
                OnAddLayer(new LayerEventArgs(CurrentSelectedIndex));
        }

        private void toolStripRemove_Click(object sender, EventArgs e)
        {
            if (OnRemoveLayer != null)
                OnRemoveLayer(new LayerEventArgs(CurrentSelectedIndex));
        }

        private void toolStripMoveUp_Click(object sender, EventArgs e)
        {
            if (OnMoveLayerUp != null)
                OnMoveLayerUp(new LayerEventArgs(CurrentSelectedIndex));
        }

        private void toolStripMoveDown_Click(object sender, EventArgs e)
        {
            if (OnMoveLayerDown != null)
                OnMoveLayerDown(new LayerEventArgs(CurrentSelectedIndex));
        }

        private void toolStripDuplicate_Click(object sender, EventArgs e)
        {
            if (OnDuplicateLayer != null)
                OnDuplicateLayer(new LayerEventArgs(CurrentSelectedIndex));
        }

        private void toolStripVisibility_Click(object sender, EventArgs e)
        {
            if (OnLayerVisibilityChanged != null)
                OnLayerVisibilityChanged(new LayerEventArgs(CurrentSelectedIndex));
        }

        private void toolStripRename_Click(object sender, EventArgs e)
        {
            if (OnRenameLayer != null)
                OnRenameLayer(new LayerEventArgs(CurrentSelectedIndex));
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OnLayerIndexChanged != null)
                OnLayerIndexChanged(new LayerEventArgs(CurrentSelectedIndex));
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
        /// Show window on top of parent
        /// </summary>
        public void ShowWindow(IMainView parent)
        {
            if (!Visible)
                Show((Form)parent);
        }

        /// <summary>
        /// Hide window
        /// </summary>
        public void HideWindow()
        {
            if (Visible)
                Hide();
        }

        /// <summary>
        /// Adds list item to listbox
        /// </summary>
        public void AddListItem(object item)
        {
            listBox.Items.Add(item);
        }

        /// <summary>
        /// Removes list item from listbox at index
        /// </summary>
        /// <param name="index"></param>
        public void RemoveListItem(int index)
        {
            listBox.Items.RemoveAt(index);
        }

        /// <summary>
        /// Clear all list items
        /// </summary>
        public void ClearAllListItems()
        {
            listBox.Items.Clear();
        }

        #endregion              
    }
}
