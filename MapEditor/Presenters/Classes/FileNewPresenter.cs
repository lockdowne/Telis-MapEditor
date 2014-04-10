using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapEditor.Models;
using MapEditor.UI;

namespace MapEditor.Presenters
{
    /// <summary>
    /// Handles New File form
    /// </summary>
    public class FileNewPresenter : IFileNewPresenter
    {
        #region Fields

        private readonly IFileNewView view;

        public event Action Confirmed;

        #endregion

        public string MapName
        {
            get { return view.GetFileName; }
        }

        public string TilesetPath
        {
            get { return view.GetTilesetPath; }
        }

        public int MapWidth
        {
            get { return view.GetMapWidth; }
        }
   
        public int MapHeight
        {
            get { return view.GetMapHeight; }
        }
      
        public int TileWidth
        {
            get { return view.GetTileWidth; }
        }

        public int TileHeight
        {
            get { return view.GetTileHeight; }
        }

        public FileNewPresenter(IFileNewView view)
        {
            this.view = view;

            view.Browse += new EventHandler(Browse);
            view.Confirm += new EventHandler(Confirm);
            view.Cancel += new EventHandler(Cancel);
            view.ValueChanged += new EventHandler(ValueChanged);
        }

        

        void ValueChanged(object sender, EventArgs e)
        {
            view.SetDisplay = view.GetMapWidth * view.GetTileWidth + " x " + view.GetMapHeight * view.GetTileHeight + " pixels";
        }        


        /// <summary>
        /// Search for image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Browse(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Image Files (.bmp, .jpeg, .jpg, .png)|*.bmp;*.jpeg;*.jpg*;*.png;";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                view.SetTilesetPath(openFileDialog.FileName);
            }
        }

        /// <summary>
        /// Confirm information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Confirm(object sender, EventArgs e)
        {
            view.CloseForm();

            if (Confirmed != null)
                Confirmed();        
        }

        /// <summary>
        /// Close form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Cancel(object sender, EventArgs e)
        {
            view.CloseForm();
        }

        /// <summary>
        /// Show form
        /// </summary>
        public void Load()
        {
            view.ShowForm();
        }

        



        
    }
}
