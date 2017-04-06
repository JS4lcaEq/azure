using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace mvc.Models
{
    public class UsersModel
    {
        private int _id;
        private string _nickName;
        private string _emailHash;
        private string _PasswordHash;
        private string _email;
        private string _password;
        private string _invite;

        public int Id { get { return _id; } set { _id = value; } }

        public string NickName { get { return _nickName; } set { _nickName = value; } }

        public string Invite { get { return _invite; } set { _invite = value; } }

        public string EmailHash { get { return _emailHash; } set { _emailHash = value; } }

        public UsersModel(string name): this(0, name) { }


        public UsersModel(int id): this(id, null) { }

        public UsersModel(int id, string nickName)
        {
            _id = id;
            _nickName = nickName;
        }

        public static string CreateInvite(string email)
        {
            string invite = Guid.NewGuid().ToString();
            string emailHash = Hash(email);
            string trueInvite;

            //int rowsCount = App_Start.Db.Execute("INSERT INTO Users (Invite, EmailHash) VALUES ('" + invite + "', '" + emailHash + "')");
            /*
            @invite nvarchar(1024),
	        @emailHash nvarchar(1024),
	        @trueInvite nvarchar(1024) output
            */

            string sql = string.Format(@"ProcedureCreateInvite");
            SqlCommand command = new SqlCommand(sql);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@emailHash", emailHash);
            command.Parameters.AddWithValue("@invite", invite);
            //command.Parameters.AddWithValue("@trueInvite", trueInvite).Direction = ParameterDirection.Output;
            SqlParameter trueInviteParametr = new SqlParameter
            {
                ParameterName = "@trueInvite",
                SqlDbType = SqlDbType.NVarChar,
                Size = 1024
            };
            trueInviteParametr.Direction = ParameterDirection.Output;
            command.Parameters.Add(trueInviteParametr);

            App_Start.Db.Execute(command);

            trueInvite = command.Parameters["@trueInvite"].Value.ToString();
            return trueInvite;
        }

        public static UsersModel Login(string email, string password)
        {
            UsersModel user = null;
            string emailHash = Hash(email);
            string passwordHash = Hash(password);

            string sql = string.Format(@"ProcedureLogin");

            SqlCommand command = new SqlCommand(sql);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@EmailHash", emailHash);
            command.Parameters.AddWithValue("@PasswordHash", passwordHash);
            command.Connection = App_Start.Db.GetConnection();

   

            SqlDataAdapter adapter = new SqlDataAdapter(command);


            DataSet dset = new DataSet();
            adapter.Fill(dset);
            DataTable table = dset.Tables[0];

            List<UsersModel> list = new List<UsersModel>();

            foreach (DataRow row in table.Rows)
            {
                UsersModel item = new UsersModel((int)row["Id"], row["NickName"].ToString());
                item.Invite = row["Invite"].ToString();
                item.EmailHash = row["EmailHash"].ToString();
                list.Add(item);
            }
            if(list.Count > 0)
            {
                return list[0];
            }
            return user;
        }

        public static string CreateUser(string invite, string email, string password, string nickName)
        {
            string error = null;
            string emailHash = Hash(email);
            string passwordHash = Hash(password);

            string sql = string.Format(@"ProcedureCreateUser");
            SqlCommand command = new SqlCommand(sql);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@EmailHash", emailHash);
            command.Parameters.AddWithValue("@PasswordHash", passwordHash);
            command.Parameters.AddWithValue("@NickName", nickName);
            command.Parameters.AddWithValue("@Invite", invite);

            int rowsCount = App_Start.Db.Execute(command);

            return error;
        }

        public static List<UsersModel> GetInvitesList()
        {
            List<UsersModel> list = new List<UsersModel>();

            DataTable table = App_Start.Db.GetTable("SELECT * FROM Users WHERE Invite IS NOT NULL");

            foreach (DataRow row in table.Rows)
            {
                UsersModel item = new UsersModel((int)row["Id"], row["NickName"].ToString());
                item.Invite = row["Invite"].ToString();
                item.EmailHash = row["EmailHash"].ToString();
                list.Add(item);
            }
            return list;
        }

        public static UsersModel GetSingle(int id)
        {
            DataTable table = App_Start.Db.GetTable("SELECT * FROM Users WHERE Id = " + id);

            foreach (DataRow row in table.Rows)
            {
                UsersModel item = new UsersModel((int)row["Id"], (string)row["NickName"]);
                return item;
            }
            return null;
        }

        public static List<UsersModel> GetList()
        {
            List<UsersModel> list = new List<UsersModel>();

            DataTable table = App_Start.Db.GetTable("SELECT * FROM Users");

            foreach (DataRow row in table.Rows)
            {
                UsersModel item = new UsersModel((int)row["Id"], (string)row["NickName"]);
                list.Add(item);
            }
            return list;
        }

        public static string Hash(string input)
        {
            return Convert.ToBase64String((new SHA512CryptoServiceProvider()).ComputeHash(Encoding.UTF8.GetBytes(input)));

            //return BCrypt.Net.BCrypt.HashString(input);
        }
    }
}