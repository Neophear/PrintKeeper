using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace PrintKeeper
{
    class TimerPlus : IDisposable
    {
        private DateTime started = new DateTime();

        public event EventHandler Tick;
        public void OnTick()
        {
            EventHandler handler = Tick;
            if (null != handler) handler(this, EventArgs.Empty);
        }

        private Timer _timer;

        private int _minutes;
        public int Minutes
        {
            get
            {
                return _minutes;
            }
            set
            {
                _minutes = value;
                _timer.Interval = value * 1000 * 60;
            }
        }
        public string TimeLeft
        {
            get
            {
                string returnString = "";

                if (!_timer.Enabled)
                    returnString = "Timer is not running";
                else
                {
                    TimeSpan left = (started.Add(new TimeSpan(0, _minutes, 0)) - DateTime.Now);

                    returnString = String.Format("{0}h {1}m {2}s", left.Hours, left.Minutes, left.Seconds);
                }

                return returnString;
            }
        }

        public TimerPlus(int minutes)
        {
            _timer = new Timer();
            Minutes = minutes;
            _timer.Elapsed += _timer_Elapsed;
        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            OnTick();
            started = DateTime.Now;
        }

        /// <summary>
        /// Starts the timer
        /// </summary>
        public void Start()
        {
            started = DateTime.Now;
            _timer.Start();
        }
        /// <summary>
        /// Stops the timer
        /// </summary>
        public void Stop()
        {
            _timer.Stop();
        }
        public void Dispose()
        {
            _timer.Dispose();
        }
    }
}
