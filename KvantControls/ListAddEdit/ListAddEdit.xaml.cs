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
using KvantControls.Utils;

namespace KvantControls.ListAddEdit
{
    /// <summary>
    /// Interaction logic for ListAddEdit2.xaml
    /// </summary>
    public partial class ListAddEdit : UserControl
    {
        public ListAddEdit()
        {
            InitializeComponent();

            MainGrid.RemoveChild(ListGrid);
            MainGrid.RemoveChild(AddEditGrid);
            ListGrid.Visibility = Visibility.Visible;
            AddEditGrid.Visibility = Visibility.Visible;
            TransitionBox.Content = ListGrid;
        }


        public UIElement List
        {
            get => (UIElement)GetValue(ListProperty);
            set => SetValue(ListProperty, value);
        }

        public static readonly DependencyProperty ListProperty =
            DependencyProperty.Register("List", typeof(UIElement),
                typeof(ListAddEdit), new UIPropertyMetadata(ListTarget));

        public UIElement AddEdit
        {
            get => (UIElement)GetValue(AddEditProperty);
            set => SetValue(AddEditProperty, value);
        }

        public static readonly DependencyProperty AddEditProperty =
            DependencyProperty.Register("AddEdit", typeof(UIElement),
                typeof(ListAddEdit), new UIPropertyMetadata(AddEditTarget));

        private static void ListTarget(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private static void AddEditTarget(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void BtnAdd_OnClick(object sender, RoutedEventArgs e)
        {
            TransitionAnimation.StartPoint = new Point(1, 0);
            TransitionBox.Content = AddEditGrid;
        }

        private void BtnEdit_OnClick(object sender, RoutedEventArgs e)
        {
            TransitionAnimation.StartPoint = new Point(1, 0);
            TransitionBox.Content = AddEditGrid;
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            TransitionAnimation.StartPoint = new Point(-1, 0);
            TransitionBox.Content = ListGrid;
        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            TransitionAnimation.StartPoint = new Point(-1, 0);
            TransitionBox.Content = ListGrid;
        }
    }
}
