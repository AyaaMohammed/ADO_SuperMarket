using DataAccessLayer;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBusinessLayer
{
    public static class CategoryManager
    {
        public static CategoryList GetAll()
        {
            DataTable dt =DBManger.GetQueryResult("SELECT * FROM Category");
            return MapFromDTtoCategList(dt);
        }
        public static List<KeyValuePair<int, string>> GetAllNames()
        {
            DataTable dt = DBManger.GetQueryResult("SELECT Id, Name FROM Category"); 
            List<KeyValuePair<int, string>> names = new List<KeyValuePair<int, string>>();

            foreach (DataRow dr in dt.Rows)
            {
                names.Add(new KeyValuePair<int, string>(Convert.ToInt32(dr["Id"]), dr["Name"].ToString()));
            }

            return names;
        }
        public static string GetById(int id)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@ID", id);
            DataTable dt = DBManger.GetQueryResult("SELECT top 1 * FROM Category WHERE ID=@ID", parameters);
            Category category = MapFromDataRowtoCateg(dt.Rows[0]);
            return category.Name;
        }
        public static int Insert(Category category)
        {
            string cmdText = "INSERT INTO Category (Name, Image) VALUES (@Name, @Image)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Name", category.Name),
                new SqlParameter("@Image", category.Image)
            };
            return DBManger.ExecuteNonQuery(cmdText, parameters);
        }
        public static int Update(Category category)
        {
            string cmdText = "UPDATE Category SET Name=@Name, Image=@Image WHERE ID=@ID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ID", category.ID),
                new SqlParameter("@Name", category.Name),
                new SqlParameter("@Image", category.Image)
            };
            return DBManger.ExecuteNonQuery(cmdText, parameters);
        }
        public static int Delete(int id)
        {
            string cmdText = "DELETE FROM Category WHERE ID=@ID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ID", id)
            };
            return DBManger.ExecuteNonQuery(cmdText, parameters);
        }
        public static CategoryList SearchByName(string name)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Name", name);
            DataTable dt = DBManger.GetQueryResult("SELECT * FROM Category WHERE Name LIKE @Name", parameters);
            return MapFromDTtoCategList(dt);
        }
        public static int GetCount()
        {
            int count = DBManger.ExecuteScalar<int>("SELECT COUNT(*) FROM Category");
            return count;
        }
        static CategoryList MapFromDTtoCategList(DataTable dt)
        {
            CategoryList categoryList = new CategoryList();
            foreach (DataRow dr in dt.Rows)
            {
                categoryList.Add(MapFromDataRowtoCateg(dr));
            }
            return categoryList;
        }
        static Category MapFromDataRowtoCateg(DataRow dr)
        {
            Category category = new Category();
            category.ID= Convert.ToInt32(dr["ID"]);
            category.Name = dr["Name"].ToString();
            category.Image = dr["Image"].ToString();    
            return category;
        }

    }
}
