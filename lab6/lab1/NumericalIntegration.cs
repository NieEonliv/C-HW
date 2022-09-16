using ScottPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    public static class NumericalIntegration
    {
        public static double GausMethod(double a, double b)
        {
            double tempLeftOperand = (a + b) / 2d;
            double tempRightOperand = (b - a) / (2 * Math.Sqrt(3d));

            double f0 = CalculateFunction(tempLeftOperand - tempRightOperand);
            double f1 = CalculateFunction(tempLeftOperand + tempRightOperand);

            double resultCalculate = ((b - a) / 2d) * (f0 + f1);
            return resultCalculate;
        }
        

        public static double Trapezoid(double[] dataY, double step)
        {
            double sumTemp = 0;
            for (int i = 1; i < dataY.Length - 1; i++)
                sumTemp += dataY[i];

            double resultCalculate = dataY[0] + dataY[dataY.Length - 1] / 2d;
            resultCalculate = (sumTemp + resultCalculate) * step;

            return resultCalculate;
        }
        
        public static double CalculateFunction(double value)
        {
            double degreeValue4 = Math.Pow(value, 4);
            double degreeValue2 = 3d * Math.Pow(value, 2);
            double znam = degreeValue4 + degreeValue2 + 2d;
            return value / znam;
        }
        public static List<double[]> GetGraphikCoordinate(double startCoordinate, double endCoordinate, double step)
        {
            List<double[]> coordinate = new List<double[]>();

            double difference = (endCoordinate - startCoordinate) + 1;
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

            coordinate.Add(dataX);
            coordinate.Add(dataY);

            return coordinate;
        }
    }
}
