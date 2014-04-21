using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapEditor.Core.EventsArgs;

namespace MapEditor.UI
{
    public interface IRenameView
    {
        event Action OnCancel;
        event RenameEventHandler OnConfirm;

        void ShowWindow(ILayerView parent);
        void CloseWindow();
    }
}
