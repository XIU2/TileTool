using System.Security.Principal;

namespace Other_cs
{
    public class Other
    {
        public static bool IsRunAsAdmin() // 判断是否以管理员身份运行，返回：真/假
        {
            WindowsIdentity id = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(id);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
