using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antsimulation.Eventlistener;
using Antsimulation.Managers;
using Raylib_cs;

namespace Antsimulation.entities
{
    public class Ant
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

        public static List<Ant> ants = new List<Ant>();
        public static List<Ant> AddAnts = new List<Ant>();
        public static List<Ant> RemoveAnts = new List<Ant>();

        public Ant (float x, float y, int health, double nutrition, float strength, float speed, float sight, int gen, double duration)
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

        private static float perstep = 0.0005f;

        public static void CreateAnt(int gen)
        {
            Random random = new Random();
            int health = 5;

            float xpos = random.Next(0, 600);
            float ypos = random.Next(0, 800);

            float speedModify = Program.antSpeed;
            float strengthModify = Program.antStrength;
            float sightModify = Program.antSightLength;
            float liveLengthModify = Program.antLiveLength;

            //the modifyers are in percent, like 50 and so on, i want for speed a random number between 0 and 3 where the percentage sets the max of three
            //so if the percentage is 50 the max speed is 1.5
            //so i need to divide the percentage by 100 and then multiply it by 3
            float speed = random.Next(1, (int)(speedModify / 100 * 5));
            float strength = random.Next(1, (int)(strengthModify / 100 * 4));
            //for the sight i want something between 0 and 100
            float sight = random.Next(1, (int)sightModify) * 10;
            //for the live length i want something between 1000 and 100000
            float liveLength = random.Next(1000, (int)liveLengthModify * 1000);
            double nutrition = 5;

            Ant ant = new Ant(xpos, ypos, health, nutrition, strength, speed, sight, gen, liveLength);
            // add this ant to the ants list
            ants.Add(ant);
        }

        private static void CreateAntWithStat(float x, float y, float speed, float strength, float sight, int gen)
        {
            Random random = new Random();
            int health = 5;
            double nutrition = 5;
            float liveLengthModify = Program.antLiveLength;
            float liveLength = random.Next(1000, (int)liveLengthModify * 1000);
            Ant ant = new Ant(x, y, health, nutrition, strength, speed, sight, gen, liveLength);
            // add this ant to the ants list
            AddAnts.Add(ant);
        }

