using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapEditor.Core.Helpers;

namespace MapEditor.Core.Controls
{
    public class TESTXnaRenderer : GraphicsDeviceControl
    {
        public Action OnInitialize;
        public Action<SpriteBatch> OnDraw;

        private SpriteBatch spriteBatch = null;

        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Application.Idle += delegate { Invalidate(); };

            if (OnInitialize != null)
            {
                this.OnInitialize();
            }
           
        }

        protected override void Draw()
        {
            if (OnDraw != null)
            {
                this.OnDraw(spriteBatch);
            }
        }

    }
}
