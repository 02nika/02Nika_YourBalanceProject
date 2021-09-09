using Dapper;
using System.Data;
using System.Data.SqlClient;
using WebApplication1.Models;

namespace ClassLibrary1.InserOps
{
    public static class InsertIntoBalanceTable
    {
        public static void InsertB(UserBalance userBalance)
        {
            using (IDbConnection db = new SqlConnection(ConnectionWithDb.BackSideConnection()))
            {
                db.Query<UserBalance>("addIntoBalance1", userBalance, commandType: CommandType.StoredProcedure);
            }
        }
        public static void ChangeBalance(UserBalance userBalance)
        {
            using (IDbConnection db = new SqlConnection(ConnectionWithDb.BackSideConnection()))
            {
                db.Query<UserBalance>("ChangeUserBalance", userBalance, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
