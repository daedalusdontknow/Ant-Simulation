using Antsimulation.Eventlistener;
using Antsimulation.Managers.UI;
using Raylib_cs;

namespace Antsimulation.Frames
{
    public class DiedWindow
    {
        WindowManager windowManager = new WindowManager(800, 600, "Joke Simulations");

        // Create the "Start" button for the start menu
        Button startButton = new Button(300, 250, 200, 80, "Start", () =>
        {
            // Action to execute when the "Start" button is clicked
            ConfigurationWindow configurationWindow = new ConfigurationWindow();
            configurationWindow.Run();
        });

        // Add the "Start" button to the window manager

    }
}
