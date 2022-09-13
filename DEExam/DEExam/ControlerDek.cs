using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Reflection;

namespace DEExam
{
    static class ControlerDek
    {
        private const int LIMITED_ITEMS_PAGE = 15;
        private static List<Material> materials;
        public static List<Material> Materials 
        {
            get { return materials; }
            set 
            { 
                materials = value;
                float temp = (float)materials.Count / (float)LIMITED_ITEMS_PAGE;
                maxPage = (int)Math.Round(temp,MidpointRounding.AwayFromZero);
            }
        }
        public static int currentPage = 2;
        public static int maxPage;


        public static List<Border> CreateItems()
        {
            int endIndex = currentPage * LIMITED_ITEMS_PAGE;
            int startIndex = (currentPage - 1) * LIMITED_ITEMS_PAGE;

            List<Border> borderList = new List<Border>();
            for (int i = startIndex; i < endIndex && i < materials.Count; i++)
            {
                Grid grid = new Grid();
                grid.Margin = new Thickness(10);

                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition());

                grid.RowDefinitions[0].Height = new GridLength(35);
                grid.RowDefinitions[1].Height = new GridLength(35);
                grid.RowDefinitions[2].Height = new GridLength(35);
                grid.RowDefinitions[3].Height = new GridLength(35);

                var cb = new BrushConverter();

                Border border = new Border
                {
                    Width = 750,
                    BorderThickness = new Thickness(2),
                    BorderBrush = (Brush)cb.ConvertFrom("#FFC1C1"),
                    CornerRadius = new CornerRadius(20, 20, 20, 20),
                    Margin = new Thickness(15)
                };

                TextBlock nText = new TextBlock
                {
                    FontFamily = new FontFamily("Verdana"),
                    Foreground = (Brush)cb.ConvertFrom("#1b1464"),
                    Margin = new Thickness(180, 0, 0, 0),
                    FontSize = 16,
                    Text = "Название товара: " + materials[i].Title + "\n"
                };

                TextBlock cgText = new TextBlock
                {
                    FontFamily = new FontFamily("Verdana"),
                    Foreground = (Brush)cb.ConvertFrom("#1b1464"),
                    FontSize = 16,
                    Margin = new Thickness(180, 0, 0, 0),
                    Text = "Категория: " + materials[i].MaterialType.Title + "\n"
                };

                TextBlock prText = new TextBlock
                {
                    FontFamily = new FontFamily("Verdana"),
                    Foreground = (Brush)cb.ConvertFrom("#1b1464"),
                    FontSize = 16,
                    Margin = new Thickness(180, 0, 0, 0),
                    Text = "Стоимость : " + materials[i].Cost + "\n"
                };

                TextBlock posText = new TextBlock
                {
                    FontFamily = new FontFamily("Verdana"),
                    Foreground = (Brush)cb.ConvertFrom("#1b1464"),
                    FontSize = 16,
                    Margin = new Thickness(180, 0, 0, 0),
                    Text = "Поставщик : " + materials[i].SuppliersNames + "\n"
                };

                Image image = new Image();
                image.Source = new BitmapImage(new Uri($"{materials[i].Image}", UriKind.Relative));
                image.Width = 110;
                image.Height = 110;
                image.Margin = new Thickness(10, 20, 0, 0);
                image.HorizontalAlignment = HorizontalAlignment.Left;
                image.VerticalAlignment = VerticalAlignment.Center;

                Grid.SetRowSpan(image, 3);
                grid.Children.Add(image);

                Grid.SetRow(nText, 0);
                grid.Children.Add(nText);
                Grid.SetRow(cgText, 1);
                grid.Children.Add(cgText);
                Grid.SetRow(prText, 2);
                grid.Children.Add(prText);
                Grid.SetRow(posText, 3);
                grid.Children.Add(posText);

                border.Child = grid;

                borderList.Add(border);
            }
            return borderList;
        }
    }
}
