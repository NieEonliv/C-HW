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

namespace DEExam
{
    /// <summary>
    /// Логика взаимодействия для MaterialsPage.xaml
    /// </summary>
    public partial class MaterialsPage : Page
    {
        private static TextBlock _lastTextBlock = new TextBlock();
        public MaterialsPage()
        {
            InitializeComponent();
            firstBlock.TextDecorations = TextDecorations.Underline;
            _lastTextBlock = firstBlock;
            List<Material> materials = demExamEntities.GetContext().Materials.ToList();
            foreach (var material in materials)
            {
                foreach (var suppliers in material.Suppliers)
                {
                    material.SuppliersNames += suppliers.Title + ", "; 
                }
                if (material.SuppliersNames != null)
                    material.SuppliersNames = material.SuppliersNames.TrimEnd(new char[] { ',', ' ' });
                else
                    material.SuppliersNames = "на данный момент поставщиков нет";               
            }

            ControlerDek.Materials = materials;
            UpdateGrid();
        }
        private void UpdateGrid()
        {
            wrap.Children.Clear();
            foreach (var item in ControlerDek.CreateItems())
                wrap.Children.Add(item);
        }
        private void leftPage_Click(object sender, EventArgs e)
        {
            if (ControlerDek.currentPage == 1)
                return;
            ControlerDek.currentPage -= 1;
            UpdateGrid();
        }
        private void RightPage_Click(object sender, EventArgs e)
        {
            if (ControlerDek.maxPage < ControlerDek.currentPage + 1)
                return;
            ControlerDek.currentPage += 1;
            UpdateGrid();
        }

        private void numberPage_Click(object sender, EventArgs e)
        {
            TextBlock temp = (TextBlock)sender;
            _lastTextBlock.TextDecorations = null;
            temp.TextDecorations = TextDecorations.Underline;

            _lastTextBlock = temp;
            ControlerDek.currentPage = int.Parse(temp.Text);
            UpdateGrid();
        }

    }
}
