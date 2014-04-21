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

        /// <summary>
        /// Draws a single line from vectorA to vectorB
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="texture"></param>
        /// <param name="vectorA"></param>
        /// <param name="vectorB"></param>
        /// <param name="color"></param>
        /// <param name="thickness"></param>
        public static void DrawLine(SpriteBatch spriteBatch, Texture2D texture, Vector2 vectorA, Vector2 vectorB, Color color, int thickness)
        {
            int distance = (int)Vector2.Distance(vectorA, vectorB);

            Vector2 difference = vectorB - vectorA;
            Vector2 baseVector = new Vector2(1, 0);

            float alpha = (float)Math.Acos(Vector2.Dot(difference, baseVector) / (difference.Length() * baseVector.Length()));

            if (texture != null)
                spriteBatch.Draw(texture, new Rectangle((int)vectorA.X, (int)vectorA.Y, distance, thickness),
                                  null, color, alpha, new Vector2(0, 0), SpriteEffects.None, 0);
        }


        /// <summary>
        /// Draws a rectangle with the help of DrawLine
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="texture"></param>
        /// <param name="Rect"></param>
        /// <param name="XnaColor"></param>
        /// <param name="thickness"></param>

        public static void DrawRectangle(SpriteBatch spriteBatch, Texture2D texture, Rectangle rectangle, Color color, int thickness)
        {
            // left
            DrawLine(spriteBatch, texture, new Vector2(rectangle.X, rectangle.Y), new Vector2(rectangle.X, rectangle.Y + rectangle.Height), color, thickness);
            // top
            DrawLine(spriteBatch, texture, new Vector2(rectangle.X, rectangle.Y), new Vector2(rectangle.X + rectangle.Width, rectangle.Y), color, thickness);
            // bottom
            DrawLine(spriteBatch, texture, new Vector2(rectangle.X, rectangle.Y + rectangle.Height), new Vector2(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height), color, thickness);
            // right
            DrawLine(spriteBatch, texture, new Vector2(rectangle.X + rectangle.Width, rectangle.Y), new Vector2(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height), color, thickness);
        }

        #endregion
    }
}
