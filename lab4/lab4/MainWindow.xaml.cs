using ScottPlot;
using ScottPlot.Renderable;
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
using System.Drawing;
using Color = System.Drawing.Color;

namespace lab4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const double COUNT_OF_OPERATION = 100;
        private const double LAMDA = 0.1;

        public MainWindow()
        {
            InitializeComponent();
            var array = new double[(int)COUNT_OF_OPERATION];
            double[] xs = DataGen.Consecutive((int)COUNT_OF_OPERATION);

            for (int i = 0; i < COUNT_OF_OPERATION; i++)
                array[i] = RandomExp();

            double[] despercion = new double[(int)COUNT_OF_OPERATION];

            for (int i = 1; i < COUNT_OF_OPERATION; i++)
                array[i] += array[i - 1];

            for (int i = 0; i < COUNT_OF_OPERATION; i++)
                despercion[i] = (FunctionRight(array[i]) - FunctionRight(array[0])) - Math.Pow(FunctionLeft(array[i]) - FunctionLeft(array[0]), 2);

            double avangarde = array.Average();

            graph.Plot.Title($"Среднее значение {avangarde}");
            graph.Plot.AddScatter(xs, array, label: "cлуч. послед. после преобразования");
            graph.Plot.AddScatter(xs, despercion, label: "Значание дисперсии");
            graph.Plot.XAxis.MajorGrid(lineWidth: 1, lineStyle: LineStyle.Solid, color: Color.Black);

            graph.Plot.Legend();
            graph.Refresh();

            PrintDialog printDialog = new PrintDialog();
            printDialog.PrintVisual(graph, "Печать");
            Close();
        }
        private double RandomExp()
        {
            double temp = new Random().NextDouble();
            return 1d - Math.Exp(-LAMDA*temp);
        }
        private double FunctionLeft(double x)
        {
            double temp = Math.Exp(LAMDA * x);
            return -x - (10d / temp);
        }
        private double FunctionRight(double x)
        {
            double temp = Math.Exp(LAMDA * Math.Pow(x,2));
            return Math.Pow(-x, 2) - (10d / temp);
        }
    }
}
