using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Antsimulation
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //switch to window Simulation.xaml

            //get the values from the sliders as integers
            int Strength = (int)StrenthSlider.Value;
            int speed = (int)speedSlider.Value;
            int food = (int)FoodSlider.Value;
            double duration = (double)LiveDuration.Value;


            Simulation simulation = new Simulation(Strength, speed, food, duration);
            simulation.Show();
            this.Close();
        }

    }
}