        public static void Behaviour(WindowManager wm)
        {
            foreach (var food in Food.Foods)
            {
                wm.DrawCircle(food.x, food.y, 2, Color.GREEN);
            }

            foreach (var ant in ants)
            {
                float x = ant.x;
                float y = ant.y;
                wm.DrawCircle(x, y, 2, Color.BLACK);

                //check if a Antbear is near the ant, if so the ant should run
                float ABDistance = 0;
                Antbear NearestAntbear = null;

                foreach (var antbear in Antbear.Antbears)
                {
                    float dx = antbear.x - ant.x;
                    float dy = antbear.y - ant.y;
                    float distance = (float)Math.Sqrt(dx * dx + dy * dy);

                    if (distance < ABDistance || ABDistance == 0)
                    {
                        ABDistance = distance;
                        NearestAntbear = antbear;
                    }
                }

                if (NearestAntbear != null && (float)Math.Sqrt(NearestAntbear.x * NearestAntbear.x + NearestAntbear.y * NearestAntbear.y) < 15)
                {
                    //run in the opposite direction of the antbear
                    float dx = NearestAntbear.x - ant.x;
                    float dy = NearestAntbear.y - ant.y;
                    float distance = (float)Math.Sqrt(dx * dx + dy * dy);

                    float xMove = dx / distance;
                    float yMove = dy / distance;

                    ant.x -= xMove * ant.speed*2;
                    ant.y -= yMove * ant.speed*2;
                    ant.nutrition -= perstep * ant.speed;
                }
                else
                {

                    if (ant.nutrition >= 7)
                    {
                        //reproducing code, two ants within a distance of 2 can get a child with one of their trades and two random
                        //the child gets the average of the parents trades
                        float ADistance = 0;
                        Ant NearestAnt = null;

                        foreach (var antSearch in ants)
                        {
                            float dx = antSearch.x - ant.x;
                            float dy = antSearch.y - ant.y;
                            float distance = (float)Math.Sqrt(dx * dx + dy * dy);

                            if (distance < ADistance || ADistance == 0)
                            {
                                ADistance = distance;
                                NearestAnt = antSearch;
                            }
                        }

                        if (NearestAnt != null)
                        {
                            float dx = NearestAnt.x - ant.x;
                            float dy = NearestAnt.y - ant.y;
                            float distance = (float)Math.Sqrt(dx * dx + dy * dy);

                            if (distance < 2)
                            {
                                //the trades of the new ant are in range of the parents, but some are completly random
                                Random random = new Random();

                                float speed = 0;
                                float strength = 0;
                                float sight = 0;

                                //get the highest ant.gen
                                int gen = 0;
                                if (ant.gen > NearestAnt.gen)
                                {
                                    gen = ant.gen;
                                }
                                else
                                {
                                    gen = NearestAnt.gen;
                                }

                                switch (random.Next(0, 2))
                                {
                                    case 0:
                                        switch (random.Next(0, 4))
                                        {
                                            case 0:
                                                speed = ant.speed;
                                                break;
                                            case 1:
                                                strength = ant.strength;
                                                break;
                                            case 2:
                                                sight = ant.sight;
                                                break;
                                        }

                                        break;

                                    case 1:
                                        switch (random.Next(0, 4))
                                        {
                                            case 0:
                                                speed = NearestAnt.speed;
                                                break;
                                            case 1:
                                                strength = NearestAnt.strength;
                                                break;
                                            case 2:
                                                sight = NearestAnt.sight;
                                                break;
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

                                CreateAntWithStat(ant.x, ant.y, speed, strength, sight, gen + 1);
                                ant.nutrition -= 2;
                                NearestAnt.nutrition -= 2;
                            }
                            //move towards partner
                            else
                            {
                                float dx2 = NearestAnt.x - ant.x;
                                float dy2 = NearestAnt.y - ant.y;
                                float distance2 = (float)Math.Sqrt(dx2 * dx2 + dy2 * dy2);

                                float speed = ant.speed;

                                float angle = (float)Math.Atan2(dy2, dx2);

                                float vx = (float)Math.Cos(angle) * speed;
                                float vy = (float)Math.Sin(angle) * speed;

                                if (distance2 <= ant.sight)
                                {
                                    ant.x += vx;
                                    ant.y += vy;
                                    ant.nutrition -= perstep * ant.speed;
                                }
                            }
                        }
                    }
                    else if (ant.nutrition < 11)
                    {
                        float FDisctance = 0;
                        Food NearestFood = null;

                        foreach (var food in Food.Foods)
                        {
                            float dx = food.x - ant.x;
                            float dy = food.y - ant.y;
                            float distance = (float)Math.Sqrt(dx * dx + dy * dy);

                            if (distance < FDisctance || FDisctance == 0 && distance <= ant.sight)
                            {
                                FDisctance = distance;
                                NearestFood = food;
                            }

                        }

                        if (NearestFood != null)
                        {
                            float dx = NearestFood.x - ant.x;
                            float dy = NearestFood.y - ant.y;
                            float distance = (float)Math.Sqrt(dx * dx + dy * dy);

                            if (distance < 2)
                            {
                                if (ant.strength <= NearestFood.size)
                                {
                                    NearestFood.size = -ant.strength;
                                    ant.nutrition += ant.strength;
                                }
                                else if (ant.strength > NearestFood.size)
                                {
                                    ant.nutrition += NearestFood.size;
                                    NearestFood.size = 0;
                                }
                            }
                            else
                            {
                                float angle = (float)Math.Atan2(dy, dx);
                                float vx = (float)Math.Cos(angle) * ant.speed;
                                float vy = (float)Math.Sin(angle) * ant.speed;

                                ant.x += vx;
                                ant.y += vy;
                                ant.nutrition -= perstep * ant.speed;
                            }

                            Food.RemoveNull();
                        }
                        else
                        {
                            //move in a random direction
                            Random random = new Random();

                            float angle = random.Next(0, 360);
                            float vx = (float)Math.Cos(angle) * ant.speed;
                            float vy = (float)Math.Sin(angle) * ant.speed;

                            ant.x += vx;
                            ant.y += vy;
                            ant.nutrition -= perstep * ant.speed;
                        }
                    }
                    else
                    {
                        //move in a random direction
                        Random random = new Random();

                        float angle = random.Next(0, 360);
                        float vx = (float)Math.Cos(angle) * ant.speed;
                        float vy = (float)Math.Sin(angle) * ant.speed;

                        ant.x += vx;
                        ant.y += vy;
                        ant.nutrition -= perstep * ant.speed;
                    }
                }

                if (ant.nutrition <= 0)
                {
                    ant.health--;
                }

                if (ant.health <= 0)
                {
                    RemoveAnts.Add(ant);
                }
            }

            ants.RemoveAll(a => RemoveAnts.Contains(a));
            ants.AddRange(AddAnts);

            AddAnts.Clear();
            RemoveAnts.Clear();
        }

        public static int GetAntCount()
        {
            return ants.Count;
        }

        public static float GetAverageNutrition()
        {
            return (float)ants.Average(ant => ant.nutrition);
        }   

        public static float GetAverageSpeed()
        {
            return ants.Average(ant => ant.speed);
        }

        public static float GetAverageStrength()
        {
            return ants.Average(ant => ant.strength);
        }

        public static float GetAverageSight()
        {
            return ants.Average(ant => ant.sight);
        }

        public static int GetHighestGen()
        {
            //get the highest ant.gen
            return ants.Max(ant => ant.gen);
        }

        public static float GetAverageHealth()
        {
            return (float)ants.Average(ant => ant.health);
        }
    }
}
