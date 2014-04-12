using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapEditor.Core.Controls;

namespace MapEditor.UI
{
    /// <summary>
    /// Layer UI
    /// Holds events to interact with layers in the map
    /// </summary>
    public class LayerView : Form, ILayerView
    {
        #region Fields

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.ToolStrip toolStrip1;
        private ToolStripButton toolStripAdd;
        private ToolStripButton toolStripRemove;
        private ToolStripButton toolStripMoveUp;
        private ToolStripButton toolStripMoveDown;
        private ToolStripButton toolStripButton1;
        private ToolStripSeparator toolStripSeparator1;
        private CheckedListBox checkedListBox1;

        #endregion
 
        #region Properties

        public event EventHandler MoveLayerDown;
        public event EventHandler MoveLayerUp;
        public event EventHandler RemoveLayerItem;
        public event EventHandler AddLayerItem;
        public event EventHandler LayerItemChecked;
        public event EventHandler LayerIndexChanged;
        public event EventHandler DuplicateLayer;
        
        /// <summary>
        /// Get the check box list control
        /// </summary>
        public CheckedListBox CheckedListBox
        {
            get { return checkedListBox1; }
        }

        #endregion

        #region Initialize

        public LayerView()
        {
            InitializeComponent();
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
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMoveUp = new System.Windows.Forms.ToolStripButton();
            this.toolStripMoveDown = new System.Windows.Forms.ToolStripButton();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripAdd,
            this.toolStripRemove,
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.toolStripMoveUp,
            this.toolStripMoveDown});
            this.toolStrip1.Location = new System.Drawing.Point(0, 236);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(284, 25);
            this.toolStrip1.TabIndex = 0;
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
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::MapEditor.Properties.Resources.copy_32;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.ToolTipText = "Duplicate Layer";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
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
            // checkedListBox1
            // 
            this.checkedListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(0, 0);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(284, 236);
            this.checkedListBox1.TabIndex = 1;
            this.checkedListBox1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // LayerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.toolStrip1);
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

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            if (LayerIndexChanged != null)
                LayerIndexChanged(sender, e);
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (LayerItemChecked != null)
                LayerItemChecked(sender, e);
        }


        private void toolStripMoveUp_Click(object sender, EventArgs e)
        {
            if (MoveLayerUp != null)
                MoveLayerUp(sender, e);
        }

        private void toolStripMoveDown_Click(object sender, EventArgs e)
        {
            if (MoveLayerDown != null)
                MoveLayerDown(sender, e);
        }

        private void toolStripAdd_Click(object sender, EventArgs e)
        {
            if (AddLayerItem != null)
                AddLayerItem(sender, e);
        }

        private void toolStripRemove_Click(object sender, EventArgs e)
        {
            if (RemoveLayerItem != null)
                RemoveLayerItem(sender, e);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (DuplicateLayer != null)
                DuplicateLayer(sender, e);
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

        public void ShowForm(IMainView parent)
        {
            this.Show((Form)parent);
        }

        public void CloseForm()
        {
            this.Close();
        }

        #endregion

    }
}


