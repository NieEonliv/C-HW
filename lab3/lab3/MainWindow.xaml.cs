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

namespace lab3
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
        private int GetElement(int i, int j)
        {
            if (i == 1 || j ==1)
                return 1;
            else
                return GetElement(i, j-1) + GetElement(i-1, j);            
        }

        private void GetElement_Clilk(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(iValue.Text , out int resi) || !int.TryParse(jValue.Text, out int resj))
            {
                return;
                MessageBox.Show("Ввод был некоректен");
            }
            result.Text = GetElement(resi, resj).ToString();
        }
    }
}
