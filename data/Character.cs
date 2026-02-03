using Microsoft.Data.Sqlite;

namespace FirstAPI.data
{
    public class Character
    {
        private readonly string _connectionString;

        public Character(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int GetHealth(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = connection.CreateCommand();

            command.CommandText = "SELECT Health FROM Character WHERE Id = $id";
            command.Parameters.AddWithValue("$id", id);

            var result = command.ExecuteScalar();
            connection.Close();

            return Convert.ToInt32(result);
        }

        public int UpdateHealth(int id, int affectedValue)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = connection.CreateCommand();

            command.CommandText = "UPDATE Character SET Health = $value WHERE Id = $id";
            command.Parameters.AddWithValue("$id", id);
            command.Parameters.AddWithValue("$value", affectedValue + GetHealth(id));
            return command.ExecuteNonQuery();
        }

        public string GetName(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = connection.CreateCommand();

            command.CommandText = "SELECT Name FROM Character WHERE Id = $id";
            command.Parameters.AddWithValue("$id", id);

            var result = command.ExecuteScalar();
            connection.Close();

            return Convert.ToString(result);
        }

        public int UpdateXp(int id, int earnedValue)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = connection.CreateCommand();

            command.CommandText = "UPDATE Character SET XP = $value WHERE Id = $id";
            command.Parameters.AddWithValue("$id", id);
            command.Parameters.AddWithValue("$value", earnedValue + GetXp(id));

            return command.ExecuteNonQuery();
        }

        public int GetXp(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = connection.CreateCommand();

            command.CommandText = "SELECT XP FROM Character WHERE Id = $id";
            command.Parameters.AddWithValue("$id", id);

            var result = command.ExecuteScalar();
            return Convert.ToInt32(result);
        }
    }
}
