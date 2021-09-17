using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using log4net;
using System.Threading.Tasks;

namespace ClassLibrary1.Login
{
    public static class CheckToken
    {
        private static ILog log = LogManager.GetLogger("CheckToken");

        public static int CheckTokenByUserId(int UserId)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(ConnectionWithDb.BackSideConnection()))
                {
                    DynamicParameters dp = new DynamicParameters();
                    dp.Add("UserId", UserId);
                    dp.Add("Out1", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    db.Query("CheckAndGetTokenValidationWithUserId", dp, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    int returnedUserId = dp.Get<int>("Out1");
                    return returnedUserId;
                }
            }
            catch
            {
                log.Debug("failed\n\tCheckAndGetTokenValidationWithUserId doesn't worked");
                return default(int);
            }

        }

        public static async Task<int> CheckTokenByUserIdAsync(int UserId)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(ConnectionWithDb.BackSideConnection()))
                {
                    DynamicParameters dp = new DynamicParameters();
                    dp.Add("UserId", UserId);
                    dp.Add("Out1", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    await db.QueryFirstOrDefaultAsync("CheckAndGetTokenValidationWithUserId", dp, commandType: CommandType.StoredProcedure).ConfigureAwait(false); ;
                    int returnedUserId = dp.Get<int>("Out1");
                    return returnedUserId;
                }
            }
            catch
            {
                log.Debug("failed\n\tCheckAndGetTokenValidationWithUserId doesn't worked");
                return default(int);
            }

        }
    }
}
