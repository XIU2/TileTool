using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using Xiu2.TileTool.Core;

namespace Xiu2.TileTool
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // 启动前一个进程实例。
            Mutex? newMutex = null;
            try
            {
#pragma warning disable IDE0067 // 丢失范围之前释放对象
                newMutex = new Mutex(true, "a8851f5e-cbb5-4466-bd72-d95c9bea4dea", out bool createdNew);
#pragma warning restore IDE0067 // 丢失范围之前释放对象
                if (!createdNew)
                {
                    if (args.Length > 0 && args[0] != "")
                    {
                        // 右键菜单启动：杀掉已存在的进程。
                        KillSameNameProcess();
                    }
                    else
                    {
                        // 用户双击启动，打开之前已打开的窗口。
                        var current = Process.GetCurrentProcess();
                        var process = Process.GetProcessesByName(current.ProcessName).FirstOrDefault(x => x.Id != current.Id);
                        if (process != null)
                        {
                            var hwnd = process.MainWindowHandle;
                            ShowWindow(hwnd, 9);
                            SetForegroundWindow(hwnd);
                            return;
                        }
                    }
                }
            }
            catch (Exception)
            {
                // 忽略任何多实例处理相关的异常。
            }

            // 启动自己。
            var app = new App();
            app.InitializeComponent();
            app.Run();
        }

        /// <summary>
        /// 结束同名进程。
        /// </summary>
        private static void KillSameNameProcess()
        {
            var name = AppInfo.Main.AppName;
            // 获取当前进程信息。
            Process currentProcess = Process.GetCurrentProcess();
            // 获取同名进程信息。
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                // 判断该进程主窗口标题是否含有 XXX 字符串。
                if (process.MainWindowTitle.Contains(name))
                {
                    // 如果不是当前进程，就结束它。
                    if (process.Id != currentProcess.Id)
                    {
                        process.Kill();
                    }
                }
            }
        }

        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hwnd, uint nCmdShow);

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
    }
}
