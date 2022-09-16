using ScottPlot;
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

namespace lab2
{
    /// <summary>
    /// Логика взаимодействия для PageAnalys.xaml
    /// </summary>
    public partial class PageAnalys : Page
    {
        public PageAnalys()
        {
            InitializeComponent();
            CorrelationDependence();
            AnalisBook();          
        }
        //double[] incomeAll = clients.Select(x => x.Income).Select(x => (double)x).ToArray();

        private void AnalisBook()
        {
            bestFormSell.Text = LibraryEntities.GetInstance().books.OrderByDescending(x => x.SellCount).ToList()[0].FormSell.Title;
            incomeSell.Text = LibraryEntities.GetInstance().books.Sum(x => x.Cost * x.SellCount).ToString();


            var a = LibraryEntities.GetInstance().books.GroupBy(x => x.Topic.Title).Select(x => x.GroupBy(c => c.Cost * c.SellCount)).ToList();
            var b = a.Select(x => x.Sum(z => z.Key)).ToList();

            int indexMaxPrice = 0;
            for (int i = 0; i < b.Count - 1; i++)
                if (b[i] > b[i + 1])
                    indexMaxPrice = i;

            bestTopic.Text = a[indexMaxPrice].First().First().Topic.Title;
        }
        private void CorrelationDependence()
        {
            var clients = LibraryEntities.GetInstance().Clients.OrderBy(x => x.Income);

            double incomeAll = clients.Select(x => x.Income).Select(x => (double)x).ToArray().Sum();
            var education = clients.Where(y => y.books.Count != 0).GroupBy(x => x.Education.Title).ToList();
            var incomeEducztionList = education.Select(x => x.Sum(y => y.Income)).Select(x => double.Parse(x.ToString())).ToList();

            incomeEducztionList.Add(incomeAll);
            var incomeEducztion = incomeEducztionList.ToArray();

            double[] position = new double[incomeEducztion.Length];
            string[] lables = new string[education.Count + 1];

            for (int i = 0; i < incomeEducztion.Length; i++)
                position[i] = i + 1;
            for (int i = 0; i < education.Count; i++)
                lables[i] = education[i].First().Education.Title.Trim();
            lables[lables.Length - 1] = "Общая доходность";

            korEducation.Plot.AddBar(incomeEducztion, position);
            korEducation.Plot.XTicks(position, lables);
            korEducation.Refresh();
        }
    }
}
