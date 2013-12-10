using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQUtil;

namespace IQChampionsServiceLibrary
{
    public class Room
    {
        public const int MAXPLAYERS = 4;

        public GameTable Table { get; set; }
        public string Name { get; set; }
        public List<User> Players { get; set; }
        public List<Message> Chat { get; set; }
        public Question Question { get { return question; } }
        public bool Finished { get { return finished; } }
        public int TimeLeft
        {
            get
            {
                lock (lockObject)
                {
                    return timeleft;
                }
            }
        }

        private const int turnTimeout = 30000;
        private static readonly byte[] defaultcolor = new byte[] { 225, 225, 225 };
        private static readonly byte[][] playercolors = new byte[][] { 
            new byte[] { 0, 0, 255 },   // Blue
            new byte[] { 0, 255, 0 },   // Green
            new byte[] { 255, 0, 0 },   // Red
            new byte[] { 255, 175, 0 }  // Orange
        };

        private static Object lockObject = new Object();
        private BackgroundWorker turnworker = null;
        private List<User> rollable;
        private User actualPlayer = new User();
        private Cell actualCell = null;
        private Question question = null;
        private bool finished = false;
        private int maxturns;
        private int timeleft;

        public Room()
        {
            init();
            Name = Guid.NewGuid().ToString();
        }

        public Room(string name)
        {
            init();
            Name = name;
        }

        private void init()
        {
            Players = new List<User>();
            Table = new GameTable(6, 4);
            turnworker = new BackgroundWorker();
            turnworker.DoWork += turn;
            turnworker.RunWorkerCompleted += stop;

            Chat = new List<Message>();
            maxturns = IQService.Turns;
            timeleft = -1;
        }

        public void addUser(User user)
        {
            if (Players.Count <= Room.MAXPLAYERS)
            {
                user.Color = playercolors[Players.Count];
                Players.Add(user);
            }
            else throw new OverflowException("Too much users in " + Name + " room!");
        }

        public void start()
        {
            foreach (Cell c in Table.Table)
            {
                if (c.Col == 0 && c.Row == 0) c.Owner = Players[0];
                else if (c.Col == 0 && c.Row == 1) c.Owner = Players[0];
                else if (c.Col == 0 && c.Row == 4) c.Owner = Players[1];
                else if (c.Col == 0 && c.Row == 5) c.Owner = Players[1];
                else if (c.Col == 3 && c.Row == 0) c.Owner = Players[2];
                else if (c.Col == 3 && c.Row == 1) c.Owner = Players[2];

                else if (c.Col == 3 && c.Row == 4) c.Owner = Players[3];
                else if (c.Col == 3 && c.Row == 5) c.Owner = Players[3];
                else c.Owner = new User() { Color = defaultcolor, State = States.IDLE };
            }
            foreach (User u in Players)
            {
                u.Point = 0;
            }
            turnworker.RunWorkerAsync();
        }

        private void turn(object sender, DoWorkEventArgs e)
        {
            int turn = 0;
            do
            {
                actualPlayer = null;
                actualCell = null;
                using (IQDatabase database = new IQDatabase())
                {
                    question = new Question(database.dbQuestionSet.OrderBy(r => Guid.NewGuid()).First());
                }
                selectNextMove();

                timeleft = turnTimeout / 1000;
                Stopwatch stopper = new Stopwatch();
                stopper.Start();
                // Lépésre vár
                while (stopper.ElapsedMilliseconds < turnTimeout && actualCell == null)
                {
                    lock (lockObject)
                    {
                        timeleft = (turnTimeout / 1000) - (int)(stopper.ElapsedMilliseconds / 1000);
                    }
                    Thread.Sleep(IQService.Pingperiod);
                }

                if (actualCell != null)
                {

                    actualCell.Owner.State = States.ANSWER;

                    stopper.Restart();
                    while (stopper.ElapsedMilliseconds < turnTimeout &&
                        (actualPlayer.State == States.ANSWER ||
                        (actualCell.Owner.State == States.ANSWER && actualCell.Owner.Name != null)))
                    {
                        lock (lockObject)
                        {
                            timeleft = (turnTimeout / 1000) - (int)(stopper.ElapsedMilliseconds / 1000);
                        }
                        Thread.Sleep(IQService.Pingperiod);
                    }
                    timeleft = 0;

                    // Lejárt az idő: a válasz rossz
                    if (actualPlayer.AnswerResult == null)
                        actualPlayer.AnswerResult = new AnswerResult() { Answer = false, Time = DateTime.Now };
                    if (actualCell.Owner.AnswerResult == null)
                        actualCell.Owner.AnswerResult = new AnswerResult() { Answer = false, Time = DateTime.Now };

                    if (actualPlayer.AnswerResult.Answer == false || actualPlayer.AnswerResult.Time > actualCell.Owner.AnswerResult.Time)
                    {
                        // Védő nyert
                        actualCell.Owner.Point += 10;
                        actualPlayer.Point -= 10;
                    }
                    else
                    {
                        // Támadó nyert
                        actualCell.Owner.Point -= 10;
                        actualPlayer.Point += 10;
                        actualCell.Owner = actualPlayer;
                    }

                    actualCell.Owner.State = States.IDLE;
                    actualCell.Owner.AnswerResult = null;
                }
                actualPlayer.State = States.IDLE;
                actualPlayer.AnswerResult = null;
                turn++;
            } while (turn < maxturns);

            // Játék vége
            foreach (User u in Players)
            {
                u.State = States.FINISHED;
            }
            Thread.Sleep(IQService.Timeout);
            finished = true;
        }

