using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp1
{
    class notify
    {
        System.Windows.Forms.NotifyIcon n;
        System.Windows.Forms.ContextMenuStrip nIconMenu;
        public notify(Action Closing,Action DoubleClick)
        {
            n = new System.Windows.Forms.NotifyIcon();
            n.DoubleClick += (object sender, EventArgs e) => { DoubleClick(); };
             n.Icon = new System.Drawing.Icon("HYTE_LOGO.ico");
            nIconMenu = new System.Windows.Forms.ContextMenuStrip();
            System.Windows.Forms.ToolStripMenuItem nIconMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            nIconMenuItem.Text = "exit()";
            nIconMenuItem.Click += (object sender, EventArgs e) => { Closing(); };
            System.Windows.Forms.ToolStripMenuItem nIconMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            nIconMenuItem2.Text = "exit2()";
            nIconMenuItem2.Click += (object sender, EventArgs e) => { Closing(); };
            nIconMenuItem.DropDownItems.Add(nIconMenuItem2);
            nIconMenu.Items.Add(nIconMenuItem);
            n.ContextMenuStrip = nIconMenu;
            n.Visible = true;
        }


        public void ShowIcon()
        {
            n.Visible = true;
        }

        public void Closing()
        {
            n.Visible = false;
        }
    }
}
