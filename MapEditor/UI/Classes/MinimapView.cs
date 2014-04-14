using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapEditor.Core.Controls;

namespace MapEditor.UI
{
    public class MinimapView : Form, IMinimapView
    {       
        #region Fields

        private System.ComponentModel.IContainer components = null;
        
        private XnaRenderView xnaRenderView1;

        #endregion

        #region Properties
        
        public event Action<SpriteBatch> OnDraw;
        public event Action OnInitialize;
        public event EventHandler OnXnaDown;
        public event EventHandler OnXnaUp;
        public event EventHandler OnXnaMove;

        public GraphicsDevice GraphicsDevice
        {
            get { return xnaRenderView1.GetGraphicsDevice; }
        }
        
        #endregion

        #region Initialize

        public MinimapView()
        { 
            InitializeComponent();

            this.FormClosing += (sender, e) =>
            {
                this.Hide();
                e.Cancel = true;
            };

            xnaRenderView1.OnDraw += (spriteBatch) =>
                {
                    if (OnDraw != null)
                        OnDraw(spriteBatch);
                };

            xnaRenderView1.OnInitialize += () =>
                {
                    if (OnInitialize != null)
                        OnInitialize();
                };

            xnaRenderView1.OnXnaDown += (sender, e) =>
                {

                };

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        }

        #endregion

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.xnaRenderView1 = new MapEditor.UI.XnaRenderView();
            this.SuspendLayout();
            // 
            // xnaRenderView1
            // 
            this.xnaRenderView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xnaRenderView1.Location = new System.Drawing.Point(0, 0);
            this.xnaRenderView1.Name = "xnaRenderView1";
            this.xnaRenderView1.Size = new System.Drawing.Size(284, 261);
            this.xnaRenderView1.TabIndex = 0;
            this.xnaRenderView1.Text = "xnaRenderView1";
            // 
            // TilesetView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.xnaRenderView1);
            this.Name = "MinimapView";
            this.Text = "Minimap";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
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

        public void SetFormSize(int width, int height)
        {
            this.Width = width;
            this.Height = height;
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
