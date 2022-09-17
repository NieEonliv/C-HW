using ScottPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
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
        private bool checkNullInput()
        {
            if (startCoordinate.Text == "" || endCoordinate.Text == "" || stepCoordinate.Text == "")
            {
                MessageBox.Show("Введите коректрые данные");
                return true;
            }
            else if (startCoordinate.Text.Contains(".") || endCoordinate.Text.Contains(".") || stepCoordinate.Text.Contains("."))
            {
                startCoordinate.Text = startCoordinate.Text.Replace('.', ',');
                endCoordinate.Text = endCoordinate.Text.Replace('.', ',');
                stepCoordinate.Text = stepCoordinate.Text.Replace('.', ',');
                return false;
            }
            else if (Regex.IsMatch(startCoordinate.Text, "[A-z]|[А-я]") ||
                Regex.IsMatch(endCoordinate.Text, "[A-z]|[А-я]") ||
                Regex.IsMatch(stepCoordinate.Text, "[A-z]|[А-я]"))
            {
                MessageBox.Show("Введите коректрые данные");
                return true;
            }
            else
                return false;
        }
        private double[] BrushGraphikAndReturnDataY()
        {
            List<double[]> coordinates = NumericalIntegration.GetGraphikCoordinate(double.Parse(startCoordinate.Text),
                                                              double.Parse(endCoordinate.Text),
                                                              double.Parse(stepCoordinate.Text));
            WpfPlot.Plot.AddScatter(coordinates[0], coordinates[1]);
            WpfPlot.Refresh();
            return coordinates[1];
        }
       
        private void TrapezoidMethod_Click(object sender, RoutedEventArgs e)
        {
            if (checkNullInput())
                return;
            double[] dataY = BrushGraphikAndReturnDataY();
            resultCalculate.Text = "Ответ: " + NumericalIntegration.Trapezoid(dataY, double.Parse(stepCoordinate.Text)).ToString();
        }
    }
}
