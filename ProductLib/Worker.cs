using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Drawing.Text;
using System.Drawing;
using System.ComponentModel;


namespace ProductLib
{
    /// <summary>
    /// Статусы работы Воркера
    /// </summary>
    public enum StateOfWork 
    {
        [Description("Не запущен")] Unstarted,
        [Description("Запущен")]    Running,
        [Description("На паузе")]   Paused,
        [Description("Завершен")]   Finished
    };

    /// <summary>
    /// Класс, приводящий в действие объекты Mover, содержащиеся в списке внутри класса
    /// </summary>
    public class Worker
    {
        List<Mover> movers = new();

        volatile bool working = false;

        volatile bool paused;

        StateOfWork state = StateOfWork.Unstarted;

        public static object syncObject = new object();

        public event EventHandler? AnotherPartOfWorkDone;

        public delegate void StateActualisation(StateOfWork state);
        public event StateActualisation? StateChanged;
        public Worker()
        {   } 


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

        /// <summary>
        /// Запускает работу муверов пока не отсановлен или не поставлен на паузу
        /// </summary>
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

            double dt;
            double current = 0;
            double lastTime = 0;
            sw.Start();

            state = StateOfWork.Running;
            StateChanged?.Invoke(state);
            while (working)
            {
                
                dt = current - lastTime;
                lastTime = current;

                foreach (Mover mover in movers)
                {
                    if (!paused && working)
                        mover.Step((float)dt);
                    else
                        break;
                }

                AnotherPartOfWorkDone?.Invoke(this, EventArgs.Empty);

                Thread.Sleep(10);


                while (paused)
                {
                    lock (Worker.syncObject)
                    {
                        state = StateOfWork.Paused;
                        StateChanged?.Invoke(state);
                        Monitor.Wait(syncObject);
                    }
                    continue;
                }

                state = StateOfWork.Running;
                StateChanged?.Invoke(state);

                current = sw.Elapsed.TotalSeconds;
            }

            state = StateOfWork.Finished;
            StateChanged?.Invoke(state);
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