        public void selectNextMove()
        {
            foreach (User u in Players) u.State = States.IDLE;
            if (rollable == null || rollable.Count == 0)
            {
                rollable = new List<User>();
                rollable.AddRange(Players);
            }

            int r = IQService.rand.Next(rollable.Count);

            if (IQService.Debug)
            {
                int debug = -1;
                debug = rollable.FindIndex(x => x.Name.Equals("debug"));
                if (debug != -1) r = debug;
            }

            actualPlayer = rollable[r];
            rollable.RemoveAt(r);

            actualPlayer.State = States.MOVE;
        }

        public bool Move(int col, int row)
        {
            actualCell = Table.Table.Find(c => c.Row == col && c.Col == row);
            bool attackable = false;
            foreach (Cell c in Table.Table)
            {
                if (c.Owner == actualPlayer)
                    if (c.neighbor(actualCell))
                    {
                        attackable = true;
                        break;
                    }
            }
            // vizsgálat nincs megvalósítva
            if (attackable)
            {
                // Támad
                actualPlayer.State = States.ANSWER;
                actualCell.Owner.State = States.ANSWER;
                return true;
            }
            else
            {
                // Rossz mező
                actualCell = null;
                return false;
            }
        }

        public bool Answer(User user, int id)
        {
            user.State = States.IDLE;
            if (id == 0)
            {
                user.AnswerResult = new AnswerResult() { Answer = true, Time = DateTime.Now };
                return true;
            }
            else
            {
                user.AnswerResult = new AnswerResult() { Answer = false, Time = DateTime.Now };
                return false;
            }

        }

        private void stop(object sender, RunWorkerCompletedEventArgs e)
        {
            using (IQDatabase database = new IQDatabase())
            {
                try
                {
                    Statistic s = new Statistic(this);
                    string name = s.Users[0].Name;
                    database.dbUserSet.First(x => x.name.Equals(name)).win++;
                                      
                    foreach (User u in s.Users)
                    {
                        name = u.Name;
                        database.dbUserSet.First(x => x.name.Equals(name)).played++;                        
                    }
                    database.SaveChanges();

                }
                catch (Exception ex)
                {
                    Logger.log(Errorlevel.WARN, ex.Message + Environment.NewLine + ex.StackTrace);
                }
            }
            /*
            using (IQDatabase database = new IQDatabase())
            {
                try
                {
                    
                    IQueryable<dbUserSet> dbul = database.dbUserSet.Where(x =>
                        x.name.Equals(s.Users[0].Name) ||
                        x.name.Equals(s.Users[1].Name) ||
                        x.name.Equals(s.Users[2].Name) ||
                        x.name.Equals(s.Users[3].Name)
                        );
                    foreach (dbUserSet u in dbul)
                    {
                        if (u.name.Equals(s.Users[0].Name))
                        {
                            u.win++;
                        }
                        u.played++;
                    }
                    database.SaveChanges();
                }
                catch (Exception ex)
                {
                    Logger.log(Errorlevel.ERROR, ex.Message + Environment.NewLine + ex.StackTrace);
                }
            }*/
        }
    }
}
