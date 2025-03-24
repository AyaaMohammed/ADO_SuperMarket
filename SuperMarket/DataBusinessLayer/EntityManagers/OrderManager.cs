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
    public static class OrderManager
    {
        public static OrderList GetAll()
        {
            DataTable dt = DBManger.GetQueryResult("SELECT * FROM [Order]");
            return MapFromDTtorderOist(dt);
        }
        public static Order GetById(int id)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@ID", id);
            DataTable dt = DBManger.GetQueryResult("SELECT top 1* FROM [Order] WHERE ID=@ID", parameters);
            Order order = MapFromDataRowtoOrder(dt.Rows[0]);
            return order;
        }
        public static OrderList GetByUserID(int userID)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@UserID", userID);
            DataTable dt = DBManger.GetQueryResult("SELECT * FROM [Order] WHERE UserID=@UserID", parameters);
            return MapFromDTtorderOist(dt);
        }
        public static int CountTotalPrice()
        {
            DataTable dt = DBManger.GetQueryResult("SELECT SUM(TotalPrice) FROM [Order]");
            return Convert.ToInt32(dt.Rows[0][0]);
        }
        public static int CountOrder()
        {
            DataTable dt = DBManger.GetQueryResult("SELECT COUNT(*) FROM [Order]");
            return Convert.ToInt32(dt.Rows[0][0]);
        }
        public static int CountOrderByDate(DateTime date)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Date", date);
            DataTable dt = DBManger.GetQueryResult("SELECT COUNT(*) FROM [Order] WHERE Date=@Date", parameters);
            return Convert.ToInt32(dt.Rows[0][0]);
        }
        public static int Insert(Order order)
        {
            string cmdText = "INSERT INTO [Order] (Date, TotalPrice, Payment_Method, Customer_Name, Customer_Phone, UserID) VALUES (@Date, @TotalPrice, @Payment_Method, @Customer_Name, @Customer_Phone, @UserID)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Date", order.Date),
                new SqlParameter("@TotalPrice", order.TotalPrice),
                new SqlParameter("@Payment_Method", order.Payment_Method),
                new SqlParameter("@Customer_Name", order.Customer_Name),
                new SqlParameter("@Customer_Phone", order.Customer_Phone),
                new SqlParameter("@UserID", order.UserID)
            };
            return DBManger.ExecuteNonQuery(cmdText, parameters);
        }
        public static int Update(Order order) {
            string cmdText = "UPDATE [Order] SET Date=@Date, TotalPrice=@TotalPrice, Payment_Method=@Payment_Method, Customer_Name=@Customer_Name, Customer_Phone=@Customer_Phone, UserID=@UserID WHERE ID=@ID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ID", order.ID),
                new SqlParameter("@Date", order.Date),
                new SqlParameter("@TotalPrice", order.TotalPrice),
                new SqlParameter("@Payment_Method", order.Payment_Method),
                new SqlParameter("@Customer_Name", order.Customer_Name),
                new SqlParameter("@Customer_Phone", order.Customer_Phone),
                new SqlParameter("@UserID", order.UserID)
            };
            return DBManger.ExecuteNonQuery(cmdText, parameters);
        }
        public static int Delete(int id)
        {
            string cmdText = "DELETE FROM [Order] WHERE ID=@ID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ID", id)
            };
            return DBManger.ExecuteNonQuery(cmdText, parameters);
        }

        static OrderList MapFromDTtorderOist(DataTable dt)
        {
            OrderList orderlist = new OrderList();
            foreach (DataRow dr in dt.Rows)
            {
                orderlist.Add(MapFromDataRowtoOrder(dr));
            }
            return orderlist;
        }
        static Order MapFromDataRowtoOrder(DataRow dr)
        {
            Order order = new Order();
            order.ID = Convert.ToInt32(dr["ID"]);
            order.Date = Convert.ToDateTime(dr["Date"]);
            order.TotalPrice = Convert.ToSingle(dr["TotalPrice"]);
            order.Payment_Method = dr["Payment_Method"].ToString();
            order.Customer_Name = dr["Customer_Name"].ToString();
            order.Customer_Phone = dr["Customer_Phone"].ToString();
            order.UserID = Convert.ToInt32(dr["UserID"]);
            return order;
        }
    }
}
