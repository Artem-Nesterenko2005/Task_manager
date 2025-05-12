using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;

namespace TaskManager;

static internal class DataBase
{
    private static string dbSetting = "Host=localhost;Username=postgres;Password=8937367iii;Database=records";

    public static void AddTaskDb(string name, string text, string priority, DateTime date)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(dbSetting))
        {
            connection.Open();
            string request = $"INSERT INTO records (name, content, priority, date) VALUES (@name, @text, @priority, @date)";
            using (NpgsqlCommand command = new NpgsqlCommand(request, connection))
            {
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@text", text);
                command.Parameters.AddWithValue("@priority", priority);
                command.Parameters.AddWithValue("@date", NpgsqlDbType.Date, date.Date);

                int rowsAffected = command.ExecuteNonQuery();
            }
        }
    }

    public static List<Record> GetAllTasksDb()
    {
        List<Record> records = new ();
        using (NpgsqlConnection connection = new NpgsqlConnection(dbSetting))
        {
            connection.Open();
            string request = "SELECT * FROM records";
            using (NpgsqlCommand command = new NpgsqlCommand(request, connection))
            {
                var rowsAffected = command.ExecuteReader();
                while (rowsAffected.Read())
                {
                    records.Add(new Record(rowsAffected.GetString(0), rowsAffected.GetString(1), rowsAffected.GetString(2), rowsAffected.GetDateTime(3)));
                }
            }
            return records;
        }
    }

    public static void DeleteTaskDb(string name)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(dbSetting))
        {
            connection.Open();
            string request = "DELETE FROM records WHERE name = @name";
            using (NpgsqlCommand command = new NpgsqlCommand(request, connection))
            {
                command.Parameters.AddWithValue("@name", name);
                var rowsAffected = command.ExecuteNonQuery();
            }
        }
    }
}
