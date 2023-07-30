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
            for (int i = 0; i < 5; i++)
            {
                entities.Ant.CreateAnt(0);
            }

            RunSimulation();
        }

        public static int delay = 100;

        private void RunSimulation()
        {
            SimulationWindow SW = new SimulationWindow();
            while (true)
            {
                SW.Run();
                int tmpDelay = 0;
                while (!(tmpDelay >= delay))
                {
                    Thread.Sleep(1);
                    tmpDelay++;
                }
            }
        }

        public static void OnTimeControlButtonClicked(int v)
        {
            delay = v;
        }
    }
}
