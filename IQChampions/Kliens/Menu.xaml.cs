using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using iqchampion_design.ServiceReference;

namespace iqchampion_design
{
    public partial class Menu : Window
    {
        private Login parent = null;
        private BackgroundWorker pingworker = null;
        private BackgroundWorker queueworker = null;

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

        public Menu(Login parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.Title = "Bejelentkezve mint " + User;

            pingworker = new BackgroundWorker();
            pingworker.WorkerSupportsCancellation = true;
            pingworker.DoWork += ping;
            pingworker.RunWorkerCompleted += timeout;

            queueworker = new BackgroundWorker();
            queueworker.WorkerSupportsCancellation = true;
            queueworker.WorkerReportsProgress = true;
            queueworker.DoWork += queueCheck;
            queueworker.ProgressChanged += queueProgressChanged;
            queueworker.RunWorkerCompleted += startGame;
        }

        public void ClearFields()
        {
            ButtonJatek.Content = "Játék";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            pingworker.RunWorkerAsync();

            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 2 && args[2].Equals("Play"))
            {
                ButtonClickGameRandom(this, null);
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (queueworker.IsBusy)
            {
                queueworker.CancelAsync();
                ButtonJatek.Content = "Játék";
            }
            if (pingworker.IsBusy)
            {
                ButtonClickLogout(sender, null);
            }
            parent.Show();
            Cursor = Cursors.Arrow;
        }

        private void ping(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                int sleepdur = 250;
                for (int i = 0; i < PingPeriod / sleepdur; i++)
                {
                    if ((sender as BackgroundWorker).CancellationPending)
                    {
                        Client.Logout(User);
                        return;
                    }
                    Thread.Sleep(sleepdur);
                }
                if (!stop && !Login.Client.Ping(User))
                {
                    MessageBox.Show("Időtúllépés!");
                    return;
                }
            }
        }

        private void timeout(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }

        private void queueCheck(object sender, DoWorkEventArgs e)
        {
            while (!Client.haveRoom(User))
            {
                if ((sender as BackgroundWorker).CancellationPending)
                {
                    return;
                }
                (sender as BackgroundWorker).ReportProgress(0, Client.getQueuePosition(User));
                Thread.Sleep(PingPeriod);
            }
        }

        void queueProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //kiiratni csicsa picsa
            ButtonJatek.Content = ((int)e.UserState < 1 ? "Még " + ((int)e.UserState * -1) + " játékos kell" : e.UserState + ". vagy a sorban");
        }

        void startGame(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                GameTable game = new GameTable(this);
                this.Hide();
                game.Show();
            }
        }

        bool stop = false;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            stop = !stop;
            debugbutton.Content = "Pinging is " + (stop ? "off" : "on");
        }
        
        private void ButtonClickGameRandom(object sender, RoutedEventArgs e)
        {
            if (queueworker.IsBusy && MessageBox.Show("Valóban ki akarsz lépni a sorból?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Client.leaveQueue(User);
                queueworker.CancelAsync();
                ButtonJatek.Content = "Játék";
            }

            else
            {
                Client.joinQueue(User);
                queueworker.RunWorkerAsync();
            }
        }

        private void ButtonClickGameWithFriends(object sender, RoutedEventArgs e)
        {
            Lobby lobby = new Lobby(this);
            lobby.Show();
            this.Hide();
        }

        private void ButtonClickProfil(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://localhost:3032/Account/Register"); // Link to user profile
        }

        private void ButtonClickLogout(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            parent.ClearFields();
            pingworker.CancelAsync();
        }
    }
}
