using iqchampion_design.ServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections;
using IQUtil;

namespace iqchampion_design
{
    public partial class GameTable : Window
    {
        private Menu parent = null;
        private BackgroundWorker refreshworker = null;
        private BackgroundWorker activityworker = null;
        private bool enableMoving = false;

        private string User
        {
            get { return Login.User; }
        }
        private IQServiceClient Client
        {
            get { return Login.Client; }
        }
        private int PingPeriod
        {
            get { return Login.PingPeriod; }
        }

        ImageBrush myBrush;
        ImageBrush myBrush2;

        ImageBrush korong_normal;
        ImageBrush korong_red;
        ImageBrush korong_blue;
        ImageBrush korong_green;
        ImageBrush korong_yellow;
        ImageBrush korong_base;
        ImageBrush korong_base_red;
        ImageBrush korong_base_blue;
        ImageBrush korong_base_green;
        ImageBrush korong_base_yellow;

        private void initImages()
        {
            myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri(".\\Resources\\keret.png", UriKind.Relative));

            korong_normal = new ImageBrush();
            korong_base = new ImageBrush();
            korong_red = new ImageBrush();
            korong_base_red = new ImageBrush();
            korong_blue = new ImageBrush();
            korong_base_blue = new ImageBrush();
            korong_green = new ImageBrush();
            korong_base_green = new ImageBrush();
            korong_yellow = new ImageBrush();
            korong_base_yellow = new ImageBrush();
            korong_normal.ImageSource = new BitmapImage(new Uri(".\\Resources\\korong_normal.png", UriKind.Relative));
            korong_base.ImageSource = new BitmapImage(new Uri(".\\Resources\\korong_base.png", UriKind.Relative));
            korong_red.ImageSource = new BitmapImage(new Uri(".\\Resources\\korong_red.png", UriKind.Relative));
            korong_base_red.ImageSource = new BitmapImage(new Uri(".\\Resources\\korong_base_red.png", UriKind.Relative));
            korong_blue.ImageSource = new BitmapImage(new Uri(".\\Resources\\korong_blue.png", UriKind.Relative));
            korong_base_blue.ImageSource = new BitmapImage(new Uri(".\\Resources\\korong_base_blue.png", UriKind.Relative));
            korong_green.ImageSource = new BitmapImage(new Uri(".\\Resources\\korong_green.png", UriKind.Relative));
            korong_base_green.ImageSource = new BitmapImage(new Uri(".\\Resources\\korong_base_green.png", UriKind.Relative));
            korong_yellow.ImageSource = new BitmapImage(new Uri(".\\Resources\\korong_yellow.png", UriKind.Relative));
            korong_base_yellow.ImageSource = new BitmapImage(new Uri(".\\Resources\\korong_base_yellow.png", UriKind.Relative));

        }

