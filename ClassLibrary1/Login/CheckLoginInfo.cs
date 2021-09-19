using ClassLibrary1.Model;
using Dapper;
using log4net;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ClassLibrary1.Login
{
    public static class CheckLoginInfo
    {
        private static ILog log = LogManager.GetLogger("LoginController");

        public static UserInformation UserExist(string UserName, string NoneHashedPass)
        {
            string hashedPass = ComputeSha256Hash(NoneHashedPass);
            return ConnectionWithDb.GetUserInfo(UserName, hashedPass);
        }
        


        /// <summary>
        /// გადაეცემა ლოგინ ფეიჯის username და არადაჰეშილი პაროლი. 
        /// მეთოდი არადაჰეშილ პარამეტრს გადააქცევს დაჰეშილად და მოძებნის, user თუ არსებობს ბაზაში
        /// თუ იუზერი ბაზაშია, მაშინ კი შექმნის ახალ ტოკენს და დააბრუნებს იუზერის ინფორმაციას
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="NoneHashedPass"></param>
        /// <returns>აბრუნებს იუზერის ინფორმაციას. თუ იუზერი არ მოიძებნა აბრუმნებს null-ს</returns>
        public static UserInformation IfUserExistInsertToken(string UserName, string NoneHashedPass)
        {
            string hashedPass = ComputeSha256Hash(NoneHashedPass);
            DynamicParameters Dp = new DynamicParameters();
            Dp.Add("UserName", UserName);
            Dp.Add("Password", hashedPass.ToString());
            using (IDbConnection db = new SqlConnection(ConnectionWithDb.BackSideConnection()))
            {
                UserInformation Ui = db.Query<UserInformation>("CheckUserValidation", Dp, commandType: CommandType.StoredProcedure).FirstOrDefault();
                
                if (Ui != null)
                {
                    DynamicParameters dp = new DynamicParameters();
                    dp.Add("UserId", Ui.UserId);
                    dp.Add("Output1", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    db.Query("GenerateLoginUserToken", dp, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    log.Info($"New Token generated -- UserId: {Ui.UserId} -- Token: {dp.Get<int>("Output1")}");
                }
                return Ui;
            }

        }
        
        public static string ComputeSha256Hash(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

    }
}
