using MySql.Data.MySqlClient;

namespace Laba
{
    internal class Data
    {
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
        internal static void SetWinner(string Winner)
        {
            bool PlayerExist;
            MySqlConnection Connection = new("Server=localhost;Port=3306;Database=reversi_schema;Uid=root;Pwd=1324");
            Connection.Open();
            MySqlCommand Command = new MySqlCommand("select * from leaderboard where NickName = @NickName;", Connection);
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
            Connection.Open ();

            if (PlayerExist)
            {
                Command = new MySqlCommand("Update leaderboard set Wins = Wins + 1 where Nickname = @NickName;", Connection);
                Command.Parameters.AddWithValue("@NickName", Winner);
                Command.ExecuteNonQuery();
            }
            else
            {
                Command = new MySqlCommand("insert into leaderboard (NickName, Wins) value (@NickName, 1);", Connection);
                Command.Parameters.AddWithValue("@NickName", Winner);
                Command.ExecuteNonQuery();
            }
            Connection.Close();
        }
    }
}
