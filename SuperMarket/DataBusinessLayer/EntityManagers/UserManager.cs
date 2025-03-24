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
    public static class UserManager
    {
        public static UserList GetAll()
        {
            DataTable dt = DBManger.GetQueryResult("SELECT * FROM User");
            return MapFromDTtoUserList(dt);
        }
        public static User GetById(int id)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@ID", id);
            DataTable dt = DBManger.GetQueryResult("SELECT top 1* FROM User WHERE ID=@ID", parameters);
            User user = MapFromDataRowtoUser(dt.Rows[0]);
            return user;
        }
        public static User GetByEmail(string email)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Email", email);
            DataTable dt = DBManger.GetQueryResult("SELECT top 1* FROM User WHERE Email=@Email", parameters);
            User user = MapFromDataRowtoUser(dt.Rows[0]);
            return user;
        }
        public static int Insert(User user)
        {
            string cmdText = "INSERT INTO User (FName, LName, Email, Password, Role) VALUES (@FName, @LName, @Email, @Password, @Role)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@FName", user.FName),
                new SqlParameter("@LName", user.LName),
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@Password", user.Password),
                new SqlParameter("@Role", user.Role)
            };
            return DBManger.ExecuteNonQuery(cmdText, parameters);
        }
        public static int Update(User user)
        {
            string cmdText = "UPDATE User SET FName=@FName, LName=@LName, Email=@Email, Password=@Password, Role=@Role WHERE ID=@ID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ID", user.ID),
                new SqlParameter("@FName", user.FName),
                new SqlParameter("@LName", user.LName),
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@Password", user.Password),
                new SqlParameter("@Role", user.Role)
            };
            return DBManger.ExecuteNonQuery(cmdText, parameters);
        }
        public static int Delete(int id)
        {
            string cmdText = "DELETE FROM User WHERE ID=@ID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ID", id)
            };
            return DBManger.ExecuteNonQuery(cmdText, parameters);
        }
        static UserList MapFromDTtoUserList(DataTable dt)
        {
            UserList userList = new UserList();
            foreach (DataRow dr in dt.Rows)
            {
                userList.Add(MapFromDataRowtoUser(dr));
            }
            return userList;
        }
        static User MapFromDataRowtoUser(DataRow dr)
        {
            User user = new User();
            user.ID = (int)dr["ID"];
            user.FName = dr["Name"].ToString();
            user.LName = dr["LName"].ToString();
            user.Email = dr["Email"].ToString();
            user.Password = dr["Password"].ToString();
            user.Role = dr["Role"].ToString();
            return user;
        }
    }
}
