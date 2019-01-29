using System.Windows;
using System.Windows.Controls;
using KvantCard.Repos;

namespace KvantCard.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly StudentRepo _students;

        public MainWindow(StudentRepo students)
        {
            _students = students;

            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void CbKvantum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
