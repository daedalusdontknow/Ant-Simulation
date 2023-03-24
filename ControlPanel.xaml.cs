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
using System.Windows.Shapes;

namespace Antsimulation
{
    /// <summary>
    /// Interaktionslogik für ControlPanel.xaml
    /// </summary>
    public partial class ControlPanel : Window
    {
        public ControlPanel()
        {
            InitializeComponent();
        }

        private void Button_pause(object sender, RoutedEventArgs e)
        {
            Button_Pause.Background = Brushes.Green;
            Button_Start.Background = Brushes.White;
            Button_Double.Background = Brushes.White;
            Button_10o.Background = Brushes.White;

            //set isRunning in Simulation to false
            Simulation.isRunning = false;
        }

        private void Button_start(object sender, RoutedEventArgs e)
        {
            Button_Pause.Background = Brushes.White;
            Button_Start.Background = Brushes.Green;
            Button_Double.Background = Brushes.White;
            Button_10o.Background = Brushes.White;
            //set isRunning in Simulation to true
            Simulation.isRunning = true;
            Simulation.simulationSpeed = 500;
        }

        private void Button_double(object sender, RoutedEventArgs e)
        {
            Button_Pause.Background = Brushes.White;
            Button_Start.Background = Brushes.White;
            Button_Double.Background = Brushes.Green;
            Button_10o.Background = Brushes.White;
            //set isRunning in Simulation to true
            Simulation.isRunning = true;
            Simulation.simulationSpeed = 250;
        }

        private void Button_10(object sender, RoutedEventArgs e)
        {
            Button_Pause.Background = Brushes.White;
            Button_Start.Background = Brushes.White;
            Button_Double.Background = Brushes.White;
            Button_10o.Background = Brushes.Green;
            //set isRunning in Simulation to true
            Simulation.isRunning = true;
            Simulation.simulationSpeed = 50;
        }
    }
}
