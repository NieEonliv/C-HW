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
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Windows.Media.Media3D;
using System.Runtime.Remoting.Messaging;

namespace DEExam
{
    /// <summary>
    /// Логика взаимодействия для MaterialsPage.xaml
    /// </summary>
    public partial class MaterialsPage : Page
    {
        private static List<TextBlock> listens = new List<TextBlock>();
        private static int countAll = 0;
        public MaterialsPage()
        {
            InitializeComponent();
            firstBlock.TextDecorations = TextDecorations.Underline;
            List<Material> Materials = demExamEntities.GetContext().Materials.ToList();
            var types = Materials.Select(x => x.MaterialType.Title).Distinct().Select(x => x.Remove(x.Length - 2)).ToList();
            sortType.Items.Add("Все типы");
            sortType.SelectedIndex = 0;
            foreach (var item in types)
                sortType.Items.Add(item);
            ControlerDek.Materials = Materials;
            countAll = Materials.Count;
            UpdateGrid();
        }
        private void UpdateGrid()
        {
            if (wrap == null)            
                return;            
            wrap.Children.Clear();
            foreach (var item in ControlerDek.CreateItems())
                wrap.Children.Add(item);
        }
        private void leftPage_Click(object sender, EventArgs e)
        {
            ControlerDek.leftPage();
            UpdateGrid();
        }
        private void RightPage_Click(object sender, EventArgs e)
        {
            ControlerDek.RightPage();
            UpdateGrid();
        }

        private void numberPage_Click(object sender, EventArgs e)
        {
            ControlerDek.NumberPage(((TextBlock)sender).Text);
            UpdateGrid();
        }
        private void Search(string key)
        {           
            ControlerDek.Materials = demExamEntities.GetContext().Materials.ToList();
            if (sortOn == null || sortType == null || sortUpDown == null)
                return;
            if (sortType.Text == "Все типы") { }
            else
                ControlerDek.Materials = ControlerDek.Materials.Where(x => x.MaterialType.Title.Contains(sortType.Text)).ToList();
            switch (sortOn.Text)
            {
                case "Без фильтров":
                    ControlerDek.Materials = demExamEntities.GetContext().Materials.ToList().Where(x =>
                    x.Unit.ToString().Contains(key) ||
                    x.CountInStock.ToString().Contains(key) ||
                    x.MaterialType.Title.ToString().Contains(key) ||
                    x.Suppliers.Select(y => y.Title).Contains(key) ||
                    x.Title.ToString().Contains(key) ||
                    x.MinCount.ToString().Contains(key))
                    .ToList();
                    switch (sortUpDown.Text)
                    {
                        case "По убыванию": ControlerDek.Materials = ControlerDek.Materials.OrderByDescending(x => x.CountInStock).ToList(); break;
                        default: ControlerDek.Materials = ControlerDek.Materials.OrderBy(x => x.CountInStock).ToList(); break;
                    }
                    if (sortType.Text == "Все типы") { }
                    else
                        ControlerDek.Materials = ControlerDek.Materials.Where(x => x.MaterialType.Title.Contains(sortType.Text)).ToList();
                    break;

                case "Остаток на складе":
                    ControlerDek.Materials = ControlerDek.Materials.Where(x => x.CountInStock.ToString().Contains(key)).ToList();
                    switch (sortUpDown.Text)
                    {
                        case "По убыванию": ControlerDek.Materials = ControlerDek.Materials.OrderByDescending(x => x.CountInStock).ToList(); break;
                        default: ControlerDek.Materials = ControlerDek.Materials.OrderBy(x => x.CountInStock).ToList(); break;
                    }
                    break;

                case "Стоимость":
                    ControlerDek.Materials = ControlerDek.Materials.Where(x => x.Cost.ToString().Contains(key)).ToList();
                    switch (sortUpDown.Text)
                    {
                        case "По убыванию": ControlerDek.Materials = ControlerDek.Materials.OrderByDescending(x => x.Cost).ToList(); break;
                        default: ControlerDek.Materials = ControlerDek.Materials.OrderBy(x => x.Cost).ToList(); break;
                    }
                    break;

                default:
                    ControlerDek.Materials = ControlerDek.Materials.Where(x => x.Title.Contains(key)).ToList();
                    switch (sortUpDown.Text)
                    {
                        case "По убыванию": ControlerDek.Materials = ControlerDek.Materials.OrderByDescending(x => x.Title).ToList(); break;
                        default: ControlerDek.Materials = ControlerDek.Materials.OrderBy(x => x.Title).ToList(); break;
                    }
                    break;
            }
            if (searchCount != null)
                searchCount.Text = $"{ControlerDek.Materials.Count} из {countAll}";
            UpdateGrid();
        }
        private void Sherch(object sender, TextChangedEventArgs e)
        {
            ControlerDek.NumberPage("1");
            var key = ((TextBox)sender).Text;
            Search(key);
        }

        private void sortType_SelectionChanged(object sender, SelectionChangedEventArgs e) => Search(sher.Text);
        private void sortUpDown_SelectionChanged(object sender, SelectionChangedEventArgs e) => Search(sher.Text);
        private void sortOn_SelectionChanged(object sender, SelectionChangedEventArgs e) => Search(sher.Text);
        private void litsten_Initialized(object sender, EventArgs e)
        {
            foreach (var item in litsten.Children)
            {
                if (item is TextBlock)
                {
                    if (((TextBlock)item).Tag == null)
                        listens.Add((TextBlock)item);
                }
            }
            ControlerDek.Listens = listens;
        }
    }
}
