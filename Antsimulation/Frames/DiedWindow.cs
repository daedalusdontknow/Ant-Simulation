using System;
using Raylib_cs;
using Antsimulation.Managers.UI;

namespace Antsimulation.Eventlistener
{
    public class DiedWindow
    {
        private WindowManager windowManager;

        public DiedWindow()
        {
            windowManager = new WindowManager(800, 600, "Configuration Menu");

            windowManager.AddButton(new Button(300, 250, 200, 80, "Restart", OnRestartButtonClicked));
        }

        private void OnRestartButtonClicked()
        {
            windowManager.CloseWindow();

            ConfigurationWindow configurationWindow = new ConfigurationWindow();
            configurationWindow.Run();
        }

        public void Run()
        {
            while (!windowManager.IsWindowClosed())
            {
                windowManager.UpdateWindow();

                // Draw the title "Joke Simulations" on the window
                windowManager.DrawText("Joke Simulations", 280, 100, 36, Color.DARKGRAY);
                //oh no your ecosystem died, try it again!
                windowManager.DrawText("oh no your ecosystem died, try it again!", 260, 200, 20, Color.RED);
            }

            windowManager.CloseWindow();
        }
    }
}
