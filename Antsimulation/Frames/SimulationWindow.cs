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

            windowManager.AddButton(new Button(720, 70, 100, 30, "0x", () => SimulationManager.OnTimeControlButtonClicked(1000000000)));
            windowManager.AddButton(new Button(720, 110, 100, 30, "1x", () => SimulationManager.OnTimeControlButtonClicked(100)));
            windowManager.AddButton(new Button(720, 150, 100, 30, "5x", () => SimulationManager.OnTimeControlButtonClicked(50)));
            windowManager.AddButton(new Button(720, 190, 100, 30, "10x", () => SimulationManager.OnTimeControlButtonClicked(10)));
            windowManager.AddButton(new Button(720, 230, 100, 30, "100x", () => SimulationManager.OnTimeControlButtonClicked(1)));
        }

        private int FoodSpawnRate = 50;

        public void Run()
        {
            if (Ant.ants.Count <= 0)
            {
                windowManager.CloseWindow();
            }
            windowManager.UpdateWindow();
            Ant.Behaviour(windowManager);

            if (FoodSpawnRate <= 0)
            {
                Food.SpawnFood(windowManager);
                FoodSpawnRate = 50;
            }

            FoodSpawnRate--;

            windowManager.DrawLine(700, -100, 700, 900, Color.BLACK);

            windowManager.DrawText("Time", 720, 20, 40, Color.BLACK);
            windowManager.DrawText("Stats", 900, 20, 40, Color.BLACK);

            int antCount = Ant.GetAntCount();
            float aSpeed = Ant.GetAverageSpeed();
            float aStrength = Ant.GetAverageStrength();
            float aNutrition = Ant.GetAverageNutrition();
            float aSight = Ant.GetAverageSight();
            //get the ant with the highest gen
            int highestGen = Ant.GetHighestGen();
            float aHealth = Ant.GetAverageHealth();

            windowManager.DrawText($"Count:{antCount}", 900, 70, 30, Color.BLACK);
            windowManager.DrawText($"Nutrition:{aNutrition}", 900, 100, 30, Color.BLACK);
            windowManager.DrawText($"Speed:{aSpeed}", 900, 130, 30, Color.BLACK);
            windowManager.DrawText($"Strength:{aStrength}", 900, 160, 30, Color.BLACK);
            windowManager.DrawText($"Sight:{aSight}", 900, 190, 30, Color.BLACK);
            windowManager.DrawText($"Gen:{highestGen}", 900, 220, 30, Color.BLACK);
            windowManager.DrawText($"Health:{aHealth}", 900, 250, 30, Color.BLACK);
        }
    }
}