using Antsimulation.Eventlistener;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antsimulation.entities;
using Antsimulation.Managers.UI;

namespace Antsimulation.Managers
{
    public class SimulationManager
    {
        public void StartSimulation()
        {
            for (int i = 0; i < 100; i++)
            {
                entities.Ant.CreateAnt(0);
            }

            for (int i = 0; i < 5; i++)
            {
                entities.Antbear.CreateAntbear(0);
            }

            RunSimulation();
        }

        public static int delay = 100;
        public static bool isPaused = false;
        public static bool endSim = false;
        public static int GoToRestart = 0;

        public static object OnStopButtomClicked { get; internal set; }

        private void RunSimulation()
        {
            SimulationWindow SW = new SimulationWindow();
            endSim = false;
            int TimeCounter = 0;
            while (true)
            {
                if (endSim)
                {
                    SW.CloseWindow();
                    Ant.ants.Clear();
                    Antbear.Antbears.Clear();
                    Food.Foods.Clear();

                    switch (GoToRestart)
                    {
                        case 1:
                            ConfigurationWindow CW = new ConfigurationWindow();
                            CW.Run();
                            break;
                        case 2:
                            StartSimulation();
                            break;
                    }

                    Program.RunningFor = 0;
                    SimulationManager.OnTimeControlButtonClicked(100);
                    GoToRestart = 0;
                    break;
                }
                SW.Run();
                int tmpDelay = 0;
                while (!(tmpDelay >= delay))
                {
                    Thread.Sleep(1);
                    tmpDelay++;
                }

                if (!isPaused)
                {
                    TimeCounter++;
                    if (TimeCounter == 10)
                    {
                        TimeCounter = 0;
                        Program.RunningFor++;
                    }
                }
            }
        }

        public static void OnTimeControlButtonClicked(int v)
        {
            if (v == -1)
            {
                isPaused = true;
                delay = 100;
            }
            else
            {
                isPaused = false;
                delay = v;
            }

            Console.WriteLine(delay);
        }

        public static void OnStopButtonClicked()
        {
            GoToRestart = 1;
            endSim = true;
        }

        public static void OnRestartButtonClicked()
        {
            GoToRestart = 2;
            endSim = true;
        }
    }
}
