using ClassLibrary1.Login;
using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ClassLibrary1.Change
{
    public static class ChangeData
    {
        public static void ChangeUserPassword(int UserId, string newPass)
        {
            string newHashedPass = CheckLoginInfo.ComputeSha256Hash(newPass);
            
            using (IDbConnection db = new SqlConnection(ConnectionWithDb.BackSideConnection()))
            {
                DynamicParameters dp = new DynamicParameters();
                dp.Add("UserId", UserId);
                dp.Add("Password", newHashedPass);
                db.Query("ChangeUserHashedPassword", dp, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
    }
}
