using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace All_in_one_Study_Companion.Classes
{
    // Class for database operations
    public class DbHelper
    {
        // Connection string for the database
        private readonly string connectionString;

        // Constructor to initialize the connection string
        public DbHelper()
        {
            connectionString = ConfigurationManager.ConnectionStrings["StudyCompanionDB"].ConnectionString;
        }

        // Method to create and return a new SqlConnection
        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        // Method to execute a query and return results as a DataTable
        public DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the command if provided
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    DataTable dataTable = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    try
                    {
                        // Open connection and fill DataTable with query results
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

        // Method to execute a non-query command (e.g., INSERT, UPDATE, DELETE)
        public int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the command if provided
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    try
                    {
                        // Open connection and execute the command
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
