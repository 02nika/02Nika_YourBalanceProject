using ClassLibrary1.Model;
using Dapper;
using log4net;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ClassLibrary1.GetData
{
    public static class UserInformation
    {
        private static ILog log = LogManager.GetLogger("HomeController");
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

        public static List<SearchUser> GetSearchResult(int UserId, string SearchResult)
        {
            using (IDbConnection db = new SqlConnection(ConnectionWithDb.BackSideConnection()))
            {
                log.Debug($"this user: {UserId}  ---  Searching this: {SearchResult}");
                DynamicParameters dp = new DynamicParameters();
                dp.Add("UserId", UserId);
                dp.Add("SearchResult", SearchResult);
                List<SearchUser> lOUI = (List<SearchUser>)db.Query<SearchUser>("GetUserSearchResult", dp, commandType: CommandType.StoredProcedure);
                return lOUI;
            }
        }
    }
}
