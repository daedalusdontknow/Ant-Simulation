using Antsimulation.entities;
using Antsimulation.Eventlistener;
using Raylib_cs;

namespace Antsimulation.Managers.UI
{
    public class SimulationWindow
    {
        private WindowManager windowManager;

        public SimulationWindow()
        {
            windowManager = new WindowManager(1200, 700, "Simulation Window");

            windowManager.AddButton(new Button(720, 70, 100, 30, "0x", () => SimulationManager.OnTimeControlButtonClicked(-1)));
            windowManager.AddButton(new Button(720, 110, 100, 30, "1x", () => SimulationManager.OnTimeControlButtonClicked(100)));
            windowManager.AddButton(new Button(720, 150, 100, 30, "2x", () => SimulationManager.OnTimeControlButtonClicked(50)));
            windowManager.AddButton(new Button(720, 190, 100, 30, "10x", () => SimulationManager.OnTimeControlButtonClicked(10)));
            windowManager.AddButton(new Button(720, 230, 100, 30, "100x", () => SimulationManager.OnTimeControlButtonClicked(1)));
            windowManager.AddButton(new Button(1100, 670, 100, 30, "Stop", () => SimulationManager.OnStopButtonClicked()));
            windowManager.AddButton(new Button(990, 670, 100, 30, "Restart", () => SimulationManager.OnRestartButtonClicked()));

            windowManager.AddButton(new Button(720, 320, 120, 30, "Ant", () => Ant.CreateAnt(0)));
            //spawn ant x10 
            windowManager.AddButton(new Button(720, 360, 120, 30, "Ant x10", () =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Ant.CreateAnt(0);
                }
            }));

            windowManager.AddButton(new Button(720, 400, 120, 30, "Antbear", () => Antbear.CreateAntbear(0)));
            //spawn antbear x10
            windowManager.AddButton(new Button(720, 440, 120, 30, "Antbear x10", () =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Antbear.CreateAntbear(0);
                }
            }));
        }

        public void Run()
        {
            windowManager.UpdateWindow();

            if (!SimulationManager.isPaused)
            {
                Ant.Behaviour(windowManager);
                Antbear.Behaviour(windowManager);
                Food.SpawnFood(windowManager);
            }

            int running = Program.RunningFor;
            //convert Runningfor (seconds) to days, hours, minutes and seconds
            int days = running / 86400;
            int hours = (running % 86400) / 3600;
            int minutes = ((running % 86400) % 3600) / 60;
            int seconds = ((running % 86400) % 3600) % 60;

            windowManager.DrawText($"{days}d:{hours}h:{minutes}m:{seconds}s", 720, 290, 30, Color.BLACK);

            windowManager.DrawLine(700, -100, 700, 900, Color.BLACK);

            windowManager.DrawText("Time", 720, 20, 40, Color.BLACK);
            windowManager.DrawText("Stats Ant", 900, 20, 40, Color.BLACK);

            int antCount = 0;
            float aSpeed = 0;
            float aStrength = 0;
            float aNutrition = 0;
            float aSight = 0;
            //get the ant with the highest gen
            int highestGen = 0;
            float aHealth = 0;

            if (Ant.GetAntCount() > 0)
            {
                antCount = Ant.GetAntCount();
                aSpeed = Ant.GetAverageSpeed();
                aStrength = Ant.GetAverageStrength();
                aNutrition = Ant.GetAverageNutrition();
                aSight = Ant.GetAverageSight();
                highestGen = Ant.GetHighestGen();
                aHealth = Ant.GetAverageHealth();
            }

            windowManager.DrawText($"Count:{antCount}", 900, 70, 30, Color.BLACK);
            windowManager.DrawText($"Nutrition:{aNutrition}", 900, 100, 30, Color.BLACK);
            windowManager.DrawText($"Speed:{aSpeed}", 900, 130, 30, Color.BLACK);
            windowManager.DrawText($"Strength:{aStrength}", 900, 160, 30, Color.BLACK);
            windowManager.DrawText($"Sight:{aSight}", 900, 190, 30, Color.BLACK);
            windowManager.DrawText($"Gen:{highestGen}", 900, 220, 30, Color.BLACK);
            windowManager.DrawText($"Health:{aHealth}", 900, 250, 30, Color.BLACK);

            windowManager.DrawText("Stats Antbear", 900, 280, 40, Color.BLACK);


            int antbearCount = 0;
            float abSpeed = 0;
            float abStrength = 0;
            float abNutrition = 0;
            float abSight = 0;
            int abhighestGen = 0;
            float abHealth = 0;

            if (Antbear.GetAntbearCount() > 0)
            {
                antbearCount = Antbear.GetAntbearCount();
                abSpeed = Antbear.GetAverageSpeed();
                abStrength = Antbear.GetAverageStrength();
                abNutrition = Antbear.GetAverageNutrition();
                abSight = Antbear.GetAverageSight();
                abhighestGen = Antbear.GetHighestGen();
                abHealth = Antbear.GetAverageHealth();
            }

            windowManager.DrawText($"Count:{antbearCount}", 900, 320, 30, Color.BLACK);
            windowManager.DrawText($"Nutrition:{abNutrition}", 900, 350, 30, Color.BLACK);
            windowManager.DrawText($"Speed:{abSpeed}", 900, 380, 30, Color.BLACK);
            windowManager.DrawText($"Strength:{abStrength}", 900, 410, 30, Color.BLACK);
            windowManager.DrawText($"Sight:{abSight}", 900, 440, 30, Color.BLACK);
            windowManager.DrawText($"Gen:{abhighestGen}", 900, 470, 30, Color.BLACK);
            windowManager.DrawText($"Health:{abHealth}", 900, 500, 30, Color.BLACK);
        }

        public void CloseWindow()
        {
            windowManager.CloseWindow();
        }
    }
}