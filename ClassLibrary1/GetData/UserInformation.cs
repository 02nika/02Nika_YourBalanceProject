using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ClassLibrary1.GetData
{
    public static class UserInformation
    {
        public static bool UserExist(string UserName)
        {
            using (IDbConnection db = new SqlConnection(ConnectionWithDb.BackSideConnection()))
            {
                DynamicParameters dp = new DynamicParameters();
                dp.Add("UserName", UserName);
                string answer = db.Query<string>("IfUserExist", dp, commandType: CommandType.StoredProcedure).FirstOrDefault();

                if (answer != null)
                    return true;
                return false;
            }
        }
    }
}
