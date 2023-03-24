using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Antsimulation
{
    /// <summary>
    /// Interaktionslogik für Simulation.xaml
    /// </summary>
    public partial class Simulation : Window
    {
        public static List<Ant> ants = new List<Ant>();
        public static List<Food> food = new List<Food>();

        public static bool isRunning = true;
        public static int simulationSpeed = 500;

        public static int pStrength = 0;
        public static int pSpeed = 0;

        public static double pDuration = 1000;

        int pFood = 8;

        public Simulation(int strength, int speed, int Food, double duration)
        {
            InitializeComponent();
            startGame(strength, speed);
            pFood = Food - 8;

            pDuration = duration + 5000;

            //start the Controlpanel
            ControlPanel controlpanel = new ControlPanel();
            controlpanel.Show();

            Stats stats = new Stats();
            stats.Show();
        }

        private void startGame(int Strength, int Speed)
        {
            Random random = new Random();

            pSpeed = Speed;
            pStrength = Strength;

            int width = 800;
            int height = 400;

            for (int i = 0; i < 100; i++)
            {
                int x = random.Next(0, width);
                int y = random.Next(0, height);

                Ellipse ant = new Ellipse();
                ant.Width = 5;
                ant.Height = 5;
                ant.Fill = Brushes.Black;
                ant.Stroke = Brushes.Black;

                Canvas.SetLeft(ant, x);
                Canvas.SetTop(ant, y);

                Simulationspace.Children.Add(ant);

                int strength = Strength + random.Next(-1, 2);
                int speed = Speed + random.Next(-1, 2);

                //if speed 0 or lower, set it to 1
                if (speed <= 0)
                {
                    speed = 1;
                }

                if (strength <= 0)
                {
                    strength = 1;
                }


                Ant ant1 = new Ant(ant, 5, 10, strength, speed, 0, pDuration);
                ants.Add(ant1);
            }

            Thread gameloop = new Thread(new ThreadStart(GameLoop));
            gameloop.IsBackground = true;
            gameloop.Start();
        }

        private void GameLoop()
        {
            int foodSpawn = 0;

            while (true)
            {
                if (!isRunning) { continue; }

                List<Food> foodsToRemove = new List<Food>();
                List<Ant> antsToRemove = new List<Ant>();

                List<Ant> antsToAdd = new List<Ant>();

                foreach (Ant ant in ants)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        if (ant.nutrition < 11)
                        {

                            //let the ant move in the direction of the nearest food source, food is stored in a list called food
                            //get the position of the ant
                            double antX = Canvas.GetLeft(ant.ant);
                            double antY = Canvas.GetTop(ant.ant);

                            //get the position of the nearest food source
                            double foodX = 0;
                            double foodY = 0;

                            double Gdistance = 0;
                            Food nearestFood = null;

                            if (antX > 0)
                            {
                                //calculate the distance between the ant and the food source
                                foreach (Food foodobj in food)
                                {
                                    //calculate difference
                                    double x = Canvas.GetLeft(foodobj.food) - antX;
                                    double y = Canvas.GetTop(foodobj.food) - antY;

                                    //calculate distance
                                    double distance = Math.Sqrt(x * x + y * y);

                                    //if the distance is smaller than the current distance, set the new distance
                                    if (distance < Gdistance || Gdistance == 0)
                                    {
                                        Gdistance = distance;
                                        nearestFood = foodobj;
                                    }
                                }
                            }

                            if (nearestFood != null)
                            {
                                //get the position of the nearest food source
                                foodX = Canvas.GetLeft(nearestFood.food);
                                foodY = Canvas.GetTop(nearestFood.food);
                                //calculate difference
                                double x = foodX - antX;
                                double y = foodY - antY;
                                //calculate distance
                                double distance = Math.Sqrt(x * x + y * y);
                                //calculate the direction
                                double directionX = x / distance;
                                double directionY = y / distance;
                                //move the ant
                                antX += directionX * ant.speed;
                                antY += directionY * ant.speed;
                                //set the new position

                                //remove nutrition
                                ant.nutrition -= 0.01 * ant.speed;

                                Canvas.SetLeft(ant.ant, antX);
                                Canvas.SetTop(ant.ant, antY);

                                if (antX > foodX - 5 && antX < foodX + 5 && antY > foodY - 5 && antY < foodY + 5)
                                {
                                    //mark the food for removal
                                    foodsToRemove.Add(nearestFood);
                                    ant.nutrition += nearestFood.size;
                                    nearestFood.size = 0;
                                }
                            }

                        }
                        else
                        {
                            //let two ants mate if they are close enough and have enough nutrition and spawn a new ant, copy one trade of the parents and add a random value to the other values
                            //get the position of the ant
                            foreach (Ant antO in ants)
                            {
                                if (antO != ant)
                                {
                                    double antX = Canvas.GetLeft(ant.ant);
                                    double antY = Canvas.GetTop(ant.ant);
                                    //get the position of the nearest food source
                                    double antOX = Canvas.GetLeft(antO.ant);
                                    double antOY = Canvas.GetTop(antO.ant);
                                    //calculate difference
                                    double x = antOX - antX;
                                    double y = antOY - antY;
                                    //calculate distance
                                    double distance = Math.Sqrt(x * x + y * y);
                                    if (distance < 10 && ant.nutrition >= 6 && antO.nutrition >= 6)
                                    {
                                        //calculate the direction
                                        double directionX = x / distance;
                                        double directionY = y / distance;
                                        //move the ant
                                        antX += directionX * ant.speed;
                                        antY += directionY * ant.speed;
                                        //set the new position
                                        //remove nutrition
                                        ant.nutrition -= 0.01 * ant.speed;
                                        antO.nutrition -= 0.01 * antO.speed;
                                        Canvas.SetLeft(ant.ant, antX);
                                        Canvas.SetTop(ant.ant, antY);

                                        if (antX > antOX - 5 && antX < antOX + 5 && antY > antOY - 5 && antY < antOY + 5)
                                        {
                                            //spawn a new ant
                                            Ellipse ant1 = new Ellipse();
                                            ant1.Width = 5;
                                            ant1.Height = 5;
                                            ant1.Fill = Brushes.Black;
                                            ant1.Stroke = Brushes.Black;
                                            Canvas.SetLeft(ant1, antX);
                                            Canvas.SetTop(ant1, antY);
                                            Simulationspace.Children.Add(ant1);
                                            Random random = new Random();
                                            int strength = 0;
                                            int speed = 0;
                                            if (random.Next(0, 2) == 0)
                                            {
                                                strength = ant.strength;
                                            }
                                            else
                                            {
                                                strength = antO.strength;
                                            }
                                            if (random.Next(0, 2) == 0)
                                            {
                                                speed = ant.speed;
                                            }
                                            else
                                            {
                                                speed = antO.speed;
                                            }
                                            strength= random.Next(strength);

                                            ant.nutrition -= 2;
                                            antO.nutrition -= 2;

                                            int gen = 0;
                                            if (ant.gen > antO.gen)
                                            {
                                                gen = ant.gen;
                                            }
                                            else
                                            {
                                                gen = antO.gen;
                                            }

                                            //let all three ants move in a different direction
                                            double antX1 = Canvas.GetLeft(ant.ant);
                                            double antY1 = Canvas.GetTop(ant.ant);
                                            double antX2 = Canvas.GetLeft(antO.ant);
                                            double antY2 = Canvas.GetTop(antO.ant);
                                            double antX3 = Canvas.GetLeft(ant1);
                                            double antY3 = Canvas.GetTop(ant1);

                                            Canvas.SetLeft(ant.ant, antX1 + 5);
                                            Canvas.SetTop(ant.ant, antY1 + 5);
                                            Canvas.SetLeft(antO.ant, antX2 + 5);
                                            Canvas.SetTop(antO.ant, antY2 + 5);
                                            Canvas.SetLeft(ant1, antX3 + 5);
                                            Canvas.SetTop(ant1, antY3 + 5);


                                            Ant newAnt = new Ant(ant1, 5, 10, strength, speed, gen + 1, pDuration);
                                            antsToAdd.Add(newAnt);
                                        }
                                    } 
                                    else
                                    {
                                        //move in a random direction
                                        Random random = new Random();
                                        double antXr = Canvas.GetLeft(ant.ant);
                                        double antYr = Canvas.GetTop(ant.ant);
                                        antXr += random.Next(-1, 2);
                                        antYr += random.Next(-1, 2);

                                        Canvas.SetLeft(ant.ant, antXr);
                                        Canvas.SetTop(ant.ant, antYr);
                                    }
                                }
                            }
                        }

                        ant.duration -= 10;

                        if (ant.nutrition <= 0) ant.health = -1;
                        if (ant.health <= 0) antsToRemove.Add(ant); 
                        if (ant.duration <= 0) antsToRemove.Add(ant);
                    });
                }

                this.Dispatcher.Invoke(() =>
                {
                    if (ants.Count > 0)
                    {
                        Stats.ants = ants.Count;
                        Stats.nutrition = ants.Average(x => x.nutrition);
                        Stats.speed = ants.Average(x => x.speed);
                        Stats.strength = ants.Average(x => x.strength);
                        Stats.health = ants.Average(x => x.health);
                        Stats.gen = ants.Max(x => x.gen);
                    }
                });

                //remove the marked foods from the list and from the canvas
                foreach (Food foodToRemove in foodsToRemove)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        food.Remove(foodToRemove);
                        Simulationspace.Children.Remove(foodToRemove.food);
                    });
                }

                //remove the marked ants from the list and from the canvas
                foreach (Ant antToRemove in antsToRemove)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        ants.Remove(antToRemove);
                        Simulationspace.Children.Remove(antToRemove.ant);
                    });
                }

                foreach (Ant antToAdd in antsToAdd)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        ants.Add(antToAdd);
                    });
                }
                

                Thread.Sleep(simulationSpeed);

                if (foodSpawn >= pFood) { 
                    Random random = new Random();
                    int x = random.Next(0, 800);
                    int y = random.Next(0, 400);

                    this.Dispatcher.Invoke(() =>
                    {
                        Ellipse food1 = new Ellipse();
                        food1.Width = 3;
                        food1.Height = 3;
                        food1.Fill = Brushes.Green;
                        food1.Stroke = Brushes.Green;
                        food1.StrokeThickness = 2;

                        Canvas.SetLeft(food1, x);
                        Canvas.SetTop(food1, y);

                        Simulationspace.Children.Add(food1);

                        Food foodObj = new Food(food1, 1);
                        food.Add(foodObj);
                    });

                    foodSpawn = 0;
                }

                foodSpawn++;
            }
        }

        private void Button_pause(object sender, RoutedEventArgs e)
        {


            isRunning = false;
        }

        private void Button_start(object sender, RoutedEventArgs e)
        {


            isRunning = true;
            simulationSpeed = 500;

        }

        private void Button_double(object sender, RoutedEventArgs e)
        {


            isRunning = true;
            simulationSpeed = 250;
        }

        private void Button_10(object sender, RoutedEventArgs e)
        {


            isRunning = true;
            simulationSpeed = 50;
        }
    }
}
