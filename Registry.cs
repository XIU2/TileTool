using Microsoft.Win32;

namespace Registry_cs
{
    public class Registry_RightClickMenu
    {
        public static bool Check(string Name, string Extension = "") // 检查是否添加右键菜单，返回：真/假，参数：名称、后缀
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
        public static bool Add(string Name, string Path, bool NoIcon = true, string Icon = "", string Extension = "", string command = "") // 添加右键菜单，返回：真/假，参数：名称、程序路径、不添加图标、图标路径、后缀、命令行
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
}
