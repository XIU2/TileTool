using System.IO;
using System.Text;
using System.Configuration;

namespace AppConfig_cs
{
    public class AppConfig
    {
        public static string GetValue(string Key, string Default, string ConfigPath) // 读取程序自身配置文件，返回：值，参数：项、默认值、配置文件路径
        {
            if (!File.Exists(ConfigPath))
            {
                File.WriteAllText(ConfigPath, 磁贴美化小工具.Properties.Resources.AppConfig, Encoding.UTF8);
            }
            Configuration App_Config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (App_Config.AppSettings.Settings[Key] == null || App_Config.AppSettings.Settings[Key].Value == null)
            {
                return Default;
            }
            return App_Config.AppSettings.Settings[Key].Value;
        }

        public static void SetValue(string Key, string Value, string ConfigPath) // 写出程序自身配置文件，无返回，参数：项、值、配置文件路径
        {
            if (!File.Exists(ConfigPath))
            {
                File.WriteAllText(ConfigPath, 磁贴美化小工具.Properties.Resources.AppConfig, Encoding.UTF8);
            }
            Configuration App_Config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if(App_Config.AppSettings.Settings[Key] == null)
            {
                App_Config.AppSettings.Settings.Add(Key, Value);
            }
            else
            {
                App_Config.AppSettings.Settings[Key].Value = Value;
            }
            App_Config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
