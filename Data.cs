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
                Connection.Close();
            }
            else
            {
                TopNicks = "";
                TopWins = 0;
            }
        }
    }
}
