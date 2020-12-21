using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using Xiu2.TileTool.Core;

namespace Xiu2.TileTool
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // 判断系统版本
            if (Environment.OSVersion.Version.Major != 10)
            {
                _ = MessageBox.Show($"{AppInfo.Main.AppName}仅支持 Windows10 系统！", "错误", MessageBoxButton.OK);
                Shutdown();
            }
        }
    }
}
