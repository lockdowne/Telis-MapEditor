using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapEditor.UI;

namespace MapEditor.Presenters
{
    public class LayerPresenter
    {
        private readonly ILayerView view;    
    
        // NEEDS SHOW FORM STUFF

        public event EventHandler OnRemoveLayer;
        public event EventHandler OnRaiseLayer;
        public event EventHandler OnLowerLayer;
        public event EventHandler OnAddLayer;

        public LayerPresenter(ILayerView view)
        {
            this.view = view;

            view.AddLayerItem += new EventHandler(view_AddLayerItem);
            view.MoveLayerDown += new EventHandler(view_MoveLayerDown);
            view.MoveLayerUp += new EventHandler(view_MoveLayerUp);
            view.RemoveLayerItem += new EventHandler(view_RemoveLayerItem);
            
        }

        void view_RemoveLayerItem(object sender, EventArgs e)
        {
            if (OnRemoveLayer != null)
                OnRemoveLayer(sender, e);
        }

        void view_MoveLayerUp(object sender, EventArgs e)
        {
            if (OnRaiseLayer != null)
                OnRaiseLayer(sender, e);
        }

        void view_MoveLayerDown(object sender, EventArgs e)
        {
            if (OnLowerLayer != null)
                OnLowerLayer(sender, e);
        }

        void view_AddLayerItem(object sender, EventArgs e)
        {
            if (OnAddLayer != null)
                OnAddLayer(sender, e);
        }
    }
}
