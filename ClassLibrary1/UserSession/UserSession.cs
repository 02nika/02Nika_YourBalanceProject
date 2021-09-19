using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.UserSession
{
    public static class UserSession
    {
        public static bool CheckUserSessionValidation()
        {
            if (GetUserIdFromWebConfiguration.CheckTokenValidation(Int32.Parse(GetUserIdFromWebConfiguration.GetInfo())) == 0)
            {
                GetUserIdFromWebConfiguration.ClearInfo();
                return false;
            }
            return true;
        }
    }
}