        public GameTable(Menu parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.Title = parent.Title;
            ListBoxChatMessages.Items.Clear();

            refreshworker = new BackgroundWorker();
            refreshworker.WorkerSupportsCancellation = true;
            refreshworker.WorkerReportsProgress = true;
            refreshworker.DoWork += refresh;
            refreshworker.ProgressChanged += refreshUi;

            activityworker = new BackgroundWorker();
            activityworker.WorkerSupportsCancellation = true;
            activityworker.DoWork += wait;
            activityworker.RunWorkerCompleted += doActivity;

            initImages();


            kérdéskártya.Background = myBrush;

        }




        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            refreshworker.RunWorkerAsync();
            activityworker.RunWorkerAsync();
        }

        private void refresh(object sender, DoWorkEventArgs e)
        {
            do
            {
                (sender as BackgroundWorker).ReportProgress(0, Client.getGameTable(User));
                (sender as BackgroundWorker).ReportProgress(0, Client.getStatistics(User));
                (sender as BackgroundWorker).ReportProgress(0, Client.getMesages(User));
                Thread.Sleep(PingPeriod);
            } while (!(sender as BackgroundWorker).CancellationPending);
        }

        private void refreshUi(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState == null) return;
            else if (e.UserState is ServiceReference.GameTable)
            {
                foreach (Cell c in (e.UserState as ServiceReference.GameTable).Table)
                {
                    ImageBrush cellakep;

                    if (c.isBase)
                    {
                        switch ("" + (int)c.Owner.Color[0] + (int)c.Owner.Color[1] + (int)c.Owner.Color[2])
                        {
          //  new byte[] { 0, 0, 255 },   // Blue
           // new byte[] { 0, 255, 0 },   // Green
          //  new byte[] { 255, 0, 0 },   // Red
          //  new byte[] { 255, 175, 0 }  // Orange
                            case "00255":
                                cellakep = korong_base_blue;
                                break;
                            case "25500":
                                cellakep = korong_base_red;
                                break;
                            case "02550":
                                cellakep = korong_base_green;
                                break;
                            case "2551750":
                                cellakep = korong_base_yellow;
                                break;
                            default:
                                cellakep = korong_normal;
                                break;
                        }
                    }
                    else {
                        switch ("" + (int)c.Owner.Color[0] + (int)c.Owner.Color[1] + (int)c.Owner.Color[2])
                        {
                            //  new byte[] { 0, 0, 255 },   // Blue
                            // new byte[] { 0, 255, 0 },   // Green
                            //  new byte[] { 255, 0, 0 },   // Red
                            //  new byte[] { 255, 175, 0 }  // Orange
                            case "00255":
                                cellakep = korong_blue;
                                break;
                            case "25500":
                                cellakep = korong_red;
                                break;
                            case "02550":
                                cellakep = korong_green;
                                break;
                            case "2551750":
                                cellakep = korong_yellow;
                                break;
                            default:
                                cellakep = korong_normal;
                                break;
                        }
                    }

                    ((Rectangle)GridGameTable.FindName("cell" + c.Row + c.Col)).Fill = cellakep;
                }
            }
            else if (e.UserState is Statistic)
            {
                Statistic stat = e.UserState as Statistic;
                LabelActualPlayer.Content = "Aktív: ";
                foreach (User u in stat.Users)
                {
                    if (u.State != States.IDLE) LabelActualPlayer.Content += u.Name + " ";
                }

                for (int i = 0; i < 4; i++)
                {
                    Label l = (WindowGrid.FindName("LabelScore" + i) as Label);
                    User u = stat.Users[i];

                    l.Foreground = new SolidColorBrush(Color.FromRgb(u.Color[0], u.Color[1], u.Color[2]));
                    if (u.Name.Equals(User)) l.Content = "Én: " + u.Point + " pont";
                    else l.Content = u.Name + ": " + u.Point + " pont";
                }
            }
            else if (e.UserState is Message[])
            {
                Message[] chat = (e.UserState as Message[]);
                ListBoxChatMessages.Items.Clear();
                for (int i = chat.Length - 1; i >= 0; i--)
                {
                    User u = chat[i].Sender;
                    Label l = new Label();
                    l.Foreground = new SolidColorBrush(Color.FromRgb(u.Color[0], u.Color[1], u.Color[2]));
                    string s = chat[i].Time.ToString("HH:mm") + " [" + u.Name + "] " + chat[i].Msg;
                    l.Content = StringExtensions.MultiInsert(s, "\r\n  ", 30);
                    ListBoxChatMessages.Items.Add(l);
                }
            }
        }


        private void wait(object sender, DoWorkEventArgs e)
        {
            while ((States)(e.Result = Client.getMyState(User)) == States.IDLE)
            {
                Thread.Sleep(PingPeriod);
            }
        }

        private void doActivity(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((States)e.Result == States.ANSWER)
            {
                //get the question here
                MessageBox.Show(Client.getQuestion(User).Questionn);

                bool good = Client.answerQuestion(User, 0);
                activityworker.RunWorkerAsync();
                if (good)
                {
                    MessageBox.Show("Helyes válasz");
                }
                else
                {
                    MessageBox.Show("Rossz válasz");
                }
            }
            else if ((States)e.Result == States.MOVE)
            {
                enableMoving = true;
                GridGameTable.Opacity = 1;
                //MessageBox.Show("Te jösz!");
            }
            else if ((States)e.Result == States.FINISHED)
            {
                refreshworker.CancelAsync();
                GridGameTable.Opacity = 1;

                MessageBox.Show("Játék vége!\r\n" + Client.getStatistics(User).Users[0].Name + " megnyerte a játékot!");
            }
        }

        private void cellMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (enableMoving)
                {
                    string rname = (sender as Rectangle).Name;
                    if (Client.Move(User, int.Parse(rname.Substring(4, 1)), int.Parse(rname.Substring(5, 1))))
                    {
                        GridGameTable.Opacity = 0.3;
                        enableMoving = false;
                        MessageBox.Show(Client.getQuestion(User).Questionn);
                        // megválaszolta...
                        bool good = Client.answerQuestion(User, 0);
                        activityworker.RunWorkerAsync();
                        if (good)
                        {
                            MessageBox.Show("Helyes válasz");
                        }
                        else
                        {
                            MessageBox.Show("Rossz válasz");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Rossz mező!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            parent.ClearFields();
            parent.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextEntered(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Client.Send(User, TextBoxChatWrite.Text);
                TextBoxChatWrite.Text = "";
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxChatWrite.Text = "";
        }
    }
}
