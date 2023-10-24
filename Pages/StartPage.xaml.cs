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
    public partial class StartPage : Page
    {
        public StartPage()
        {
            InitializeComponent();
        }
        private void Leaderboard(object sender, RoutedEventArgs e)
        {
            Label[] TopNicks = { TopPlayer1, TopPlayer2, TopPlayer3, TopPlayer4, TopPlayer5,
                                 TopPlayer6, TopPlayer7, TopPlayer8, TopPlayer9, TopPlayer10 }; // Разобраться

            Label[] TopScores = { TopScore1, TopScore2, TopScore3, TopScore4, TopScore5,
                                  TopScore6, TopScore7, TopScore8, TopScore9, TopScore10 };

            for (int Index = 0; Index < TopNicks.Length; Index++)
            {
                GameLogic.StartLeaderboard(Index, out Leaderboard Player);
                if (Player.Wins == 0)
                {
                    break;
                }
                else
                {
                    TopNicks[Index].Content = Player.Nick;
                    TopScores[Index].Content = Player.Wins;
                }

            }
        }
        private void Exit(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private async void StartGame(object sender, MouseButtonEventArgs e)
        {
            UIElement[] ElementsRight = { StartButton, LoadSaveButton, ExitButton };
            UIElement[] ElementsLeft = { TopPlayers,
                                         TopPlayer1, TopPlayer2, TopPlayer3, TopPlayer4, TopPlayer5, TopPlayer6, TopPlayer7, TopPlayer8, TopPlayer9, TopPlayer10,
                                         TopScore1, TopScore2, TopScore3, TopScore4, TopScore5, TopScore6, TopScore7, TopScore8, TopScore9, TopScore10 };

            for (int Index = 0; Index < ElementsRight.Length; Index++) StartAnimation(ElementsRight[Index], 3000);
            for (int Index = 0; Index < ElementsLeft.Length; Index++) StartAnimation(ElementsLeft[Index], -3000);
            await Task.Delay(1000);

            NavigationService.RemoveBackEntry();
            NavigationService.Navigate(new Uri("/Pages/PlayersNicksPage.xaml", UriKind.Relative));
        }
        private static void StartAnimation(UIElement Element, int Position)
        {
            TranslateTransform Transform = new();
            Element.RenderTransform = Transform;
            DoubleAnimation Xposition = new(0, Position, TimeSpan.FromSeconds(1));
            Transform.BeginAnimation(TranslateTransform.XProperty, Xposition);
        }
    }
}
