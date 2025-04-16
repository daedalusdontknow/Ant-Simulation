using Antsimulation.Eventlistener;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antsimulation.entities;
using Antsimulation.Frames;
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

            for (int i = 0; i < 10; i++)
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

                    Program.RunningFor = 0;

                    switch (GoToRestart)
                    {
                        case 1:
                            SimulationStopWindow SSW = new SimulationStopWindow();
                            SSW.Run();
                            break;
                        case 2:
                            StartSimulation();

                            Program.antStats.Clear();
                            Program.antbearStats.Clear();
                            break;
                        case 3:
                            ConfigurationWindow CW = new ConfigurationWindow();
                            CW.Run();
                            break;
                    }

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

        private static int downloadCSV = 0;

        public static void onDownloadButtonClicked()
        {
            if (downloadCSV == 1) return;
            downloadCSV = 1;

            // create a new csv file with the name "simulation-{date+time}.csv with the stats of the simulation 
            string fileName = $"simulation-{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}.csv";
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                /*        public float DStrength { get; set; }
                   public float DSight { get; set; }
                   public float HGen { get; set; }
                   public float DHealth { get; set; }
                   public int Timestamp { get; set; }*/

                String antstimeStamps = "TimeStamp;";
                String antsCount = "Count;";
                String antSpeed = "Speed;";
                String antStrength = "Strength;";
                String antSight = "Sight;";
                String antHealth = "Health;";
                String antNutrition = "Nutrition;";
                String antGen = "Highest gen;";


                for (int i = 0; i < Program.antStats.Count; i++)
                {
                    antstimeStamps += $"{Program.antStats[i].Timestamp};";
                    antsCount += $"{Program.antStats[i].Count};";
                    antSpeed += $"{Program.antStats[i].DSpeed};";
                    antStrength += $"{Program.antStats[i].DStrength};";
                    antSight += $"{Program.antStats[i].DSight};";
                    antHealth += $"{Program.antStats[i].DHealth};";
                    antNutrition += $"{Program.antStats[i].DNutrition};";
                    antGen += $"{Program.antStats[i].HGen};";
                }

                writer.WriteLine("Ants");
                writer.WriteLine(antstimeStamps);
                writer.WriteLine(antsCount);
                writer.WriteLine(antSpeed);
                writer.WriteLine(antStrength);
                writer.WriteLine(antSight);
                writer.WriteLine(antHealth);
                writer.WriteLine(antNutrition);
                writer.WriteLine(antGen);

                String antBearCount = "Count;";
                String antBearSpeed = "Speed;";
                String antBearStrength = "Strength;";
                String antBearSight = "Sight;";
                String antBearHealth = "Health;";
                String antBearNutrition = "Nutrition;";
                String antBearGen = "Highest gen;";

                for (int i = 0; i < Program.antbearStats.Count; i++)
                {
                    antBearCount += $"{Program.antbearStats[i].Count};";
                    antBearSpeed += $"{Program.antbearStats[i].DSpeed};";
                    antBearStrength += $"{Program.antbearStats[i].DStrength};";
                    antBearSight += $"{Program.antbearStats[i].DSight};";
                    antBearHealth += $"{Program.antbearStats[i].DHealth};";
                    antBearNutrition += $"{Program.antbearStats[i].DNutrition};";
                    antBearGen += $"{Program.antbearStats[i].HGen};";
                }

                writer.WriteLine("Antbears");
                writer.WriteLine(antstimeStamps);
                writer.WriteLine(antBearCount);
                writer.WriteLine(antBearSpeed);
                writer.WriteLine(antBearStrength);
                writer.WriteLine(antBearSight);
                writer.WriteLine(antBearHealth);
                writer.WriteLine(antBearNutrition);
                writer.WriteLine(antBearGen);

                LogManager logManager = new LogManager();
                logManager.WriteLog("CSV created");

                //open the file in the default program
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = fileName,
                    UseShellExecute = true,
                    Verb = "open"
                });
            }
        }
    }
}
