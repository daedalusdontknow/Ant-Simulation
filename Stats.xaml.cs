using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaktionslogik für Stats.xaml
    /// </summary>
    public partial class Stats : Window
    {

        public static int ants = 0;
        public static double nutrition = 0;
        public static double speed = 0;
        public static double strength = 0;
        public static double health = 0;
        public static int gen = 0;


        public Stats()
        {
            InitializeComponent();

            Thread statsLoop = new Thread(new ThreadStart(StatsLoop));
            statsLoop.Start();

        }

        private void StatsLoop()
        {
            while (true)
            {
                this.Dispatcher.Invoke(() =>
                {
                    AntCount.Content = "Ants: " + ants;
                    NutritionCount.Content = "a. Nutrition: " + nutrition;
                    StrengthCount.Content = "a. Strength: " + strength;
                    HealthCount.Content = "a. Health: " + health;
                    SpeedCount.Content = "a. Speed: " + speed;
                    GenerationCount.Content = "Generation: " + gen;
                });

                //wait 100ms
                Thread.Sleep(100);
            }
        }
    }
}
