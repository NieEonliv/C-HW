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

namespace lab7
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string expression = input.Text;
            TreeExpasion treeExpasion = new TreeExpasion();
            treeExpasion.Expasion = expression;

            try
            {
                treeExpasion.ShowTree();
            }
            catch (Exception)
            {
                MessageBox.Show("Некоректные данные");
            }

            tree.DataContext = treeExpasion;
        }
    }
}
