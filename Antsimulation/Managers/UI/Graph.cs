using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;

namespace Antsimulation.Managers.UI
{
    public class Graph
    {
        private List<Vector2> dataPoints;
        private Color lineColor;

        private float viewStartX = 0f;
        private float viewWidth = 100f;
        private float minViewWidth = 10f;
        private float maxViewWidth = 1000f;

        private float fixedMinY = 0;
        private float fixedMaxY = 100;
        private bool useFixedYRange = false;

        private float leftKeyHeldTime = 0f;
        private float rightKeyHeldTime = 0f;


        public Graph(Color lineColor)
        {
            this.lineColor = lineColor;
            dataPoints = new List<Vector2>();
        }

        public void AddDataPoint(float x, float y)
        {
            dataPoints.Add(new Vector2(x, y));
            ScrollToLatest();
        }

        public void ClearData()
        {
            dataPoints.Clear();
        }

        public void SetFixedYRange(float minY, float maxY)
        {
            useFixedYRange = true;
            fixedMinY = minY;
            fixedMaxY = maxY;
        }

        public void UseAutoYRange()
        {
            useFixedYRange = false;
        }

        public void ScrollToLatest()
        {
            if (dataPoints.Count > 0)
            {
                float latestX = dataPoints[dataPoints.Count - 1].X;
                viewStartX = latestX - viewWidth;
                if (viewStartX < 0) viewStartX = 0;
            }
        }

        public void UpdateControls()
        {
            float deltaTime = Raylib.GetFrameTime();
            float basePanSpeed = viewWidth * 0.01f;

            // Left Arrow Key
            if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
            {
                leftKeyHeldTime += deltaTime;

                float speedMultiplier = 1f;
                if (leftKeyHeldTime > 10f)
                    speedMultiplier = 10f;
                else if (leftKeyHeldTime > 5f)
                    speedMultiplier = 2f;

                viewStartX -= basePanSpeed * speedMultiplier;
            }
            else
            {
                leftKeyHeldTime = 0f;
            }

            // Right Arrow Key
            if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
            {
                rightKeyHeldTime += deltaTime;

                float speedMultiplier = 1f;
                if (rightKeyHeldTime > 10f)
                    speedMultiplier = 15f;
                else if (rightKeyHeldTime > 5f)
                    speedMultiplier = 4f;
                else if (rightKeyHeldTime > 15f)
                    speedMultiplier = 50f;

                viewStartX += basePanSpeed * speedMultiplier;
            }
            else
            {
                rightKeyHeldTime = 0f;
            }

            // Zooming
            float wheel = Raylib.GetMouseWheelMove();
            if (wheel != 0)
            {
                float zoomSpeed = 0.1f;
                viewWidth *= 1f - wheel * zoomSpeed;
                viewWidth = Math.Clamp(viewWidth, minViewWidth, maxViewWidth);
            }

            if (viewStartX < 0) viewStartX = 0;
        }

        public void Draw(int x, int y, int width, int height)
        {
            Raylib.DrawRectangleLines(x, y, width, height, Color.LIGHTGRAY);
            UpdateControls();

            var visiblePoints = dataPoints.FindAll(p => p.X >= viewStartX && p.X <= viewStartX + viewWidth);

            if (visiblePoints.Count > 1)
            {
                float minY, maxY;

                if (useFixedYRange)
                {
                    minY = fixedMinY;
                    maxY = fixedMaxY;
                }
                else
                {
                    minY = visiblePoints[0].Y;
                    maxY = visiblePoints[0].Y;

                    foreach (var point in visiblePoints)
                    {
                        if (point.Y < minY) minY = point.Y;
                        if (point.Y > maxY) maxY = point.Y;
                    }
                }

                float rangeY = maxY - minY;
                if (rangeY == 0) rangeY = 1;

                for (int i = 0; i < visiblePoints.Count - 1; i++)
                {
                    Vector2 p1 = new Vector2(
                        x + ((visiblePoints[i].X - viewStartX) / viewWidth) * width,
                        y + height - ((visiblePoints[i].Y - minY) / rangeY) * height
                    );

                    Vector2 p2 = new Vector2(
                        x + ((visiblePoints[i + 1].X - viewStartX) / viewWidth) * width,
                        y + height - ((visiblePoints[i + 1].Y - minY) / rangeY) * height
                    );

                    Raylib.DrawLineEx(p1, p2, 2, lineColor);
                }

                Raylib.DrawText(maxY.ToString("0"), x - 30, y - 5, 10, Color.LIGHTGRAY);
                Raylib.DrawText(minY.ToString("0"), x - 30, y + height - 5, 10, Color.LIGHTGRAY);
                Raylib.DrawText(viewStartX.ToString("0"), x, y + height + 5, 10, Color.LIGHTGRAY);
                Raylib.DrawText((viewStartX + viewWidth).ToString("0"), x + width - 40, y + height + 5, 10, Color.LIGHTGRAY);
            }
        }
    }
}