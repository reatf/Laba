using System;
using System.Collections.Generic;
using System.Text;

namespace Laba
{

    internal class GameLogic
    {
        public enum Positions
        {
            Space = 0,
            White = 1,
            Black = 2,
        }

        private readonly int[,] Board = { { 0, 0, 0, 0, 0, 0, 0, 0},
                                          { 0, 0, 0, 0, 0, 0, 0, 0},
                                          { 0, 0, 0, 0, 0, 0, 0, 0},
                                          { 0, 0, 0, 2, 1, 0, 0, 0},
                                          { 0, 0, 0, 1, 2, 0, 0, 0},
                                          { 0, 0, 0, 0, 0, 0, 0, 0},
                                          { 0, 0, 0, 0, 0, 0, 0, 0},
                                          { 0, 0, 0, 0, 0, 0, 0, 0} };

        private int EndGame = 0; // Счетчик завершения игры
        private int Turn = (int)Positions.Black; // Текущий ход (черные начинают)

        // Метод для определения доступных ходов и проверки завершения игры
        public void AvailableMoves(out List<Coordinates>? AvailableMoves, out bool Ending)
        {
            AvailableMoves = new List<Coordinates>();
            Ending = false; // Игра не завершена по умолчанию

            for (var Row = 0; Row < Board.GetLength(0); Row++)
            {
                for (var Column = 0; Column < Board.GetLength(1); Column++)
                {
                    if (CheckMove(Row, Column)) // Проверяем, можно ли сделать ход
                    {
                        AvailableMoves.Add(new Coordinates(Row, Column)); // Добавляем координаты в список доступных ходов
                        EndGame = 0;
                    }
                }
            }

            if (AvailableMoves.Count == 0)
            {
                EndGame++;
            }

            if(EndGame == 2)
            {
                Ending = true; // Устанавливаем флаг завершения игры

                // Количество фишек каждого цвета в конце игры
                Save.BlackChips = 0;
                Save.WhiteChips = 0;

                for (int Row = 0; Row < Board.GetLength(0); Row++)
                {
                    for (int Column = 0; Column< Board.GetLength(1); Column++)
                    {
                        if (Board[Row, Column] == (int)Positions.Black)
                        {
                            Save.BlackChips++;
                        }
                        else if(Board[Row, Column] == (int)Positions.White)
                        {
                            Save.WhiteChips++;
                        }
                    }
                }
            }
        }

        // Метод для проверки хода в указанной позиции
        public bool CheckMove(int Row, int Column)
        {
            if (Board[Row, Column] != (int)Positions.Space)
            {
                return false; // Проверка: если позиция уже занята, вернуть false
            }

            for (var Hight = -1; Hight <= 1; Hight++)
            {
                for (var Weight = -1; Weight <= 1; Weight++)
                {
                    if (Hight == 0 && Weight == 0)
                    {
                        continue;
                    }

                    int RowVector = Row + Hight;
                    int ColumnVector = Column + Weight;
                    bool FoundOpponent = false;

                    while (RowVector >= 0 && RowVector < Board.GetLength(0) && ColumnVector >= 0 && ColumnVector < Board.GetLength(1))
                    {
                        if (Board[RowVector, ColumnVector] == Turn)
                        {
                            if (FoundOpponent)
                            {
                                return true; // Если был найден оппонент и затем своя фишка, возвращаем true
                            }
                            break;
                        }
                        else if (Board[RowVector, ColumnVector] == (int)Positions.Space)
                        {
                            break; // Если попали на пустую позицию, завершаем проверку
                        }
                        else
                        {
                            FoundOpponent = true; // Обнаружен оппонент
                        }
                        RowVector += Hight;
                        ColumnVector += Weight;
                    }
                }
            }
            return false; // Если не нашли допустимый ход, возвращаем false
        }

        // Метода для получения информации о Топе игроков
        public static void StartLeaderboard(out string? Nicks, out int Wins) => Data.SetTop(Save.LeaderIndex, out Nicks, out Wins);

        // Метод для проверки игроков на корректность
        public static bool CheckNickNames()
        {
            bool Key;
            if (String.IsNullOrWhiteSpace(Save.Player1) || String.IsNullOrWhiteSpace(Save.Player2)
                || (Save.Player1.Length < 3 || Save.Player1.Length > 30) || (Save.Player2.Length < 3 || Save.Player2.Length > 30)
                || Save.Player1 == Save.Player2)
            {
                Key = false;
            }
            else
            {
                Key = true;
            }
            return Key;
        }

