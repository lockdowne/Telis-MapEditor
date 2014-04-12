using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MapEditor.Models
{
    /// <summary>
    /// Draw tool to draw rectangles
    /// </summary>
    public static class DrawingTool
    {
        #region Methods

        public static void DrawLine(SpriteBatch spriteBatch, Texture2D texture, Vector2 PointA, Vector2 PointB, Color XnaColor, int thickness)
        {
            int distance = (int)Vector2.Distance(PointA, PointB);

            Vector2 connection = PointB - PointA;
            Vector2 base_vector = new Vector2(1, 0);

            float alpha = (float)Math.Acos(Vector2.Dot(connection, base_vector) / (connection.Length() * base_vector.Length()));

            if (texture != null)
            {
                spriteBatch.Draw(texture, new Rectangle((int)PointA.X, (int)PointA.Y, distance, thickness),
                                  null, XnaColor, alpha, new Vector2(0, 0), SpriteEffects.None, 0);
                
            }
        }


        /// <summary>
        /// Draws a rectangle with the help of DrawLine
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="texture"></param>
        /// <param name="Rect"></param>
        /// <param name="XnaColor"></param>
        /// <param name="thickness"></param>

        public static void DrawRectangle(SpriteBatch spriteBatch, Texture2D texture, Rectangle Rect, Color XnaColor, int thickness)
        {
            // | left
            DrawLine(spriteBatch, texture, new Vector2(Rect.X, Rect.Y), new Vector2(Rect.X, Rect.Y + Rect.Height), XnaColor, thickness);
            // - top
            DrawLine(spriteBatch, texture, new Vector2(Rect.X, Rect.Y), new Vector2(Rect.X + Rect.Width, Rect.Y), XnaColor, thickness);
            // - bottom
            DrawLine(spriteBatch, texture, new Vector2(Rect.X, Rect.Y + Rect.Height), new Vector2(Rect.X + Rect.Width, Rect.Y + Rect.Height), XnaColor, thickness);
            // | right
            DrawLine(spriteBatch, texture, new Vector2(Rect.X + Rect.Width, Rect.Y), new Vector2(Rect.X + Rect.Width, Rect.Y + Rect.Height), XnaColor, thickness);
        }

        #endregion
    }
}
