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
        
        /// <summary>
        /// Gets or sets vector position
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Gets or sets the viewport width
        /// </summary>
        public float ViewportWidth { get; set; }

        /// <summary>
        /// Gets or sets the viewport height
        /// </summary>
        public float ViewportHeight { get; set; }

        /// <summary>
        /// Gets or sets zoom
        /// </summary>
        public float Zoom { get; set; }

        /// <summary>
        /// Gets or sets rotation
        /// </summary>
        public float Rotation { get; set; }       

        /// <summary>
        /// Gets the matrix transformation
        /// </summary>
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
