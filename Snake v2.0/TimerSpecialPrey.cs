using System;
using System.Timers;

namespace Snake_v2._0
{
    public class TimerSpecialPrey
    {
        private Timer _timerDrawSpecialPrey = new Timer();
        private Timer _timerDeleteSpecialPrey = new Timer();

        private Random _rnd = new Random();

        private DrawingLogic _drawingLogic = new DrawingLogic();

        internal void SetTimer()
        {
            _timerDrawSpecialPrey.Interval = _rnd.Next(20000, 40000);
            _timerDrawSpecialPrey.AutoReset = true;
            _timerDrawSpecialPrey.Enabled = true;
            _timerDrawSpecialPrey.Start();

            if (Settings.GenerateSpecialPrey == true)
            {
                _timerDrawSpecialPrey.Elapsed += new ElapsedEventHandler(DrawNewSpecialPrey);
            }
        }

        private void DrawNewSpecialPrey(object sender, ElapsedEventArgs e)
        {
            _drawingLogic.DrawSpecialPrey();

            _timerDrawSpecialPrey.Interval = _rnd.Next(15000, 50000);

            _timerDeleteSpecialPrey.Interval = _rnd.Next(3000, 10000);
            _timerDeleteSpecialPrey.Start();
            _timerDeleteSpecialPrey.Elapsed += new ElapsedEventHandler(DeleteSpecialPrey);
        }

        private void DeleteSpecialPrey(object sender, ElapsedEventArgs e)
        {
            _drawingLogic.DeleteSpecialPrey();
        }

        internal void StopTimer()
        {
            _timerDeleteSpecialPrey.Stop();
            _timerDrawSpecialPrey.Stop();
        }
    }
}
