using ScottPlot;
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

namespace lab1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GetResultButton_Click(object sender, RoutedEventArgs e)
        {
            BrushGraphick(double.Parse(startCoordinate.Text), double.Parse(endCoordinate.Text), double.Parse(stepCoordinate.Text));
        }
        private void BrushGraphick(double startCoordinate, double endCoordinate, double step)
        {
            double difference  = (endCoordinate - startCoordinate) + 1;
            double tempCountOperations = difference / step;
            int countOperations = (int)Math.Round(tempCountOperations, MidpointRounding.AwayFromZero);
            double[] dataX = new double[countOperations];
            double[] dataY = new double[countOperations];


            for (int i = 0; i < countOperations; i++)
            {
                dataX[i] = startCoordinate;
                startCoordinate += step;

                dataY[i] = CalculateFunction(dataX[i]);
            }
            WpfPlot.Plot.AddScatter(dataX, dataY);
            WpfPlot.Refresh();
        }
        private double CalculateFunction(double value)
        {
            double degreeValue = Math.Pow(value, 2);
            double sqrtValue = Math.Sqrt(degreeValue);
            return sqrtValue - 5;

        }
    }
}
