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

namespace Define.UI2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Welcom_window _welcomWindow;
        private PopupAdvice _popupAdvice;
        private PopupSearch _popupSearch;
        private Search_window _searchWindow;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _welcomWindow = new Welcom_window();
            _welcomWindow.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _popupAdvice = new PopupAdvice();
            _popupAdvice.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            _popupSearch = new PopupSearch();
            _popupSearch.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            _searchWindow = new Search_window();
            _searchWindow.Show();         
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
