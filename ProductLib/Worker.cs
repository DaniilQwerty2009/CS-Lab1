using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Drawing.Text;
using System.Drawing;


namespace ProductLib
{
    public class Worker
    {
        List<Mover> movers = new();

        volatile bool working = false;

        volatile bool paused;

        public static object syncObject = new object();

        public Worker()
        {   } 

        public Worker(List<Mover> movers)
        {
            this.movers = movers;
        }

        public bool Working
        {
            get { return working; }
        }

        public bool Paused
        {
            get { return paused; }
        }

        public IEnumerable<Mover> Movers
        {
            get { return movers; }
        }

        public void AddMover(Mover mover)
        {                
            movers.Add(mover);
        }
        public void RemoveMover(Mover mover)
        {
            if (movers.Count > 0)
                movers.Remove(mover);
        }

        public event EventHandler? AnotherPartOfWorkDone;

        //public delegate void WorkerDelegate(EventValues e);
        //public event WorkerDelegate? ClearingMoverTrace;

        public class EventValues : EventArgs
        {
            RectangleF area;
            public EventValues(RectangleF area)
            {
                this.area = area;
            }

            public RectangleF Area
            {
                get { return  area; }
            }
        }

        public void Run()
        {
            if (!Working)
                working = true;
            else
                return;

            if (movers.Count == 0)
            {
                working = false;
                return;
            }

            Stopwatch sw = new Stopwatch();

            float dt;
            float lastTime = 0;
            sw.Start();
            dt = sw.ElapsedMilliseconds - lastTime;

            //RectangleF traceOfPreviosWorkingPlace;
            
            while (working)
            {
                lastTime = sw.ElapsedMilliseconds;

                foreach (Mover mover in movers)
                {
                    //traceOfPreviosWorkingPlace = mover.Visual.AreaOfVisualisation;
                    //ClearingMoverTrace?.Invoke(new EventValues(traceOfPreviosWorkingPlace));

                    mover.Step(dt / 1000f);

                }

                AnotherPartOfWorkDone?.Invoke(this, EventArgs.Empty);

                Thread.Sleep(10);

                dt = sw.ElapsedMilliseconds - lastTime;

                while (paused)
                {
                    lock (Worker.syncObject)
                        Monitor.Wait(syncObject);
                    continue;
                }
            }

            return;
        }

        public void Stop()
        {
            if (working)
                working = false;
            if (paused)
                paused = false;
        }
    
        public void Pause()
        {
            if(!paused)
                paused = true;
        }

        public void Continue()
        {
            if (paused)
                paused = false;
                
        }
    }
}
