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
using System.Globalization;

namespace DEExam
{
    /// <summary>
    /// Логика взаимодействия для PanelInfo.xaml
    /// </summary>
    public partial class PanelInfo : UserControl
    {
        public Material Material { get; set; }
        public PanelInfo()
        {
            InitializeComponent();
            Material = (Material)DataContext;
        }
    }
    [ValueConversion(typeof(ICollection<Supplier>), typeof(string))]
    public class ListToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<string> suppliers = ((ICollection<Supplier>)value).Select(x => x.Title).ToList();
            if (suppliers.Count == 0)
                return "пока нет поставщиков";
            return String.Join(", ", (suppliers));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
