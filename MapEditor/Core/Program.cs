using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MapEditor.Presenters;
using MapEditor.UI;

namespace MapEditor.Core
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            IMainView view = new MainView();
            
            MainPresenter presenter = new MainPresenter(view,
                new LayerView(),
                new OffsetView(),
                new MapResizeView(),
                new FileNewView(),
                new TilesetView(),
                new TileResizeView(),
                new AddTilesetView(),
                new MinimapView());

            Application.Run((MainView)view);                        
        }
    }
}