        // Метод для инверсии фишек и смены текущего игрока
        public List<Coordinates> GetReversiChips(Coordinates Chip, out int CurrentTurn)
        {
            Board[Chip.Row, Chip.Column] = Turn; // Создаем список для хранения инвертированных фишек
            List<Coordinates> ResultChips = new(); // Создание списка для хранения инвертированных фишек
            int key = 0;

            for (var Hight = -1; Hight <= 1; Hight++)
            {
                for (var Weight = -1; Weight <= 1; Weight++)
                {
                    if (Hight == 0 && Weight == 0)
                    {
                        continue;
                    }

                    int RowVector = Chip.Row + Hight;
                    int ColumnVector = Chip.Column + Weight;
                    bool FoundOpponent = false;

                    while (RowVector >= 0 && RowVector < Board.GetLength(0) && ColumnVector >= 0 && ColumnVector < Board.GetLength(1))
                    {
                        if (Board[RowVector, ColumnVector] == Turn)
                        {
                            if (FoundOpponent)
                            {
                                switch(key)
                                {
                                    case 0:
                                        {
                                            int MaxRow = RowVector + 1;
                                            int MaxColumn = ColumnVector + 1;
                                            while (MaxRow != Chip.Row || MaxColumn != Chip.Column)
                                            {
                                                ResultChips.Add(new Coordinates(MaxRow, MaxColumn));
                                                MaxRow++;
                                                MaxColumn++;
                                            }
                                            break;
                                        }
                                    case 1:
                                        {
                                            int MaxRow = RowVector + 1;
                                            int MaxColumn = ColumnVector;
                                            while (MaxRow != Chip.Row || MaxColumn != Chip.Column)
                                            {
                                                ResultChips.Add(new Coordinates(MaxRow, MaxColumn));
                                                MaxRow++;
                                            }
                                            break;
                                        }
                                    case 2:
                                        {
                                            int MaxRow = RowVector + 1;
                                            int MaxColumn = ColumnVector - 1;
                                            while (MaxRow != Chip.Row || MaxColumn != Chip.Column)
                                            {
                                                ResultChips.Add(new Coordinates(MaxRow, MaxColumn));
                                                MaxRow++;
                                                MaxColumn--;
                                            }
                                            break;
                                        }
                                    case 3:
                                        {
                                            int MaxRow = RowVector;
                                            int MaxColumn = ColumnVector + 1;
                                            while (MaxRow != Chip.Row || MaxColumn != Chip.Column)
                                            {
                                                ResultChips.Add(new Coordinates(MaxRow, MaxColumn));
                                                MaxColumn++;
                                            }
                                            break;
                                        }
                                    case 4:
                                        {
                                            int MaxRow = RowVector;
                                            int MaxColumn = ColumnVector - 1;
                                            while (MaxRow != Chip.Row || MaxColumn != Chip.Column)
                                            {
                                                ResultChips.Add(new Coordinates(MaxRow, MaxColumn));
                                                MaxColumn--;
                                            }
                                            break;
                                        }
                                    case 5:
                                        {
                                            int MaxRow = RowVector - 1;
                                            int MaxColumn = ColumnVector + 1;
                                            while (MaxRow != Chip.Row || MaxColumn != Chip.Column)
                                            {
                                                ResultChips.Add(new Coordinates(MaxRow, MaxColumn));
                                                MaxRow--;
                                                MaxColumn++;
                                            }
                                            break;
                                        }
                                    case 6:
                                        {
                                            int MaxRow = RowVector - 1;
                                            int MaxColumn = ColumnVector;
                                            while (MaxRow != Chip.Row || MaxColumn != Chip.Column)
                                            {
                                                ResultChips.Add(new Coordinates(MaxRow, MaxColumn));
                                                MaxRow--;
                                            }
                                            break;
                                        }
                                    case 7:
                                        {
                                            int MaxRow = RowVector - 1;
                                            int MaxColumn = ColumnVector - 1;
                                            while (MaxRow != Chip.Row || MaxColumn != Chip.Column)
                                            {
                                                ResultChips.Add(new Coordinates(MaxRow, MaxColumn));
                                                MaxRow--;
                                                MaxColumn--;
                                            }
                                            break;
                                        }
                                }
                            }
                            break;
                        }
                        else if (Board[RowVector, ColumnVector] == (int)Positions.Space)
                        {
                            break;
                        }
                        else
                        {
                            FoundOpponent = true; // Отмечаем обнаружение оппонента
                        }

                        RowVector += Hight;
                        ColumnVector += Weight;
                    }

                    key++;
                }
            }

            foreach(Coordinates Position in ResultChips)
            {
                Board[Position.Row, Position.Column] = Turn; // Инвертируем фишки на доске
            }

            CurrentTurn = Turn; // Устанавливаем текущего игрока
            return ResultChips; // Возвращаем инвертированные фишки
        }

