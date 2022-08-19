using System.Linq;
using Microsoft.Win32;//regedit

namespace ConsoleApp5
{
    class WindowsOpen
    {
        string Name;
        string CurrentPath;
        public WindowsOpen()
        {
            CurrentPath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            Name = CurrentPath.Split('\\').Last(); 

        }
        public bool Auto
        {
            get
            {
                var registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                var value = registryKey?.GetValue(Name);
                registryKey?.Close();
                if (value != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                var registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (value)
                {
                    registryKey?.SetValue(Name, CurrentPath);//註冊打開
                }
                else
                {
                    registryKey?.DeleteValue(Name);//刪除註冊
                }
                registryKey?.Close();
            }
        }

    }
}
