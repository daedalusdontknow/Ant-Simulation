using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antsimulation.Eventlistener;
using Antsimulation.Managers;
using Antsimulation.entities;
using Antsimulation.Eventlistener;
using Raylib_cs;

namespace Antsimulation.entities
{
    public class Antbear
    {
        public float x { get; set; }
        public float y { get; set; }
        private int health { get; set; }
        private double nutrition { get; set; }
        private float strength { get; set; }
        private float speed { get; set; }
        private float sight { get; set; }
        private int gen { get; set; }
        private double duration { get; set; }

        public static List<Antbear> Antbears = new List<Antbear>();
        public static List<Antbear> AddAntbears = new List<Antbear>();
        public static List<Antbear> RemoveAntbears = new List<Antbear>();

        public Antbear(float x, float y, int health, double nutrition, float strength, float speed, float sight, int gen, double duration)
        {
            this.x = x;
            this.y = y;
            this.health = health;
            this.nutrition = nutrition;
            this.strength = strength;
            this.speed = speed;
            this.sight = sight;
            this.gen = gen;
            this.duration = duration;
        }

        private static float perstep = 0.01f;

        public static void CreateAntbear(int gen)
        {
            Random random = new Random();
            int health = 5;

            float xpos = random.Next(0, 600);
            float ypos = random.Next(0, 800);

            float speedModify = Program.AntbearSpeed;
            float strengthModify = Program.AntbearStrength;
            float sightModify = Program.AntbearSightLength;
            float liveLengthModify = Program.AntbearLiveLength;

            //the modifyers are in percent, like 50 and so on, i wAntbear for speed a random number between 0 and 3 where the percentage sets the max of three
            //so if the percentage is 50 the max speed is 1.5
            //so i need to divide the percentage by 100 and then multiply it by 3
            float speed = random.Next(1, (int)(speedModify / 100 * 3));
            float strength = random.Next(1, (int)(strengthModify / 100 * 6));
            float sight = random.Next(1, (int)sightModify) * 10;
            float liveLength = random.Next(1000, (int)liveLengthModify * 1000);
            double nutrition = 5;

            Antbear Antbear = new Antbear(xpos, ypos, health, nutrition, strength, speed, sight, gen, liveLength);
            // add this Antbear to the Antbears list
            Antbears.Add(Antbear);
        }

        private static void CreateAntbearWithStat(float x, float y, float speed, float strength, float sight, int gen)
        {
            Random random = new Random();
            int health = 5;
            double nutrition = 5;
            float liveLengthModify = Program.AntbearLiveLength;
            float liveLength = random.Next(1000, (int)liveLengthModify * 1000);
            Antbear Antbear = new Antbear(x, y, health, nutrition, strength, speed, sight, gen, liveLength);
            // add this Antbear to the Antbears list
            AddAntbears.Add(Antbear);
        }

