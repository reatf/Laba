using Laba.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Xml.Linq;

namespace Laba
{
    public partial class StartPage : Page
    {
        private StartPageVM View;
        public StartPage()
        {
            View = new(this);
            InitializeComponent();
            View.Leaders();
        }

        // Флаг исключения загрузки
        private static bool LoadException = false;

        public static bool Exception
        {
            get { return LoadException; }
            set { LoadException = value; }
        }

        // Метод, вызываемый при загрузке страницы StartPage.
        public void StartPageLoaded(object sender, RoutedEventArgs e)
        {
            var WindowMain = Window.GetWindow(this);
            WindowMain.KeyDown += View.StartEsc;
        }
        // Метод для отображения Топ игроков
        public void Leaderboard(int Index, string Nick, int Wins)
        {
            Label[] TopNicks = { TopPlayer1, TopPlayer2, TopPlayer3, TopPlayer4, TopPlayer5,
                                 TopPlayer6, TopPlayer7, TopPlayer8, TopPlayer9, TopPlayer10 }; 

            Label[] TopScores = { TopScore1, TopScore2, TopScore3, TopScore4, TopScore5,
                                  TopScore6, TopScore7, TopScore8, TopScore9, TopScore10 };

            TopNicks[Index].Content = Nick;
            TopScores[Index].Content = Wins;            
        }

        // Метод для выхода из игры
        private void Exit(object sender, MouseButtonEventArgs e) => Application.Current.Shutdown();

        // Метод для начала игры, анимации и закрытии стартового окна
        private async void StartGame(object sender, MouseButtonEventArgs e)
        {
            StartAnimationRight(3000);
            StartAnimationLeft(-3000);
            await Task.Delay(1000);

            View.End();
            View = default!;
            NavigationService Navigate = this.NavigationService;
            Navigate.RemoveBackEntry();
            Navigate.Navigate(new Uri("/Pages/PlayersNicksPage.xaml", UriKind.Relative));
        }

        // Метод для запуска анимации перемещения элементов вправо
        private void StartAnimationRight(int Position)
        {
            TranslateTransform Transform = new();
            StarterMenu.RenderTransform = Transform;
            DoubleAnimation Xposition = new(0, Position, TimeSpan.FromSeconds(1));
            Transform.BeginAnimation(TranslateTransform.XProperty, Xposition);
        }

        // Метод для запуска анимации перемещения элементов влево
        private void StartAnimationLeft(int Position)
        {
            TranslateTransform Transform = new();
            LeaderboardVisual.RenderTransform = Transform;
            DoubleAnimation Xposition = new(0, Position, TimeSpan.FromSeconds(1));
            Transform.BeginAnimation(TranslateTransform.XProperty, Xposition);
        }

        // Метод для отображения меню загрузки
        private void LoadSave(object sender, MouseButtonEventArgs e)
        {
            LoadMenu.Visibility = Visibility.Visible;
            View.LoadSaveBack();
        }

        // Метод для отображения сохраненных игр и их имен
        public void VisualLoadBack(int Index, string Name)
        {
            UIElement[] SaveBack = { SaveBack1, SaveBack2, SaveBack3, SaveBack4,
                                     SaveBack5, SaveBack6, SaveBack7, SaveBack8 };
            Label[] SaveText = { SaveText1, SaveText2, SaveText3, SaveText4,
                                 SaveText5, SaveText6, SaveText7, SaveText8 };

            SaveBack[Index].Visibility = Visibility.Visible;
            SaveText[Index].Content = Name;
            SaveText[Index].Visibility = Visibility.Visible;
        }

        // Метод для выхода из меню загрузки
        public void EscLoad() => LoadMenu.Visibility = Visibility.Hidden;

        // Метод для закрытия меню загрузки
        private void ExitLoad(object sender, MouseButtonEventArgs e) => LoadMenu.Visibility = Visibility.Hidden;

        // Метод для загрузки сохраненной игры
        private void LoadingSave(object sender, MouseButtonEventArgs e)
        {
            Label GetLabel = (Label)sender;
            StartPageVM.PreviousName(GetLabel.Content.ToString()!);
            View.End();
            View = default!;
            Exception = true;

            NavigationService Navigate = this.NavigationService;
            Navigate.RemoveBackEntry();
            Navigate.Navigate(new Uri("/Pages/GamePage.xaml", UriKind.Relative));
        }
    }
}