using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ClassLibrary1.Login
{
    public static class UserEquality
    {
        public static int CheckUserName(string userName)
        {
            using (IDbConnection db = new SqlConnection(ConnectionWithDb.BackSideConnection()))
            {
                DynamicParameters dp = new DynamicParameters();
                dp.Add("UserName", userName);
                return db.Query<int>("GetUserIdByUserName", dp, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
    }
}
