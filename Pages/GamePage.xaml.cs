using System;
using System.Collections.Generic;
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
        public GamePage()
        {
            InitializeComponent();

            // Вызов методов для поиска доступных ходов и загрузки сохраненной игры.
            FoundMoves();
            LoadedSave();
        }

        private GameLogic Logic = new(); // Создание экземпляра класса GameLogic для управления игровой логикой.

        // Метод для анимации смены состояния игровой клетки/фишки.
        private async void MainAnimation(int Column, int Row)
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

            DoubleAnimation WidthAnimation = new(Positions[Column, Row].Width, Positions[Column, Row].Width-2*Change, TimeSpan.FromSeconds(0.2));
            DoubleAnimation LeftAnimation = new(0, Change, TimeSpan.FromSeconds(0.2));
            TranslateTransform Transform = new();

            // Настройка анимаций для визуального эффекта.
            WidthAnimation.AutoReverse = true;
            LeftAnimation.AutoReverse = true;
            Positions[Column, Row].RenderTransform = Transform;

            // Запуск анимации изменения ширины и положения клетки.
            Positions[Column, Row].BeginAnimation(WidthProperty, WidthAnimation);
            Transform.BeginAnimation(TranslateTransform.XProperty, LeftAnimation);

            await Task.Delay(200); // Задержка для визуализации анимации.

            // Изменение цвета фишки на противоположное.
            if (Positions[Column, Row].Source.ToString() == WhiteChip.Source.ToString())
            {

                Positions[Column, Row].Source = BlackChip.Source;
            }
            else
            {
                Positions[Column, Row].Source = WhiteChip.Source;
            }
        }

        // Метод для визуального отображения количества фишек игроков
        public void ChangeChipsCountVisual (int WhiteTens, int WhiteUnits, int BlackTens, int BlackUnits)
        {
            int ChangeOne = 1690;
            int ChangeAnother = 1674;

            // Обновление визуального представления количества десятков белых фишек.
            switch (WhiteTens)
            {
                case 0:
                    {
                        this.WhiteTens.Visibility = Visibility.Hidden;
                        break;
                    }
                case 1:
                    {
                        this.WhiteTens.Source = new BitmapImage(new Uri("/Images/Numbers/1.png", UriKind.Relative));
                        this.WhiteTens.Visibility = Visibility.Visible;
                        break;
                    }
                case 2:
                    {
                        this.WhiteTens.Source = new BitmapImage(new Uri("/Images/Numbers/2.png", UriKind.Relative));
                        this.WhiteTens.Visibility = Visibility.Visible;
                        break;
                    }
                case 3:
                    {
                        this.WhiteTens.Source = new BitmapImage(new Uri("/Images/Numbers/3.png", UriKind.Relative));
                        this.WhiteTens.Visibility = Visibility.Visible;
                        break;
                    }
                case 4:
                    {
                        this.WhiteTens.Source = new BitmapImage(new Uri("/Images/Numbers/4.png", UriKind.Relative));
                        this.WhiteTens.Visibility = Visibility.Visible;
                        break;
                    }
                case 5:
                    {
                        this.WhiteTens.Source = new BitmapImage(new Uri("/Images/Numbers/5.png", UriKind.Relative));
                        this.WhiteTens.Visibility = Visibility.Visible;
                        break;
                    }
                case 6:
                    {
                        this.WhiteTens.Source = new BitmapImage(new Uri("/Images/Numbers/6.png", UriKind.Relative));
                        this.WhiteTens.Visibility = Visibility.Visible;
                        break;
                    }
            }

            // Обновление визуального представления единиц белых фишек.
            switch (WhiteUnits)
            {
                case 0:
                    {
                        this.WhiteUnits.Source = new BitmapImage(new Uri("/Images/Numbers/0.png", UriKind.Relative));
                        break;
                    }
                case 1:
                    {
                        this.WhiteUnits.Source = new BitmapImage(new Uri("/Images/Numbers/1.png", UriKind.Relative));
                        break;
                    }
                case 2:
                    {
                        this.WhiteUnits.Source = new BitmapImage(new Uri("/Images/Numbers/2.png", UriKind.Relative));
                        break;
                    }
                case 3:
                    {
                        this.WhiteUnits.Source = new BitmapImage(new Uri("/Images/Numbers/3.png", UriKind.Relative));
                        break;
                    }
                case 4:
                    {
                        this.WhiteUnits.Source = new BitmapImage(new Uri("/Images/Numbers/4.png", UriKind.Relative));
                        break;
                    }
                case 5:
                    {
                        this.WhiteUnits.Source = new BitmapImage(new Uri("/Images/Numbers/5.png", UriKind.Relative));
                        break;
                    }
                case 6:
                    {
                        this.WhiteUnits.Source = new BitmapImage(new Uri("/Images/Numbers/6.png", UriKind.Relative));
                        break;
                    }
                case 7:
                    {
                        this.WhiteUnits.Source = new BitmapImage(new Uri("/Images/Numbers/7.png", UriKind.Relative));
                        break;
                    }
                case 8:
                    {
                        this.WhiteUnits.Source = new BitmapImage(new Uri("/Images/Numbers/8.png", UriKind.Relative));
                        break;
                    }
                case 9:
                    {
                        this.WhiteUnits.Source = new BitmapImage(new Uri("/Images/Numbers/9.png", UriKind.Relative));
                        break;
                    }
            }

            // Обновление визуального представления количества десятков черных фишек.
            switch (BlackTens)
            {
                case 0:
                    {
                        this.BlackTens.Visibility = Visibility.Hidden;
                        break;
                    }
                case 1:
                    {
                        this.BlackTens.Source = new BitmapImage(new Uri("/Images/Numbers/1.png", UriKind.Relative));
                        this.BlackTens.Visibility = Visibility.Visible;
                        break;
                    }
                case 2:
                    {
                        this.BlackTens.Source = new BitmapImage(new Uri("/Images/Numbers/2.png", UriKind.Relative));
                        this.BlackTens.Visibility = Visibility.Visible;
                        break;
                    }
                case 3:
                    {
                        this.BlackTens.Source = new BitmapImage(new Uri("/Images/Numbers/3.png", UriKind.Relative));
                        this.BlackTens.Visibility = Visibility.Visible;
                        break;
                    }
                case 4:
                    {
                        this.BlackTens.Source = new BitmapImage(new Uri("/Images/Numbers/4.png", UriKind.Relative));
                        this.BlackTens.Visibility = Visibility.Visible;
                        break;
                    }
                case 5:
                    {
                        this.BlackTens.Source = new BitmapImage(new Uri("/Images/Numbers/5.png", UriKind.Relative));
                        this.BlackTens.Visibility = Visibility.Visible;
                        break;
                    }
                case 6:
                    { 
                        this.BlackTens.Source = new BitmapImage(new Uri("/Images/Numbers/6.png", UriKind.Relative));
                        this.BlackTens.Visibility = Visibility.Visible;
                        break;
                    }
            }

            // Обновление визуального представления единиц черных фишек.
            switch (BlackUnits)
            {
                case 0:
                    {
                        this.BlackUnits.Source = new BitmapImage(new Uri("/Images/Numbers/0.png", UriKind.Relative));
                        break;
                    }
                case 1:
                    {
                        this.BlackUnits.Source = new BitmapImage(new Uri("/Images/Numbers/1.png", UriKind.Relative));
                        break;
                    }
                case 2:
                    {
                        this.BlackUnits.Source = new BitmapImage(new Uri("/Images/Numbers/2.png", UriKind.Relative));
                        break;
                    }
                case 3:
                    {
                        this.BlackUnits.Source = new BitmapImage(new Uri("/Images/Numbers/3.png", UriKind.Relative));
                        break;
                    }
                case 4:
                    {
                        this.BlackUnits.Source = new BitmapImage(new Uri("/Images/Numbers/4.png", UriKind.Relative));
                        break;
                    }
                case 5:
                    {
                        this.BlackUnits.Source = new BitmapImage(new Uri("/Images/Numbers/5.png", UriKind.Relative));
                        break;
                    }
                case 6:
                    {
                        this.BlackUnits.Source = new BitmapImage(new Uri("/Images/Numbers/6.png", UriKind.Relative));
                        break;
                    }
                case 7:
                    {
                        this.BlackUnits.Source = new BitmapImage(new Uri("/Images/Numbers/7.png", UriKind.Relative));
                        break;
                    }
                case 8:
                    {
                        this.BlackUnits.Source = new BitmapImage(new Uri("/Images/Numbers/8.png", UriKind.Relative));
                        break;
                    }
                case 9:
                    {
                        this.BlackUnits.Source = new BitmapImage(new Uri("/Images/Numbers/9.png", UriKind.Relative));
                        break;
                    }
            }

            if(WhiteTens == 1)
            {
                Canvas.SetLeft(this.WhiteTens, ChangeOne);
            }
            else if(WhiteTens > 1)
            {
                Canvas.SetLeft(this.WhiteTens, ChangeAnother);
            }
            if (BlackTens == 1)
            {
                Canvas.SetLeft(this.BlackTens, ChangeOne);
            }
            else if (BlackTens > 1)
            {
                Canvas.SetLeft(this.BlackTens, ChangeAnother);
            }
        }

        // Метод для изменения визуального представление текущего хода игрока и фишек.
        public void ChangeMoveVisual()
        {
            if(MoveChip.Source.ToString() == BlackChip.Source.ToString())
            {
                MoveOfPlayer.Source = new BitmapImage(new Uri("/Images/Player2.png", UriKind.Relative));
                MoveChip.Source = new BitmapImage(new Uri("/Images/WhiteChip.png", UriKind.Relative));
            }
            else
            {
                MoveOfPlayer.Source = new BitmapImage(new Uri("/Images/Player1.png", UriKind.Relative));
                MoveChip.Source = new BitmapImage(new Uri("/Images/BlackChip.png", UriKind.Relative));
            }
        }

        // Метод для нахождения доступных ходов текущего игрока и отображения их на игровой доске.
        public async void FoundMoves()
        {
            Image[,] Positions = { { A1, B1, C1, D1, E1, F1, G1, H1 },
                                   { A2, B2, C2, D2, E2, F2, G2, H2 },
                                   { A3, B3, C3, D3, E3, F3, G3, H3 },
                                   { A4, B4, C4, D4, E4, F4, G4, H4 },
                                   { A5, B5, C5, D5, E5, F5, G5, H5 },
                                   { A6, B6, C6, D6, E6, F6, G6, H6 },
                                   { A7, B7, C7, D7, E7, F7, G7, H7 },
                                   { A8, B8, C8, D8, E8, F8, G8, H8 } };

            Logic.AvailableMoves(out List<Coordinates>? AvailableMoves, out bool Ending);

            if (AvailableMoves!.Count > 0)
            {
                foreach (Coordinates Coordinates in AvailableMoves)
                {
                    Positions[Coordinates.Row, Coordinates.Column].Visibility = Visibility.Visible;
                }
            }
            else if(Ending)
            {
                await Task.Delay(1000);

                End();
            }
            else
            {
                NextTurn();
            }
        }

        // Метод для боработки размещения фишек игроком на игровой доске при щелчке мыши.
        private async void ChipPlace(object sender, MouseButtonEventArgs e)
        {
            Image GetImage = (Image) sender;
            if(GetImage.Source.ToString() == HintMove.Source.ToString())
            {
            Image[,] Positions = { { A1, B1, C1, D1, E1, F1, G1, H1 },
                                   { A2, B2, C2, D2, E2, F2, G2, H2 },
                                   { A3, B3, C3, D3, E3, F3, G3, H3 },
                                   { A4, B4, C4, D4, E4, F4, G4, H4 },
                                   { A5, B5, C5, D5, E5, F5, G5, H5 },
                                   { A6, B6, C6, D6, E6, F6, G6, H6 },
                                   { A7, B7, C7, D7, E7, F7, G7, H7 },
                                   { A8, B8, C8, D8, E8, F8, G8, H8 } };

                // Проверяет возможные ходы и осуществляет переворот фишек по правилам игры.
                for (var Row = 0; Row < Positions.GetLength(0); Row++)
                {
                    for (var Column = 0; Column < Positions.GetLength(1); Column++)
                    {
                        if (Positions[Row, Column].Name == GetImage.Name)
                        {
                            List<Coordinates> Reversi = Logic.GetReversiChips(new Coordinates(Row, Column), out int Turn);
                            
                            if (Turn == 1)
                            {
                                Positions[Row, Column].Source = new BitmapImage(new Uri("/Images/WhiteChip.png", UriKind.Relative));
                                foreach (Coordinates Position in Reversi)
                                {
                                    MainAnimation(Position.Row, Position.Column);
                                }
                            }
                            else
                            {
                                Positions[Row, Column].Source = new BitmapImage(new Uri("/Images/BlackChip.png", UriKind.Relative));
                                foreach (Coordinates Position in Reversi)
                                {
                                    MainAnimation(Position.Row, Position.Column);
                                }
                            }
                        }
                    }
                }

                // Скрывает выделение доступных ходов после размещения фишек.
                for (var Row = 0; Row < Positions.GetLength(0); Row++)
                {
                    for (var Column = 0; Column < Positions.GetLength(1); Column++)
                    {
                        if (Positions[Row,Column].Source.ToString() == HintMove.Source.ToString())
                        {
                            Positions[Row, Column].Visibility = Visibility.Hidden;
                        }
                    }
                }

                await Task.Delay(400);
                NextTurn();
            }
        }

        // Метод для переключения хода, обновления счетчиков фишек и поиска доступных ходов.
        private void NextTurn()
        {
            ChangeMoveVisual();
            Logic.ChangeTurn();
            Logic.ChipsCount(out int WhiteTens, out int WhiteUnits, out int BlackTens, out int BlackUnits);
            ChangeChipsCountVisual(WhiteTens, WhiteUnits, BlackTens, BlackUnits);
            FoundMoves();
        }

        // Метод завершает игру, освобождает ресурсы и переходит на страницу окончания игры.
        public void End()
        {
            Logic = default!; // Освобождение ресурсов
            NavigationService Navigate = this.NavigationService;
            Navigate.RemoveBackEntry();
            Navigate.Navigate(new Uri("/Pages/EndPage.xaml", UriKind.Relative));
        }

        // Метод для управления меню с использованием клавиши Escape.
        private void VisualMenu(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape && Saving.Visibility == Visibility.Visible)
            {
                Saving.Visibility = Visibility.Hidden;
                SaveMenu.Visibility = Visibility.Visible;
            }
            else if(e.Key == Key.Escape && (SaveMenu.Visibility == Visibility.Visible || LoadingMenu.Visibility == Visibility.Visible))
            {
                LoadingMenu.Visibility = Visibility.Hidden;
                SaveMenu.Visibility = Visibility.Hidden;
                Menu.Visibility = Visibility.Visible;
            }
            else if (e.Key == Key.Escape && Menu.Visibility == Visibility.Hidden)
            {
                Menu.Visibility = Visibility.Visible;
            }
            else if(e.Key == Key.Escape && Menu.Visibility == Visibility.Visible)
            {
                Menu.Visibility = Visibility.Hidden;
            }
        }

        private void BackMenu(object sender, MouseButtonEventArgs e)
        {
            LoadingMenu.Visibility = Visibility.Hidden;
            SaveMenu.Visibility = Visibility.Hidden;
            Menu.Visibility = Visibility.Visible;
        }

        private void SavingBackMenu(object sender, MouseButtonEventArgs e)
        {
            Saving.Visibility = Visibility.Hidden;
            SaveMenu.Visibility = Visibility.Visible;
        }

        // Метод, вызываемый при загрузке страницы игры, устанавливает обработчик клавиш для управления меню.
        private void GamePageLoaded(object sender, RoutedEventArgs e)
        {
            var WindowMain = Window.GetWindow(this);
            WindowMain.KeyDown += VisualMenu;
        }

        // Метод для скрытия игрового меню и возврата к игре.
        private void ReturnGame(object sender, MouseButtonEventArgs e) => Menu.Visibility = Visibility.Hidden;

        // Метод для завершения приложения при выборе выхода из игры.
        private void ExitGame(object sender, MouseButtonEventArgs e) => Application.Current.Shutdown();

        // Метод для управления отображением меню сохранения игры и списка сохранений. 
        private void SaveGame(object sender, MouseButtonEventArgs e)
        {
            // Загружает список сохранений из базы данных и отображает их.
            Menu.Visibility = Visibility.Hidden;
            SaveMenu.Visibility = Visibility.Visible;

            UIElement[] SaveBack = { SaveBack1, SaveBack2, SaveBack3, SaveBack4,
                                     SaveBack5, SaveBack6, SaveBack7, SaveBack8 };
            Label[] SaveText = { SaveText1, SaveText2, SaveText3, SaveText4,
                                 SaveText5, SaveText6, SaveText7, SaveText8 };
            GameLogic.GetSavesNumber(out int SaveNumber, out List<string>? SavesName);

            if(SaveNumber != 8)
            {
                for(var i = 0; i < SaveNumber+1; i++)
                {
                    SaveBack[i].Visibility = Visibility.Visible;
                    SaveText[i].Content = "Сохранить игру";
                    SaveText[i].Visibility= Visibility.Visible;
                }
                for(var i = 0; i < SaveNumber; i++)
                {
                    SaveText[i].Content = SavesName![i];
                }
            }
            else
            {
                for (var i = 0; i < SaveNumber; i++)
                {
                    SaveBack[i].Visibility = Visibility.Visible;
                    SaveText[i].Content = SavesName![i];
                    SaveText[i].Visibility = Visibility.Visible;
                }
            }
        }

        // Метод, вызываемый при выборе сохранения игры. Подготавливает имя сохранения для загрузки.
        private void SavingGame(object sender, MouseButtonEventArgs e)
        {
            Label[] SaveText = { SaveText1, SaveText2, SaveText3, SaveText4,
                                 SaveText5, SaveText6, SaveText7, SaveText8 };
            Label GetLabel = (Label)sender;
            SavingText.Text = "";
            Saving.Visibility = Visibility.Visible;
            SaveMenu.Visibility = Visibility.Hidden;
            for (var i = 0; i < SaveText.Length; i++)
            {
                if (SaveText[i].Name == GetLabel.Name)
                {
                    Save.PreviousSavingName = SaveText[i].Content.ToString()!;
                }
            }
        }

        // Метод для получения информации о текущем состоянии игры (Cид).
        private void GetSeed() => Logic.GetInformation();

        // Метод, вызываемый при подтверждении сохранения игры.
        private void SavingEnter(object sender, MouseButtonEventArgs e)
        {
            GetSeed();
            Save.SavingName = SavingText.Text;
            GameLogic.CheckSaveName(out bool Except);
            if(Except)
            {
                Label[] SaveText = { SaveText1, SaveText2, SaveText3, SaveText4,
                                 SaveText5, SaveText6, SaveText7, SaveText8 };

                for (var i = 0; i < SaveText.Length;i++)
                {
                    if (SaveText[i].Content.ToString() == Save.PreviousSavingName)
                    {
                        if (SaveText[i].Content.ToString() != "Сохранить игру")
                        {
                            GameLogic.OverwriteSave();
                        }
                        else
                        {
                            GameLogic.WriteSave();
                        }
                    }
                }

                Saving.Visibility = Visibility.Hidden;
                Menu.Visibility = Visibility.Visible;
            }
            else
            {
                SavingText.Text = "";
            }
        }

        // Метод для управления отображением меню загрузки игры и списка сохранений. 
        private void LoadSave(object sender, MouseButtonEventArgs e)
        {
            Menu.Visibility = Visibility.Hidden;
            LoadingMenu.Visibility = Visibility.Visible;

            UIElement[] SaveBack = { LoadBack1, LoadBack2, LoadBack3, LoadBack4,
                                     LoadBack5, LoadBack6, LoadBack7, LoadBack8 };
            Label[] LoadText = { LoadText1, LoadText2, LoadText3, LoadText4,
                                 LoadText5, LoadText6, LoadText7, LoadText8 };
            GameLogic.GetSavesNumber(out int SaveNumber, out List<string>? SavesName);

            for (var i = 0; i < SaveNumber; i++)
            {
                SaveBack[i].Visibility = Visibility.Visible;
                LoadText[i].Content = SavesName![i];
                LoadText[i].Visibility = Visibility.Visible;
            }
        }

        // Метод для загрузки выбранного сохранения.
        // Загружает сохранение, обновляет игровое состояние и отображение.
        private void LoadingSave(object sender, MouseButtonEventArgs e)
        {
            Label GetLabel = (Label)sender;
            Save.PreviousSavingName = GetLabel.Content.ToString()!;

            Label[] LoadText = { LoadText1, LoadText2, LoadText3, LoadText4,
                                 LoadText5, LoadText6, LoadText7, LoadText8 };

            for (var i = 0; i < LoadText.Length; i++)
            {
                if (LoadText[i].Content.ToString() == Save.PreviousSavingName)
                {

                    NullBoard();
                    Logic.LoadSave();
                    Logic.ChipsCount(out int WhiteTens, out int WhiteUnits, out int BlackTens, out int BlackUnits);
                    ChangeChipsCountVisual(WhiteTens, WhiteUnits, BlackTens, BlackUnits);

                    CurreentMove();
                    NullHint();
                    FoundMoves();
                    CurrentChips();
                }
            }
            LoadingMenu.Visibility = Visibility.Hidden;
        }

        // Метод для отображения текущего хода игрока
        private void CurreentMove()
        {
            if (Logic.NowMove())
            {
                MoveOfPlayer.Source = new BitmapImage(new Uri("/Images/Player2.png", UriKind.Relative));
                MoveChip.Source = new BitmapImage(new Uri("/Images/WhiteChip.png", UriKind.Relative));
            }
            else
            {
                MoveOfPlayer.Source = new BitmapImage(new Uri("/Images/Player1.png", UriKind.Relative));
                MoveChip.Source = new BitmapImage(new Uri("/Images/BlackChip.png", UriKind.Relative));
            }
        }

        // Метод для отображения текущего расположения фишек на доске.
        private void CurrentChips()
        {
            Image[,] Positions = { { A1, B1, C1, D1, E1, F1, G1, H1 },
                                   { A2, B2, C2, D2, E2, F2, G2, H2 },
                                   { A3, B3, C3, D3, E3, F3, G3, H3 },
                                   { A4, B4, C4, D4, E4, F4, G4, H4 },
                                   { A5, B5, C5, D5, E5, F5, G5, H5 },
                                   { A6, B6, C6, D6, E6, F6, G6, H6 },
                                   { A7, B7, C7, D7, E7, F7, G7, H7 },
                                   { A8, B8, C8, D8, E8, F8, G8, H8 } };

            Logic.CurrentChip(out List<Coordinates> Chips);

            for(var Index = 0; Index < Chips.Count; Index++)
            {
                if (Chips[Index].Color == 1)
                {
                    Positions[Chips[Index].Row, Chips[Index].Column].Visibility = Visibility.Visible;
                    Positions[Chips[Index].Row, Chips[Index].Column].Source = new BitmapImage(new Uri("/Images/WhiteChip.png", UriKind.Relative));
                }
                else
                {
                    Positions[Chips[Index].Row, Chips[Index].Column].Visibility = Visibility.Visible;
                    Positions[Chips[Index].Row, Chips[Index].Column].Source = new BitmapImage(new Uri("/Images/BlackChip.png", UriKind.Relative));
                }
            }
        }

        // Метод для скрытия всех подсказок на доске.
        private void NullHint()
        {
            Image[,] Positions = { { A1, B1, C1, D1, E1, F1, G1, H1 },
                                   { A2, B2, C2, D2, E2, F2, G2, H2 },
                                   { A3, B3, C3, D3, E3, F3, G3, H3 },
                                   { A4, B4, C4, D4, E4, F4, G4, H4 },
                                   { A5, B5, C5, D5, E5, F5, G5, H5 },
                                   { A6, B6, C6, D6, E6, F6, G6, H6 },
                                   { A7, B7, C7, D7, E7, F7, G7, H7 },
                                   { A8, B8, C8, D8, E8, F8, G8, H8 } };

            for (var Row = 0; Row < Positions.GetLength(0); Row++)
            {
                for(var Column = 0; Column < Positions.GetLength(1); Column++)
                {
                    if (Positions[Row, Column].Source.ToString() == HintMove.Source.ToString())
                    {
                        Positions[Row, Column].Visibility = Visibility.Hidden;
                    }
                }
            }
        }

        // Метод для загрузки сохранения при запуске игры. 
        private void LoadedSave()
        {
            if(StartPage.Exception == true)
            {
                Logic.LoadSave();
                Logic.ChipsCount(out int WhiteTens, out int WhiteUnits, out int BlackTens, out int BlackUnits);
                ChangeChipsCountVisual(WhiteTens, WhiteUnits, BlackTens, BlackUnits);

                CurreentMove();
                NullHint();
                FoundMoves();
                CurrentChips();
                StartPage.Exception = false;
            }
        }

        // Метод для сброса доски.
        private void NullBoard()
        {
            Image[,] Positions = { { A1, B1, C1, D1, E1, F1, G1, H1 },
                                   { A2, B2, C2, D2, E2, F2, G2, H2 },
                                   { A3, B3, C3, D3, E3, F3, G3, H3 },
                                   { A4, B4, C4, D4, E4, F4, G4, H4 },
                                   { A5, B5, C5, D5, E5, F5, G5, H5 },
                                   { A6, B6, C6, D6, E6, F6, G6, H6 },
                                   { A7, B7, C7, D7, E7, F7, G7, H7 },
                                   { A8, B8, C8, D8, E8, F8, G8, H8 } };

            for (var Row = 0; Row < Positions.GetLength(0); Row++)
            {
                for (var Column = 0; Column < Positions.GetLength(1); Column++)
                {
                    Positions[Row, Column].Source = HintMove.Source;
                    Positions[Row, Column].Visibility = Visibility.Hidden;
                }
            }
        }
    }
}
