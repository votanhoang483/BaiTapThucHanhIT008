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
using MaterialDesignThemes.Wpf;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Markup;
using HtmlAgilityPack;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using VideoLibrary;
using System.IO;

namespace doan_TH
   
{
    
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string strcon = @"Server=tcp:it008music.database.windows.net,1433;Initial Catalog=Music;Persist Security Info=False;User ID=IT008_TH;Password=Phamkhaihung123.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
        SqlConnection sqlcon=null;
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

        private bool isPlaying = false;
        public MainWindow()
        {
            InitializeComponent();
            if (sqlcon == null)
            {
                sqlcon = new SqlConnection(strcon);
            }
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.CommandType = CommandType.Text;
            sqlcmd.CommandText = "select * from playlist";
            sqlcmd.Connection = sqlcon;
            SqlDataReader reader = sqlcmd.ExecuteReader();

            data = new ObservableCollection<string>() { };
            lsvlist.ItemsSource = data;
            while (reader.Read())
            {
                string name = reader.GetString(1);
                string link = reader.GetString(0);
                data.Add(name);
                fileMap[name] = link;
            }
            reader.Close();
            textBlockName = (TextBlock)FindName("Name");
            mediaPlayer.MediaOpened += MediaPlayer_MediaOpened;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // Cập nhật mỗi giây
            timer.Tick += Timer_Tick;
            updateTimer = new DispatcherTimer();
            updateTimer.Interval = TimeSpan.FromSeconds(1); // Cập nhật mỗi giây
            updateTimer.Tick += updateTimer_Tick;
            CurrentTrackIndex = 0;

            mediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
        }
        
        private void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {
            mediaPlayer.Stop();
            setPlayIcon();
            isPlaying = false;
            playNextSong();
        }

        private void playNextSong()
        {
            CurrentTrackIndex = (CurrentTrackIndex + 1) % data.Count;
            PlaySelectedTrack();
            setPlayIcon();
            isPlaying = true;
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

           if (sqlcon == null)
            {
                sqlcon = new SqlConnection(strcon);
            }
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
           
         
                        SqlCommand sqlcmd1 = new SqlCommand();

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (String file in openFileDialog.FileNames)
                {
                    fullfilepath = openFileDialog.FileName;
                    filename = System.IO.Path.GetFileNameWithoutExtension(fullfilepath);
                    data.Add(filename);
                    fileMap[filename] = fullfilepath;

                }



                sqlcmd1.CommandType = CommandType.Text;
                sqlcmd1.CommandText = "insert into playlist (link,name) " + "values (N'" + fullfilepath + "', N'" + filename + "')";
                sqlcmd1.Connection = sqlcon;


                try
                {
                    int kq = sqlcmd1.ExecuteNonQuery();
                    if (kq > 0)
                    {
                        MessageBox.Show("Add Song successfully");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }

                sqlcmd1.CommandType = CommandType.Text;
                sqlcmd1.CommandText = "insert into playlist (link,name) " + "values (N'" + fullfilepath + "', N'" + filename + "')";
                sqlcmd1.Connection = sqlcon;

             
            }


                }
        private void btnReMove_Click(object sender, RoutedEventArgs e)
        {
            if (sqlcon == null)
            {
                sqlcon = new SqlConnection(strcon);
            }
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sqlcmd2 = new SqlCommand();
            mediaPlayer.Pause();
            data.Remove(content);
            textBlockName.Text = "Choose a song";
            sqlcmd2.CommandType = CommandType.Text;
            sqlcmd2.CommandText = "Delete  from playlist where name=N'" + content + "'";
            sqlcmd2.Connection = sqlcon;
            try
            {
                int kq = sqlcmd2.ExecuteNonQuery();
                if (kq > 0)
                {
                    MessageBox.Show("Delete Song successfully");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }


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
        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            
            if(data.Count>0 &&content !=null)
            {
                CurrentTrackIndex = (CurrentTrackIndex - 1 + data.Count) % data.Count;
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
            if (content == content1)
            {
                mediaPlayer.Play();
            }
            if(isPlaying)
            {
                mediaPlayer.Pause();
            }    
            else
            {
                mediaPlayer.Play();
            }    
            setPlayIcon();
            isPlaying = !isPlaying;

        }

        public void setPlayIcon()
        {
            PackIconKind iconKind;
            if (isPlaying)
            {
                iconKind = PackIconKind.Play;
            }
            else
            {
                iconKind = PackIconKind.Pause;
            }
            btnPlay.Content = new PackIcon { Kind = iconKind };
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
            isPlaying = false;
            btnPlay_Click(sender, e);
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
            volumeSlider.Value = mediaPlayer.Volume * 100.0;
            double volumeValue = volumeSlider.Value;
            setVolumeIcon(volumeValue);
        }

        private void setVolumeIcon(double volumeValue)
        {
            PackIconKind iconKind;

            if (volumeValue == 0)
            {
                iconKind = PackIconKind.VolumeOff;
            }
            else if (volumeValue < 50)
            {
                iconKind = PackIconKind.VolumeLow;
            } 
            else
            {
                iconKind = PackIconKind.VolumeHigh;
            }    
            btnVolume.Content = new PackIcon { Kind = iconKind }; 
        }

        private void volumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double volumeValue = volumeSlider.Value;
            mediaPlayer.Volume = volumeValue / 100.0;
            setVolumeIcon(e.NewValue);
        }


        private void btnVolume_MouseEnter(object sender, MouseEventArgs e)
        {
            volumeSlider.Visibility = Visibility.Visible;
        }

        private void btnVolume_MouseLeave(object sender, MouseEventArgs e)
        {
            
            if(!IsMouseOverButtonOrSlider())
            {
                volumeSlider.Visibility = Visibility.Collapsed;
            }    
        }

        private bool IsMouseOverButtonOrSlider()
        {

            return btnVolume.IsMouseOver || volumeSlider.IsMouseOver ;

       

        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            volumeSlider.Visibility = Visibility.Visible;
            volumeSlider.Focus();
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!IsMouseOverButtonOrSlider())
            {
                volumeSlider.Visibility = Visibility.Collapsed;
            }
        }

        private void volumeSlider_MouseEnter(object sender, MouseEventArgs e)
        {
            volumeSlider.Visibility = Visibility.Visible;
        }

        private void volumeSlider_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!IsMouseOverButtonOrSlider())
            {
                volumeSlider.Visibility = Visibility.Collapsed;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                Download(Link.Text);
                MessageBox.Show("Downloaded");
            }
            catch
            {
                MessageBox.Show("Failed to download");
            }
        }
     
        public void Download(string s)
        {
            var yt = YouTube.Default;
            var video = yt.GetVideo(s);
            File.WriteAllBytes(@"C: \Users\PC\OneDrive\Máy tính\BaiTapThucHanhIT008\doan_TH\Downloaded media\"+video.FullName,video.GetBytes());
        }

    }
}
