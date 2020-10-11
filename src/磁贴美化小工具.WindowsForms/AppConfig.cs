using System.IO;
using System.Text;
using System.Configuration;

namespace AppConfig_cs
{
    public class AppConfig
    {
        /// <summary>
        /// 读取程序自身配置文件
        /// </summary>
        /// <param name="Key">项</param>
        /// <param name="Default">默认值，为空是返回</param>
        /// <param name="ConfigPath">配置文件路径</param>
        /// <returns>成功返回项的值，值为空返回 Default 参数</returns>
        public static string GetValue(string Key, string Default, string ConfigPath)
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

        /// <summary>
        /// 写出程序自身配置文件
        /// </summary>
        /// <param name="Key">项</param>
        /// <param name="Value">值</param>
        /// <param name="ConfigPath">配置文件路径</param>
        public static void SetValue(string Key, string Value, string ConfigPath)
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
