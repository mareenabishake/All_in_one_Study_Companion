using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace All_in_one_Study_Companion.Classes
{
    public class DbHelper
    {
        private readonly string connectionString;

        public DbHelper()
        {
            connectionString = ConfigurationManager.ConnectionStrings["StudyCompanionDB"].ConnectionString;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    DataTable dataTable = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    try
                    {
                        connection.Open();
                        adapter.Fill(dataTable);
                    }
                    catch (Exception ex)
                    {
                        // Log the exception
                        Console.WriteLine($"Error executing query: {ex.Message}");
                        throw;
                    }

                    return dataTable;
                }
            }
        }

        public int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    try
                    {
                        connection.Open();
                        return command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // Log the exception
                        Console.WriteLine($"Error executing non-query: {ex.Message}");
                        throw;
                    }
                }
            }
        }
    }
}
