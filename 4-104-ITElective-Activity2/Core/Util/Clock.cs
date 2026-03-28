using _4_104_ITElective_Activity2.core;
using System;

namespace _4_104_ITElective_Activity2.Core.Util
{
    public class ClockTickDTO
    {
        public DateTime Now { get; set; }
    }

    public class Clock
    {
        private readonly System.Windows.Forms.Timer _timer;

        public Clock()
        {
            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 1000;
            _timer.Tick += (s, e) => EventBus.Publish(new ClockTickDTO { Now = DateTime.Now });
        }

        public void Start() => _timer.Start();
        public void Stop()  => _timer.Stop();
    }
}
