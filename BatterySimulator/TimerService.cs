using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BatterySimulator
{
    public class TimerService
    {
        private readonly System.Timers.Timer _timer;

        public TimerService(double interval)
        {
            _timer = new System.Timers.Timer(interval);
            _timer.AutoReset = true;

            Start();
        }

        public void Start()
        {
            _timer.Enabled = true;
        }

        public void Stop()
        {
            _timer.Enabled = false;
        }

        public void addTimerEvent(ElapsedEventHandler handler)
        {
            _timer.Elapsed += handler;
        }

    }
}
