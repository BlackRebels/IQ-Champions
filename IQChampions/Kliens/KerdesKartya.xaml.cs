using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public enum AnswerState
    {
        Null,
        Good,
        Bad,
        Timeout
    }
    /// <summary>
    /// Interaction logic for KerdesKartya.xaml
    /// </summary>
    public partial class KerdesKartya : Window
    {
        private BackgroundWorker timeoutworker = null;
        private AnswerState answer;

        public AnswerState Answer
        {
            get { return answer; }
        }
        private string User { get { return Login.User; } }
        private IQServiceClient Client { get { return Login.Client; } }
        private int PingPeriod { get { return Login.PingPeriod; } }

        public KerdesKartya(GameTable parent)
        {
            InitializeComponent();
            this.Left = parent.Left + 75;
            this.Top = parent.Top + 100;
            /*
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri(".\\Resources\\keret.png", UriKind.Relative));
            this.Background = myBrush;
             */
            answer = AnswerState.Null;

            timeoutworker = new BackgroundWorker();
            timeoutworker.WorkerReportsProgress = true;
            timeoutworker.WorkerSupportsCancellation = true;
            timeoutworker.DoWork += getTime;
            timeoutworker.ProgressChanged += tick;
            timeoutworker.RunWorkerCompleted += timeout;

            Question kerdes = Client.getQuestion(User);

            kerdess.Text = kerdes.Questionn;
            valasz1.Content = kerdes.GoodAnswer;
            valasz2.Content = kerdes.BadAnswer1;
            valasz3.Content = kerdes.BadAnswer2;
            valasz4.Content = kerdes.BadAnswer3;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timeoutworker.RunWorkerAsync();
        }

        void getTime(object sender, DoWorkEventArgs e)
        {
            int last = 30;
            int prev = 30;
            BackgroundWorker bw = (sender as BackgroundWorker);
            while (!bw.CancellationPending && (last = Client.getTimeLeft(User)) > 0)
            {
                if (prev != last)
                {
                    prev = last;
                    bw.ReportProgress(last);
                }
                Thread.Sleep(PingPeriod);
            }
            if (last == 0)
            {
                answer = AnswerState.Timeout;
            }
        }

        void tick(object sender, ProgressChangedEventArgs e)
        {
            time.Content = e.ProgressPercentage;
        }

        private void timeout(object sender, RunWorkerCompletedEventArgs e)
        {
            Hide();
        }

        private void valasz(object sender, RoutedEventArgs e)
        {
            int i = 0;
            if (valasz1.IsChecked == true) i = 0;
            else if (valasz2.IsChecked == true) i = 1;
            else if (valasz3.IsChecked == true) i = 2;
            else if (valasz4.IsChecked == true) i = 3;

            if (Client.answerQuestion(User, i)) answer = AnswerState.Good;
            else answer = AnswerState.Bad;

            timeoutworker.CancelAsync();
        }
    }
}
