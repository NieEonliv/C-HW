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
        public static double Chebyshevs(double[] dataY, double step , double a)
        {
            double resultCalculate = 0;
            for (int i = 0; i < dataY.Length -1; i++)
            {
                double temp = a + step * i;
                for (int j = 0; j < 5; j++)
                    resultCalculate += CalculateFunction(temp + step * dataY[j]);
            }
            resultCalculate *= step / 4d;
            return resultCalculate;
        }

        public static double Simpsons(double[] dataY, double step)
        {
            double f0 = dataY[0] + dataY[dataY.Length - 1];
            double fche = 0;
            double fnch = 0;
            for (int i = 1; i < dataY.Length - 1; i++)
            {
                if (i % 2 == 0)
                    fche += dataY[i];
                else
                    fnch += dataY[i];
            }
            double resultCalculate = step / 3d * (f0 + 2d * fche + 4d * fnch);
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
        public static double Rectangle(double[] dataY, double step)
        {
            double resultCalculate = 0;
            foreach (double CoorditateY in dataY)
                resultCalculate += CoorditateY * step;
            return resultCalculate;
        }
        public static double CalculateFunction(double value)
        {
            double degreeValue = Math.Pow(value, 2);
            double sqrtValue = Math.Sqrt(degreeValue + 1);
            return sqrtValue;
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
