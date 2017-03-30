using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace mvc.Models
{
    public class PartiesModel
    {
        private int _id;
        private string _name;

        public int Id { get { return _id; } set { _id = value; } }

        public string Name { get { return _name; } set { _name = value; } }

        public PartiesModel(string name): this(0, name) { }


        public PartiesModel(int id): this(id, null) { }

        public PartiesModel(int id, string name)
        {
            _id = id;
            _name = name;
        }

        public static List<PartiesModel> GetList()
        {
            List<PartiesModel> list = new List<PartiesModel>();

            DataTable table = App_Start.Db.GetTable("SELECT * FROM Parties");

            foreach( DataRow row in table.Rows)
            {
                PartiesModel item = new PartiesModel((int) row["Id"], (string) row["Name"]);
                list.Add(item);
            }

            return list;
        }
    }
}