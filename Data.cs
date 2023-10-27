using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Laba
{
    internal class Data
    {
        // Метод для получения данных о Топе игроков
        internal static void SetTop(int Index, out string? TopNicks, out int TopWins)
        {
            MySqlConnection Connection = new("Server=localhost;Port=3306;Database=reversi_schema;Uid=root;Pwd=1324");
            Connection.Open();

            MySqlCommand Command = new("Select NickName, Wins from leaderboard order by Wins DESC Limit @Index,1;", Connection);
            Command.Parameters.AddWithValue("@Index", Index);
            MySqlDataReader Reader = Command.ExecuteReader();
            Reader.Read();

            if (Reader.HasRows)
            {
                TopNicks = Reader["NickName"].ToString();
                TopWins = int.Parse(Reader["Wins"].ToString());
            }
            else
            {
                TopNicks = "";
                TopWins = 0;
            }

            Connection.Close();
        }

        // Метод для обновления информации о победителе
        internal static void SetWinner(string Winner)
        {
            bool PlayerExist;
            MySqlConnection Connection = new("Server=localhost;Port=3306;Database=reversi_schema;Uid=root;Pwd=1324");
            Connection.Open();

            // Проверка наличия игрока в таблице лидеров
            MySqlCommand Command = new("select * from leaderboard where NickName = @NickName;", Connection);
            Command.Parameters.AddWithValue("@NickName", Winner);
            MySqlDataReader Reader = Command.ExecuteReader();

            if (Reader.HasRows)
            {
                PlayerExist = true;
            }
            else
            {
                PlayerExist = false;
            }

            Connection.Close();
            Connection.Open();

            if (PlayerExist)
            {
                // Обновление количества побед игрока
                Command = new MySqlCommand("Update leaderboard set Wins = Wins + 1 where Nickname = @NickName;", Connection);
                Command.Parameters.AddWithValue("@NickName", Winner);
                Command.ExecuteNonQuery();
            }
            else
            {
                // Добавление нового игрока в таблицу лидеров
                Command = new MySqlCommand("insert into leaderboard (NickName, Wins) value (@NickName, 1);", Connection);
                Command.Parameters.AddWithValue("@NickName", Winner);
                Command.ExecuteNonQuery();
            }
            Connection.Close();
        }

        // Метод для получения количества сохранений и их имен
        internal static void GetSaveNumber(out int SaveNumber, out List<string>? SaveName)
        {
            SaveNumber = 0;
            SaveName = new();
            MySqlConnection Connection = new("Server=localhost;Port=3306;Database=reversi_schema;Uid=root;Pwd=1324");
            Connection.Open();

            MySqlCommand Command = new("Select SaveName from Saves;", Connection);
            MySqlDataReader Reader = Command.ExecuteReader();

            while (Reader.Read())
            {
                SaveNumber++;
                SaveName.Add(Reader["SaveName"].ToString());
            }

            Connection.Close();
        }

        // Метод для обновления сохранения
        internal static void ChangeSave()
        {
            MySqlConnection Connection = new("Server=localhost;Port=3306;Database=reversi_schema;Uid=root;Pwd=1324");
            Connection.Open();

            MySqlCommand Command = new("Update saves set SaveName = @SavingName, Save = @Save, Player1 = @Player1, Player2 = @Player2 where SaveName = @PreviousSaveName;", Connection);
            Command.Parameters.AddWithValue("@SavingName", Save.SavingName);
            Command.Parameters.AddWithValue("@Save", Save.Information);
            Command.Parameters.AddWithValue("@Player1", Save.Player1);
            Command.Parameters.AddWithValue("@Player2", Save.Player2);
            Command.Parameters.AddWithValue("@PreviousSaveName", Save.PreviousSavingName);
            Command.ExecuteNonQuery();

            Connection.Close();
        }

        // Метод для создания нового сохранения
        internal static void NewSave()
        {
            MySqlConnection Connection = new("Server=localhost;Port=3306;Database=reversi_schema;Uid=root;Pwd=1324");
            Connection.Open();

            MySqlCommand Command = new("insert into saves (SaveName, Save, Player1, Player2) value (@SaveName, @Save, @Player1, @Player2);", Connection);
            Command.Parameters.AddWithValue("@SaveName", Save.SavingName);
            Command.Parameters.AddWithValue("@Save", Save.Information);
            Command.Parameters.AddWithValue("@Player1", Save.Player1);
            Command.Parameters.AddWithValue("@Player2", Save.Player2);
            Command.ExecuteNonQuery();

            Connection.Close();
        }

        // Метод для проверки уникальности имени 
        internal static void CheckSaveName(out bool Except)
        {
            MySqlConnection Connection = new("Server=localhost;Port=3306;Database=reversi_schema;Uid=root;Pwd=1324");
            Connection.Open();

            MySqlCommand Command = new("select * from saves where SaveName = @SaveName;", Connection);
            Command.Parameters.AddWithValue("@SaveName", Save.SavingName);
            MySqlDataReader Reader = Command.ExecuteReader();
            Reader.Read();

            if (Reader.HasRows)
            {
                Except = false ;
            }
            else
            {
                Except= true;
            }

            Connection.Close();
        }

        // Метод для получения данных сохранения
        internal static void GetSave()
        {
            MySqlConnection Connection = new("Server=localhost;Port=3306;Database=reversi_schema;Uid=root;Pwd=1324");
            Connection.Open();

            MySqlCommand Command = new("select Save, Player1, Player2 from Saves where SaveName = @SaveName;", Connection);
            Command.Parameters.AddWithValue("@SaveName", Save.PreviousSavingName);
            MySqlDataReader Reader = Command.ExecuteReader();
            Reader.Read();

            Save.Information = Reader["Save"].ToString();
            Save.Player1 = Reader["Player1"].ToString();
            Save.Player2 = Reader["Player2"].ToString();

            Connection.Close();
        }
    }
}
