using System;
using Antsimulation.Managers;
using Raylib_cs;
using Antsimulation.Managers.UI;

namespace Antsimulation.Eventlistener
{
    public class AntConfigWindow
    {
        private WindowManager windowManager;

        private Slider speedSlider;
        private Slider strengthSlider;
        private Slider liveLengthSlider;
        private Slider sightLengthSlider;
        private bool configurationApplied;

        public AntConfigWindow()
        {
            windowManager = new WindowManager(800, 600, "Ant Configuration");

            // Create sliders for ant speed, strength, livelength, and sight length
            speedSlider = new Slider(100, 200, 200, 20, 0f, 100f, 50f);
            strengthSlider = new Slider(100, 250, 200, 20, 0f, 100f, 50f);
            liveLengthSlider = new Slider(100, 300, 200, 20, 0f, 100f, 50f);
            sightLengthSlider = new Slider(100, 350, 200, 20, 0f, 100f, 50f);

            // Add sliders to the window manager
            windowManager.AddButton(new Button(100, 450, 200, 50, "Apply", OnApplyButtonClicked));

            configurationApplied = false;
        }

        private void OnApplyButtonClicked()
        {
            // Retrieve the ant configuration values from the sliders
            Program.antSpeed = speedSlider.GetValueAsPercentage() + 1;
            Program.antStrength = strengthSlider.GetValueAsPercentage() + 1 ;
            Program.antLiveLength = liveLengthSlider.GetValueAsPercentage() + 1;
            Program.antSightLength = sightLengthSlider.GetValueAsPercentage() + 1;

            windowManager.CloseWindow();

            SimulationManager SM = new SimulationManager();

            SM.StartSimulation();
        }

        public void Run()
        {
            while (!configurationApplied)
            {
                windowManager.UpdateWindow();

                // Draw the title "Ant Configuration" on the window
                windowManager.DrawText("Ant Configuration", 250, 100, 30, Color.DARKGRAY);

                // Draw sliders for ant speed, strength, livelength, and sight length
                windowManager.DrawText("Ant Speed", 100, 180, 20, Color.DARKGRAY);
                speedSlider.Update();
                speedSlider.Draw();

                windowManager.DrawText("Ant Strength", 100, 230, 20, Color.DARKGRAY);
                strengthSlider.Update();
                strengthSlider.Draw();

                windowManager.DrawText("Ant Live Length", 100, 280, 20, Color.DARKGRAY);
                liveLengthSlider.Update();
                liveLengthSlider.Draw();

                windowManager.DrawText("Ant Sight Length", 100, 330, 20, Color.DARKGRAY);
                sightLengthSlider.Update();
                sightLengthSlider.Draw();
            }

            // Close the ant configuration window
            windowManager.CloseWindow();
        }
    }
}
