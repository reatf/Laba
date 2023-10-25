using MySqlX.XDevAPI.Relational;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Laba
{
    public partial class EndPage : Page
    {
        public EndPage()
        {
            InitializeComponent();
        }
        public void Start(object sender, RoutedEventArgs e)
        {
            if(Save.BlackChips > Save.WhiteChips)
            {
                WinPlayer.Content = Save.Player1;
                GameLogic.Winner(Save.Player1);
            }
            else if(Save.WhiteChips > Save.BlackChips)
            {
                WinPlayer.Content = Save.Player2;
                GameLogic.Winner(Save.Player2);
            }
            else
            {
                WinText.Visibility = Visibility.Hidden;
                DrawText.Visibility = Visibility.Visible;
            }
            ContinueAnimation();
        }
        public async void ContinueAnimation()
        {
            DoubleAnimation TopAnimation = new(0, -588, TimeSpan.FromSeconds(2));
            TranslateTransform Transform = new();
            ContinueButton.RenderTransform = Transform;
            Transform.BeginAnimation(TranslateTransform.YProperty, TopAnimation);
            await Task.Delay(2000);

        }

        private void Menu(object sender, MouseButtonEventArgs e)
        {
            NavigationService Navigate = this.NavigationService;
            Navigate.RemoveBackEntry();
            Navigate.Navigate(new Uri("/Pages/StartPage.xaml", UriKind.Relative));          
        }
    }
}
