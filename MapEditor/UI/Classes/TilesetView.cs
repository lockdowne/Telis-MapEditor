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
       
        #region Fields

        private System.ComponentModel.IContainer components = null;
        private TabControl tabControl1;

        #endregion

        #region Properties
        
        public IXnaRenderView GetCurrentView
        {
            get
            {
                if(tabControl1.SelectedTab.Controls.Count > 0)
                    return (IXnaRenderView)tabControl1.SelectedTab.Controls[0];

                return null;
            }
        }

        #endregion

        #region Initialize

        public TilesetView()
        {
            InitializeComponent();

            this.FormClosing += (sender, e) =>
            {
                this.Hide();
                e.Cancel = true;
            };
            
        }

        #endregion

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(284, 261);
            this.tabControl1.TabIndex = 0;
            // 
            // TilesetView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.tabControl1);
            this.Name = "TilesetView";
            this.Text = "TilesetView";
            this.ResumeLayout(false);

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

            tabControl1.TabPages.Add(tab);
        }

        public void ShowForm(IMainView view)
        {
            if (!Visible)
                Show((Form)view);
        }

        public void CloseForm()
        {
            Hide();
        }

        #endregion
    }
}
