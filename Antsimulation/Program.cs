using System;
using System.Runtime.CompilerServices;
using Antsimulation.entities;
using Antsimulation.Frames;
using Antsimulation.Managers;
using Antsimulation.Managers.UI;
using Raylib_cs;

namespace Antsimulation.Eventlistener
{
    public class Program
    {
        public static int RunningFor = 0;

        public static float waterLevel;
        public static float foodAmount;
        public static float foodSize;

        public static float antSpeed;
        public static float antStrength;
        public static float antLiveLength;
        public static float antSightLength;

        public static float AntbearSpeed;
        public static float AntbearStrength;
        public static float AntbearLiveLength;
        public static float AntbearSightLength;

        public static List<DataEntry> antStats { get; set; } = new List<DataEntry>();
        public static List<DataEntry> antbearStats { get; set; } = new List<DataEntry>();

        static void Main(string[] args)
        {
            LogManager log = new LogManager();
            WindowManager windowManager = new WindowManager(800, 600, "danmark.science Simulations");

            // Create the "Start" button for the start menu
            Button startButton = new Button(300, 250, 200, 80, "Start", () =>
            {
                // Action to execute when the "Start" button is clicked
                log.WriteLog("Started with Configuration");
                windowManager.CloseWindow();

                ConfigurationWindow configurationWindow = new ConfigurationWindow();
                configurationWindow.Run();
            });

            // Add the "Start" button to the window manager
            windowManager.AddButton(startButton);

            while (!windowManager.IsWindowClosed())
            {
                windowManager.UpdateWindow();
                windowManager.DrawText("danmark.science Simulations", 180, 100, 36, Color.DARKGRAY);
            }

            windowManager.CloseWindow();
        }
    }
}