using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Laba.ViewModels;

namespace Laba
{
    public partial class PlayersNicksPage : Page
    {
        private PlayersNickVM View;

        public PlayersNicksPage()
        {
            View = new(this);
            InitializeComponent();
        }


        // Метод для отображения подсказки и анимации при запуске страницы
        private async void HintsShow(object sender, RoutedEventArgs e)
        {
            MainAnimation(0, 500);
            await Task.Delay(1100);
            HintAnimation(-150);
            EnterButtonAnimation(90);
        }

        // Метод для анимации элементов страницы при старте
        private async void MainAnimation(int Start, int Vector)
        {
            TranslateTransform TransformMain = new();
            DoubleAnimation Position = new(Start, Vector, TimeSpan.FromSeconds(1));

            NicksMenu.RenderTransform = TransformMain;
            TransformMain.BeginAnimation(TranslateTransform.YProperty, Position);
            await Task.Delay(1000);
        }

        // Метод для анимации подсказки
        private async void HintAnimation(int Vector)
        {
            TranslateTransform TransformLeft = new();
            DoubleAnimation Position = new(0, Vector, TimeSpan.FromSeconds(1));

            PlayersHint.RenderTransform = TransformLeft;
            TransformLeft.BeginAnimation(TranslateTransform.XProperty, Position);
            await Task.Delay(1000);

            TransformLeft.BeginAnimation(TranslateTransform.XProperty, null);
            Canvas.SetLeft(PlayersHint, Canvas.GetLeft(PlayersHint) + Vector);
        }

        // Метод для анимации кнопки "Ввод"
        private async void EnterButtonAnimation(int Vector)
        {
            TranslateTransform TransformTop = new();
            DoubleAnimation Position = new(0, Vector, TimeSpan.FromSeconds(1));

            EnterButton.RenderTransform = TransformTop;
            TransformTop.BeginAnimation(TranslateTransform.YProperty, Position);
            await Task.Delay(1000);

            TransformTop.BeginAnimation(TranslateTransform.YProperty, null);
            Canvas.SetTop(EnterButton, Canvas.GetTop(EnterButton) + Vector);
        }

        // Метод для анимации доски
        public async void BoardAnimation(int Vector)
        {
            TranslateTransform TransformTop = new();
            DoubleAnimation Position = new(0, Vector, TimeSpan.FromSeconds(2));

            Board.RenderTransform = TransformTop;
            TransformTop.BeginAnimation(TranslateTransform.YProperty, Position);
            await Task.Delay(2000);

            TransformTop.BeginAnimation(TranslateTransform.YProperty, null);
            Canvas.SetTop(Board, Canvas.GetTop(Board) + Vector);
        }

        // Метод для анимации счетчика фишек
        public async void ChipCountAnimation(int Vector)
        {
            TranslateTransform TransformLeft = new();
            DoubleAnimation Position = new(0, Vector, TimeSpan.FromSeconds(2));

            ChipCount.RenderTransform = TransformLeft;
            TransformLeft.BeginAnimation(TranslateTransform.XProperty, Position);
            await Task.Delay(2000);

            Canvas.SetLeft(ChipCount, Canvas.GetLeft(ChipCount) + Vector);
        }

        // Обработчик клика по кнопке "Ввод"
        private void EnterButtonClick(object sender, MouseButtonEventArgs e) => View.SetNames(Player1Nick.Text, Player2Nick.Text);

        // Метод для начала игры и анимации перехода
        public async void StartGame()
        {
            ErrorName.Visibility = Visibility.Hidden;
            HintAnimation(150);
            EnterButtonAnimation(-90);
            await Task.Delay(1100);
            MainAnimation(500, 0);
            await Task.Delay(1100);
            BoardAnimation(-1191);
            ChipCountAnimation(-760);
            await Task.Delay(2100);

            View.End();
            View = default!;

            NavigationService.RemoveBackEntry();
            NavigationService.Navigate(new Uri("/Pages/GamePage.xaml", UriKind.Relative));
        }

        // Метод для отображения сообщения об некорректности ввода имён
        public void Error() => ErrorName.Visibility = Visibility.Visible;
    }
}
