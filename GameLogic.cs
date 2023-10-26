﻿using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Collections.Generic;
using System.Windows.Documents.DocumentStructures;
using System.Security.RightsManagement;
using System.Windows.Media.Media3D;
using Org.BouncyCastle.Utilities;
using System.ComponentModel;
using Google.Protobuf.WellKnownTypes;
using MySqlX.XDevAPI.Relational;
using System.Numerics;
using System.Windows.Controls;

namespace Laba
{
    struct Leaderboard
    {
        internal string Nick;
        internal int Wins;
    }
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
        private int EndGame = 0;
        private int Turn = (int)Positions.Black;
        public void AvailableMoves(out List<Coordinates>? AvailableMoves, out bool Ending)
        {
            AvailableMoves = new List<Coordinates>();
            Ending = false;
            for (int Row = 0; Row < Board.GetLength(0); Row++)
            {
                for (int Column = 0; Column < Board.GetLength(1); Column++)
                {
                    if (CheckMove(Row, Column))
                    {
                        AvailableMoves.Add(new Coordinates(Row, Column));
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
                Ending = true;
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
        public bool CheckMove(int Row, int Column)
        {
            if (Board[Row, Column] != (int)Positions.Space)
            {
                return false;
            }

            for (int Hight = -1; Hight <= 1; Hight++)
            {
                for (int Weight = -1; Weight <= 1; Weight++)
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
                                return true;
                            }
                            break;
                        }
                        else if (Board[RowVector, ColumnVector] == (int)Positions.Space)
                        {
                            break;
                        }
                        else
                        {
                            FoundOpponent = true;
                        }
                        RowVector += Hight;
                        ColumnVector += Weight;
                    }
                }
            }
            return false;
        }
        public static void StartLeaderboard(int Index, out Leaderboard Player)
        {
            Data.SetTop(Index, out Player.Nick, out Player.Wins);
        }
        public static void CheckNickNames(out bool Key)
        {
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
        }
        public List<Coordinates> GetReversiChips(Coordinates Chip, out int CurrentTurn)
        {
            Board[Chip.Row, Chip.Column] = Turn;
            List<Coordinates> ResultChips = new();
            int key = 0;
            for (int Hight = -1; Hight <= 1; Hight++)
            {
                for (int Weight = -1; Weight <= 1; Weight++)
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
                            FoundOpponent = true;
                        }
                        RowVector += Hight;
                        ColumnVector += Weight;
                    }
                    key++;
                }
            }
            foreach(var Position in ResultChips)
            {
                Board[Position.Row, Position.Column] = Turn;
            }
            CurrentTurn = Turn;
            return ResultChips;
        }
        public void ChangeTurn()
        {
            Turn = (Turn == (int)Positions.Black) ? (int)Positions.White : (int)Positions.Black;
        }
        public void ChipsCount(out int WhiteTens, out int WhiteUnits, out int BlackTens, out int BlackUnits)
        {
            int WhiteChips = 0, BlackChips = 0;
            for(int Row = 0; Row< Board.GetLength(0); Row++)
            {
                for(int Column = 0;  Column < Board.GetLength(1); Column++)
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
        public static void Winner(string PlayerWinner)
        {
            Data.SetWinner(PlayerWinner);
        }
        public static void GetSavesNumber(out int SaveNumber, out List<string>? SaveName)
        {
            Data.GetSaveNumber(out SaveNumber, out SaveName);
        }
        public static void CheckSaveName(out bool Except)
        {
            if(String.IsNullOrWhiteSpace(Save.SavingName) || Save.SavingName.Length < 1 || Save.SavingName.Length > 15)
            {
                Except = false;
            }
            else
            {  
                Data.CheckSaveName(out Except);
            }
        }
        public static void OverwriteSave()
        {
            Data.ChangeSave();
        }
        public static void WriteSave()
        {
            Data.NewSave();
        }
        public void GetInformation()
        {
            string Information = "";
            for(int Row = 0; Row < Board.GetLength(0); Row++)
            {
                for(int Column = 0; Column < Board.GetLength(1); Column++)
                {
                    Information += Board[Row, Column].ToString();
                }
            }
            Information += Turn.ToString();
            Save.Information = Information;
        }
        public void LoadSave()
        {
            Data.GetSave();
            List<string> Symbol = new();
            int SymbolIndex = 0;
            for(int Index = 0;  Index < Board.Length; Index++)
            {
                Symbol.Add(Save.Information[Index].ToString());
            }
            for(int Row = 0; Row < Board.GetLength(0); Row++)
            {
                for( int Column = 0; Column < Board.GetLength(1);Column++)
                {
                    Board[Row, Column] = int.Parse(Symbol[SymbolIndex]);
                    SymbolIndex++;
                }
            }
            Turn = int.Parse(Save.Information[^1].ToString());
        }
        public void NowMove(out bool Key)
        {
            if(Turn != (int) Positions.Black)
            {
                Key = true;
            }
            else
            {
                Key = false;
            }
        }
        public void CurrentChip(out List<Coordinates> Chip)
        {
            Chip = new();
            for (int Row = 0; Row < Board.GetLength(0);Row++)
            {
                for(int Column = 0;Column < Board.GetLength(1); Column++)
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
