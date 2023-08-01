using System;
using Raylib_cs;
using Antsimulation.Managers.UI;
using Antsimulation.Managers;

namespace Antsimulation.Eventlistener
{
    public class ConfigurationWindow
    {
        private WindowManager windowManager;

        private Slider waterSlider;
        private Slider foodAmountSlider;
        private Slider foodSizeSlider;

        public ConfigurationWindow()
        {
            windowManager = new WindowManager(800, 600, "Configuration Menu");

            // Create sliders for water level, food amount, and food size
            waterSlider = new Slider(100, 200, 200, 20, 0f, 100f, 50f);
            foodAmountSlider = new Slider(100, 250, 200, 20, 0f, 100f, 50f);
            foodSizeSlider = new Slider(100, 300, 200, 20, 0f, 100f, 50f);

            // Add sliders to the window manager
            windowManager.AddButton(new Button(100, 400, 200, 50, "Apply", OnApplyButtonClicked));
        }

        private void OnApplyButtonClicked()
        {
            Program.waterLevel = waterSlider.GetValueAsPercentage() + 1;
            Program.foodAmount = foodAmountSlider.GetValueAsPercentage() + 1;
            Program.foodSize = foodSizeSlider.GetValueAsPercentage() + 1;

            // Retrieve the configuration values from the sliders
            windowManager.CloseWindow();
            AntConfigWindow antConfigWindow = new AntConfigWindow();
            antConfigWindow.Run();
        }

        public void Run()
        {
            while (!windowManager.IsWindowClosed())
            {
                windowManager.UpdateWindow();

                // Draw the title "Configuration Menu" on the window
                windowManager.DrawText("Configuration Menu", 250, 100, 30, Color.DARKGRAY);

                // Draw sliders for water level, food amount, and food size
                windowManager.DrawText("Water Level", 100, 180, 20, Color.DARKGRAY);
                waterSlider.Update();
                waterSlider.Draw();

                windowManager.DrawText("Food Amount", 100, 230, 20, Color.DARKGRAY);
                foodAmountSlider.Update();
                foodAmountSlider.Draw();

                windowManager.DrawText("Food Size", 100, 280, 20, Color.DARKGRAY);
                foodSizeSlider.Update();
                foodSizeSlider.Draw();
            }

            windowManager.CloseWindow();
        }
    }
}
