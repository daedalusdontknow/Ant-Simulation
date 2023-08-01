using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antsimulation.Eventlistener;
using Raylib_cs;

namespace Antsimulation.entities
{
    public class Food
    {
        public float x { get; set; }
        public float y { get; set; }
        public float size { get; set; }

        public static List<Food> Foods = new List<Food>();

        public Food(float x, float y, float size)
        {
            this.x = x;
            this.y = y;
            this.size = size;
        }

        static int foodSpawn = 0;

        public static void SpawnFood(WindowManager WM)
        {
            if (foodSpawn == 0)
            {
                int foodAmount = (int)Program.foodAmount;
                float foodsize = Program.foodSize;

                Random random = new Random();

                for (int i = 0; i < foodAmount / 10; i++)
                {
                    float xpos = random.Next(0, 600);
                    float ypos = random.Next(0, 800);

                    Food food = new Food(xpos, ypos, foodsize);
                    Foods.Add(food);
                }

                foodSpawn = 50;
            }
            else
            {
                foodSpawn--;
            }

            foreach (var food in Food.Foods)
            {
                WM.DrawCircle(food.x, food.y, 2, Color.GREEN);
            }
        }

        public static void RemoveNull()
        {
            //remove all food objects with size = 0
            Foods.RemoveAll(food => food.size == 0);
        }
    }
}
