using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
using static System.Net.Mime.MediaTypeNames;


namespace doan_TH
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> data;
        private string s;
        public MainWindow()
        {
            InitializeComponent();
            data = new List<string>() { "Song1" };
            lsvlist.ItemsSource = data;
        }
        MediaPlayer mediaPlayer=new MediaPlayer();
        private void Card_MouseDown(object sender, MouseEventArgs e)
        {
            DragMove();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
       private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg=new OpenFileDialog();
            dlg.ShowDialog();
            s=dlg.FileName;
            dlg.ShowDialog();
            data.Add(s);
            lsvlist.ItemsSource= data;

        }
        private void btnReMove_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Open(new Uri("D:\\Project1\\BaiTapThucHanhIT008\\doan_TH\\test\\5016944936596504302.mp4"));
            mediaPlayer.Play();
        }
        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Pause();
        }
    }
}
