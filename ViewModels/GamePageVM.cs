using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Laba.ViewModels
{
    internal class GamePageVM
    {
        private GameLogic Logic;
        private GamePage Visual;

        public GamePageVM(GamePage Page)
        {
            Visual = Page;
            Logic = new();
        }

        const int Boarder = 8;
        public bool OnGamePage = true;

        // Метод изменения цвета инверсии фишек.
        public void ReversiColor(int Row, int Column)
        {
            if (Logic.Turn == (int)GameLogic.Positions.White)
            {
                Visual.ReversiWhite(Row, Column);
            }
            else
            {
                Visual.ReversiBlack(Row, Column);
            }
        }

        // Метод для обновления представления количества фишек.
        public void NewChipCounters(int WhiteTens, int WhiteUnits, int BlackTens, int BlackUnits)
        {
            const int ChangeOne = 1690;
            const int ChangeAnother = 1674;
            string[] Path = { "/Images/Numbers/0.png", "/Images/Numbers/1.png", "/Images/Numbers/2.png", "/Images/Numbers/3.png", "/Images/Numbers/4.png",
                              "/Images/Numbers/5.png", "/Images/Numbers/6.png", "/Images/Numbers/7.png", "/Images/Numbers/8.png", "/Images/Numbers/9.png" };

            // Обновление визуального представления количества десятков белых фишек.
            switch (WhiteTens)
            {
                case 0:
                    {
                        Visual.WhiteTensNull();
                        break;
                    }
                case 1:
                    {
                        Visual.ChangeWhiteTens(Path[1]);
                        break;
                    }
                case 2:
                    {
                        Visual.ChangeWhiteTens(Path[2]);
                        break;
                    }
                case 3:
                    {
                        Visual.ChangeWhiteTens(Path[3]);
                        break;
                    }
                case 4:
                    {
                        Visual.ChangeWhiteTens(Path[4]);
                        break;
                    }
                case 5:
                    {
                        Visual.ChangeWhiteTens(Path[5]);
                        break;
                    }
                case 6:
                    {
                        Visual.ChangeWhiteTens(Path[6]);
                        break;
                    }
            }

            // Обновление визуального представления единиц белых фишек.
            switch (WhiteUnits)
            {
                case 0:
                    {
                        Visual.ChangeWhiteUnits(Path[0]);
                        break;
                    }
                case 1:
                    {
                        Visual.ChangeWhiteUnits(Path[1]);
                        break;
                    }
                case 2:
                    {
                        Visual.ChangeWhiteUnits(Path[2]);
                        break;
                    }
                case 3:
                    {
                        Visual.ChangeWhiteUnits(Path[3]);
                        break;
                    }
                case 4:
                    {
                        Visual.ChangeWhiteUnits(Path[4]);
                        break;
                    }
                case 5:
                    {
                        Visual.ChangeWhiteUnits(Path[5]);
                        break;
                    }
                case 6:
                    {
                        Visual.ChangeWhiteUnits(Path[6]);
                        break;
                    }
                case 7:
                    {
                        Visual.ChangeWhiteUnits(Path[7]);
                        break;
                    }
                case 8:
                    {
                        Visual.ChangeWhiteUnits(Path[8]);
                        break;
                    }
                case 9:
                    {
                        Visual.ChangeWhiteUnits(Path[9]);
                        break;
                    }
            }

            // Обновление визуального представления количества десятков черных фишек.
            switch (BlackTens)
            {
                case 0:
                    {
                        Visual.BlackTensNull();
                        break;
                    }
                case 1:
                    {
                        Visual.ChangeBlackTens(Path[1]);
                        break;
                    }
                case 2:
                    {
                        Visual.ChangeBlackTens(Path[2]);
                        break;
                    }
                case 3:
                    {
                        Visual.ChangeBlackTens(Path[3]);
                        break;
                    }
                case 4:
                    {
                        Visual.ChangeBlackTens(Path[4]);
                        break;
                    }
                case 5:
                    {
                        Visual.ChangeBlackTens(Path[5]);
                        break;
                    }
                case 6:
                    {
                        Visual.ChangeBlackTens(Path[6]);
                        break;
                    }
            }

            // Обновление визуального представления единиц черных фишек.
            switch (BlackUnits)
            {
                case 0:
                    {
                        Visual.ChangeBlackUnits(Path[0]);
                        break;
                    }
                case 1:
                    {
                        Visual.ChangeBlackUnits(Path[1]);
                        break;
                    }
                case 2:
                    {
                        Visual.ChangeBlackUnits(Path[2]);
                        break;
                    }
                case 3:
                    {
                        Visual.ChangeBlackUnits(Path[3]);
                        break;
                    }
                case 4:
                    {
                        Visual.ChangeBlackUnits(Path[4]);
                        break;
                    }
                case 5:
                    {
                        Visual.ChangeBlackUnits(Path[5]);
                        break;
                    }
                case 6:
                    {
                        Visual.ChangeBlackUnits(Path[6]);
                        break;
                    }
                case 7:
                    {
                        Visual.ChangeBlackUnits(Path[7]);
                        break;
                    }
                case 8:
                    {
                        Visual.ChangeBlackUnits(Path[8]);
                        break;
                    }
                case 9:
                    {
                        Visual.ChangeBlackUnits(Path[9]);
                        break;
                    }
            }

            if (WhiteTens == 1)
            {
                Visual.ChangeOneWhiteTens(ChangeOne);
            }
            else if (WhiteTens > 1)
            {
                Visual.ChangeAnotherWhiteTens(ChangeAnother);
            }
            if (BlackTens == 1)
            {
                Visual.ChangeOneBlackTens(ChangeOne);
            }
            else if (BlackTens > 1)
            {
                Visual.ChangeAnotherBlackTens(ChangeAnother);
            }
        }

        // Метод изменения текущего хода.
        public void ReferenceChangeMove()
        {
            if (Logic.Turn == (int)GameLogic.Positions.White)
            {
                Visual.ChangeMoveToWhite();
            }
            else
            {
                Visual.ChangeMoveToBlack();
            }
        }

        // Метод поиска доступных ходов на игровой доске.
        public async void FoundMove()
        {
            Logic.AvailableMoves(out List<Coordinates>? AvailableMoves, out bool Ending);

            if (AvailableMoves!.Count > 0)
            {
                // Отображает доступные ходы на доске.
                foreach (Coordinates Coordinates in AvailableMoves)
                {
                    Visual.ShowAvailableMoves(Coordinates);
                }
            }
            else if (Ending)
            {
                // Если игра завершена, вызывает окончание игры.
                await Task.Delay(1000);

                Visual.End();
            }
            else
            {
                // Если нет доступных ходов, передает ход другому игроку.
                NextTurn();
            }
        }

        // Метод размещения фишки.
        public async void ReferenceChipPlace(string Path, string Name)
        {
            if (Path == Visual.HintMove.Source.ToString())
            {
                for (var Row = 0; Row < Boarder; Row++)
                {
                    for (var Column = 0; Column < Boarder; Column++)
                    {
                        if (Visual.GetName(Row, Column) == Name)
                        {
                            List<Coordinates> Reversi = Logic.GetReversiChips(new Coordinates(Row, Column), out int Turn);

                            if (Turn == 1)
                            {
                                Visual.PlaceWhiteChip(Row, Column);
                                foreach (Coordinates Position in Reversi)
                                {
                                    Visual.MainAnimation(Position.Row, Position.Column);
                                }
                            }
                            else
                            {
                                Visual.PlaceBlackChip(Row, Column);
                                foreach (Coordinates Position in Reversi)
                                {
                                    Visual.MainAnimation(Position.Row, Position.Column);
                                }
                            }
                        }
                    }
                }
                // Скрывает доступные ходы после размещения фишек.
                for (var Row = 0; Row < Boarder; Row++)
                {
                    for (var Column = 0; Column < Boarder; Column++)
                    {
                        if (Visual.GetSource(Row, Column) == Visual.HintMove.Source.ToString())
                        {
                            Visual.HideHint(Row, Column);
                        }
                    }
                }

                await Task.Delay(400);
                NextTurn();
            }
        }

        // Метод перехода к следующему ходу.
        public void NextTurn()
        {
            Logic.ChangeTurn();
            ReferenceChangeMove();
            Logic.ChipsCount(out int WhiteTens, out int WhiteUnits, out int BlackTens, out int BlackUnits);
            NewChipCounters(WhiteTens, WhiteUnits, BlackTens, BlackUnits);
            FoundMove();
        }

        // Метод завершения игры и освобождения ресурсов.
        public void ReferenceEnd()
        {
            OnGamePage = false;
            Logic = default!;
            Visual = default!;
        }

        // Метод выхода из всевозможных меню.
        public void VisualMenu(object sender, KeyEventArgs e)
        {
            if (OnGamePage)
            {
                if (e.Key == Key.Escape && Visual.Saving.Visibility == Visibility.Visible)
                {
                    Visual.SavingEsc();
                }
                else if (e.Key == Key.Escape && (Visual.SaveMenu.Visibility == Visibility.Visible || Visual.LoadingMenu.Visibility == Visibility.Visible))
                {
                    Visual.ReturnToMenu();
                }
                else if (e.Key == Key.Escape && Visual.Menu.Visibility == Visibility.Hidden)
                {
                    Visual.OpenMenu();
                }
                else if (e.Key == Key.Escape && Visual.Menu.Visibility == Visibility.Visible)
                {
                    Visual.CloseMenu();
                }
            }
        }

        // Метод отображения сохранений.
        public void ReferenceSaveMenu()
        {
            GameLogic.GetSavesNumber(out int SaveNumber, out List<string>? SavesName);

            if (SaveNumber != Boarder)
            {
                for (var i = 0; i < SaveNumber + 1; i++)
                {
                    Visual.PlaceSavesBack(i);
                }

                for (var i = 0; i < SaveNumber; i++)
                {
                    Visual.PlaceSaves(i, SavesName![i]);
                }
            }
            else
            {
                for (var i = 0; i < SaveNumber; i++)
                {
                    Visual.PlaceSaves(i, SavesName![i]);
                }
            }
        }

        // Метод сохранения предыдущего имени сохранения.
        public static void SavingPreviousName(string Name) => Save.PreviousSavingName = Name;

        // Метод получения информации о игре.
        public void GetSeed() => Logic.GetInformation();

        // Метод обработки подтверждения сохранения игры.
        public void ReferenceSavingEnter(string Name, string[] Content)
        {
            Save.SavingName = Name;
            GameLogic.CheckSaveName(out bool Except);

            if (Except)
            {
                for (var i = 0; i < Boarder; i++)
                {
                    if (Content[i] == Save.PreviousSavingName)
                    {
                        if (Content[i] != "Сохранить игру")
                        {
                            GameLogic.OverwriteSave();
                        }
                        else
                        {
                            GameLogic.WriteSave();
                        }
                        break;
                    }
                }

                Visual.ReturnToMenuFromSaving();
            }
            else
            {
                Visual.NullSaving();
            }
        }

        // Метод отображения списка доступных сохранений для загрузки.
        public void ReferenceLoadSave()
        {
            GameLogic.GetSavesNumber(out int SaveNumber, out List<string>? SavesName);

            for (var i = 0; i < SaveNumber; i++)
            {
                Visual.PlaceLoads(i, SavesName![i]);
            }
        }

        // Метод определения текущего хода.
        public void CurrentMove()
        {
            if (Logic.NowMove())
            {
                Visual.ChangeMoveToWhite();
            }
            else
            {
                Visual.ChangeMoveToBlack();
            }
        }

        // Метод отображения текущего состояния фишек.
        public void CurrentChips()
        {
            Logic.CurrentChip(out List<Coordinates> Chips);

            for (var Index = 0; Index < Chips.Count; Index++)
            {
                if (Chips[Index].Color == (int)GameLogic.Positions.White)
                {
                    Visual.PlaceCurrentWhite(Chips[Index].Row, Chips[Index].Column);
                }
                else
                {
                    Visual.PlaceCurrentBlack(Chips[Index].Row, Chips[Index].Column);
                }
            }
        }

        // Метод обновления фишек при загрузке сохранения.
        public void ViewSave(out int WhiteTens, out int WhiteUnits, out int BlackTens, out int BlackUnits)
        {
            Logic.LoadSave();
            Logic.ChipsCount(out WhiteTens, out WhiteUnits, out BlackTens, out BlackUnits);
        }

        // Метод сокрытия всех доступных ходов (Корректная загрузка сохранения).
        public void NullHint()
        {
            for (var Row = 0; Row < Boarder; Row++)
            {
                for (var Column = 0; Column < Boarder; Column++)
                {
                    if (Visual.GetSource(Row, Column) == Visual.HintMove.Source.ToString())
                    {
                        Visual.HideHint(Row, Column);
                    }
                }
            }
        }

        // Метод загрузки сохранения (Из стартовой страницы).
        public void LoadedSave()
        {
            if (StartPage.Exception == true)
            {
                ViewSave(out int WhiteTens, out int WhiteUnits, out int BlackTens, out int BlackUnits);
                NewChipCounters(WhiteTens, WhiteUnits, BlackTens, BlackUnits);
                NewChipCounters(WhiteTens, WhiteUnits, BlackTens, BlackUnits);

                CurrentMove();
                NullHint();
                FoundMove();
                CurrentChips();
                StartPage.Exception = false;
            }
        }

        // Метод для сброса доски (Для корректной загрузки сохранения).
        public void NullBoard()
        {
            for (var Row = 0; Row < Boarder; Row++)
            {
                for (var Column = 0; Column < Boarder; Column++)
                {
                    Visual.NullingBoard(Row, Column);
                }
            }
        }

        // Метод загрузки сохранения и обновления игрового состояния (Во время игры).
        public void ReferenceLoadingSave(string Content)
        {
            Save.PreviousSavingName = Content;

            NullBoard();
            ViewSave(out int WhiteTens, out int WhiteUnits, out int BlackTens, out int BlackUnits);
            NewChipCounters(WhiteTens, WhiteUnits, BlackTens, BlackUnits);

            CurrentMove();
            NullHint();
            FoundMove();
            CurrentChips();
        }
    }
}