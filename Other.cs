using System.Security.Principal;

namespace Other_cs
{
    public class Other
    {
        /// <summary>
        /// 检查当前程序是否以管理员身份运行
        /// </summary>
        /// <returns>当前程序拥有管理员权限返回 true，反之 false</returns>
        public static bool IsRunAsAdmin()
        {
            WindowsIdentity id = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(id);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
