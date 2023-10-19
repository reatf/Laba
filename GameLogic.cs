using System;
using System.Windows;

namespace Laba
{
    struct Leaderboard
    {
        internal string Nick;
        internal int Wins;
    }
    internal class GameLogic
    {       
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
