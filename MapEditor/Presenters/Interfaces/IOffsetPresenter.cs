using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapEditor.UI;

namespace MapEditor.Presenters
{
    public interface IOffsetPresenter
    {
        event EventHandler Confirm;

        int OffsetX { get; }
        int OffsetY { get; }

        void Load();
    }
}
