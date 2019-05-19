using System;

namespace Chronometer
{
    class StartUp
    {
        static void Main(string[] args)
        {
            IChronometer chronometer = new Chronometer();

            var input = Console.ReadLine().ToLower();

            while (input != "exit")
            {
                switch (input)
                {
                    case "start":
                        chronometer.Start();
                        break;
                    case "stop":
                        chronometer.Stop();
                        break;
                    case "lap":
                        chronometer.Lap();
                        break;
                    case "time":
                        Console.WriteLine(chronometer.GetTime);
                        break;
                    case "reset":
                        chronometer.Reset();
                        break;
                }

                input = Console.ReadLine().ToLower();
            }
        }
    }
}
