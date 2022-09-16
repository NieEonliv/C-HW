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
using System.Data.Entity;

namespace lab2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            frame.Navigate(new PageBook());
        }

        private void ShowBooks_Click(object sender, RoutedEventArgs e) => frame.Navigate(new PageBook());

        private void ShowClients_Click(object sender, RoutedEventArgs e) => frame.Navigate(new PageClient());

        private void ShowAnalitik_Click(object sender, RoutedEventArgs e) => frame.Navigate(new PageAnalys());

        private void SelectionOfBooks_Click(object sender, RoutedEventArgs e) => frame.Navigate(new PageSelectionOfBooks());
    }
}
