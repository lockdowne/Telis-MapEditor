using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MapEditor.UI
{
    public interface ITilesetView
    {
        void AddView(string name, IXnaRenderView renderer);

        IXnaRenderView GetCurrentView { get; }

        void ShowForm(IMainView view);
        void CloseForm();
    }
}
