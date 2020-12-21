using IWshRuntimeLibrary;
using System.Diagnostics;

namespace File_cs
{
    public class Shortcut
    {
        /// <summary>
        /// 获取快捷方式目标路径
        /// </summary>
        /// <param name="Shortcut_Path">快捷方式路径</param>
        /// <returns>成功返回 快捷方式目标路径，失败返回 null</returns>
        public static string Get_Shortcut_TargetPath(string Shortcut_Path)
        {
            if (System.IO.File.Exists(Shortcut_Path))
            {
                WshShell Shell = new WshShell();
                IWshShortcut Shortcut = (IWshShortcut)Shell.CreateShortcut(Shortcut_Path);
                return Shortcut.TargetPath;
            }
            return null;
        }

        /// <summary>
        /// 从快捷方式数组中匹配目标路径
        /// </summary>
        /// <param name="Shortcut_Path">快捷方式路径，字符串数组</param>
        /// <param name="Path">目标路径</param>
        /// <returns>成功返回 匹配目标路径的快捷方式路径，失败返回 null</returns>
        public static string Get_Shortcut_TargetPath_Array(string[] Shortcut_Path, string Path)
        {
            if (Shortcut_Path.Length > 0)
            {
                foreach (string Temp_Shortcut_Path in Shortcut_Path)
                {
                    //Debug.Print(Temp_Shortcut_Path);
                    if (Get_Shortcut_TargetPath(Temp_Shortcut_Path) == Path)
                    {
                        return Temp_Shortcut_Path;
                    }
                }
            }
            return null;
        }
        
        /// <summary>
        /// 创建快捷方式
        /// </summary>
        /// <param name="LinkPath">快捷方式路径</param>
        /// <param name="TargetPath">目标路径</param>
        /// <param name="IconPath">图标路径，为空时使用目标程序的图标</param>
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
        /// <summary>
        /// 枚举文件
        /// </summary>
        /// <param name="Path">欲寻找的路径</param>
        /// <param name="FileName">欲寻找的文件名，支持通配符，例：*.lnk</param>
        /// <param name="Traversal">是否遍历子目录</param>
        /// <returns>返回 找到的文件数组（完整路径）</returns>
        public static string[] File_Enumeration(string Path, string FileName, bool Traversal)
        {
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
