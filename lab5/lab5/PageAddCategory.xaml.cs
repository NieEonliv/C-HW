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

namespace lab5
{
    /// <summary>
    /// Логика взаимодействия для PageAddCategory.xaml
    /// </summary>
    public partial class PageAddCategory : Page
    {
        private static int _count;
        private DataGrid _currentDataGrid;
        public PageAddCategory()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            _count++;
            _currentDataGrid = new DataGrid();
            _currentDataGrid.IsReadOnly = true;
            _currentDataGrid.AutoGenerateColumns = false;

            if ((bool)ID.IsChecked)
                CreateDataGridColum("ID", "ID");
            if((bool)Title.IsChecked)
                CreateDataGridColum("Название", "Title");
            if ((bool)Code.IsChecked)
                CreateDataGridColum("Код", "Code");
            if ((bool)PackagingID.IsChecked)
                CreateDataGridColum("Упаковка", "PackahingType.Title");
            if ((bool)DateReceipt.IsChecked)
                CreateDataGridColum("Дата поставки", "DateReceipt");
            if ((bool)DateExpiry.IsChecked)
                CreateDataGridColum("Срок годности до", "DateExpiry");
            if ((bool)VolumePurchases.IsChecked)
                CreateDataGridColum("Объем закупки", "VolumePurchases");
            if ((bool)VolumeSales.IsChecked)
                CreateDataGridColum("Объем продажи", "VolumeSales");

            MainWindow.GetInstance().frame.Navigate(new DefoltPage(_currentDataGrid));
        }


        private void CreateDataGridColum(string header,string binding)
        {
            _currentDataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = header,
                FontSize = 12,
                Binding = new Binding(binding)
            });
        }
    }
}
