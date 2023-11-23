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

namespace doan_TH
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> data;
        public MainWindow()
        {
            InitializeComponent();
            data = new List<string>() { "Song1", "Song2", "Song3" };
            lsvlist.ItemsSource = data;
        }
        MediaPlayer mediaPlayer=new MediaPlayer();
        private void Card_MouseDown(object sender, MouseEventArgs e)
        {
            DragMove();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
       private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnReMove_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
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
