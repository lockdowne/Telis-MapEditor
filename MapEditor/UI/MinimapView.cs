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
    /// <summary>
    /// Represents a window that shows the minimap 
    /// </summary>
    public class MinimapView : Form, IMinimapView
    {
        
        #region Fields

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;
        
        private XnaRender xnaRender1;

        #endregion

        #region Properties

        /// <summary>
        /// Occurs when xna renderer draws graphics
        /// </summary>
        public event Action<SpriteBatch> OnXnaDraw;

        /// <summary>
        /// Occurs when xna renderer is initialized
        /// </summary>
        public event Action OnXnaInitialize;

        /// <summary>
        /// Occurs when mouse is down on xna renderer
        /// </summary>
        public event MouseEventHandler OnXnaDown;

        /// <summary>
        /// Occurs when mouse is up on xna renderer
        /// </summary>
        public event MouseEventHandler OnXnaUp;

        /// <summary>
        /// Occurs when mouse is moving on xna renderer
        /// </summary>
        public event MouseEventHandler OnXnaMove;

        /// <summary>
        /// Occurs when mouse wheel value is changed 
        /// </summary>
        public event MouseEventHandler OnXnaWheel;

        /// <summary>
        /// Gets graphic device
        /// </summary>
        public GraphicsDevice GraphicsDevice
        {
            get { return xnaRender1.GraphicsDevice; }
        }

        /// <summary>
        /// Gets the width of control
        /// </summary>
        public int MinimapWidth
        {
            get { return xnaRender1.Width; }
        }

        /// <summary>
        /// Gets the height of control
        /// </summary>
        public int MinimapHeight
        {
            get { return xnaRender1.Height; }
        }

        #endregion

        #region Initialize

        public MinimapView()
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
            this.xnaRender1 = new MapEditor.Core.Controls.XnaRender();
            this.SuspendLayout();
            // 
            // xnaRender1
            // 
            this.xnaRender1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xnaRender1.Location = new System.Drawing.Point(0, 0);
            this.xnaRender1.Name = "xnaRender1";
            this.xnaRender1.Size = new System.Drawing.Size(284, 261);
            this.xnaRender1.TabIndex = 0;
            this.xnaRender1.Text = "xnaRender1";
            this.xnaRender1.OnXnaDown += new System.Windows.Forms.MouseEventHandler(this.xnaRender_OnXnaDown);
            this.xnaRender1.OnXnaUp += new System.Windows.Forms.MouseEventHandler(this.xnaRender_OnXnaUp);
            this.xnaRender1.OnXnaMove += new System.Windows.Forms.MouseEventHandler(this.xnaRender_OnXnaMove);
            this.xnaRender1.OnXnaWheel += new System.Windows.Forms.MouseEventHandler(this.xnaRender_OnXnaWheel);
            this.xnaRender1.OnInitialize += new System.Action(this.xnaRender_OnInitialize);
            this.xnaRender1.OnDraw += new System.Action<Microsoft.Xna.Framework.Graphics.SpriteBatch>(this.xnaRender_OnDraw);
            // 
            // MinimapView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.xnaRender1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MinimapView";
            this.Text = "Minimap";
            this.ResumeLayout(false);

        }

        #endregion

        #endregion

        #region Windows Form Designer generated events

        void xnaRender_OnXnaWheel(object sender, MouseEventArgs e)
        {
            if (OnXnaWheel != null)
                OnXnaWheel(sender, e);
        }

        void xnaRender_OnXnaUp(object sender, MouseEventArgs e)
        {
            if (OnXnaUp != null)
                OnXnaUp(sender, e);
        }

        void xnaRender_OnXnaMove(object sender, MouseEventArgs e)
        {
            if (OnXnaMove != null)
                OnXnaMove(sender, e);
        }

        void xnaRender_OnXnaDown(object sender, MouseEventArgs e)
        {
            if (OnXnaDown != null)
                OnXnaDown(sender, e);
        }

        void xnaRender_OnInitialize()
        {
            if (OnXnaInitialize != null)
                OnXnaInitialize();
        }

        void xnaRender_OnDraw(SpriteBatch spriteBatch)
        {
            if (OnXnaDraw != null)
                OnXnaDraw(spriteBatch);
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


        #endregion
    }
}
