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
    public static class FavoritesManager
    {
        public static FavoriteList GetAll()
        {
            DataTable dt = DBManger.GetQueryResult("SELECT * FROM Favorites");
            return MapFromDTtoFavList(dt);
        }
        public static Favorites GetById(int id)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@ID", id);
            DataTable dt = DBManger.GetQueryResult("SELECT top 1* FROM Favorites WHERE ID=@ID", parameters);
            Favorites favorites = MapFromDataRowtoFav(dt.Rows[0]);
            return favorites;
        }
        public static FavoriteList GetByUserId(int userId)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@UserID", userId);
            DataTable dt = DBManger.GetQueryResult("SELECT * FROM Favorites WHERE UserID=@UserID", parameters);
            return MapFromDTtoFavList(dt);
        }
        public static FavoriteList GetByProductId(int productId)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@ProductID", productId);
            DataTable dt = DBManger.GetQueryResult("SELECT * FROM Favorites WHERE ProductID=@ProductID", parameters);
            return MapFromDTtoFavList(dt);
        }
        public static Favorites GetbyUserProductID(int userId, int productId)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@UserID", userId);
            parameters[1] = new SqlParameter("@ProductID", productId);
            DataTable dt = DBManger.GetQueryResult("SELECT top 1* FROM Favorites WHERE UserID=@UserID AND ProductID=@ProductID", parameters);
            return MapFromDataRowtoFav(dt.Rows[0]);
        }
        public static FavoriteList GetByDate(DateTime date)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Created_At", date);
            DataTable dt = DBManger.GetQueryResult("SELECT * FROM Favorites WHERE Created_At=@Created_At", parameters);
            return MapFromDTtoFavList(dt);
        }
        public static int Insert(Favorites favorites)
        {
            string cmdText = "INSERT INTO Favorites (Created_At, UserID, ProductID) VALUES (@Created_At, @UserID, @ProductID)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Created_At", favorites.Created_At),
                new SqlParameter("@UserID", favorites.UserID),
                new SqlParameter("@ProductID", favorites.ProductID)
            };
            return DBManger.ExecuteNonQuery(cmdText, parameters);
        }
        public static int Update(Favorites favorites)
        {
            string cmdText = "UPDATE Favorites SET Created_At=@Created_At, UserID=@UserID, ProductID=@ProductID WHERE ID=@ID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ID", favorites.ID),
                new SqlParameter("@Created_At", favorites.Created_At),
                new SqlParameter("@UserID", favorites.UserID),
                new SqlParameter("@ProductID", favorites.ProductID)
            };
            return DBManger.ExecuteNonQuery(cmdText, parameters);
        }
        public static int Delete(int id)
        {
            string cmdText = "DELETE FROM Favorites WHERE ID=@ID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ID", id)
            };
            return DBManger.ExecuteNonQuery(cmdText, parameters);
        }

        static FavoriteList MapFromDTtoFavList(DataTable dt)
        {
            FavoriteList favoritesList = new FavoriteList();
            foreach (DataRow dr in dt.Rows)
            {
                favoritesList.Add(MapFromDataRowtoFav(dr));
            }
            return favoritesList;
        }
        static Favorites MapFromDataRowtoFav(DataRow dr)
        {
            Favorites favorites = new Favorites();
            favorites.ID = Convert.ToInt32(dr["Id"]);
            favorites.Created_At = Convert.ToDateTime(dr["Created_At"]);
            favorites.UserID = Convert.ToInt32(dr["UserID"]);
            favorites.ProductID = Convert.ToInt32(dr["ProductID"]);
            return favorites;
        }
    }
}