        // Метод для смены текущего игрока
        public void ChangeTurn() => Turn = (Turn == (int)Positions.Black) ? (int)Positions.White : (int)Positions.Black;
        
        // Метод для подсчета количества фишек каждого цвета
        public void ChipsCount(out int WhiteTens, out int WhiteUnits, out int BlackTens, out int BlackUnits)
        {
            int WhiteChips = 0, BlackChips = 0;
            for(var Row = 0; Row< Board.GetLength(0); Row++)
            {
                for(var Column = 0;  Column < Board.GetLength(1); Column++)
                {
                    if (Board[Row, Column] == (int)Positions.Black)
                    {
                        BlackChips++;
                    }
                    else if (Board[Row, Column] == (int)Positions.White)
                    {
                        WhiteChips++;
                    }
                }
            }
            WhiteTens = WhiteChips / 10;
            WhiteUnits = WhiteChips % 10;
            BlackTens = BlackChips / 10;
            BlackUnits = BlackChips % 10;
        }

        // Метод для определения победителя и сохранения его имени
        public static void Winner(string PlayerWinner) => Data.SetWinner(PlayerWinner);

        // Метод для определения победителя и сохранения его имени
        public static void GetSavesNumber(out int SaveNumber, out List<string>? SaveName) => Data.GetSaveNumber(out SaveNumber, out SaveName);

        // Метод для проверки корректности имени сохранения
        public static void CheckSaveName(out bool Except)
        {
            if(String.IsNullOrWhiteSpace(Save.SavingName) || Save.SavingName.Length < 1 || Save.SavingName.Length > 15)
            {
                Except = false; // Некорректное имя сохранения
            }
            else
            {  
                Data.CheckSaveName(out Except); // Проверка имени сохранения в данных
            }
        }

        // Метод для перезаписи сохранения
        public static void OverwriteSave() => Data.ChangeSave();   

        // Метод для создания нового сохранения
        public static void WriteSave() => Data.NewSave();

        // Метод для получения информации об игровой доске и текущем игроке
        public void GetInformation()
        {
            var Information = new StringBuilder();
            for(var Row = 0; Row < Board.GetLength(0); Row++)
            {
                for(var Column = 0; Column < Board.GetLength(1); Column++)
                {
                    Information.Append(Board[Row, Column]);
                }
            }
            Information.Append(Turn);
            Save.Information = Information.ToString();
        }

        // Метод для загрузки сохранения
        public void LoadSave()
        {
            Data.GetSave();
            List<string> Symbol = new();
            int SymbolIndex = 0;
            for(var Index = 0;  Index < Board.Length; Index++)
            {
                Symbol.Add(Save.Information[Index].ToString());
            }
            for(var Row = 0; Row < Board.GetLength(0); Row++)
            {
                for(var Column = 0; Column < Board.GetLength(1);Column++)
                {
                    Board[Row, Column] = int.Parse(Symbol[SymbolIndex]);
                    SymbolIndex++;
                }
            }
            Turn = int.Parse(Save.Information[^1].ToString());
        }

        // Метод для проверки текущего хода(чей ход)
        public bool NowMove()
        {
            bool Key;
            if(Turn != (int) Positions.Black)
            {
                Key = true; // Ход белых
            }
            else
            {
                Key = false; // Ход белых
            }
            return Key;
        }

        // Метод для получения текущих фишек на доске
        public void CurrentChip(out List<Coordinates> Chip)
        {
            Chip = new();
            for(var Row = 0; Row < Board.GetLength(0);Row++)
            {
                for(var Column = 0;Column < Board.GetLength(1); Column++)
                {
                    if (Board[Row, Column] != (int)Positions.Space)
                    {
                        Chip.Add(new Coordinates(Row, Column, Board[Row, Column]));
                    }
                }
            }
        }
    }
}
