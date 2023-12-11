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
using System.Windows.Threading;

namespace doan_TH
   
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer updateTimer;
        string fullfilepath;
        string content;
        string filename;
        private DispatcherTimer timer;
        private ObservableCollection<string> data;
        private Dictionary<string, string> fileMap = new Dictionary<string, string>();
        TextBlock textBlockName;
        public MainWindow()
        {
            InitializeComponent();
            data = new ObservableCollection<string>() {  };
            lsvlist.ItemsSource = data;
            textBlockName = (TextBlock)FindName("Name");
            mediaPlayer.MediaOpened += MediaPlayer_MediaOpened;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // Cập nhật mỗi giây
            timer.Tick += Timer_Tick;
            updateTimer = new DispatcherTimer();
            updateTimer.Interval = TimeSpan.FromSeconds(1); // Cập nhật mỗi giây
            updateTimer.Tick += updateTimer_Tick;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                TimerSlider.Value = mediaPlayer.Position.TotalSeconds;
                lblCurrenttime.Text = mediaPlayer.Position.ToString(@"mm\:ss");
            }
        }
        private void updateTimer_Tick(object sender, EventArgs e)
        {
            if (mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                TimerSlider.Value = mediaPlayer.Position.TotalSeconds;
                lblCurrenttime.Text = mediaPlayer.Position.ToString(@"mm\:ss");
            }
        }

        private void MediaPlayer_MediaOpened(object sender, EventArgs e)
        {
            if (mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                TimeSpan duration = mediaPlayer.NaturalDuration.TimeSpan;
                TimerSlider.Maximum = duration.TotalSeconds;
                lblMusiclength.Text = duration.ToString(@"mm\:ss");
            }
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
            //string sampleMusicPath = @"C:\Users\ACER\Source\Repos\BaiTapThucHanhIT008\doan_TH\bin\Debug\sample.mp3";
            //filename = System.IO.Path.GetFileNameWithoutExtension(sampleMusicPath);
            //data.Add(filename);
            //fileMap[filename] = sampleMusicPath;
        }

        private void btnReMove_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
           
        }
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            if (content != null)
            {
                string fullFilePath = fileMap[content];
                mediaPlayer.Open(new Uri(fullFilePath));
                mediaPlayer.Play();
                updateTimer.Start();
            }
        }
        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Pause();
            updateTimer.Stop();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            content = button.Content as string;
            textBlockName.Text = content;
        }

        private void TimerSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (mediaPlayer.NaturalDuration.HasTimeSpan && Math.Abs(TimerSlider.Value - mediaPlayer.Position.TotalSeconds) > 1.0)
            {
                mediaPlayer.Position = TimeSpan.FromSeconds(TimerSlider.Value);
                lblCurrenttime.Text = mediaPlayer.Position.ToString(@"mm\:ss");
            }
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            updateTimer.Stop();
            Application.Current.Shutdown();
        }
    }
}
