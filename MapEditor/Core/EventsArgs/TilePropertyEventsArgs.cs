using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditor.Core.EventsArgs
{
    public delegate void TilePropertyEventHandler(TilePropertyEventsArgs e);

    public class TilePropertyEventsArgs : EventArgs
    {
        #region Properties

        public object Item { get; private set; }        

        #endregion

        #region Initialize

        public TilePropertyEventsArgs(object item)
        {
            Item = item;
        }

        #endregion
    }
}
