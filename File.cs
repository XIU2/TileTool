using IWshRuntimeLibrary;
using System.Diagnostics;

namespace File_cs
{
    public class Shortcut
    {
        public static string Get_Shortcut_TargetPath(string Shortcut_Path) // 获取快捷方式目标路径，返回：目标路径，参数：快捷方式路径
        {
            if (System.IO.File.Exists(Shortcut_Path))
            {
                WshShell Shell = new WshShell();
                IWshShortcut Shortcut = (IWshShortcut)Shell.CreateShortcut(Shortcut_Path);
                return Shortcut.TargetPath;
            }
            return "";
        }
        
        public static string Get_Shortcut_TargetPath_Array(string[] Shortcut_Path, string Path) // 从数组中匹配快捷方式目标路径，返回：匹配目标的快捷方式路径，参数：快捷方式数组(字符串)、目标路径
        {
            if (Shortcut_Path.Length > 0)
            {
                foreach (string Temp_Shortcut_Path in Shortcut_Path)
                {
                    if (Get_Shortcut_TargetPath(Temp_Shortcut_Path) == Path)
                    {
                        return Temp_Shortcut_Path;
                    }
                }
            }
            return "";
        }
        public static void Create_Shortcut(string LinkPath, string TargetPath, string IconPath = "")
        {
            WshShell shell = new WshShell();
            IWshShortcut Shortcut = (IWshShortcut)shell.CreateShortcut(LinkPath);
            Shortcut.TargetPath = TargetPath;
            if (IconPath != "")
            {
                Shortcut.IconLocation = IconPath;
            }
            Shortcut.Save();
        }
    }
    public class File
    {
        public static string[] File_Enumeration(string Path, string FileName, bool Traversal) // 枚举文件，返回：文件数组(字符串)，参数：欲寻找的路径、欲寻找的文件名(允许通配符 *.lnk)、是否遍历子目录
        {
            if (Path.Substring(Path.Length - 1, 1) != @"\")
            {
                Path += @"\";
            }
            if (Traversal == true)
            {
                return System.IO.Directory.GetFiles(Path, FileName, System.IO.SearchOption.AllDirectories);
            }
            else
            {
                return System.IO.Directory.GetFiles(Path, FileName, System.IO.SearchOption.TopDirectoryOnly);
            }
        }
    }
}
