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
    public class Color
    {
        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern int GetDC(int hwnd); // 获取DC
        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern int ReleaseDC(int hwnd, int hdc); // 释放DC
        [System.Runtime.InteropServices.DllImport("gdi32")]
        private static extern int GetPixel(int hdc, int x, int y);
        /// <summary>
        /// 获取屏幕指定像素点HEX颜色值
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <returns>HEX颜色值，不带 #</returns>
        public static string Get_ScreenColor(int x, int y)
        {
            //Debug.Print(x + "," + y); // 把坐标显示到窗口上
            int Temp_hDc = GetDC(0);
            int c = GetPixel(Temp_hDc, x, y);
            int r = (c & 0xFF); // 转换R
            int g = (c & 0xFF00) / 256; // 转换G
            int b = (c & 0xFF0000) / 65536; // 转换B
            //Debug.Print(c.ToString()); // 输出10进制颜色
            //Debug.Print(r.ToString("x").PadLeft(2, '0') + g.ToString("x").PadLeft(2, '0') + b.ToString("x").PadLeft(2, '0')); // 输出16进制颜色
            //Debug.Print(r.ToString() + ',' + g.ToString() + ',' + b.ToString()); // 输出RGB
            ReleaseDC(0, Temp_hDc);
            return (r.ToString("x").PadLeft(2, '0') + g.ToString("x").PadLeft(2, '0') + b.ToString("x").PadLeft(2, '0'));
        }
    }
}
