using System;
using System.Collections.Generic;
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
        public StartPage() => InitializeComponent();
        private static bool LoadException = false;
        public static bool Exception
        {
            get { return LoadException; }
            set { LoadException = value; }
        }

        // Метод для отображения Топ игроков
        private void Leaderboard(object sender, RoutedEventArgs e)
        {
            Label[] TopNicks = { TopPlayer1, TopPlayer2, TopPlayer3, TopPlayer4, TopPlayer5,
                                 TopPlayer6, TopPlayer7, TopPlayer8, TopPlayer9, TopPlayer10 }; 

            Label[] TopScores = { TopScore1, TopScore2, TopScore3, TopScore4, TopScore5,
                                  TopScore6, TopScore7, TopScore8, TopScore9, TopScore10 };

            for (var Index = 0; Index < TopNicks.Length; Index++)
            {
                Save.LeaderIndex = Index;
                GameLogic.StartLeaderboard(out string? Nick, out int Wins);
                if (Wins == 0)
                {
                    break;
                }
                else
                {
                    TopNicks[Index].Content = Nick;
                    TopScores[Index].Content = Wins;
                }
            }

            var WindowMain = Window.GetWindow(this);
            WindowMain.KeyDown += EscLoad;
        }

        // Метод для выхода из игры
        private void Exit(object sender, MouseButtonEventArgs e) => Application.Current.Shutdown();

        // Метод для начала игры, анимации и закрытии стартового окна
        private async void StartGame(object sender, MouseButtonEventArgs e)
        {
            UIElement[] ElementsRight = { StartButton, LoadSaveButton, ExitButton };
            UIElement[] ElementsLeft = { TopPlayers,
                                         TopPlayer1, TopPlayer2, TopPlayer3, TopPlayer4, TopPlayer5, TopPlayer6, TopPlayer7, TopPlayer8, TopPlayer9, TopPlayer10,
                                         TopScore1, TopScore2, TopScore3, TopScore4, TopScore5, TopScore6, TopScore7, TopScore8, TopScore9, TopScore10 };

            for (var Index = 0; Index < ElementsRight.Length; Index++) StartAnimation(ElementsRight[Index], 3000);
            for (var Index = 0; Index < ElementsLeft.Length; Index++) StartAnimation(ElementsLeft[Index], -3000);
            await Task.Delay(1000);

            NavigationService Navigate = this.NavigationService;
            Navigate.RemoveBackEntry();
            Navigate.Navigate(new Uri("/Pages/PlayersNicksPage.xaml", UriKind.Relative));
        }

        // Метод для запуска анимации перемещения элементов
        private static void StartAnimation(UIElement Element, int Position)
        {
            TranslateTransform Transform = new();
            Element.RenderTransform = Transform;
            DoubleAnimation Xposition = new(0, Position, TimeSpan.FromSeconds(1));
            Transform.BeginAnimation(TranslateTransform.XProperty, Xposition);
        }

        // Метод для отображения меню загрузки
        private void LoadSave(object sender, MouseButtonEventArgs e)
        {
            LoadMenu.Visibility = Visibility.Visible;
            UIElement[] SaveBack = { SaveBack1, SaveBack2, SaveBack3, SaveBack4,
                                     SaveBack5, SaveBack6, SaveBack7, SaveBack8 };
            Label[] SaveText = { SaveText1, SaveText2, SaveText3, SaveText4,
                                 SaveText5, SaveText6, SaveText7, SaveText8 };
            GameLogic.GetSavesNumber(out int SaveNumber, out List<string>? SavesName);
            for(var Index = 0; Index < SaveNumber; Index++)
            {
                SaveBack[Index].Visibility = Visibility.Visible;
                SaveText[Index].Content = SavesName![Index];
                SaveText[Index].Visibility = Visibility.Visible;
            }
        }

        // Метод для выхода из меню загрузки
        private void EscLoad(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape && LoadMenu.Visibility == Visibility.Visible)
            {
                LoadMenu.Visibility = Visibility.Hidden;
            }
        }
        private void ExitLoad(object sender, MouseButtonEventArgs e)
        {
            LoadMenu.Visibility = Visibility.Hidden;
        }
        // Метод для загрузки сохраненной игры
        private void LoadingSave(object sender, MouseButtonEventArgs e)
        {
            Label GetLabel = (Label)sender;
            Save.PreviousSavingName = GetLabel.Content.ToString()!;
            Exception = true;
            NavigationService Navigate = this.NavigationService;
            Navigate.RemoveBackEntry();
            Navigate.Navigate(new Uri("/Pages/GamePage.xaml", UriKind.Relative));
        }
    }
}