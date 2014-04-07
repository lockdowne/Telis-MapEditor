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
    public class TilesetView : Form, ITilesetView
    {
        private ClosableTabControl closableTabControl1;        
        private System.ComponentModel.IContainer components = null;

        public IXnaRenderView GetCurrentView
        {
            get
            {
                // TODO: Add exceptions
                return (IXnaRenderView)closableTabControl1.SelectedTab.Controls[0];
            }
        }

        public TilesetView()
        {
            InitializeComponent();


        }        

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.closableTabControl1 = new ClosableTabControl();
            this.SuspendLayout();
            // 
            // closableTabControl1
            // 
            this.closableTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.closableTabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.closableTabControl1.ItemSize = new System.Drawing.Size(90, 0);
            this.closableTabControl1.Location = new System.Drawing.Point(0, 0);
            this.closableTabControl1.Name = "closableTabControl1";
            this.closableTabControl1.SelectedIndex = 0;
            this.closableTabControl1.ShowToolTips = true;
            this.closableTabControl1.Size = new System.Drawing.Size(284, 261);
            this.closableTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.closableTabControl1.TabIndex = 0;
            // 
            // TilesetView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.closableTabControl1);
            this.Name = "TilesetView";
            this.Text = "TilesetView";
            this.ResumeLayout(false);

        }

        #endregion

        public void AddView(string name, IXnaRenderView renderer)
        {
            /*closableTabControl1.TabPages.Add(name);
            closableTabControl1.TabPages[closableTabControl1.TabPages.Count].Controls.Add((Control)renderer);*/

            Control view = (Control)renderer;
            view.Name = name;

            TabPage tab = new TabPage(name);
            tab.Name = name;
            tab.Controls.Add(view);

            closableTabControl1.TabPages.Add(tab);
        }

        public void ShowForm()
        {
            Show();
        }

        public void CloseForm()
        {
            Close();
        }
    }
}
