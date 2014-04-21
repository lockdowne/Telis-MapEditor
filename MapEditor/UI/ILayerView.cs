using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapEditor.Core.EventsArgs;

namespace MapEditor.UI
{
    public interface ILayerView
    {
        event LayerEventHandler OnMoveLayerDown;
        event LayerEventHandler OnMoveLayerUp;
        event LayerEventHandler OnRemoveLayer;
        event LayerEventHandler OnAddLayer;
        event LayerEventHandler OnDuplicateLayer;
        event LayerEventHandler OnLayerVisibilityChanged;
        event LayerEventHandler OnRenameLayer;
        event LayerEventHandler OnLayerIndexChanged;

        int SelectedIndex { set; }

        void ShowWindow(IMainView parent);
        void HideWindow();
        void AddListItem(object item);
        void RemoveListItem(int index);
        void ClearAllListItems();
    }
}
