using ClassLibrary1.Model;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ClassLibrary1.Login
{
    static public class RegisterNewUser
    {
        public static int Add(RegistrationInfo registrationInfo)
        {
            using (IDbConnection db = new SqlConnection(ConnectionWithDb.BackSideConnection()))
            {
                return db.Query<int>("addNewUserInformation", registrationInfo, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
    }
}