        public static void Behaviour(WindowManager wm)
        {

            foreach (var Antbear in Antbears)
            {
                float x = Antbear.x;
                float y = Antbear.y;
                wm.DrawCircle(x, y, 6, Color.RED);

                if (Antbear.nutrition >= 7)
                {
                    //reproducing code, two Antbears within a distance of 2 can get a child with one of their trades and two random
                    //the child gets the average of the parents trades
                    float ADistance = 0;
                    Antbear NearestAntbear = null;

                    foreach (var AntbearSearch in Antbears)
                    {
                        float dx = AntbearSearch.x - Antbear.x;
                        float dy = AntbearSearch.y - Antbear.y;
                        float distance = (float)Math.Sqrt(dx * dx + dy * dy);

                        if (distance < ADistance || ADistance == 0)
                        {
                            ADistance = distance;
                            NearestAntbear = AntbearSearch;
                        }
                    }

                    if (NearestAntbear != null)
                    {
                        float dx = NearestAntbear.x - Antbear.x;
                        float dy = NearestAntbear.y - Antbear.y;
                        float distance = (float)Math.Sqrt(dx * dx + dy * dy);

                        if (distance < 2)
                        {
                            //the trades of the new Antbear are in range of the parents, but some are completly random
                            Random random = new Random();

                            float speed = 0;
                            float strength = 0;
                            float sight = 0;

                            //get the highest Antbear.gen
                            int gen = 0;
                            if (Antbear.gen > NearestAntbear.gen)
                            {
                                gen = Antbear.gen;
                            }
                            else
                            {
                                gen = NearestAntbear.gen;
                            }

                            switch (random.Next(0, 2))
                            {
                                case 0:
                                    switch (random.Next(0, 4))
                                    {
                                        case 0: speed = Antbear.speed; break;
                                        case 1: strength = Antbear.strength; break;
                                        case 2: sight = Antbear.sight; break;
                                    }
                                    break;

                                case 1:
                                    switch (random.Next(0, 4))
                                    {
                                        case 0: speed = NearestAntbear.speed; break;
                                        case 1: strength = NearestAntbear.strength; break;
                                        case 2: sight = NearestAntbear.sight; break;
                                    }
                                    break;
                            }

                            //one of the trade is set, the other two should be random
                            if (speed == 0)
                            {
                                speed = random.Next(1, 4);
                            }

                            if (strength == 0)
                            {
                                strength = random.Next(1, 4);
                            }

                            if (sight == 0)
                            {
                                sight = random.Next(1, 100) * 10;
                            }

                            CreateAntbearWithStat(Antbear.x, Antbear.y, speed, strength, sight, gen + 1);
                            Antbear.nutrition -= 2;
                            NearestAntbear.nutrition -= 2;
                        }
                        //move towards partner
                        else
                        {
                            float dx2 = NearestAntbear.x - Antbear.x;
                            float dy2 = NearestAntbear.y - Antbear.y;
                            float distance2 = (float)Math.Sqrt(dx2 * dx2 + dy2 * dy2);

                            float speed = Antbear.speed;

                            float angle = (float)Math.Atan2(dy2, dx2);

                            float vx = (float)Math.Cos(angle) * speed;
                            float vy = (float)Math.Sin(angle) * speed;

                            if (distance2 <= Antbear.sight)
                            {
                                Antbear.x += vx;
                                Antbear.y += vy;
                                Antbear.nutrition -= perstep * Antbear.speed;
                            }
                        }
                    }
                }
                else if (Antbear.nutrition < 11)
                {
                    float FDisctance = 0;
                    Ant NearestFood = null;

                    foreach (var ant in Ant.ants)
                    {
                        float dx = ant.x - Antbear.x;
                        float dy = ant.y - Antbear.y;
                        float distance = (float)Math.Sqrt(dx * dx + dy * dy);

                        if (distance < FDisctance || FDisctance == 0 && distance <= Antbear.sight)
                        {
                            FDisctance = distance;
                            NearestFood = ant;
                        }
                    }

                    if (NearestFood != null)
                    {
                        float dx = NearestFood.x - Antbear.x;
                        float dy = NearestFood.y - Antbear.y;
                        float distance = (float)Math.Sqrt(dx * dx + dy * dy);

                        if (distance < 2)
                        {
                            Antbear.nutrition += 1;
                            Ant.RemoveAnts.Add(NearestFood);
                        }
                        else
                        {
                            float angle = (float)Math.Atan2(dy, dx);
                            float vx = (float)Math.Cos(angle) * Antbear.speed;
                            float vy = (float)Math.Sin(angle) * Antbear.speed;

                            Antbear.x += vx;
                            Antbear.y += vy;
                            Antbear.nutrition -= perstep * Antbear.speed;
                        }
                        Food.RemoveNull();
                    }
                    else
                    {
                        //move in a random direction
                        Random random = new Random();

                        float angle = random.Next(0, 360);
                        float vx = (float)Math.Cos(angle) * Antbear.speed;
                        float vy = (float)Math.Sin(angle) * Antbear.speed;

                        Antbear.x += vx;
                        Antbear.y += vy;
                        Antbear.nutrition -= perstep * Antbear.speed;
                    }
                }
                else
                {
                    //move in a random direction
                    Random random = new Random();

                    float angle = random.Next(0, 360);
                    float vx = (float)Math.Cos(angle) * Antbear.speed;
                    float vy = (float)Math.Sin(angle) * Antbear.speed;

                    Antbear.x += vx;
                    Antbear.y += vy;
                    Antbear.nutrition -= perstep * Antbear.speed;
                }

                if (Antbear.nutrition <= 0)
                {
                    Antbear.health--;
                }

                if (Antbear.health <= 0)
                {
                    RemoveAntbears.Add(Antbear);
                }
            }

            Antbears.RemoveAll(a => RemoveAntbears.Contains(a));
            Antbears.AddRange(AddAntbears);

            AddAntbears.Clear();
            RemoveAntbears.Clear();
        }

        public static int GetAntbearCount()
        {
            return Antbears.Count;
        }

        public static float GetAverageNutrition()
        {
            return (float)Antbears.Average(Antbear => Antbear.nutrition);
        }

        public static float GetAverageSpeed()
        {
            return Antbears.Average(Antbear => Antbear.speed);
        }

        public static float GetAverageStrength()
        {
            return Antbears.Average(Antbear => Antbear.strength);
        }

        public static float GetAverageSight()
        {
            return Antbears.Average(Antbear => Antbear.sight);
        }

        public static int GetHighestGen()
        {
            //get the highest Antbear.gen
            return Antbears.Max(Antbear => Antbear.gen);
        }

        public static float GetAverageHealth()
        {
            return (float)Antbears.Average(Antbear => Antbear.health);
        }
    }
}
