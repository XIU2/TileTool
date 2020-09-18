using System;
using System.Diagnostics;
using Microsoft.Win32;

namespace Registry_cs
{
    public class Registry_RightClickMenu
    {
        /// <summary>
        /// 检查是否添加右键菜单
        /// </summary>
        /// <param name="Name">名称</param>
        /// <param name="Extension">文件后缀类型，为空是指所有文件类型，例：exe</param>
        /// <returns>存在返回 true，反之 false</returns>
        public static bool Check(string Name, string Extension = "")
        {
            if (Extension == "")
            {
                if (Registry.GetValue(@"HKEY_CLASSES_ROOT\*\shell\" + Name + @"\command\", "", null) == null)
                {
                    return false;
                }
                return true;
            }
            else
            {
                if (Registry.GetValue(@"HKEY_CLASSES_ROOT\" + Extension + @"file\shell\" + Name + @"\command\", "", null) == null)
                {
                    return false;
                }
                return true;
            }
        }
        /// <summary>
        /// 添加右键菜单
        /// </summary>
        /// <param name="Name">名称</param>
        /// <param name="Path">程序路径</param>
        /// <param name="NoIcon">不添加图标，默认为 true（即不添加）</param>
        /// <param name="Icon">图标路径，为空时使用程序路径</param>
        /// <param name="Extension">文件后缀类型，为空是指所有文件类型，例：exe</param>
        /// <param name="command">命令行，可空</param>
        /// <returns>添加成功返回 true，反之 false</returns>
        public static bool Add(string Name, string Path, bool NoIcon = true, string Icon = "", string Extension = "", string command = "")
        {
            if (command != "") // 如果命令行不等于空，则尾部追加空格
            {
                command += " ";
            }
            Name = Name.Replace(@"\", ""); // 删除名称中的反斜杠：\
            if (Extension == "")
            {
                // 后缀为空，则添加至 * 注册表（全部文件）
                RegistryKey Temp_Software = Registry.ClassesRoot.CreateSubKey(@"*\shell\" + Name, true); // 创建项
                RegistryKey Temp_Command = Registry.ClassesRoot.CreateSubKey(@"*\shell\" + Name + @"\command"); // 创建项
                Temp_Command.SetValue("", '"' + Path + '"' + command + " " + '"' + "%1" + '"'); // 创建 command 默认值
                Temp_Command.Close(); // 关闭 Name\command
                if (NoIcon == false) // 是否添加图标，默认否
                {
                    if (Icon == "")
                    {
                        Temp_Software.SetValue("Icon", '"' + Path + '"'); // 为空时，则使用程序路径
                    }
                    else
                    {
                        Temp_Software.SetValue("Icon", '"' + Icon + '"'); // 不为空，则使用图标路径
                    }
                    Temp_Software.Close();// 关闭 Name
                }
                if (Check(Name, Extension))
                {
                    return true; // 如果注册表存在，则返回真
                }
                else
                {
                    return false; // 如果注册表不存在，则返回假
                }
            }
            else
            {
                // 后缀不为空，则添加至 后缀 注册表（如 exe 文件）
                RegistryKey Temp_Software = Registry.ClassesRoot.CreateSubKey(Extension + @"file\shell\" + Name, true); // 创建项
                RegistryKey Temp_Command = Registry.ClassesRoot.CreateSubKey(Extension + @"file\shell\" + Name + @"\command"); // 创建项
                Temp_Command.SetValue("", '"' + Path + '"' + command + " " + '"' + "%1" + '"'); // 创建 command 默认值
                Temp_Command.Close(); // 关闭 Name\command
                if (NoIcon == false) // 是否添加图标，默认否
                {
                    if (Icon == "")
                    {
                        Temp_Software.SetValue("Icon", '"' + Path + '"'); // 为空时，则使用程序路径
                    }
                    else
                    {
                        Temp_Software.SetValue("Icon", '"' + Icon + '"'); // 不为空，则使用图标路径
                    }
                    Temp_Software.Close();// 关闭 Name
                }
                if (Check(Name, Extension))
                {
                    return true; // 如果注册表存在，则返回真
                }
                else
                {
                    return false; // 如果注册表不存在，则返回假
                }
            }
        }
        /// <summary>
        /// 删除右键菜单
        /// </summary>
        /// <param name="Name">名称</param>
        /// <param name="Extension">文件后缀类型，为空是指所有文件类型，例：exe</param>
        /// <returns>删除成功返回 true，反之 false</returns>
        public static bool Del(string Name, string Extension = "") // 删除右键菜单，返回：真/假，参数：名称、后缀
        {
            if (Check(Name, Extension))
            {
                RegistryKey Temp_Key = Registry.ClassesRoot;
                if (Extension == "")
                {
                    Temp_Key.DeleteSubKey(@"*\shell\" + Name + @"\command", true);
                    Temp_Key.DeleteSubKey(@"*\shell\" + Name, true);
                    Temp_Key.Close();
                    if (Check(Name, Extension))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                Temp_Key.DeleteSubKey(Extension + @"file\shell\" + Name + @"\command", true);
                Temp_Key.DeleteSubKey(Extension + @"file\shell\" +  Name, true);
                Temp_Key.Close();
                if (Check(Name, Extension))
                {
                    return false;
                } 
            }
            return true;
        }
    }
    public class Registry_SystemColor
    {
        /// <summary>
        /// 获取磁贴主题色
        /// </summary>
        /// <returns>HEX颜色值（不带#），如果系统模式为浅色，则直接返回白色</returns>
        public static string Get_SystemColor()
        {
            // 如果系统是浅色模式，则磁贴背景颜色强制为白色 #EFEFEF
            if (Get_SystemUsesLightTheme() == true)
            {
                return "EFEFEF";
            }
            // 如果没有勾选(开启)[开始菜单、任务栏、操作中心]，则磁贴背景颜色强制为黑色 #353535
            else if (Get_SystemColorPrevalence() == false)
            {
                return "353535";
            }
            return ((int)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "ColorizationColor", 1052688)).ToString("x8").Substring(2);
        }
        /// <summary>
        /// 获取系统主题模式（浅色/深色）
        /// </summary>
        /// <returns>浅色模式返回 true，深色模式返回 false</returns>
        public static bool Get_SystemUsesLightTheme()
        {
            /*if (Convert.ToInt32(Registry_SystemVersion.Get_ReleaseId()) < 1903)
            {
                return false;
            }*/
            // 浅色模式下开始菜单磁贴背景颜色强制为白色（浅色为1，深色为 0）
            if ((int)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "SystemUsesLightTheme", 0) == 1)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 获取开始菜单(磁贴)、任务栏、操作中心等是否使用主题色
        /// </summary>
        /// <returns>勾选(开启)返回 true，未勾选(关闭)返回 false</returns>
        public static bool Get_SystemColorPrevalence()
        {
            if (Get_SystemUsesLightTheme() == true)
            {
                return false;
            }
            // 判断是否勾选(开启)[开始菜单、任务栏、操作中心]，勾选后磁贴背景颜色为主题色（开启为1，关闭为 0）
            if ((int)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", 0) == 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 获取系统是否开启透明效果
        /// </summary>
        /// <returns>开启返回 true，关闭返回 false</returns>
        public static bool Get_EnableTransparency()
        {
            // 判断是否勾选(开启)[透明效果]（开启为1，关闭为 0）
            if ((int)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", 0) == 0)
            {
                return false;
            }
            return true;
        }
    }
    public class Registry_SystemVersion
    {
        /// <summary>
        /// 获取系统显示版本（例：2004）
        /// </summary>
        /// <returns>返回系统显示版本（例：2004）或 null</returns>
        public static string Get_DisplayVersion()
        {
            return (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "DisplayVersion", null);
        }
        /// <summary>
        /// 获取系统发行ID（例：2004）
        /// </summary>
        /// <returns>返回系统发行ID（例：2004）或 null</returns>
        public static string Get_ReleaseId()
        {
            return (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", null);
        }
        /// <summary>
        /// 获取系统当前版本号（例：19041）
        /// </summary>
        /// <returns>返回系统当前版本号（例：19041）或 null</returns>
        public static string Get_CurrentBuild()
        {
            return (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentBuild", null);
        }
        /// <summary>
        /// 获取系统当前内部版本号（例：19041）
        /// </summary>
        /// <returns>返回系统当前内部版本号（例：19041）或 null</returns>
        public static string Get_CurrentBuildNumber()
        {
            return (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentBuildNumber", null);
        }
    }
    public class Registry_Other
    {
        /// <summary>
        /// 获取系统是否开启 2004 新版磁贴样式
        /// </summary>
        /// <returns>开启返回 true 关闭返回 false</returns>
        public static bool Get_NewTileState()
        {
            if (Convert.ToInt32(Registry_SystemVersion.Get_ReleaseId()) < 2004)
            {
                return false;
            }
            Debug.Print(Registry.GetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\FeatureManagement\Overrides\0\2093230218", "EnabledState", null).ToString());
            if (Registry.GetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\FeatureManagement\Overrides\0\2093230218", "EnabledState", null) == null)
            {
                return false;
            }
            return true;
        }
    }
}
