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
    /// Логика взаимодействия для PageSelectionOfBooks.xaml
    /// </summary>
    public partial class PageSelectionOfBooks : Page
    {
        private List<book> _books;
        public PageSelectionOfBooks()
        {
            InitializeComponent();

            clients.ItemsSource = LibraryEntities.GetInstance().Clients.ToList()
                .Where(x => x.books.Count != 0)
                .Select(x => x.ID.ToString()+ " : "+ x.LastName.Trim() + " " + x.FirstName.Trim() + " " + x.Patronymic.Trim());
            topics.ItemsSource = LibraryEntities.GetInstance().Topics.ToList().Select(x => x.Title);
        }
        private void ShowRecomindation_Click(object sender, RoutedEventArgs e)
        {
            if (clients.SelectedIndex == -1 || topics.SelectedIndex == -1)
                return;
            var haveBook = LibraryEntities.GetInstance().Clients.ToList().Where(x => x.ID.ToString().Contains(clients.SelectedValue.ToString().Split()[0])).ToList()[0].books;
            table.ItemsSource = LibraryEntities.GetInstance().books.ToList()
                .Where(x => x.Topic.Title.Contains(topics.SelectedValue.ToString()))
                .Except(haveBook);
        }
    }
}
