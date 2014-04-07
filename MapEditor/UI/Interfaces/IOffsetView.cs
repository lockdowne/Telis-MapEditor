using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditor.UI
{
    public interface IOffsetView
    {
        event EventHandler OnConfirm;
        event EventHandler OnCancel;

        int OffsetX { get; }
        int OffsetY { get; }
    }
}
