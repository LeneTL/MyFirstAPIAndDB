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

        public int UpdateHealth(int id, int healedValue)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = connection.CreateCommand();

            command.CommandText = "UPDATE Character SET Health = $value WHERE Id = $id";
            command.Parameters.AddWithValue("$id", id);
            command.Parameters.AddWithValue("$value", healedValue + GetHealth(id));
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
        //legge til et endepunkt som henter Navn - fixed
        //legge til random heal
        //legge til dmg
    }
}
