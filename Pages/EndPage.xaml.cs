using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace Laba
{
    public partial class EndPage : Page
    {
        public EndPage() => InitializeComponent();

        // Метод, вызываемый при запуске экрана завершения
        public void Start(object sender, RoutedEventArgs e)
        {
            // Проверка результатов игры и определение победителя
            if (Save.BlackChips > Save.WhiteChips)
            {
                WinPlayer.Content = Save.Player1;
                GameLogic.Winner(Save.Player1); // Установка победителя в БЛ
            }
            else if(Save.WhiteChips > Save.BlackChips)
            {
                WinPlayer.Content = Save.Player2;
                GameLogic.Winner(Save.Player2); // Установка победителя в БЛ
            }
            else
            {
                WinText.Visibility = Visibility.Hidden;
                DrawText.Visibility = Visibility.Visible;
            }

            ContinueAnimation();
        }

        // Метод для анимации кнопки "Продолжить"
        public async void ContinueAnimation()
        {
            DoubleAnimation TopAnimation = new(0, -588, TimeSpan.FromSeconds(2));
            TranslateTransform Transform = new();
            ContinueButton.RenderTransform = Transform;
            Transform.BeginAnimation(TranslateTransform.YProperty, TopAnimation);
            await Task.Delay(2000);

        }

        // Метод для перехода в главное меню
        private void Menu(object sender, MouseButtonEventArgs e)
        {
            NavigationService Navigate = this.NavigationService;
            Navigate.RemoveBackEntry();
            Navigate.Navigate(new Uri("/Pages/StartPage.xaml", UriKind.Relative));          
        }
    }
}
