using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;


namespace Orgonizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BindingList<Line> _currentGroup;
        private BindingList<Line> _currentGroupSearh;
        public MainWindow()
        {
            InitializeComponent();
            DataBase.Load();
            _currentGroup = DataBase.DataResorse.PhoneLines;
            listResorce.ItemsSource = DataBase.DataResorse.PhoneLines;
        }     

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text == "")
                return;
            _currentGroup.Add(new Line(textBox.Text));
        } 

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            Line selectLine = (Line)listResorce.SelectedItem;
            if (_currentGroupSearh != null)
                _currentGroupSearh.Remove(selectLine);
            _currentGroup.Remove(selectLine);          
        } 

        private void ButtonSearh_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text == "")
                return;
            BindingList<Line> list = new BindingList<Line>();
            foreach (var item in _currentGroup)
            {
                if (item.Data.Contains(textBox.Text))
                    list.Add(item);
            }
            listResorce.ItemsSource = list;
            _currentGroupSearh = list;
            lableSelect.Content = "Результат поиска";
        }

        private void ShowSourceButton(object sender, BindingList<Line> group)
        {
            _currentGroupSearh = null;
            lableSelect.Content = ((Button)sender).Content;
            _currentGroup = group;
            listResorce.ItemsSource = group;
        }

        private void ButtonOrganization_Click(object sender, RoutedEventArgs e) => ShowSourceButton(sender, DataBase.DataResorse.OrganizationLines);

        private void ButtonPhone_Click(object sender, RoutedEventArgs e) => ShowSourceButton(sender, DataBase.DataResorse.PhoneLines);

        private void ButtonAdress_Click(object sender, RoutedEventArgs e) => ShowSourceButton(sender, DataBase.DataResorse.AdressLines);

        private void ButtonMeeting_Click(object sender, RoutedEventArgs e) => ShowSourceButton(sender, DataBase.DataResorse.MeetingLines);

        private void ButtonPeople_Click(object sender, RoutedEventArgs e) => ShowSourceButton(sender, DataBase.DataResorse.PeopleLines);
    }
}
