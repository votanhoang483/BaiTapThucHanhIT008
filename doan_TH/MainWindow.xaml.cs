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
        private int CurrentTrackIndex;
        string content1 = "";
        private double previousVolume;
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
            CurrentTrackIndex = 0;
        }
        
        private void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (String file in openFileDialog.FileNames)
                {
                    fullfilepath = file;
                    filename = System.IO.Path.GetFileNameWithoutExtension(fullfilepath);
                    data.Add(filename);
                    fileMap[filename] = fullfilepath;
                }

            }
            //string sampleMusicPath = @"C:\Users\ACER\Source\Repos\BaiTapThucHanhIT008\doan_TH\bin\Debug\sample.mp3";
            //filename = System.IO.Path.GetFileNameWithoutExtension(sampleMusicPath);
            //data.Add(filename);
            //fileMap[filename] = sampleMusicPath;
        }

        private void btnReMove_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Pause();
            data.Remove(content);
            textBlockName.Text = "Choose a song";

        }
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (data.Count > 0 && content != null)
            {
                CurrentTrackIndex = (CurrentTrackIndex + 1) % data.Count;
                PlaySelectedTrack();
            }
        }
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            if (content != content1 && content!=null )
            {
                string fullFilePath = fileMap[content];
                mediaPlayer.Open(new Uri(fullFilePath));
                content1 = content;
                mediaPlayer.Play();
                updateTimer.Start();
            }
            if (content ==content1 )
            {
               mediaPlayer.Play();
            }   

        }
        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            
            if(data.Count>0 &&content !=null)
            {
                CurrentTrackIndex=(CurrentTrackIndex-1+data.Count)%data.Count;
                PlaySelectedTrack();
            }
        }
   
        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Pause();
        }
        private void btnRePlay_Click(Object sender, RoutedEventArgs e)
        {
            if (content != null)
            {
                string fullFilePath = fileMap[content];
                mediaPlayer.Open(new Uri(fullFilePath));
                mediaPlayer.Play();
                updateTimer.Start();
            }
        }
        private void PlaySelectedTrack()
        {
            if(CurrentTrackIndex>=0 &&CurrentTrackIndex<data.Count)
            {
                content = data[CurrentTrackIndex];
                textBlockName.Text = content;
                string fullFilePath = fileMap[content];
                mediaPlayer.Open(new Uri(fullFilePath) );
                mediaPlayer.Play();
                updateTimer.Start();
            }
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

        private void btnVolume_Click(object sender, RoutedEventArgs e)
        {
            if (mediaPlayer.Volume > 0)
            {
                previousVolume = mediaPlayer.Volume;
                mediaPlayer.Volume = 0;
            }
            else
            {
                mediaPlayer.Volume = previousVolume;
            }
            volumeSlider.Value = mediaPlayer.Volume;
        }

        private void volumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double volumeValue = volumeSlider.Value;
            mediaPlayer.Volume = volumeValue;
        }

        private void btnVolume_MouseEnter(object sender, MouseEventArgs e)
        {
            volumeSlider.Visibility = Visibility.Visible;
        }

        private void btnVolume_MouseLeave(object sender, MouseEventArgs e)
        {
            volumeSlider.Visibility = Visibility.Collapsed;
        }


    }
}
