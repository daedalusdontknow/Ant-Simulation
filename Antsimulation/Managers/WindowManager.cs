using System;
using System.Collections.Generic;
using System.Numerics;
using Antsimulation.Managers.UI;
using Raylib_cs;

namespace Antsimulation.Eventlistener
{
    public class WindowManager
    {
        private int screenWidth;
        private int screenHeight;
        private string windowTitle;
        private List<Button> buttons;
        private List<Slider> sliders;
        private bool isWindowClosed;

        public WindowManager(int screenWidth, int screenHeight, string windowTitle)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.windowTitle = windowTitle;
            buttons = new List<Button>();
            sliders = new List<Slider>();
            isWindowClosed = false;
            Raylib.InitWindow(screenWidth, screenHeight, windowTitle);
        }

        public void AddButton(Button button)
        {
            buttons.Add(button);
        }

        public void AddSlider(Slider slider)
        {
            sliders.Add(slider);
        }

        public void clearAll()
        {
            buttons.Clear();
            sliders.Clear();
            Raylib.ClearBackground(Color.RAYWHITE);
            //reset all memory
        }

        public void Update()
        {
            foreach (var button in buttons)
            {
                button.Update();
            }

            foreach (var slider in sliders)
            {
                slider.Update();
            }
        }

        public void Draw()
        {
            foreach (var button in buttons)
            {
                button.Draw();
            }
        }

        public void DrawText(string text, int x, int y, int fontSize, Color color)
        {
            Raylib.DrawText(text, x, y, fontSize, color);
        }

        public void DrawLine(int startPosX, int startPosY, int endPosX, int endPosY, Color color)
        {
            Raylib.DrawLine(startPosX, startPosY, endPosX, endPosY, color);
        }

        public void DrawCircle(float centerX, float centerY, float radius, Color color)
        {
            Raylib.DrawCircle((int)centerX, (int)centerY, radius, color);
        }

        public void UpdateWindow()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.RAYWHITE);

            // Update and draw all the buttons
            Update();
            Draw();

            Raylib.EndDrawing();
        }

        public void CloseWindow()
        {
            Raylib.CloseWindow();
            isWindowClosed = true;
        }

        public bool IsWindowClosed()
        {
            return isWindowClosed;
        }
    }
}
