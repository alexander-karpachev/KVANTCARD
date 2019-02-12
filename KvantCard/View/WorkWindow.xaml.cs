using System.Windows;
using System.Windows.Controls;

namespace KvantCard.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

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
