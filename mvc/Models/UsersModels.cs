using System.Collections.Generic;
using System.Data;

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
            List<UsersModel> list = new List<UsersModel>();

            DataTable table = App_Start.Db.GetTable("SELECT * FROM Parties");

            foreach (DataRow row in table.Rows)
            {
                UsersModel item = new UsersModel((int)row["Id"], (string)row["Name"]);
                list.Add(item);
            }
            return list;
        }
    }
}