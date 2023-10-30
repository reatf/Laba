using Laba.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace Laba
{
    public partial class GamePage : Page
    {
        private GamePageVM View;

        public GamePage()
        {
            View = new(this);
            InitializeComponent();

            // Вызов методов для поиска доступных ходов и загрузки сохраненной игры.
            View.FoundMove();
            View.LoadedSave();
        }

        // Метод для анимации смены состояния игровой клетки/фишки.
        public async void MainAnimation(int Row, int Column)
        {
            const int Change = 42;
            Image[,] Positions = { { A1, B1, C1, D1, E1, F1, G1, H1 },
                                   { A2, B2, C2, D2, E2, F2, G2, H2 },
                                   { A3, B3, C3, D3, E3, F3, G3, H3 },
                                   { A4, B4, C4, D4, E4, F4, G4, H4 },
                                   { A5, B5, C5, D5, E5, F5, G5, H5 },
                                   { A6, B6, C6, D6, E6, F6, G6, H6 },
                                   { A7, B7, C7, D7, E7, F7, G7, H7 },
                                   { A8, B8, C8, D8, E8, F8, G8, H8 } };

            DoubleAnimation WidthAnimation = new(Positions[Row, Column].Width, Positions[Row, Column].Width-2*Change, TimeSpan.FromSeconds(0.2));
            DoubleAnimation LeftAnimation = new(0, Change, TimeSpan.FromSeconds(0.2));
            TranslateTransform Transform = new();

            // Настройка анимаций для визуального эффекта.
            WidthAnimation.AutoReverse = true;
            LeftAnimation.AutoReverse = true;
            Positions[Row, Column].RenderTransform = Transform;

            // Запуск анимации изменения ширины и положения клетки.
            Positions[Row, Column].BeginAnimation(WidthProperty, WidthAnimation);
            Transform.BeginAnimation(TranslateTransform.XProperty, LeftAnimation);

            await Task.Delay(200); // Задержка для визуализации анимации.

            View.ReversiColor(Row, Column);
        }

        // Метод визуализации реверси (Из белых в чёрные).
        public void ReversiBlack(int Row, int Column)
        {
            Image[,] Positions = { { A1, B1, C1, D1, E1, F1, G1, H1 },
                                   { A2, B2, C2, D2, E2, F2, G2, H2 },
                                   { A3, B3, C3, D3, E3, F3, G3, H3 },
                                   { A4, B4, C4, D4, E4, F4, G4, H4 },
                                   { A5, B5, C5, D5, E5, F5, G5, H5 },
                                   { A6, B6, C6, D6, E6, F6, G6, H6 },
                                   { A7, B7, C7, D7, E7, F7, G7, H7 },
                                   { A8, B8, C8, D8, E8, F8, G8, H8 } };

            Positions[Row, Column].Source = BlackChip.Source;
        }

        // Метод визуализации реверси (Из чёрных в белые).
        public void ReversiWhite(int Row, int Column)
        {
            Image[,] Positions = { { A1, B1, C1, D1, E1, F1, G1, H1 },
                                   { A2, B2, C2, D2, E2, F2, G2, H2 },
                                   { A3, B3, C3, D3, E3, F3, G3, H3 },
                                   { A4, B4, C4, D4, E4, F4, G4, H4 },
                                   { A5, B5, C5, D5, E5, F5, G5, H5 },
                                   { A6, B6, C6, D6, E6, F6, G6, H6 },
                                   { A7, B7, C7, D7, E7, F7, G7, H7 },
                                   { A8, B8, C8, D8, E8, F8, G8, H8 } };

            Positions[Row, Column].Source = WhiteChip.Source;
        }

        #region ChangeNumber
        // Метод сокрытия белых десятков.
        public void WhiteTensNull() => this.WhiteTens.Visibility = Visibility.Hidden;

        // Метод сокрытия чёрных десятков.
        public void BlackTensNull() => this.BlackTens.Visibility = Visibility.Hidden;

        // Метод для установки белых десятков.
        public void ChangeWhiteTens(string Path)
        {
            this.WhiteTens.Source = new BitmapImage(new Uri($"{Path}", UriKind.Relative));
            this.WhiteTens.Visibility = Visibility.Visible;
        }

        // Метод установки чёрных десятков
        public void ChangeBlackTens(string Path)
        {
            this.BlackTens.Source = new BitmapImage(new Uri($"{Path}", UriKind.Relative));
            this.BlackTens.Visibility = Visibility.Visible;
        }

        // Метод установки белых единиц.
        public void ChangeWhiteUnits(string Path) => this.WhiteUnits.Source = new BitmapImage(new Uri($"{Path}", UriKind.Relative));

        // Метод установки чёрных единиц.
        public void ChangeBlackUnits(string Path) => this.BlackUnits.Source = new BitmapImage(new Uri($"{Path}", UriKind.Relative));

        // Метод изменения положения белых десятков при условии единицы в десятках.
        public void ChangeOneWhiteTens(int ChangeOne) => Canvas.SetLeft(this.WhiteTens, ChangeOne);

        // Метод изменения положения чёрных десятков при условии единицы в десятках.
        public void ChangeOneBlackTens(int ChangeOne) => Canvas.SetLeft(this.BlackTens, ChangeOne);

        // Метод изменения положения белых десятков.
        public void ChangeAnotherWhiteTens(int ChangeAnother) => Canvas.SetLeft(this.WhiteTens, ChangeAnother);

        // Метод изменения положения чёрных десятков.
        public void ChangeAnotherBlackTens(int ChangeAnother) => Canvas.SetLeft(this.BlackTens, ChangeAnother);
        #endregion

        // Метод визуального изменения хода (На белых).
        public void ChangeMoveToWhite()
        {
            MoveOfPlayer.Source = new BitmapImage(new Uri("/Images/Player2.png", UriKind.Relative));
            MoveChip.Source = new BitmapImage(new Uri("/Images/WhiteChip.png", UriKind.Relative));
        }

        // Метод визуального изменения хода (На чёрных).
        public void ChangeMoveToBlack()
        {
            MoveOfPlayer.Source = new BitmapImage(new Uri("/Images/Player1.png", UriKind.Relative));
            MoveChip.Source = new BitmapImage(new Uri("/Images/BlackChip.png", UriKind.Relative));
        }

        // Метод отображения доступных ходов.
        public void ShowAvailableMoves(Coordinates AvailableMove)
        {
            Image[,] Positions = { { A1, B1, C1, D1, E1, F1, G1, H1 },
                                   { A2, B2, C2, D2, E2, F2, G2, H2 },
                                   { A3, B3, C3, D3, E3, F3, G3, H3 },
                                   { A4, B4, C4, D4, E4, F4, G4, H4 },
                                   { A5, B5, C5, D5, E5, F5, G5, H5 },
                                   { A6, B6, C6, D6, E6, F6, G6, H6 },
                                   { A7, B7, C7, D7, E7, F7, G7, H7 },
                                   { A8, B8, C8, D8, E8, F8, G8, H8 } };

            Positions[AvailableMove.Row, AvailableMove.Column].Visibility = Visibility.Visible;
        }

        // Метод визуального размещения фишки.
        private void ChipPlace(object sender, MouseButtonEventArgs e)
        {
            Image GetImage = (Image) sender;

            View.ReferenceChipPlace(GetImage.Source.ToString(), GetImage.Name.ToString());
        }

        // Метод получения имени клетки.
        public string GetName(int Row, int Column)
        {
            Image[,] Positions = { { A1, B1, C1, D1, E1, F1, G1, H1 },
                                   { A2, B2, C2, D2, E2, F2, G2, H2 },
                                   { A3, B3, C3, D3, E3, F3, G3, H3 },
                                   { A4, B4, C4, D4, E4, F4, G4, H4 },
                                   { A5, B5, C5, D5, E5, F5, G5, H5 },
                                   { A6, B6, C6, D6, E6, F6, G6, H6 },
                                   { A7, B7, C7, D7, E7, F7, G7, H7 },
                                   { A8, B8, C8, D8, E8, F8, G8, H8 } };

            return Positions[Row, Column].Name.ToString();
        }

        // Метод для получения ресурса клетки.
        public string GetSource(int Row, int Column)
        {
            Image[,] Positions = { { A1, B1, C1, D1, E1, F1, G1, H1 },
                                   { A2, B2, C2, D2, E2, F2, G2, H2 },
                                   { A3, B3, C3, D3, E3, F3, G3, H3 },
                                   { A4, B4, C4, D4, E4, F4, G4, H4 },
                                   { A5, B5, C5, D5, E5, F5, G5, H5 },
                                   { A6, B6, C6, D6, E6, F6, G6, H6 },
                                   { A7, B7, C7, D7, E7, F7, G7, H7 },
                                   { A8, B8, C8, D8, E8, F8, G8, H8 } };

            return Positions[Row, Column].Source.ToString();
        }

        // Метод для размещения белой фишки.
        public void PlaceWhiteChip(int Row, int Column)
        {
            Image[,] Positions = { { A1, B1, C1, D1, E1, F1, G1, H1 },
                                   { A2, B2, C2, D2, E2, F2, G2, H2 },
                                   { A3, B3, C3, D3, E3, F3, G3, H3 },
                                   { A4, B4, C4, D4, E4, F4, G4, H4 },
                                   { A5, B5, C5, D5, E5, F5, G5, H5 },
                                   { A6, B6, C6, D6, E6, F6, G6, H6 },
                                   { A7, B7, C7, D7, E7, F7, G7, H7 },
                                   { A8, B8, C8, D8, E8, F8, G8, H8 } };

            Positions[Row, Column].Source = new BitmapImage(new Uri("/Images/WhiteChip.png", UriKind.Relative));
        }

        // Метод для размещения черной фишки на указанной позиции.
        public void PlaceBlackChip(int Row, int Column)
        {
            Image[,] Positions = { { A1, B1, C1, D1, E1, F1, G1, H1 },
                                   { A2, B2, C2, D2, E2, F2, G2, H2 },
                                   { A3, B3, C3, D3, E3, F3, G3, H3 },
                                   { A4, B4, C4, D4, E4, F4, G4, H4 },
                                   { A5, B5, C5, D5, E5, F5, G5, H5 },
                                   { A6, B6, C6, D6, E6, F6, G6, H6 },
                                   { A7, B7, C7, D7, E7, F7, G7, H7 },
                                   { A8, B8, C8, D8, E8, F8, G8, H8 } };

            Positions[Row, Column].Source = new BitmapImage(new Uri("/Images/BlackChip.png", UriKind.Relative));
        }

        // Метод скрытия подсказки хода.
        public void HideHint(int Row, int Column)
        {
            Image[,] Positions = { { A1, B1, C1, D1, E1, F1, G1, H1 },
                                   { A2, B2, C2, D2, E2, F2, G2, H2 },
                                   { A3, B3, C3, D3, E3, F3, G3, H3 },
                                   { A4, B4, C4, D4, E4, F4, G4, H4 },
                                   { A5, B5, C5, D5, E5, F5, G5, H5 },
                                   { A6, B6, C6, D6, E6, F6, G6, H6 },
                                   { A7, B7, C7, D7, E7, F7, G7, H7 },
                                   { A8, B8, C8, D8, E8, F8, G8, H8 } };

            Positions[Row, Column].Visibility = Visibility.Hidden;
        }

        // Метод завершения игры.
        public void End()
        {
            View.ReferenceEnd();
            View = default!; // Освобождение ресурсов

            NavigationService Navigate = this.NavigationService;
            Navigate.RemoveBackEntry();
            Navigate.Navigate(new Uri("/Pages/EndPage.xaml", UriKind.Relative));
        }

        #region Save

        // Метод отображения меню сохранений.
        public void SavingEsc()
        {
            Saving.Visibility = Visibility.Hidden;
            SaveMenu.Visibility = Visibility.Visible;
        }

        // Метод возврата в меню.
        public void ReturnToMenu()
        {
            LoadingMenu.Visibility = Visibility.Hidden;
            SaveMenu.Visibility = Visibility.Hidden;
            Menu.Visibility = Visibility.Visible;
        }

        // Метод возвравта в меню после сохраненя игры.
        public void ReturnToMenuFromSaving()
        {
            Saving.Visibility = Visibility.Hidden;
            Menu.Visibility = Visibility.Visible;
        }

        // Метод открытия игрового меню.
        public void OpenMenu() => Menu.Visibility = Visibility.Visible;

        // Метод закрытия игрового меню.
        public void CloseMenu() => Menu.Visibility = Visibility.Hidden;

        // Метод возвращения в меню игры.
        private void BackMenu(object sender, MouseButtonEventArgs e) => ReturnToMenu();

        // Метод выхода из меню сохранения игры.
        private void SavingBackMenu(object sender, MouseButtonEventArgs e) => SavingEsc();

        // Метод установки выхода из меню по нажатию esc.
        private void GamePageLoaded(object sender, RoutedEventArgs e)
        {
            var WindowMain = Window.GetWindow(this);
            WindowMain.KeyDown += View.VisualMenu;
        }

        // Метод возврата к игре.
        private void ReturnGame(object sender, MouseButtonEventArgs e) => Menu.Visibility = Visibility.Hidden;

        // Метод выхода из игры.
        private void ExitGame(object sender, MouseButtonEventArgs e) => Application.Current.Shutdown();

        // Метод отображения списка сохранений. 
        private void SaveGame(object sender, MouseButtonEventArgs e)
        {
            // Загружает меню сохранений.
            Menu.Visibility = Visibility.Hidden;
            SaveMenu.Visibility = Visibility.Visible;

            View.ReferenceSaveMenu();
        }

        // Метод отображения ячеек сохрнения.
        public void PlaceSavesBack(int Index)
        {
            UIElement[] SaveBack = { SaveBack1, SaveBack2, SaveBack3, SaveBack4,
                                     SaveBack5, SaveBack6, SaveBack7, SaveBack8 };

            Label[] SaveText = { SaveText1, SaveText2, SaveText3, SaveText4,
                                 SaveText5, SaveText6, SaveText7, SaveText8 };

            SaveBack[Index].Visibility = Visibility.Visible;
            SaveText[Index].Content = "Сохранить игру";
            SaveText[Index].Visibility = Visibility.Visible;
        }

        // Метод отображения названия сохранения.
        public void PlaceSaves(int Index, string SaveName)
        {
            UIElement[] SaveBack = { SaveBack1, SaveBack2, SaveBack3, SaveBack4,
                                     SaveBack5, SaveBack6, SaveBack7, SaveBack8 };

            Label[] SaveText = { SaveText1, SaveText2, SaveText3, SaveText4,
                                 SaveText5, SaveText6, SaveText7, SaveText8 };

            SaveBack[Index].Visibility = Visibility.Visible;
            SaveText[Index].Content = SaveName;
            SaveText[Index].Visibility = Visibility.Visible;
        }

        // Метод визуализации ввода названия сохранения.
        private void SavingGame(object sender, MouseButtonEventArgs e)
        {
            Label GetLabel = (Label)sender;
            SavingText.Text = "";
            Saving.Visibility = Visibility.Visible;
            SaveMenu.Visibility = Visibility.Hidden;

            GamePageVM.SavingPreviousName(GetLabel.Content.ToString()!);
        }

        // Метод передачи имени нового сохранения.
        private void SavingEnter(object sender, MouseButtonEventArgs e)
        {
            string[] Content = { SaveText1.Content.ToString()!, SaveText2.Content.ToString()!, SaveText3.Content.ToString()!, SaveText4.Content.ToString()!,
                                 SaveText5.Content.ToString()!, SaveText6.Content.ToString()!, SaveText7.Content.ToString()!, SaveText8.Content.ToString()! };

            View.GetSeed();
            View.ReferenceSavingEnter(SavingText.Text, Content);

        }

        // Метод сброса текста в окне сохранения.
        public void NullSaving()
        {
            SavingText.Text = "";
        }

        // Метод отображения меню загрузки сохранений. 
        private void LoadSave(object sender, MouseButtonEventArgs e)
        {
            Menu.Visibility = Visibility.Hidden;
            LoadingMenu.Visibility = Visibility.Visible;

            View.ReferenceLoadSave();
        }

        // Метод отображения имени сохранения.
        public void PlaceLoads(int Index, string Name)
        {
            UIElement[] SaveBack = { LoadBack1, LoadBack2, LoadBack3, LoadBack4,
                                     LoadBack5, LoadBack6, LoadBack7, LoadBack8 };

            Label[] LoadText = { LoadText1, LoadText2, LoadText3, LoadText4,
                                 LoadText5, LoadText6, LoadText7, LoadText8 };

            SaveBack[Index].Visibility = Visibility.Visible;
            LoadText[Index].Content = Name;
            LoadText[Index].Visibility = Visibility.Visible;
        }

        // Метод для загрузки выбранного сохранения.
        private void LoadingSave(object sender, MouseButtonEventArgs e)
        {
            Label GetLabel = (Label)sender;
            View.ReferenceLoadingSave(GetLabel.Content.ToString()!);

            LoadingMenu.Visibility = Visibility.Hidden;
        }

        // Метод отображения текущей белой фишки на доске.
        public void PlaceCurrentWhite(int Row, int Column)
        {
            Image[,] Positions = { { A1, B1, C1, D1, E1, F1, G1, H1 },
                                   { A2, B2, C2, D2, E2, F2, G2, H2 },
                                   { A3, B3, C3, D3, E3, F3, G3, H3 },
                                   { A4, B4, C4, D4, E4, F4, G4, H4 },
                                   { A5, B5, C5, D5, E5, F5, G5, H5 },
                                   { A6, B6, C6, D6, E6, F6, G6, H6 },
                                   { A7, B7, C7, D7, E7, F7, G7, H7 },
                                   { A8, B8, C8, D8, E8, F8, G8, H8 } };

            Positions[Row, Column].Visibility = Visibility.Visible;
            Positions[Row, Column].Source = new BitmapImage(new Uri("/Images/WhiteChip.png", UriKind.Relative));
        }

        // Метод отображения текущей черной фишки на доске.
        public void PlaceCurrentBlack(int Row, int Column)
        {
            Image[,] Positions = { { A1, B1, C1, D1, E1, F1, G1, H1 },
                                   { A2, B2, C2, D2, E2, F2, G2, H2 },
                                   { A3, B3, C3, D3, E3, F3, G3, H3 },
                                   { A4, B4, C4, D4, E4, F4, G4, H4 },
                                   { A5, B5, C5, D5, E5, F5, G5, H5 },
                                   { A6, B6, C6, D6, E6, F6, G6, H6 },
                                   { A7, B7, C7, D7, E7, F7, G7, H7 },
                                   { A8, B8, C8, D8, E8, F8, G8, H8 } };

            Positions[Row, Column].Visibility = Visibility.Visible;
            Positions[Row, Column].Source = new BitmapImage(new Uri("/Images/BlackChip.png", UriKind.Relative));
        }

        // Метод сброса доски.
        public void NullingBoard(int Row, int Column)
        {
            Image[,] Positions = { { A1, B1, C1, D1, E1, F1, G1, H1 },
                                   { A2, B2, C2, D2, E2, F2, G2, H2 },
                                   { A3, B3, C3, D3, E3, F3, G3, H3 },
                                   { A4, B4, C4, D4, E4, F4, G4, H4 },
                                   { A5, B5, C5, D5, E5, F5, G5, H5 },
                                   { A6, B6, C6, D6, E6, F6, G6, H6 },
                                   { A7, B7, C7, D7, E7, F7, G7, H7 },
                                   { A8, B8, C8, D8, E8, F8, G8, H8 } };

            Positions[Row, Column].Source = HintMove.Source;
            Positions[Row, Column].Visibility = Visibility.Hidden;
        }
        #endregion
    }
}
