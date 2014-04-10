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
    /// Holds logic for offset view
    /// </summary>
    public class OffsetPresenter : IOffsetPresenter
    {
        #region Fields

        private readonly IOffsetView view;

        public event EventHandler Confirm;

        #endregion

        #region Properties

        public int OffsetX
        {
            get { return view.OffsetX; }
        }

        public int OffsetY
        {
            get { return view.OffsetY; }
        }

        #endregion

        #region Initialize

        public OffsetPresenter(IOffsetView view)
        {
            this.view = view;

            this.view.OnCancel += new EventHandler(view_OnCancel);
            this.view.OnConfirm += new EventHandler(view_OnConfirm);
        }

        #endregion

        #region Events

        void view_OnConfirm(object sender, EventArgs e)
        {
            view.CloseForm();

            if (Confirm != null)
                Confirm(sender, e);
        }

        void view_OnCancel(object sender, EventArgs e)
        {
            view.CloseForm();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Show UI
        /// </summary>
        /// <param name="parent"></param>
        public void Load()
        {
            view.ShowForm();
        }

        #endregion
    }
}
