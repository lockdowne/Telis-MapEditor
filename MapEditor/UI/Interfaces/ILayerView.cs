using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MapEditor.UI
{
    /// <summary>
    /// Base for layer UI
    /// </summary>
    public interface ILayerView
    {
        event EventHandler MoveLayerDown;
        event EventHandler MoveLayerUp;
        event EventHandler RemoveLayerItem;
        event EventHandler AddLayerItem;
        event ItemCheckEventHandler LayerItemChecked;
        event Action<int> LayerIndexChanged;
        event EventHandler DuplicateLayer;

        CheckedListBox CheckedListBox { get;  set; }

        void ShowForm(IMainView parent);
        void CloseForm();
    }
}
