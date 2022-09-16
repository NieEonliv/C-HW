using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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

namespace lab5
{
    /// <summary>
    /// Логика взаимодействия для DefoltPage.xaml
    /// </summary>
    public partial class DefoltPage : Page
    {
        public DefoltPage(DataGrid dataGrid)
        {
            InitializeComponent();
            table.Children.Add(dataGrid);
            dataGrid.ItemsSource = ShopEntities.GetInstance().Products.ToList();
        }

    }
}
