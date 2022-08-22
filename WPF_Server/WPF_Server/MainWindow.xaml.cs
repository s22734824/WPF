using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;
using System.Windows.Threading;

namespace WPF_Server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Socket serverSocket;
        public MainWindow()
        {
            InitializeComponent();
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint ie = new IPEndPoint(ip, 8000);
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(ie);
            serverSocket.Listen(10);    //設定最多10個排隊連線請求
            new System.Threading.Thread(work) { IsBackground = true }.Start();
        }
        void work()
        {
            while (true)
            {
                Socket clientSocket = serverSocket.Accept();
                MemoryStream fs = new MemoryStream();
                //int size = 0;
                int length = 0;
                do
                {
                    byte[] b = new byte[512];
                    length = clientSocket.Receive(b, b.Length, SocketFlags.None);
                    fs.Write(b, 0, length);
                }
                while (length != 0);
                //{
                //    byte[] b = new byte[512];
                //    length = clientSocket.Receive(b, b.Length, SocketFlags.None);
                //    fs.Write(b, 0, length);
                //}
                fs.Flush();
                this.Dispatcher.Invoke(() =>
                {
                    img.Source = BitmapFrame.Create(fs,
                                      BitmapCreateOptions.None,
                                      BitmapCacheOption.OnLoad);
                });
            }
        }
    }
}
