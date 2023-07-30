using System;
using Raylib_cs;

namespace Antsimulation.Managers.UI
{
    public class Button
    {
        public int x { get; set; }
        public int y { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public Action action { get; set; }
        public string text { get; set; }
        public bool isMouseOver { get; private set; }
        public Color buttonColor { get; set; }
        public Color textColor { get; set; }

        public Button(int x, int y, int width, int height, string text, Action action)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.text = text;
            this.action = action;
            isMouseOver = false;
            buttonColor = Color.GRAY; // Default color
            textColor = Color.BLACK; // Default color
        }

        public void Update()
        {
            float mouseX = Raylib.GetMouseX();
            float mouseY = Raylib.GetMouseY();

            // Check if the mouse is over the button
            isMouseOver = mouseX >= x && mouseX <= x + width && mouseY >= y && mouseY <= y + height;

            // Check for mouse click and execute the button action if clicked
            if (isMouseOver && Raylib.IsMouseButtonReleased(MouseButton.MOUSE_LEFT_BUTTON))
            {
                action?.Invoke();
            }
        }

        public void Draw()
        {
            Raylib.DrawRectangle(x, y, width, height, buttonColor);

            // Use MeasureText to get the text size
            float textSizeWidth = Raylib.MeasureText(text, 20);
            float textSizeHeight = 20; // Assuming font size is 20

            float textX = x + (width - textSizeWidth) / 2;
            float textY = y + (height - textSizeHeight) / 2;

            Raylib.DrawText(text, (int)textX, (int)textY, 20, textColor);
        }
    }
}
