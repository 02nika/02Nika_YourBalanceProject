using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ClassLibrary1.InserOps
{
    public static class InsertLoginT
    {
        public static int InsertToken(int Userid)
        {
            using (IDbConnection db = new SqlConnection(ConnectionWithDb.BackSideConnection()))
            {
                DynamicParameters dp = new DynamicParameters();
                dp.Add("UserId", Userid);
                dp.Add("Output1", dbType: DbType.Int32, direction: ParameterDirection.Output);
                db.Query("GenerateLoginUserToken", dp, commandType: CommandType.StoredProcedure).FirstOrDefault();
                int Out1 = dp.Get<int>("Output1");
                return Out1;
            }
        }
    }
}
