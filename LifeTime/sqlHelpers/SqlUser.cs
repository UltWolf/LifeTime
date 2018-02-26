using LifeTime.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeTime.sqlHelpers
{
    class SqlUser
    {
        private static string connectionString = @"Data Source=(localdb)\MSSQLLocaldb;Initial Catalog = TablePlanneryDays.mdf; Integrated Security = True";

        public static  void RegistrateUser(string Username,string Password)
        {
            string sqlExpression = "CreateUser";
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
                SqlParameter PasswordParam = new SqlParameter
                {
                    ParameterName = "@Password",
                    Value = Password
                };
                command.Parameters.Add(PasswordParam);
                var result = command.ExecuteScalar();
            }
        }

        public static User GetUser(string Username)
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
                string FindName =  String.Empty;
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
                return new User() { Username = FindName,Password = FindPassword };
            }
        }

    }
}
