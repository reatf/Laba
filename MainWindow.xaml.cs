using System;
using System.Windows;
using System.Windows.Navigation;

namespace Laba
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Метод, вызываемый при запуске главного окна
        public void Start (object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("/Pages/StartPage.xaml", UriKind.Relative));
        }
    }
}
