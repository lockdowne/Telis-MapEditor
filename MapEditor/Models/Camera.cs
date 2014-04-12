using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MapEditor.Models
{
    /// <summary>
    /// Camera used to navigate through rendered areas
    /// </summary>
    public class Camera
    {
        #region Properties

        // Camera position
        public Vector2 Position { get; set; }

        // Camera width
        public float ViewportWidth { get; set; }

        // Camera height
        public float ViewportHeight { get; set; }

        // Camera zoom level
        public float Zoom { get; set; }

        // Camera rotation
        public float Rotation { get; set; }       

        // Matrix translation to calculate camera values
        public Matrix CameraTransformation
        {
            get
            {
                return Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) * 
                    Matrix.CreateRotationZ(Rotation) *
                    Matrix.CreateScale(new Vector3(Zoom, Zoom, 1));
            }
        }

        #endregion
    }
}
