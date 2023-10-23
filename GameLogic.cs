using System;
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
        private int[,] Board = { { 0, 0, 0, 0, 0, 0, 0, 0},
                                 { 0, 0, 0, 0, 0, 0, 0, 0},
                                 { 0, 0, 0, 0, 0, 0, 0, 0},
                                 { 0, 0, 0, 2, 1, 0, 0, 0},
                                 { 0, 0, 0, 1, 2, 0, 0, 0},
                                 { 0, 0, 0, 0, 0, 0, 0, 0},
                                 { 0, 0, 0, 0, 0, 0, 0, 0},
                                 { 0, 0, 0, 0, 0, 0, 0, 0} };
        public int this[int Column, int Row]
        {
            get { return Board[Column, Row]; }
            set { Board[Column, Row] = value; }
        }
        private int Turn = (int) Positions.Black;
        public int ChangeTurn
        {
            get { return Turn; }
            set
            {
                if (Turn == (int)Positions.Black) Turn = (int)Positions.White;
                else Turn = (int)Positions.Black;
            }
        }
        public void AvailableMoves(out List<Coordinates>? AvailableMoves)
        {
            AvailableMoves = new List<Coordinates>();
            for (int Row = 0; Row < Board.GetLength(0);Row++)
            {
                for (int Column = 0; Column < Board.GetLength(1);Column++)
                {
                    if (CheckMove(Row, Column))
                    {
                        AvailableMoves.Add(new Coordinates(Row, Column));
                    }
                }
            }
        }
        public bool CheckMove(int Column, int Row)
        {
            if (Row < 0 || Row >= Board.GetLength(0) || Column < 0 || Column >= Board.GetLength(1) || Board[Row, Column] != (int) Positions.Space)
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

                    while ( RowVector >= 0 && RowVector < Board.GetLength(0) && ColumnVector >= 0 && ColumnVector < Board.GetLength(1))
                    {
                        if (Board[RowVector, ColumnVector] == Turn)
                        {
                            FoundOpponent = true;
                        }
                        else if (Board[RowVector, ColumnVector] == (int) Positions.Space)
                        {
                            break;
                        }
                        else
                        {
                            if (FoundOpponent)
                            {
                                return true;
                            }
                            break;
                        }
                        RowVector += Hight;
                        ColumnVector += Weight;
                    }
                }
            }
            return false;
        }
        public static void Start(int Index, out Leaderboard Player)
        {
            Data.SetTop(Index, out Player.Nick, out Player.Wins);
        }
        public static void Check(out bool Key)
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

    }
}
