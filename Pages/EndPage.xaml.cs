using Laba.ViewModels;
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
        private EndPageVM View;
        public EndPage()
        {
            View = new(this);
            InitializeComponent();
        }

        // Метод визуализации итогов игры.
        public void Start(object sender, RoutedEventArgs e)
        {
            View.CheckEnd();

            ContinueAnimation();
        }

        // Метод установки победителя Player1.
        public void Winner1()
        {
            WinPlayer.Content = Save.Player1;
            GameLogic.Winner(Save.Player1);
        }

        // Метод установки победителя Player2.
        public void Winner2()
        {
            WinPlayer.Content = Save.Player2;
            GameLogic.Winner(Save.Player2);
        }

        // Метод для отображения ничьей.
        public void Draw()
        {
            WinText.Visibility = Visibility.Hidden;
            DrawText.Visibility = Visibility.Visible;
        }

        // Метод анимации кнопки "Продолжить"
        public async void ContinueAnimation()
        {
            DoubleAnimation TopAnimation = new(0, -588, TimeSpan.FromSeconds(2));
            TranslateTransform Transform = new();
            ContinueButton.RenderTransform = Transform;
            Transform.BeginAnimation(TranslateTransform.YProperty, TopAnimation);
            await Task.Delay(2000);

        }

        // Метод перехода в стартовое окно.
        private void Menu(object sender, MouseButtonEventArgs e)
        {
            View.Ending();
            View = default!;
            NavigationService Navigate = this.NavigationService;
            Navigate.RemoveBackEntry();
            Navigate.Navigate(new Uri("/Pages/StartPage.xaml", UriKind.Relative));          
        }

    }
}
