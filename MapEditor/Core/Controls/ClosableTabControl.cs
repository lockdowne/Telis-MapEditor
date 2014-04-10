using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace MapEditor.Core.Controls
{
    //This class is to modify the TabControl Class 
    //Making tabs closable
    public class ClosableTabControl : TabControl
    {
        public ClosableTabControl()
        {
            this.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.ItemSize = new Size(90, this.ItemSize.Height);
            this.SizeMode = TabSizeMode.Fixed;
            this.ShowToolTips = true;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);   

            TabPages[e.Index].ToolTipText = TabPages[e.Index].Text;                
            
            e.Graphics.DrawString("x", e.Font, Brushes.Black, e.Bounds.Right - 15, e.Bounds.Top + 4);

            if(TabPages[e.Index].Text.Length > 8)
                e.Graphics.DrawString(this.TabPages[e.Index].Text.Substring(0, 8) + "..." , e.Font, Brushes.Black, e.Bounds.Left + 10, e.Bounds.Top + 4);
            else
                e.Graphics.DrawString(this.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + 10, e.Bounds.Top + 4);

            e.DrawFocusRectangle();            
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            for (int i = 0; i < this.TabPages.Count; i++)
            {
                Rectangle rect = this.GetTabRect(i);

                Rectangle closeButton = new Rectangle(rect.Right - 15, rect.Top + 4, 9, 7);

                if (closeButton.Contains(e.Location))
                {
                    // Diplay a message
                    TabPages.RemoveAt(i);
                }
            }
        }

    }
}
