using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using ClassLibrary1.Model;
using Dapper;


namespace ClassLibrary1.InserOps
{
    public static class ImageOps
    {
        public static void AddImage(AddImageModel addImage)
        {
            using (IDbConnection db = new SqlConnection(ConnectionWithDb.BackSideConnection()))
            {
                db.Query("addImagePathAndConnect", addImage, commandType: CommandType.StoredProcedure);
            }
        }

        public static string GetImageByUserId(int UserId)
        {
            using (IDbConnection db = new SqlConnection(ConnectionWithDb.BackSideConnection()))
            {
                DynamicParameters dp = new DynamicParameters();
                dp.Add("UserId", UserId);
                return db.Query<string>("GetImageWithUserId", dp, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public static string RemoveImageByUserId(int UserId)
        {
            using (IDbConnection db = new SqlConnection(ConnectionWithDb.BackSideConnection()))
            {
                DynamicParameters dp = new DynamicParameters();
                dp.Add("UserId", UserId);
                dp.Add("Path", dbType: DbType.String, size: int.MaxValue, direction: ParameterDirection.Output);
                db.Query<string>("RemoveImageWithUserId", dp, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return dp.Get<string>("Path");
                
            }
        }
    }
}
