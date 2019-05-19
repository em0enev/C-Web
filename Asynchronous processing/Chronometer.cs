using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chronometer
{
    public class Chronometer : IChronometer
    {
        private int miliseconds;

        private bool isRunning;

        public Chronometer()
        {
            this.Reset();
        }

        public string GetTime => $"{miliseconds / 60000:d2}:{miliseconds / 1000:d2}:{miliseconds % 1000}";

        public List<string> Laps { get; private set; }

        public void Lap()
        {
            Laps.Add(this.GetTime);
        }

        public void Reset()
        {
            this.Stop();
            this.miliseconds = 0;
            this.Laps = new List<string>();
        }

        public void Start()
        {
            this.isRunning = true;
            Task.Run(() =>
            {
                while (isRunning)
                {
                    Thread.Sleep(1);
                    this.miliseconds++;
                }
            });
        }

        public void Stop()
        {
            this.isRunning = false;
        }
    }
}
