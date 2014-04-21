using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditor.Core.EventsArgs
{
    public delegate void RenameEventHandler(RenameEventArgs e);

    public class RenameEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// Gets name
        /// </summary>
        public string Name { get; private set; }

        #endregion

        #region Initialize

        public RenameEventArgs(string name)
        {
            Name = name;
        }

        #endregion
    }
}
