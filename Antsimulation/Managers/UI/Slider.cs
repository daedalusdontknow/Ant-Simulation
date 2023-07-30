using System;
using Raylib_cs;

namespace Antsimulation.Managers.UI
{
    public class Slider
    {
        public int x { get; set; }
        public int y { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public float minValue { get; set; }
        public float maxValue { get; set; }
        public float value { get; private set; }
        public bool isDragging { get; private set; }

        public Slider(int x, int y, int width, int height, float minValue, float maxValue, float initialValue = 0f)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.minValue = minValue;
            this.maxValue = maxValue;
            value = initialValue;
            isDragging = false;
        }

        public void Update()
        {
            float mouseX = Raylib.GetMouseX();
            float mouseY = Raylib.GetMouseY();

            // Check if the mouse is over the slider track
            bool isMouseOver = mouseX >= x && mouseX <= x + width && mouseY >= y && mouseY <= y + height;

            if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON) && isMouseOver)
            {
                isDragging = true;
            }

            if (Raylib.IsMouseButtonReleased(MouseButton.MOUSE_LEFT_BUTTON))
            {
                isDragging = false;
            }

            if (isDragging)
            {
                // Calculate the new value based on the mouse position
                float relativeX = Math.Clamp(mouseX - x, 0, width);
                float percentage = relativeX / width;
                value = minValue + (maxValue - minValue) * percentage;
            }
        }

        public float GetValueAsPercentage()
        {
            float range = maxValue - minValue;
            float relativeValue = value - minValue;
            float percentage = relativeValue / range * 100f;

            return Math.Clamp(percentage, 0f, 100f);
        }

        public void Draw()
        {
            // Draw the slider track
            Raylib.DrawRectangle(x, y + height / 3, width, height / 3, Color.GRAY);

            // Calculate the position of the slider handle based on the value
            float handleX = x + (width - 10) * ((value - minValue) / (maxValue - minValue));

            // Draw the slider handle
            Raylib.DrawRectangle((int)handleX, y, 10, height, Color.DARKGRAY);
        }
    }
}
