using Antsimulation.Eventlistener;
using Antsimulation.Managers;
using Antsimulation.Managers.UI;
using Raylib_cs;

namespace Antsimulation.Frames
{
    public class SimulationStopWindow
    {
        private WindowManager windowManager;

        public SimulationStopWindow()
        {
            windowManager = new WindowManager(1200, 600, "Stats Window");
        }

        public void Run()
        {
            float antMaxY = 0;

            Graph antGraph = new Graph(Color.BLUE);
            Graph antANutri = new Graph(Color.GREEN);
            Graph antASpeed = new Graph(Color.YELLOW);
            Graph antAStrength = new Graph(Color.BEIGE);
            Graph antASight = new Graph(Color.PINK);
            Graph antAHealth = new Graph(Color.RED);
            Graph HighestGen = new Graph(Color.BLACK);

            //same graphs for antbear
            int antBearMaxY = 0;

            Graph antBearGraph = new Graph(Color.BLUE);
            Graph antBearNutri = new Graph(Color.GREEN);
            Graph antBearSpeed = new Graph(Color.YELLOW);
            Graph antBearStrength = new Graph(Color.BEIGE);
            Graph antBearSight = new Graph(Color.PINK);
            Graph antBearHealth = new Graph(Color.RED);
            Graph HighestGenBear = new Graph(Color.BLACK);


            foreach (var ant in Program.antStats)
            {
                float[] yValues = new float[]
                {
                    ant.Count,
                    ant.DNutrition,
                    ant.DSpeed,
                    ant.DStrength,
                    ant.DSight,
                    ant.DHealth,
                    ant.HGen
                };

                foreach (int y in yValues) if (y > antMaxY) antMaxY = y;

                antGraph.AddDataPoint(ant.Timestamp, ant.Count);
                antANutri.AddDataPoint(ant.Timestamp, ant.DNutrition*10);
                antASpeed.AddDataPoint(ant.Timestamp, ant.DSpeed*10);
                antAStrength.AddDataPoint(ant.Timestamp, ant.DStrength * 10);
                antASight.AddDataPoint(ant.Timestamp, ant.DSight);
                antAHealth.AddDataPoint(ant.Timestamp, ant.DHealth * 10);
                HighestGen.AddDataPoint(ant.Timestamp, ant.HGen);
            }

            antGraph.SetFixedYRange(0, antMaxY);
            antANutri.SetFixedYRange(0, antMaxY);
            antASpeed.SetFixedYRange(0, antMaxY);
            antAStrength.SetFixedYRange(0, antMaxY);
            antASight.SetFixedYRange(0, antMaxY);
            antAHealth.SetFixedYRange(0, antMaxY);
            HighestGen.SetFixedYRange(0, antMaxY);

            foreach (var bearAnt in Program.antbearStats)
            {
                float[] yValues = new float[]
                {
                    bearAnt.Count,
                    bearAnt.DNutrition,
                    bearAnt.DSpeed,
                    bearAnt.DStrength,
                    bearAnt.DSight,
                    bearAnt.DHealth,
                    bearAnt.HGen
                };
                
                foreach (int y in yValues) if (y > antBearMaxY) antBearMaxY = y;

                antBearGraph.AddDataPoint(bearAnt.Timestamp, bearAnt.Count);
                antBearNutri.AddDataPoint(bearAnt.Timestamp, bearAnt.DNutrition*10);
                antBearSpeed.AddDataPoint(bearAnt.Timestamp, bearAnt.DSpeed * 10);
                antBearStrength.AddDataPoint(bearAnt.Timestamp, bearAnt.DStrength * 10);
                antBearSight.AddDataPoint(bearAnt.Timestamp, bearAnt.DSight);
                antBearHealth.AddDataPoint(bearAnt.Timestamp, bearAnt.DHealth * 10);
                HighestGenBear.AddDataPoint(bearAnt.Timestamp, bearAnt.HGen);
            }

            antBearGraph.SetFixedYRange(0, antBearMaxY);
            antBearNutri.SetFixedYRange(0, antBearMaxY);
            antBearSpeed.SetFixedYRange(0, antBearMaxY);
            antBearStrength.SetFixedYRange(0, antBearMaxY);
            antBearSight.SetFixedYRange(0, antBearMaxY);
            antBearHealth.SetFixedYRange(0, antBearMaxY);
            HighestGenBear.SetFixedYRange(0, antBearMaxY);


            while (!windowManager.IsWindowClosed())
            {
                windowManager.UpdateWindow();

                // UI
                windowManager.DrawText("Simulation Stopped", 500, 10, 36, Color.DARKGRAY);
                windowManager.AddButton(new Button(100, 10, 200, 36, "Download csv", () => SimulationManager.onDownloadButtonClicked()));

                // Graphs
                HighestGen.Draw(40, 60, 500, 400);
                antGraph.Draw(40, 60, 500, 400);
                antANutri.Draw(40, 60, 500, 400);
                antAStrength.Draw(40, 60, 500, 400);
                antASight.Draw(40, 60, 500, 400);
                antAHealth.Draw(40, 60, 500, 400);
                antASpeed.Draw(40, 60, 500, 400);

                HighestGenBear.Draw(600, 60, 500, 400);
                antBearGraph.Draw(600, 60, 500, 400);
                antBearNutri.Draw(600, 60, 500, 400);
                antBearStrength.Draw(600, 60, 500, 400);
                antBearSight.Draw(600, 60, 500, 400);
                antBearHealth.Draw(600, 60, 500, 400);
                antBearSpeed.Draw(600, 60, 500, 400);

                // Labels
                windowManager.DrawText("Count", 40, 480, 20, Color.BLUE);
                windowManager.DrawText("Nutrition", 200, 480, 20, Color.GREEN);
                windowManager.DrawText("Speed", 360, 480, 20, Color.YELLOW);
                windowManager.DrawText("Strength", 520, 480, 20, Color.BEIGE);
                windowManager.DrawText("Sight", 680, 480, 20, Color.PINK);
                windowManager.DrawText("Health", 840, 480, 20, Color.RED);
                windowManager.DrawText("Highest Gen", 1000, 480, 20, Color.BLACK);
            }

            windowManager.CloseWindow();
        }
    }
}