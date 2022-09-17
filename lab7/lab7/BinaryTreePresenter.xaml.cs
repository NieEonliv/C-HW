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
    /// Логика взаимодействия для BinaryTreePresenter.xaml
    /// </summary>
    public partial class BinaryTreePresenter : UserControl
    {
        public BinaryTreePresenter()
        {
            InitializeComponent();
            LeftHost.DataContextChanged += OnDataContextChanged;
            RightHost.DataContextChanged += OnDataContextChanged;
        }

        public void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var host = (Decorator)sender;
            if (host.DataContext is IBinaryTree)
                host.Child = host.Child ?? new BinaryTreePresenter();
            else
                host.Child = null;
        }
    }
}
