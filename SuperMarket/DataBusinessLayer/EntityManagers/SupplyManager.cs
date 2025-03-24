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
    public static class SupplyManager
    {
        public static int GetSupplierIDByProductID(int productID)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@ProductID", productID);
            DataTable dt = DBManger.GetQueryResult("SELECT* FROM Supply WHERE ProductID=@ProductID", parameters);
            Supply supplier = MapFromDataRowtoSupplier(dt.Rows[0]);
            return supplier.SupplierID;
        }   
        public static int AddProductToSupplier(int productID, int supplierID, int unitOfPrize)
        {
            string cmdText = "INSERT INTO Supply (ProductID, SupplierID) VALUES (@ProductID, @SupplierID)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ProductID", productID),
                new SqlParameter("@SupplierID", supplierID),
            };
            return DBManger.ExecuteNonQuery(cmdText, parameters);
        }
        public static int UpdateProductToSupplier(int productID, int supplierID, int unitOfPrize)
        {
            string cmdText = "UPDATE Supply set unitOfPrize=@unitOfPrize , SupplierID=@SupplierID  WHERE ProductID=@ProductID ";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ProductID", productID),
                new SqlParameter("@SupplierID", supplierID),
                new SqlParameter("@unitOfPrize", unitOfPrize),
            };
            return DBManger.ExecuteNonQuery(cmdText, parameters);
        }
        static SupplyList MapFromDTtoSupplierList(DataTable dt)
        {
            SupplyList supplierList = new SupplyList();
            foreach (DataRow dr in dt.Rows)
            {
                supplierList.Add(MapFromDataRowtoSupplier(dr));
            }
            return supplierList;
        }
        static Supply MapFromDataRowtoSupplier(DataRow dr)
        {
            Supply supplier = new Supply();
            supplier.ProductID = dr.IsNull("ProductID") ? 0 : Convert.ToInt32(dr["ProductID"]);
            supplier.SupplierID = dr.IsNull("SupplierID") ? 0 : Convert.ToInt32(dr["SupplierID"]);
            supplier.unitOfPrize = dr.IsNull("unitOfPrize") ? 0 : Convert.ToInt32(dr["unitOfPrize"]);
            return supplier;

        }
    }
}
