using ClassLibrary1.Model;
using System.Security.Cryptography;
using System.Text;

namespace ClassLibrary1.Login
{
    public static class CheckLoginInfo
    {
        public static UserInformation UserExist(string UserName, string NoneHashedPass)
        {
            string hashedPass = ComputeSha256Hash(NoneHashedPass);
            return ConnectionWithDb.GetUserInfo(UserName, hashedPass);
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
