using System;
using System.Windows;

namespace Laba
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Метод, перехода на стартовую страницу.
        public void Start (object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("/Pages/StartPage.xaml", UriKind.Relative));
        }
    }
}
