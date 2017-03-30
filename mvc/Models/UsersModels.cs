using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace mvc.Models
{
    public class UsersModel
    {
        private int _id;
        private string _name;

        public int Id { get { return _id; } set { _id = value; } }

        public string Name { get { return _name; } set { _name = value; } }

        public UsersModel(string name): this(0, name) { }


        public UsersModel(int id): this(id, null) { }

        public UsersModel(int id, string name)
        {
            _id = id;
            _name = name;
        }

        public static List<UsersModel> GetList()
        {
            SqlConnection connection = new SqlConnection()
            {
                ConnectionString = @"Data Source=andvas.database.windows.net;Initial Catalog=start;Integrated Security=False;User ID=nAIlIAvMN6;Password=usiq2gdKm0;Connect Timeout=15;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"

            };


            List<UsersModel> list = new List<UsersModel>();
            SqlCommand command = new SqlCommand(
              "SELECT * FROM Users;",
              connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();


            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    UsersModel item = new UsersModel(id, name);
                    list.Add(item);
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            reader.Close();
            connection.Close();
            return list;
        }
    }
}