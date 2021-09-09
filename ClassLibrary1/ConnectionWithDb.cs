using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using ClassLibrary1.Model;
using Dapper;
using log4net;
using WebApplication1.Models;

namespace ClassLibrary1
{
    public static class ConnectionWithDb
    {
        private static ILog log = LogManager.GetLogger("ConnectionWithDb");
        public static string BackSideConnection()
        {
            log.Debug("connection with database");
            string constr = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ToString();
            return constr;
        }

        public static List<UserBalance> GetInfoFromDB(int UserId)
        {

            List<UserBalance> UbL = new List<UserBalance>();
            using (IDbConnection db = new SqlConnection(BackSideConnection()))
            {
                UbL = db.Query<UserBalance>($"Select * From UserBalance where UserId = {UserId}").ToList();
            }
            return UbL;
        }

        public static UserInformation GetUserInfo(string UserName, string HashedPass)
        {
            DynamicParameters Dp = new DynamicParameters();
            Dp.Add("UserName", UserName);
            Dp.Add("Password", HashedPass.ToString());
            using (IDbConnection db = new SqlConnection(BackSideConnection()))
            {
                UserInformation Ui = db.Query<UserInformation>("CheckUserValidation", Dp, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return Ui;
            }
            
        }
    }
}
