using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _checkRadio = false;
        public MainWindow() => InitializeComponent();

        private void GradRadio_Click(object sender, RoutedEventArgs e) => ConventorRadAndGrad();

        private void RadRario_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(textResult.Text, out double result))
            {
                if (_checkRadio)
                {
                    double temp = result * Math.PI / 180;
                    textResult.Text = temp.ToString();
                    _checkRadio = false;
                }                      
            }
        }

        private void MSButton_Click(object sender, RoutedEventArgs e) => memory.Text = textResult.Text;

        private void MRButton_Click(object sender, RoutedEventArgs e) => textResult.Text += memory.Text;

        private void MCButton_click(object sender, RoutedEventArgs e) => memory.Text = string.Empty;


        private void MPlusButton_Click(object sender, RoutedEventArgs e)
        {
            double.TryParse(textResult.Text, out double result);
            if (result == 0)
                return;
            memory.Text = (double.Parse(memory.Text) + result).ToString();
        }

        private void MMuinusButton_Click(object sender, RoutedEventArgs e)
        {
            double.TryParse(textResult.Text, out double result);
            if (result == 0)
                return;
            memory.Text = (double.Parse(memory.Text) - result).ToString();
        }

        private void StandardCharactersButton_Click(object sender, RoutedEventArgs e)
            => textResult.Text += ((Button)sender).Content.ToString();

        private void SignsButton_Click(object sender, RoutedEventArgs e)
            => textResult.Text += ((Button)sender).Tag.ToString();

        private void ClearButton_Click(object sender, RoutedEventArgs e)
            => textResult.Text = string.Empty;

        private void SpecialExpressionsButton_Click(object sender, RoutedEventArgs e)
            => textResult.Text += ((Button)sender).Tag.ToString() + "(";


        private void EraseButton_Click(object sender, RoutedEventArgs e)
        {
            if (textResult.Text.Length > 0)
                textResult.Text = textResult.Text[..^1];
        }

        private void ReplacePlusMinusButton_Click(object sender, RoutedEventArgs e)
        {
            if (Regex.IsMatch(textResult.Text, "-\\d+$"))
            {
                string startStringValue = Regex.Match(textResult.Text, "-\\d+$").ToString().Substring(1);
                textResult.Text = Regex.Replace(textResult.Text, "-\\d+$", startStringValue);
            }
            else
            {
                string endStringValue = "-" + Regex.Match(textResult.Text, "\\d+$").ToString();
                textResult.Text = Regex.Replace(textResult.Text, "\\d+$", endStringValue);
            }
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(textResult.Text , "[0-9]+"))
                return;

            if (textResult.Text.Contains("Pi"))
                textResult.Text = textResult.Text.Replace("Pi", Math.Round(Math.PI, 14).ToString());
            Regex regex = new Regex(@"(?:\()[^\(\)]*?(?:\))");
            while (Regex.IsMatch(textResult.Text, "log(\\d?)+\\(\\w+\\D?\\d?[)]?[,]?[0-9]?[)]?[,]?\\w+\\D?\\d?\\)"))
            {
                string old = Regex.Match(textResult.Text, "log(\\d?)+\\(\\w+\\D?\\d?[)]?[,]?[0-9]?[)]?[,]?\\w+\\D?\\d?\\)").ToString();
                ReplaceMainTextResult(old, LogCalculate(old));
            }
            while (regex.IsMatch(textResult.Text))
            {
                foreach (var item in regex.Matches(textResult.Text))
                {
                    string temp = item.ToString()[..^1].Substring(1);
                    try
                    {
                        temp = new DataTable().Compute(temp, null).ToString();
                        ReplaceMainTextResult(item.ToString(), temp);
                    }
                    catch (Exception)
                    {
                        var specialSymbol = Regex.Match(temp, @"[a-z]+").ToString();
                        SpecialCalculate(specialSymbol, temp, item.ToString());
                    }
                }
            }

            try
            {
                ReplaceMainTextResult(",", ".");
                textResult.Text = new DataTable().Compute(textResult.Text, null).ToString();
            }
            catch (Exception)
            {
                var specialSymbol = Regex.Match(textResult.Text, @"[a-z]+").ToString();
                SpecialCalculate(specialSymbol, textResult.Text, textResult.Text);
            }

            ConventorRadAndGrad();
        }
        private void ConventorRadAndGrad()
        {
            if (double.TryParse(textResult.Text, out double result))
            {
                if (!_checkRadio)
                {
                    double temp = result * 180 / Math.PI;
                    textResult.Text = temp.ToString();
                    _checkRadio = true;
                }
                         
            }           
        }
        private string LogCalculate(string expesionLog)
        {
            if (expesionLog == "")
                return string.Empty;
            if (Regex.IsMatch(expesionLog, "log10\\("))
            {
                string result = Regex.Match(expesionLog, "(?:\\()[^\\(\\)]*?(?:\\))").ToString().Substring(1)[..^1];
                return Math.Log10(double.Parse(result)).ToString();
            }

            if (Regex.IsMatch(expesionLog, "log\\("))
            {
                string result = Regex.Match(expesionLog, "(?:\\()[^\\(\\)]*?(?:\\))").ToString().Substring(1)[..^1];
                return Math.Log(double.Parse(result)).ToString();
            }
            else
            {
                string[] operands = expesionLog.Substring(4)[..^1].Split(',');

                while (operands[0].Contains("log") || operands[1].Contains("log"))
                {
                    if (Regex.IsMatch(operands[0], "[a-z]+"))
                    {
                        if (operands[0].Contains("log"))
                            operands[0] = LogCalculate(operands[0]);
                        else
                            operands[0] = SpecialCalculateDouble(Regex.Match(operands[0], @"[a-z]+").ToString(), operands[0]).ToString();
                    }

                    if (Regex.IsMatch(operands[1], "[a-z]+"))
                    {
                        if (operands[1].Contains("log"))
                            operands[1] = LogCalculate(operands[1]);
                        else
                            operands[1] = SpecialCalculateDouble(Regex.Match(operands[1], @"[a-z]+").ToString(), operands[1]).ToString();
                    }
                }

                return Math.Log(double.Parse(operands[0]), double.Parse(operands[1])).ToString();
            }
        }

        private void SpecialCalculate(string func, string expesion, string carrentExpesion)
        {
            if (expesion.Contains('^'))
            {
                if (!Regex.IsMatch(expesion, "\\d+[\\^]\\d+"))
                    return;
                string temp = Regex.Match(expesion, "\\d+[\\^]\\d+").ToString();
                double[] operand = temp.Split('^').Select(x => double.Parse(x)).ToArray();
                ReplaceMainTextResult(temp, Math.Pow(operand[0], operand[1]).ToString());
            }
            string targetExpesion = Regex.Match(expesion, $"{func}.?[-]?\\d+([.]|[,])?\\d*").ToString();
            string targetNumber = Regex.Match(targetExpesion, "[-]?\\d+([.]|[,])?\\d*").ToString();

            if (targetNumber.Contains("."))
                targetNumber = targetNumber.Replace('.', ',');

            if (!double.TryParse(targetNumber.ToString(),out double result))
                return;

            switch (func)
            {

                case "sin": ReplaceMainTextResult(targetExpesion, Math.Sin(result).ToString()); break;
                case "cos": ReplaceMainTextResult(targetExpesion, Math.Cos(result).ToString()); break;
                case "tan": ReplaceMainTextResult(targetExpesion, Math.Tan(result).ToString()); break;
                case "sinh": ReplaceMainTextResult(targetExpesion, Math.Sinh(result).ToString()); break;
                case "cosh": ReplaceMainTextResult(targetExpesion, Math.Cosh(result).ToString()); break;
                case "tanh": ReplaceMainTextResult(targetExpesion, Math.Tanh(result).ToString()); break;

                case "asin": ReplaceMainTextResult(targetExpesion, Math.Asin(result).ToString()); break;
                case "acos": ReplaceMainTextResult(targetExpesion, Math.Acos(result).ToString()); break;
                case "atan": ReplaceMainTextResult(targetExpesion, Math.Atan(result).ToString()); break;
                case "asinh": ReplaceMainTextResult(targetExpesion, Math.Asinh(result).ToString()); break;
                case "acosh": ReplaceMainTextResult(targetExpesion, Math.Acosh(result).ToString()); break;
                case "atanh": ReplaceMainTextResult(targetExpesion, Math.Atanh(result).ToString()); break;

                case "exp": ReplaceMainTextResult(targetExpesion, Math.Exp(result).ToString()); break;
                case "ln": ReplaceMainTextResult(targetExpesion, Math.Log(result).ToString()); break;
                case "^": ReplaceMainTextResult(targetExpesion, Math.Log(result).ToString()); break;
                case "fact": ReplaceMainTextResult(targetExpesion, Factorial(result).ToString()); break;
                case "sqrt": ReplaceMainTextResult(targetExpesion, Math.Sqrt(result).ToString()); break;
                case "root": ReplaceMainTextResult(targetExpesion, SqrtPow(targetExpesion).ToString()); break;

                case "abs": ReplaceMainTextResult(targetExpesion, Math.Abs(result).ToString()); break;
                case "mod": ReplaceMainTextResult(targetExpesion, ModFunction(targetExpesion).ToString()); break;

                default: ReplaceMainTextResult(carrentExpesion, expesion); break;
            }

            if (Regex.IsMatch(textResult.Text, "[а-я]+|[А-Я]+"))
                textResult.Text = string.Empty;
        }

        private double SpecialCalculateDouble(string func, string expesion)
        {
            string targetExpesion = Regex.Match(expesion, $"{func}[-]?\\d+([.]|[,])?\\d*").ToString();
            string targetNumber = Regex.Match(targetExpesion, "[-]?\\d+([.]|[,])?\\d*").ToString();

            if (targetNumber.Contains("."))
                targetNumber = targetNumber.Replace('.', ',');

            double result = double.Parse(targetNumber.ToString());

            double final = double.NaN;
            switch (func)
            {
                case "sin": final = Math.Sin(result); break;
                case "cos": final = Math.Cos(result); break;
                case "tan": final = Math.Tan(result); break;
                case "sinh": final = Math.Sinh(result); break;
                case "cosh": final = Math.Cosh(result); break;
                case "tanh": final = Math.Tanh(result); break;

                case "asin": final = Math.Asin(result); break;
                case "acos": final = Math.Acos(result); break;
                case "atan": final = Math.Atan(result); break;
                case "asinh": final = Math.Asinh(result); break;
                case "acosh": final = Math.Acosh(result); break;
                case "atanh": final = Math.Atanh(result); break;

                case "exp": final = Math.Exp(result); break;
                case "ln": final = Math.Log(result); break;

            }
            return final;
        }
        private double Factorial(double degree)
        {
            double result = 1;
            for (int i = 1; i < degree + 1; i++)
                result *= i;
            return result;
        }
        private double SqrtPow(string expesion)
        {
            string[] operands = expesion.Substring(4).Split('.');
            double[] result = operands.Select(x => double.Parse(x)).ToArray();
            return Math.Pow(result[0], 1 / result[1]);
        }

        private double ModFunction(string expesion)
        {
            string[] operands = expesion.Substring(3).Split('.');
            double[] result = operands.Select(x => double.Parse(x)).ToArray();
            return result[0] % result[1];
        }

        private void ReplaceMainTextResult(string oldValue, string newValue)
        {
            if (oldValue != "" && newValue != "")
                textResult.Text = textResult.Text.Replace(oldValue, newValue);
        }
    }
}
