using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Laba
{
    public partial class PlayersNicksPage : Page
    {
        public PlayersNicksPage()
        {
            InitializeComponent();
        }
        private async void HintsShow(object sender, RoutedEventArgs e)
        {
            MainAnimation(500);
            await Task.Delay(1100);
            HintAnimation(-150);
            EnterButtonAnimation(90);
        }
        private async void MainAnimation(int Vector)
        {
            UIElement[] Main = { PlayersNicks, Player1Nick, Player2Nick, PlayersHint, EnterButton, ErrorName };
            TranslateTransform TransformMain = new();
            DoubleAnimation Position = new(0, Vector, TimeSpan.FromSeconds(1));

            for (int i = 0; i < Main.Length; i++)
            {
                Main[i].RenderTransform = TransformMain;
                TransformMain.BeginAnimation(TranslateTransform.YProperty, Position);
            }

            await Task.Delay(1000);
            for (int i = 0; i < Main.Length; i++)
            {
                Main[i].RenderTransform = TransformMain;
                TransformMain.BeginAnimation(TranslateTransform.YProperty, null);
            }
            for (int i = 0; i < Main.Length; i++)
            {
                Canvas.SetTop(Main[i], Canvas.GetTop(Main[i]) + Vector);
            }
        }
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
        private void EnterButtonClick(object sender, MouseButtonEventArgs e)
        {
            bool Key;
            Save.Player1 = Player1Nick.Text;
            Save.Player2 = Player2Nick.Text;
            GameLogic.Check(out Key);
            if(Key)
            {
                StartGame();
            }
            else
            {
                Error();
            }
        }
        public async void StartGame()
        {
            ErrorName.Visibility = Visibility.Hidden;
            HintAnimation(150);
            EnterButtonAnimation(-90);
            await Task.Delay(1100);
            MainAnimation(-500);
        }
        public void Error()
        {
            ErrorName.Visibility = Visibility.Visible;
        }
        public void BoardAnimation(int Vector)
        {

        }
    }
}
