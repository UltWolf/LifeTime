using LifeTime.Models;
using System;
using System.Data.SqlClient;


namespace LifeTime.sqlHelpers
{
    class sqlTask
    {
        private static string connectionString = @"Data Source=(localdb)\MSSQLLocaldb;Initial Catalog = TablePlanneryDays.mdf; Integrated Security = True";

        public static void RegistrateUser(string title , string description, string Username, DateTime time)
        {
            string sqlExpression = "CreateTask";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter UserNameParam = new SqlParameter
                {
                    ParameterName = "@Username",
                    Value = Username
                };

                SqlParameter HeaderParam = new SqlParameter
                {
                    ParameterName = "@Header",
                    Value = title
                };
               
                SqlParameter DescriptionParam = new SqlParameter
                {
                    ParameterName = "@Description",
                    Value = description
                };
               
                SqlParameter Time = new SqlParameter
                {
                    ParameterName = "@Password",
                    Value = time
                };
                command.Parameters.Add(Time);
                command.Parameters.Add(UserNameParam);
                command.Parameters.Add(HeaderParam);
                command.Parameters.Add(DescriptionParam);
                var result = command.ExecuteScalar();
            }
        }

        public static PlanneryDay GetUser(string Username)
        {
            string sqlExpression = "GetUser";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter UserNameParam = new SqlParameter
                {
                    ParameterName = "@Username",
                    Value = Username
                };

                command.Parameters.Add(UserNameParam);
                string FindName = String.Empty;
                string FindPassword = String.Empty;
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        FindName = reader.GetString(1);
                        FindPassword = reader.GetString(2);

                    }
                }
                return new PlanneryDay();
            }
        }
    }
}
