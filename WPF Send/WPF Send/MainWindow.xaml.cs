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
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace WPF_Send
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            Nullable<bool> result = openFileDialog.ShowDialog();
            if (result == true)
            {
                var img = new BitmapImage(new Uri(openFileDialog.FileName));
                BImg.Source = img;
                MemoryStream rec = StreamFromBitmapSource(img);
                SockSend(rec);
            }
            
        }
        private MemoryStream StreamFromBitmapSource(BitmapSource writeBmp)
        {
            MemoryStream bmp = new MemoryStream();
            BitmapEncoder enc = new BmpBitmapEncoder();
            enc.Frames.Add(BitmapFrame.Create(writeBmp));
            enc.Save(bmp);
            return bmp;
        }
        void SockSend(MemoryStream fs)
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(new IPEndPoint(ip, 8000)); //配置伺服器IP與埠           
            int b = 0;
            int count = 0;
            fs.Position = 0;
            //long contentLength = fs.Length;
            //clientSocket.Send(BitConverter.GetBytes(contentLength));
            while (count<fs.Length)
            {
                byte[] buffer = new byte[512];
                b = fs.Read(buffer, 0, buffer.Length);
                clientSocket.Send(buffer, b, SocketFlags.None);
                count += b;
            } 
            clientSocket.Close();
        }
    }
}
