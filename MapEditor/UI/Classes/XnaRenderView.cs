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
using MapEditor.Core.Helpers;
using MapEditor.UI;
using MapEditor.Core.Controls;

namespace MapEditor.UI
{
    public class XnaRenderView : GraphicsDeviceControl, IXnaRenderView
    {
        public event MouseEventHandler OnXnaDown;
        public event MouseEventHandler OnXnaUp;
        public event MouseEventHandler OnXnaMove;

        public event Action OnInitialize;
        public event Action<SpriteBatch> OnDraw;       

        private SpriteBatch spriteBatch;
       
        public GraphicsDevice GetGraphicsDevice
        {
            get { return GraphicsDevice; }
        }

        public string KeyName
        {
            get { return Name; }            
        }

        public XnaRenderView()
        {
            MouseDown += (sender, e) => { if (OnXnaDown != null) OnXnaDown(sender, e); };
            MouseUp += (sender, e) => { if (OnXnaUp != null) OnXnaUp(sender, e); };
            MouseMove += (sender, e) => { if (OnXnaMove != null) OnXnaMove(sender, e); };            
        }  

        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            this.Dock = DockStyle.Fill;

            if (OnInitialize != null)
                this.OnInitialize();
            
            Application.Idle += delegate { Invalidate(); };
        }

        protected override void Draw()
        {
            if (OnDraw != null)
                OnDraw(spriteBatch);
        }

       

    }
}
