using ClassLibrary1.Login;
using log4net;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace ClassLibrary1
{
    public static class GetUserIdFromWebConfiguration
    {
        private static ILog log = LogManager.GetLogger("GetUserIdFromWebConfiguration");
        public static string GetInfo()
        {
            Configuration objConfig = WebConfigurationManager.OpenWebConfiguration("~");
            AppSettingsSection objAppsettings = (AppSettingsSection)objConfig.GetSection("appSettings");

            string value1 = objAppsettings.Settings["UserId"].Value;
            log.Debug($"UserId that was used:  {value1}");
            return value1;
        }

        public static void ClearInfo()
        {
            Configuration objConfig = WebConfigurationManager.OpenWebConfiguration("~");
            AppSettingsSection objAppsettings = (AppSettingsSection)objConfig.GetSection("appSettings");
            if (objAppsettings != null)
            {
                objAppsettings.Settings["UserId"].Value = "0";
                objConfig.Save();
            }
        }

        public static void SetInfo(int userId)
        {
            Configuration objConfig = WebConfigurationManager.OpenWebConfiguration("~");
            AppSettingsSection objAppsettings = (AppSettingsSection)objConfig.GetSection("appSettings");
            if (objAppsettings != null)
            {
                objAppsettings.Settings["UserId"].Value = userId.ToString();
                objConfig.Save();
                log.Debug($"New userId was Set: {objAppsettings.Settings["UserId"].Value}");
            }
        }

        public static int CheckTokenValidation(int UserId)
        {
            int returnedUserId = CheckToken.CheckTokenByUserIdAsync(UserId).Result;
            return returnedUserId;
        }
    }
}
