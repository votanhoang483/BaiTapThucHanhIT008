using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        string fullfilepath;
        string content;
        string filename;
        private ObservableCollection<string> data;
        private Dictionary<string, string> fileMap = new Dictionary<string, string>();
        TextBlock textBlockName;
        public MainWindow()
        {
            InitializeComponent();
            
          data = new ObservableCollection<string>() {  };
            lsvlist.ItemsSource = data;
            textBlockName = (TextBlock)FindName("Name");
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
           
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                 fullfilepath = openFileDialog.FileName;
                 filename = System.IO.Path.GetFileNameWithoutExtension(fullfilepath);
                data.Add(filename);
                fileMap[filename] = fullfilepath;

            }
           
        }
        private void btnReMove_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
           
        }
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {       if (content!=null)
            {
                string fullFilePath = fileMap[content];
                mediaPlayer.Open(new Uri(fullFilePath));
                mediaPlayer.Play();
            }
            
        }
        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Pause();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            content = button.Content as string;
            textBlockName.Text = content;
        }

    }
}
