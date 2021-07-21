using System;
using System.IO;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Xml.Linq;
using WebClient_cs;
using FileDropAdmin_cs;
using AppConfig_cs;
using Registry_cs;

namespace 磁贴美化小工具
{
    public partial class Form : System.Windows.Forms.Form
    {
        private readonly string AppConfig_Path = Application.ExecutablePath + ".config"; // 本程序配置文件路径
        readonly string Line = Environment.NewLine; // 换行符
        readonly string[] args; // 启动参数（右键菜单启动）
        private System.Threading.Mutex newMutex; // 互斥体
        public FileDropAdmin FileDroper; // 管理员权限下拖放文件
        private bool IsRunAsAdmin; // 是否为管理员权限
        private bool ScreenColorFlag; // 取色
        private bool NewTileState;
        private string Now_VerInfo; // 软件版本号
        private string SystemColor; // 系统主题色
        private string UserColor; // 用户颜色
        private string Config_Path; // 磁贴配置文件路径
        private string Old_Shortcut_Path; // 快捷方式文件路径
        private string Old_Square150x150Logo_Path; // 旧图片文件路径

        public Form(string[] args)
        {
            InitializeComponent();
            this.args = args;
        }

        private void Form_Load(object sender, EventArgs e) // 主窗口创建前
        {
            // 创建互斥体，防止多开
            newMutex = new System.Threading.Mutex(true, "a8851f5e-cbb5-4466-bd72-d95c9bea4dea", out bool WExist);
            if (WExist == false)
            {
                // 判断是不是右键菜单启动的
                if (args.Length > 0 && args[0] != "")
                {
                    Kill_SameNameProcess();
                }
                else
                {
                    _ = MessageBox.Show("请勿多开！", "注意：", MessageBoxButtons.OK, MessageBoxIcon.Information);//弹出提示信息
                    Environment.Exit(0);
                }
            }

            // 判断系统版本
            if (Environment.OSVersion.Version.Major != 10)
            {
                _ = MessageBox.Show("该软件仅支持 Windows10 系统！", "错误：", MessageBoxButtons.OK);
            }

            // 获取软件当前版本号
            Now_VerInfo = FileVersionInfo.GetVersionInfo(Application.ExecutablePath).FileVersion;
            Now_VerInfo = Now_VerInfo.Substring(0,Now_VerInfo.Length - 2);
            this.Text = "磁贴美化小工具 v" + Now_VerInfo;

            // 检查DLL依赖
            Check_Dll();

            // 获取主题色并设置图片框背景颜色
            SystemColor = "#" + Registry_SystemColor.Get_SystemColor();
            PictureBox_磁贴图片预览.BackColor = ColorTranslator.FromHtml(SystemColor);
            NewTileState = Registry_Other.Get_NewTileState(); // 获取是否开启新版磁贴样式，或系统 2004 版本以上
            //Debug.Print(NewTileState.ToString());

            // 磁贴预览标签背景透明
            Label_磁贴名称预览.BackColor = Color.Transparent;
            Label_磁贴名称预览.Parent = PictureBox_磁贴图片预览;
            Label_磁贴名称预览.Location = new Point(5, 79);

            // 检查是否以管理员身份运行
            IsRunAsAdmin = Other_cs.Other.IsRunAsAdmin();
            if (IsRunAsAdmin)
            {
                this.AllowDrop = false;
                FileDroper = new FileDropAdmin(this);
            }

            // 读入程序配置文件
            Read_AppConfig();

            // 创建线程检查更新
            if (Button_自动检查更新.Text == "自动检查更新 [√]")
            {
                _ = Task.Run(() => Check_Update(false));
            }

            // 检查是否添加右键菜单
            Check_RightClickMenu();

            if (args.Length > 0 && args[0] != "")
            {
                File_DragDrop(args[0]);
                // Debug.Print(args[0]);
            }
        }
        
        private void Kill_SameNameProcess() // 结束同名进程
        {
            // 获取当前进程信息
            Process Now_Process = Process.GetCurrentProcess();
            // 获取同名进程信息
            Process[] ALL_Process = Process.GetProcesses();
            foreach (Process Temp_Process in ALL_Process)
            {
                // 判断该进程主窗口标题是否含有 XXX 字符串
                if (Temp_Process.MainWindowTitle.Contains("磁贴美化小工具 v"))
                {
                    // 如果不是当前进程，就结束它
                    if(Temp_Process.Id != Now_Process.Id)
                    {
                        Temp_Process.Kill();
                    }
                }
            }
        }
        
        private void Recognize_Text_Color(byte r = 0, byte g = 0, byte b = 0) // 智能识别文字颜色
        {
            if (PictureBox_磁贴图片预览.Image == null)
            {
                if (r * 0.299 + g * 0.578 + b * 0.114 >= 168)
                {
                    Label_磁贴名称预览.ForeColor = Color.Black;
                }
                else
                {
                    Label_磁贴名称预览.ForeColor = Color.White;
                }
            }
            else
            {
                // 创建一份磁贴(预览框)背景颜色的 Bitmap，在遇到有透明图层(png)的图片时，透明的像素点以背景颜色为准，保证磁贴颜色识别准确
                Bitmap BackColor = new Bitmap(100, 100);
                Graphics Temp_BackColor = Graphics.FromImage(BackColor);
                Temp_BackColor.Clear(PictureBox_磁贴图片预览.BackColor);
                Temp_BackColor.Dispose();
                Color Back_PixelColor = BackColor.GetPixel(50, 50);

                Bitmap ImageColor = new Bitmap((Bitmap)PictureBox_磁贴图片预览.Image, new Size(100, 100)); // 和图片框一样强制拉伸到 100*100 像素
                double Red = 0, Green = 0, Blue = 0;
                //double Alpha = 0;
                for (int temp_y = 0; temp_y < 30; temp_y++)
                {
                    for (int temp_x = 0; temp_x < 92; temp_x++)
                    {
                        Color Image_PixelColor = ImageColor.GetPixel(5 + temp_x, 65 + temp_y);
                        if (Image_PixelColor.A == 0)
                        {
                            //Alpha += Back_PixelColor.A;
                            Red += Back_PixelColor.R;
                            Green += Back_PixelColor.G;
                            Blue += Back_PixelColor.B;
                            //Debug.Print(Alpha.ToString() + " " + Red.ToString() + " " + Green.ToString() + " " + Blue.ToString() + " " + (Red * 0.299 + Green * 0.578 + Blue * 0.114).ToString());
                        }
                        else
                        {
                            //Alpha += Image_PixelColor.A;
                            Red += Image_PixelColor.R;
                            Green += Image_PixelColor.G;
                            Blue += Image_PixelColor.B;
                        }
                    }
                }
                //BackColor.Dispose();
                //ImageColor.Dispose();
                //Alpha /= 2760;
                Red /= 2760;
                Green /= 2760;
                Blue /= 2760;
                //Debug.Print(Red.ToString() + " " + Green.ToString() + " " + Blue.ToString() + " " + (Red * 0.299 + Green * 0.578 + Blue * 0.114).ToString());
                if (Red * 0.299 + Green * 0.578 + Blue * 0.114 >= 168)
                {
                    Label_磁贴名称预览.ForeColor = Color.Black;
                }
                else
                {
                    Label_磁贴名称预览.ForeColor = Color.White;
                }
            }
        }
        
        private void Form_FormClosing(object sender, FormClosingEventArgs e) // 主窗口退出前
        {
            Write_AppConfig();
        }
        
        private void Read_AppConfig() // 读入程序配置文件
        {
            if (AppConfig.GetValue("AutoCheckUpdate", "true", AppConfig_Path) == "true")
            {
                Button_自动检查更新.Text = "自动检查更新 [√]";
            }
            else
            {
                Button_自动检查更新.Text = "自动检查更新 [×]";
            }
            if (AppConfig.GetValue("LegacyTileMode", "false", AppConfig_Path) == "true")
            {
                Button_旧版磁贴模式.Text = "旧版磁贴模式 [√]";
            }
            else
            {
                Button_旧版磁贴模式.Text = "旧版磁贴模式 [×]";
            }
        }
        
        private void Write_AppConfig() // 写出程序配置文件
        {
            if (Button_自动检查更新.Text == "自动检查更新 [√]")
            {
                AppConfig.SetValue("AutoCheckUpdate", "true", AppConfig_Path);
            }
            else
            {
                AppConfig.SetValue("AutoCheckUpdate", "false", AppConfig_Path);
            }
            if (Button_旧版磁贴模式.Text == "旧版磁贴模式 [√]")
            {
                AppConfig.SetValue("LegacyTileMode", "true", AppConfig_Path);
            }
            else
            {
                AppConfig.SetValue("LegacyTileMode", "false", AppConfig_Path);
            }
        }
        
        private void Form_DragEnter(object sender, DragEventArgs e) // 拖放
        {
            if (IsRunAsAdmin)
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.Link;
            }
        }
        
        private void Form_DragDrop(object sender, DragEventArgs e) // 收到拖放，判断当前是不是以管理员身份运行
        {
            if (IsRunAsAdmin)
            {
                File_DragDrop(((string[])e.Data.GetData(typeof(string[])))[0]);
            }
            else
            {
                File_DragDrop(((string[])e.Data.GetData(DataFormats.FileDrop))[0]);
            }
        }
        
        private bool File_DragDrop(string Drag_File_Path) // 处理拖放进来的文件
        {
            string Drag_File_Path_Extension = Path.GetExtension(Drag_File_Path).ToLower();
            if (Drag_File_Path_Extension == ".exe") // 后缀为应用程序
            {
                Init_Config();
                TextBox_程序路径.Text = Drag_File_Path;
            }
            else if (Drag_File_Path_Extension == ".lnk") // 后缀为快捷方式
            {
                string Shortcut_TargetPath = File_cs.Shortcut.Get_Shortcut_TargetPath(Drag_File_Path);
                if (!System.IO.File.Exists(Shortcut_TargetPath))
                {
                    _ = MessageBox.Show("该快捷方式目标程序不存在！" + Line + Shortcut_TargetPath, "错误：", MessageBoxButtons.OK);
                    return false;
                }
                TextBox_程序路径.Text = Shortcut_TargetPath;
            }
            else if (Drag_File_Path_Extension == ".ico") // 后缀为图标文件
            {
                TextBox_磁贴图标.Text = Drag_File_Path;
            }
            else if (Drag_File_Path_Extension == ".png" || Drag_File_Path_Extension == ".jpg" || Drag_File_Path_Extension == ".jpeg") // 后缀为图片文件
            {
                TextBox_磁贴图片.Text = Drag_File_Path;
            }
            else // 都不是
            {
                _ = MessageBox.Show("不支持 [" + Drag_File_Path_Extension + "] 文件后缀！" + Line + "仅支持后缀：应用程序 [.exe / .lnk]，磁贴图片 [.png / .jpg / .jpeg]，磁贴图标 [.ico]", "错误：", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }
        
        private void Read_Config() // 读入磁贴配置文件
        {
            if (System.IO.File.Exists(Config_Path))
            {
                XDocument XML_xDoc = XDocument.Load(Config_Path);
                XElement XML_Application = XML_xDoc.Element("Application");
                XElement XML_VisualElements = XML_Application.Element("VisualElements");
                //Debug.Print(XML_VisualElements.ToString());
                if (XML_VisualElements.Attribute("BackgroundColor") != null) // 磁贴背景颜色
                {
                    if (Button_旧版磁贴模式.Text == "旧版磁贴模式 [√]" || NewTileState == false) // 如果没有开启新版磁贴样式/开启了旧版磁贴模式，则使用系统主题色
                    {
                        UserColor = XML_VisualElements.Attribute("BackgroundColor").Value.ToString();
                    }
                    else
                    {
                        UserColor = SystemColor;
                    }
                    PictureBox_磁贴图片预览.BackColor = ColorTranslator.FromHtml(UserColor);
                }
                else
                {
                    PictureBox_磁贴图片预览.BackColor = ColorTranslator.FromHtml(SystemColor);
                }
                if (XML_VisualElements.Attribute("ShowNameOnSquare150x150Logo") != null) // 显示/隐藏磁贴文字
                {
                    if (XML_VisualElements.Attribute("ShowNameOnSquare150x150Logo").Value.ToString() == "off")
                    {
                        Button_显示文字.Text = "显示文字";
                    }
                    else
                    {
                        Button_显示文字.Text = "隐藏文字";
                    }
                }
                //XML_VisualElements.Attribute("ForegroundText").Value.ToString();
                if (XML_VisualElements.Attribute("Square150x150Logo") != null) // 磁贴大图
                {
                    string Temp_Square150x150Logo = XML_VisualElements.Attribute("Square150x150Logo").Value.ToString(); // 获取值
                    if (Temp_Square150x150Logo != "") // 如果不等于空，则判断文件是否存在
                    {
                        if (System.IO.File.Exists(Path.GetDirectoryName(TextBox_程序路径.Text) + @"\" + Temp_Square150x150Logo))
                        {
                            TextBox_磁贴图片.Text = Path.GetDirectoryName(TextBox_程序路径.Text) + @"\" + Temp_Square150x150Logo;
                            Old_Square150x150Logo_Path = TextBox_磁贴图片.Text; // 旧图片文件
                        }
                    }
                    else
                    {
                        Old_Square150x150Logo_Path = "";
                    }
                }
                /*if (XML_VisualElements.Attribute("Square70x70Logo") != null) // 磁贴大图
                {
                    string Temp_Square70x70Logo = XML_VisualElements.Attribute("Square70x70Logo").Value.ToString();
                }*/
                if (XML_VisualElements.Attribute("Lnk32x32Logo") != null) // 磁贴图标
                {
                    string Temp_Lnk32x32Logo = XML_VisualElements.Attribute("Lnk32x32Logo").Value.ToString(); // 获取值
                    if (Temp_Lnk32x32Logo != "") // 如果不等于空，则判断文件是否存在
                    {
                        if (System.IO.File.Exists(Temp_Lnk32x32Logo))
                        {
                            TextBox_磁贴图标.Text = Temp_Lnk32x32Logo;
                        }
                    }
                }
            }
            else
            {
                if (TextBox_磁贴图片.Text == "")
                {
                    TextBox_磁贴图标.Text = TextBox_程序路径.Text;
                }
            }
        }
        private void Write_Config() // 写出磁贴配置文件
        {
            string Temp_BackgroundColor;
            string Temp_ShowNameOnSquare150x150Logo;
            string Temp_ForegroundText;
            string Temp_Square150x150Logo;

            // 磁贴背景颜色
            if (UserColor == "" || UserColor == null)
            {
                Temp_BackgroundColor = SystemColor;
            }
            else
            {
                Temp_BackgroundColor = UserColor;
            }

            // 磁贴是否显示文字
            if (Button_显示文字.Text == "显示文字")
            {
                Temp_ShowNameOnSquare150x150Logo = "off";
            }
            else
            {
                Temp_ShowNameOnSquare150x150Logo = "on";
            }

            // 磁贴文字颜色
            if (Label_磁贴名称预览.ForeColor == Color.White)
            {
                Temp_ForegroundText = "light";
            }
            else
            {
                Temp_ForegroundText = "dark";
            }

            // 磁贴图片
            if (TextBox_磁贴图片.Text == "")
            {
                Temp_Square150x150Logo = "";
            }
            else
            {
                Temp_Square150x150Logo = Path.GetFileNameWithoutExtension(TextBox_程序路径.Text) + ".150x150Logo" + Path.GetExtension(TextBox_磁贴图片.Text);
            }
            //Debug.Print(Temp_BackgroundColor + "|" + Temp_ShowNameOnSquare150x150Logo + "|" + Temp_ForegroundText + "|" + Temp_Square150x150Logo + "|" + TextBox_磁贴图标.Text);
            // 开始写入配置
            XElement XML_Application = new XElement("Application",
                    new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"));
            XElement XML_VisualElements = new XElement("VisualElements",
                    new XAttribute("BackgroundColor", Temp_BackgroundColor),
                    new XAttribute("ShowNameOnSquare150x150Logo", Temp_ShowNameOnSquare150x150Logo),
                    new XAttribute("ForegroundText", Temp_ForegroundText),
                    new XAttribute("Square150x150Logo", Temp_Square150x150Logo),
                    new XAttribute("Square70x70Logo", Temp_Square150x150Logo),
                    new XAttribute("Lnk32x32Logo", TextBox_磁贴图标.Text)
                    );
            XML_Application.Add(XML_VisualElements);
            XML_Application.Save(Config_Path); // 保存配置文件文件
            Copy_Img(); // 复制图片文件
        }

        private void Copy_Img() // 复制磁贴图片
        {
            if (TextBox_磁贴图片.Text != "") // 图片路径不等于空时继续
            {
                //Debug.Print(TextBox_磁贴图片.Text);
                //Debug.Print(Old_Square150x150Logo_Path);
                if (TextBox_磁贴图片.Text != Old_Square150x150Logo_Path) // 如果图片已更改，则复制到程序目录下。
                {
                    System.IO.File.Copy(TextBox_磁贴图片.Text, Path.GetDirectoryName(TextBox_程序路径.Text) + @"\" + Path.GetFileNameWithoutExtension(TextBox_程序路径.Text) + ".150x150Logo" + Path.GetExtension(TextBox_磁贴图片.Text), true);
                }
            }
        }

        private void Button_添加磁贴_Click(object sender, EventArgs e) // 添加/更新磁贴
        {
            Write_Config(); // 写出磁贴配置文件
            Add_Tile(); // 固定到开始屏幕、处理磁贴快捷方式
        }
        private void Add_Tile() // 固定到开始屏幕、处理磁贴快捷方式
        {
            if (Old_Shortcut_Path == "") // 旧快捷方式路径等于空，说明是该程序没有固定为磁贴
            {
                Add_Tile_Dll(); // 固定为磁贴
                if(TextBox_磁贴名称.Text != Path.GetFileNameWithoutExtension(TextBox_程序路径.Text)) // 如果磁贴名称与磁贴快捷方式名称不一致，则重命名快捷方式
                {
                    Rename_Shortcut(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Programs) + @"\" + Path.GetFileNameWithoutExtension(TextBox_程序路径.Text) + ".lnk", System.Environment.GetFolderPath(System.Environment.SpecialFolder.Programs) + @"\" + TextBox_磁贴名称.Text + ".lnk");
                }
            }
            else
            {
                System.IO.File.Delete(Old_Shortcut_Path); // 删除旧快捷方式文件，并创建一个新的快捷方式
                File_cs.Shortcut.Create_Shortcut(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Programs) + @"\" + TextBox_磁贴名称.Text + ".lnk", TextBox_程序路径.Text, TextBox_磁贴图标.Text);
            }

            for (int Temp_i = 1; Temp_i < 100; Temp_i += 1) // 循环等待，判断新快捷方式文件是否存在，如果存在则跳出循环
            {
                System.Threading.Thread.Sleep(10);
                if (System.IO.File.Exists(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Programs) + @"\" + TextBox_磁贴名称.Text + ".lnk"))
                {
                    break;
                }
            }
            // 如果新快捷方式不存在（用于判断前面的循环等待是否超时），则提示消息
            if (!System.IO.File.Exists(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Programs) + @"\" + TextBox_磁贴名称.Text + ".lnk"))
            {
                _ = MessageBox.Show("磁贴快捷方式重命名失败！" + Line + "原因：没有找到旧快捷方式或新快捷方式。" + Line + "旧快捷方式：" + System.Environment.GetFolderPath(System.Environment.SpecialFolder.Programs) + @"\" + Path.GetFileNameWithoutExtension(TextBox_程序路径.Text) + ".lnk" + Line + "新快捷方式：" + System.Environment.GetFolderPath(System.Environment.SpecialFolder.Programs) + @"\" + TextBox_磁贴名称.Text + ".lnk" + Line + "程序路径：" + TextBox_程序路径.Text, "错误：", MessageBoxButtons.OK);
            }
            else
            {
                Reload_Tile(); // 重载磁贴
            }
        }

        private void Rename_Shortcut(string OldPath, string NewPath) // 重命名快捷方式
        {
            for (int Temp_i = 1; Temp_i < 100; Temp_i += 1) // 循环等待，判断新快捷方式文件是否存在
            {
                System.Threading.Thread.Sleep(10);
                if (System.IO.File.Exists(OldPath)) // 如果旧快捷方式存在，则重命名 并 跳出循环
                {
                    System.IO.File.Move(OldPath, NewPath);
                    break;
                }
                if (System.IO.File.Exists(NewPath)) // 如果新快捷方式存在(?.?)，则跳出循环
                {
                    break;
                }
            }
        }

        private void Add_Tile_Dll() // 固定磁贴
        {
            Check_Dll(); // DLL 文件检查
            if (IsRunAsAdmin) // 以管理员身份运行时，则以普通用户权限去固定磁贴
            {
                ProcessStartInfo Tile = new ProcessStartInfo
                {
                    //FileName = "runas.exe",
                    FileName = Application.StartupPath + @"\syspin.dll",
                    //WindowStyle = ProcessWindowStyle.Hidden,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    Verb = "explorer",
                    //Arguments = "/trustlevel:0x20000 " + '"' + @"\" + '"' + Application.StartupPath + @"\syspin.dll" + @"\" + '"' + " " + @"\" + '"' + TextBox_程序路径.Text + @"\" + '"' + " 51201" + '"'
                    Arguments = '"' + TextBox_程序路径.Text + '"' + " 51201"
                };
                //Debug.Print(Tile.Arguments);
                Process.Start(Tile);
            }
            else
            {
                ProcessStartInfo Tile = new ProcessStartInfo
                {
                    FileName = Application.StartupPath + @"\syspin.dll",
                    //WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    Arguments = '"' + TextBox_程序路径.Text + '"' + " 51201"
                };
                Process.Start(Tile);
            }
        }

        private void Reload_Tile() // 重载磁贴配置
        {
            string Temp_Path = TextBox_程序路径.Text;
            TextBox_程序路径.Text = "";
            File_DragDrop(Temp_Path);
        }

        private void Init_Config() // 初始化当前载入的配置
        {
            Button_添加磁贴.Enabled = false; // 程序路径等于空时，禁用添加磁贴等按钮
            TextBox_程序路径.Text = TextBox_磁贴名称.Text = TextBox_磁贴图片.Text = TextBox_磁贴图标.Text = Old_Square150x150Logo_Path = "";
            PictureBox_磁贴图片预览.Image = PictureBox_磁贴图标预览.Image = null;
        }
        private void Button_查看_Click(object sender, EventArgs e) // 显示并定位程序路径文件
        {
            if (TextBox_程序路径.Text != "" && System.IO.File.Exists(TextBox_程序路径.Text))
            {
                _ = Process.Start("explorer", "/select," + TextBox_程序路径.Text);
            }
        }
        
        private void Button_初始化_Click(object sender, EventArgs e) // 初始化磁贴配置文件
        {
            if (System.IO.File.Exists(Config_Path))
            {
                if (MessageBox.Show("初始化将删除磁贴配置文件，是否删除？" + Line + "删除后不影响应用程序使用，背景图片及图标文件均不会被删除。", "提示：", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    System.IO.File.Delete(Config_Path);
                    if (System.IO.File.Exists(Config_Path))
                    {
                        _ = MessageBox.Show("删除配置文件失败！" + Line + "一般是因为文件位于 [C盘] 权限敏感目录，请 [以管理员身份运行] 本软件后再初始化磁贴（或手动删除并随意修改下磁贴对应的快捷方式文件名）。" + Line + Line + Config_Path, "错误：", MessageBoxButtons.OK);
                    }
                    else
                    {
                        Reload_Tile(); // 重载磁贴
                    }
                }
            }
        }
        
        private void Button_显示文字_Click(object sender, EventArgs e) // 显示/隐藏磁贴文字
        {
            if (Button_显示文字.Text == "显示文字")
            {
                Button_显示文字.Text = "隐藏文字";
            }
            else
            {
                Button_显示文字.Text = "显示文字";
            }
        }
        private void Button_显示文字_TextChanged(object sender, EventArgs e)
        {
            if (Button_显示文字.Text == "显示文字")
            {
                Label_磁贴名称预览.Visible = false;
            }
            else
            {
                Label_磁贴名称预览.Visible = true;
            }
        }
        
        private void Button_添加右键菜单_Click(object sender, EventArgs e) // 添加右键菜单
        {
            if (IsRunAsAdmin)
            {
                if (Button_添加右键菜单.Text == "添加右键菜单 [√]")
                {
                    if (Registry_RightClickMenu.Del("自定义并固定到" + '"' + "开始" + '"' + "屏幕", "exe"))
                    {
                        Button_添加右键菜单.Text = "添加右键菜单 [×]";
                    }
                    else
                    {
                        _ = MessageBox.Show("删除右键菜单失败！", "错误：", MessageBoxButtons.OK);
                    }

                }
                else
                {
                    if (Registry_RightClickMenu.Add("自定义并固定到" + '"' + "开始" + '"' + "屏幕", Application.ExecutablePath, false, "", "exe", ""))
                    {
                        Button_添加右键菜单.Text = "添加右键菜单 [√]";
                    }
                    else
                    {
                        _ = MessageBox.Show("添加右键菜单失败！", "错误：", MessageBoxButtons.OK);
                    }
                }
            }
            else
            {
                _ = MessageBox.Show("只有 [以管理员身份允许] 时才有权限修改注册表！", "错误：", MessageBoxButtons.OK);
            }
        }
        private void Check_RightClickMenu() // 检查是否添加右键菜单
        {
            if(Registry_RightClickMenu.Check("自定义并固定到" + '"' + "开始" + '"' + "屏幕", "exe"))
            {
                Button_添加右键菜单.Text = "添加右键菜单 [√]";
            }
            else
            {
                Button_添加右键菜单.Text = "添加右键菜单 [×]";
            }
        }

        private void Button_查看磁贴目录_Click(object sender, EventArgs e) // 查看磁贴目录
        {
            _ = Process.Start(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Programs));
        }

        private void Button_旧版磁贴模式_Click(object sender, EventArgs e) // 旧版磁贴模式
        {
            if (Button_旧版磁贴模式.Text == "旧版磁贴模式 [√]")
            {
                Button_旧版磁贴模式.Text = "旧版磁贴模式 [×]";
            }
            else
            {
                Button_旧版磁贴模式.Text = "旧版磁贴模式 [√]";
            }
        }

        private void Button_自动检查更新_Click(object sender, EventArgs e) // 自动检查更新
        {
            if (Button_自动检查更新.Text == "自动检查更新 [√]")
            {
                Button_自动检查更新.Text = "自动检查更新 [×]";
            }
            else
            {
                Button_自动检查更新.Text = "自动检查更新 [√]";
            }
        }
        
        private void Button_检查更新_Click(object sender, EventArgs e) // 立即检查更新
        {
            _ = Task.Run(() => Check_Update(true));
        }
           
        private void Check_Dll() // 检查 syspin.dll 是否存在
        {
            if (!System.IO.File.Exists(Application.StartupPath + @"\syspin.dll"))
            {
                _ = MessageBox.Show("找不到 [syspin.dll] 文件！请检查或重新下载！", "错误：", MessageBoxButtons.OK);
                Environment.Exit(0);
            }
        }
        
        private void Check_Update(bool Tip) // 检查更新
        {
            string strHTML = GetHTTP.Get_HTTP("https://api.xiuer.pw/ver/win10kscdmhxgj.txt", 10000, "utf-8");
            string[] Ver_Info = strHTML.Split('\n');
            if (Ver_Info.Length > 2 && Ver_Info[1] != Now_VerInfo)
            {
                if (MessageBox.Show("发现新版本 [v" + Ver_Info[1] + "]！是否前往更新？", "发现新版本！", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Process.Start(Ver_Info[2]);
                }
            }
            else
            {
                if (Tip == true)
                {
                    MessageBox.Show("当前已是最新版本 " + Now_VerInfo + " ！", "信息：", MessageBoxButtons.OK);
                }
            }
        }
        
        private void TextBox_程序路径_TextChanged(object sender, EventArgs e) // 内容更改后，读入程序路径
        {
            TextBox_程序路径_Leave_2();
            if (TextBox_程序路径.Text != "")
            {
                string Temp_Shortcut_Path;
                string Temp_Shortcut_Name; // 临时快捷方式文件名
                Button_添加磁贴.Enabled = Button_查看.Enabled = Button_初始化.Enabled = true; // 程序路径不等于空时，允许添加磁贴等按钮
                Temp_Shortcut_Path = File_cs.Shortcut.Get_Shortcut_TargetPath_Array(File_cs.File.File_Enumeration(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Programs), "*.lnk", true), TextBox_程序路径.Text);
                // 如果没找到磁贴快捷方式，那就去所有用户的程序目录找下
                if (Temp_Shortcut_Path == null)
                {
                    Temp_Shortcut_Path = File_cs.Shortcut.Get_Shortcut_TargetPath_Array(File_cs.File.File_Enumeration(System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonPrograms), "*.lnk", true), TextBox_程序路径.Text);
                }
                //Debug.Print("Temp_Shortcut_Path " + Temp_Shortcut_Path);
                // 如果找到了磁贴快捷方式，就设置全局变量 快捷方式文件名、快捷方式路径
                if (Temp_Shortcut_Path != null)
                {
                    Old_Shortcut_Path = Temp_Shortcut_Path;
                    //Debug.Print(Old_Shortcut_Path);
                    Temp_Shortcut_Name = Path.GetFileNameWithoutExtension(Temp_Shortcut_Path);
                    //Debug.Print(Temp_Shortcut_Name);
                }
                else
                {
                    Old_Shortcut_Path = Temp_Shortcut_Name = "";
                }
                // 如果快捷方式文件名为空，则磁贴名称设置为程序文件名
                if (Temp_Shortcut_Name != "")
                {
                    TextBox_磁贴名称.Text = Temp_Shortcut_Name;
                    if (TextBox_磁贴名称.Text == "")
                    {
                        TextBox_磁贴名称.Text = Path.GetFileNameWithoutExtension(TextBox_程序路径.Text);
                    }
                }
                else
                {
                    TextBox_磁贴名称.Text = Path.GetFileNameWithoutExtension(TextBox_程序路径.Text);
                }
                //Debug.Print("333" + TextBox_磁贴名称.Text);
                // 设置程序配置文件路径
                Config_Path = Path.GetDirectoryName(TextBox_程序路径.Text) + @"\" + Path.GetFileNameWithoutExtension(TextBox_程序路径.Text) + ".VisualElementsManifest.xml";
                //Debug.Print(Config_Path);
                // 磁贴图片、磁贴图标清空
                TextBox_磁贴图片.Text = TextBox_磁贴图标.Text = "";
                PictureBox_磁贴图片预览.Image = PictureBox_磁贴图标预览.Image = null;
                Read_Config();
            }
        }
        private void TextBox_磁贴名称_TextChanged(object sender, EventArgs e)
        {
            TextBox_磁贴名称_Leave_2();
            Label_磁贴名称预览.Text = TextBox_磁贴名称.Text;
            if (TextBox_磁贴名称.Text == "")
            {
                Button_添加磁贴.Enabled = false; // 磁贴名称等于空时，禁用添加磁贴按钮
            }
            else
            {
                Button_添加磁贴.Enabled = true; // 磁贴名称不等于空时，允许添加磁贴按钮
            }
        }
        private void TextBox_磁贴图片_TextChanged(object sender, EventArgs e) // 内容更改后，读入图片
        {
            if (TextBox_磁贴图片.Text != "")
            {
                if (System.IO.File.Exists(TextBox_磁贴图片.Text))
                {
                    if (PictureBox_磁贴图片预览.Image != null)
                    {
                        PictureBox_磁贴图片预览.Image.Dispose(); // 释放旧资源
                    }
                    FileStream Temp_ImageStream = new FileStream(TextBox_磁贴图片.Text, FileMode.Open, FileAccess.Read); // 打开文件流
                    int Temp_ImageByteLength = (int)Temp_ImageStream.Length;
                    byte[] Temp_ImageFileBytes = new byte[Temp_ImageByteLength];
                    Temp_ImageStream.Read(Temp_ImageFileBytes, 0, Temp_ImageByteLength);
                    Temp_ImageStream.Close(); // 文件流关闭，文件解除锁定
                    PictureBox_磁贴图片预览.Image = Image.FromStream(new MemoryStream(Temp_ImageFileBytes)); // 从流中读入图片
                    //PictureBox_磁贴图片预览.Image = Image.FromFile(TextBox_磁贴图片.Text); // 旧载入方式，会锁定文件
                    Recognize_Text_Color(); // 识别图片颜色
                    TextBox_磁贴图标.Text = "";
                    PictureBox_磁贴图标预览.Image = null;
                    TextBox_磁贴图标_Leave_2();
                    TextBox_磁贴图片_Leave_2();
                    //TextBox_磁贴图片.Focus();
                    //Button_添加磁贴.Focus();
                    PictureBox_磁贴图标预览.Visible = false;
                }
                else
                {
                    TextBox_磁贴图片.Text = "";
                    PictureBox_磁贴图片预览.Image = null;
                }
            }
            else
            {
                TextBox_磁贴图片_Leave_2();
            }
        }
        private void TextBox_磁贴图标_TextChanged(object sender, EventArgs e) // 内容更改后，读入图标
        {
            if(TextBox_磁贴图标.Text != "")
            {
                if (System.IO.File.Exists(TextBox_磁贴图标.Text))
                {
                    if (PictureBox_磁贴图标预览.Image != null)
                    {
                        PictureBox_磁贴图标预览.Image.Dispose(); // 释放旧资源
                    }
                    if (Path.GetExtension(TextBox_磁贴图标.Text).ToLower() == ".exe")
                    {
                        PictureBox_磁贴图标预览.Image = System.Drawing.Icon.ExtractAssociatedIcon(TextBox_磁贴图标.Text).ToBitmap();
                        //PictureBox_磁贴图标预览.Image = Bitmap.FromHicon(System.Drawing.Icon.ExtractAssociatedIcon(TextBox_磁贴图标.Text).Handle); // 旧载入方式，有锯齿
                    }
                    else if(Path.GetExtension(TextBox_磁贴图标.Text).ToLower() == ".ico")
                    {
                        FileStream Temp_IconStream = new FileStream(TextBox_磁贴图标.Text, FileMode.Open, FileAccess.Read); // 打开文件流
                        int Temp_IconByteLength = (int)Temp_IconStream.Length;
                        byte[] Temp_IconFileBytes = new byte[Temp_IconByteLength];
                        Temp_IconStream.Read(Temp_IconFileBytes, 0, Temp_IconByteLength);
                        Temp_IconStream.Close(); // 文件流关闭，文件解除锁定
                        PictureBox_磁贴图标预览.Image = Image.FromStream(new MemoryStream(Temp_IconFileBytes)); // 从流中读入图标

                        //PictureBox_磁贴图标预览.ImageLocation = TextBox_磁贴图标.Text; // 旧载入方式，无锯齿，但会锁定文件
                        //PictureBox_磁贴图标预览.Image = Bitmap.FromHicon(new Icon(TextBox_磁贴图标.Text, 32, 32).Handle); // 旧载入方式，有锯齿
                    }
                    TextBox_磁贴图片.Text = "";
                    PictureBox_磁贴图片预览.Image = null;
                    TextBox_磁贴图片_Leave_2();
                    TextBox_磁贴图标_Leave_2();
                    //TextBox_磁贴图标.Focus();
                    //Button_添加磁贴.Focus();
                    PictureBox_磁贴图标预览.Visible = true;
                }
                else
                {
                    TextBox_磁贴图标.Text = "";
                    PictureBox_磁贴图标预览.Image = null;
                    PictureBox_磁贴图标预览.Visible = false;
                }
            }
            else
            {
                TextBox_磁贴图标_Leave_2();
            }
        }
        
        private void TextBox_磁贴图片_KeyPress(object sender, KeyPressEventArgs e) // 按下退格键清理输入框和图片框，下同
        {
            if (e.KeyChar == 8)
            {
                TextBox_磁贴图片.Text = null;
                PictureBox_磁贴图片预览.Image = null;
                if (TextBox_磁贴图标.Text == "") // 如果图标路径也是空的话，那就指定为程序文件
                {
                    TextBox_磁贴图标.Text = TextBox_程序路径.Text;
                }
            }
        }
        private void TextBox_磁贴图标_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                if (TextBox_磁贴图标.Text != TextBox_程序路径.Text) // 如果图标路径等于程序路径，那就没必要继续无意义动作了
                {
                    TextBox_磁贴图标.Text = null;
                    PictureBox_磁贴图标预览.Image = null;
                    if (TextBox_磁贴图片.Text == "") // 如果图片路径也是空的话，那就指定图标为程序路径
                    {
                        TextBox_磁贴图标.Text = TextBox_程序路径.Text;
                    }
                }
            }
        }
        private void TextBox_程序路径_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                Init_Config();
            }
        }
        
        private void TextBox_程序路径_Enter(object sender, EventArgs e) // 输入框获得焦点后，置顶并修改背景颜色，下同
        {
            TextBox_程序路径.BringToFront();
            TextBox_程序路径.BackColor = Color.Gainsboro;
            Label_程序路径.BackColor = Color.Gainsboro;
        }
        private void TextBox_程序路径_Leave(object sender, EventArgs e) // 输入框失去焦点后，置底并修改背景颜色，下同
        {
            TextBox_程序路径_Leave_2();
        }
        private void TextBox_程序路径_Leave_2()
        {
            if (TextBox_程序路径.Text == "")
            {
                TextBox_程序路径.SendToBack();
            }
            else
            {
                TextBox_程序路径.BringToFront();
            }
            TextBox_程序路径.BackColor = Color.WhiteSmoke;
            Label_程序路径.BackColor = Color.WhiteSmoke;
        }
        private void TextBox_磁贴名称_Enter(object sender, EventArgs e)
        {
            TextBox_磁贴名称.BringToFront();
            TextBox_磁贴名称.BackColor = Color.Gainsboro;
            Label_磁贴名称.BackColor = Color.Gainsboro;
        }
        private void TextBox_磁贴名称_Leave(object sender, EventArgs e)
        {
            TextBox_磁贴名称_Leave_2();
        }
        private void TextBox_磁贴名称_Leave_2()
        {
            if (TextBox_磁贴名称.Text == "")
            {
                TextBox_磁贴名称.SendToBack();
            }
            else
            {
                TextBox_磁贴名称.BringToFront();
            }
            TextBox_磁贴名称.BackColor = Color.WhiteSmoke;
            Label_磁贴名称.BackColor = Color.WhiteSmoke;
        }
        private void TextBox_磁贴图片_Enter(object sender, EventArgs e)
        {
            TextBox_磁贴图片.BringToFront();
            TextBox_磁贴图片.BackColor = Color.Gainsboro;
            Label_磁贴图片.BackColor = Color.Gainsboro;
        }
        private void TextBox_磁贴图片_Leave(object sender, EventArgs e)
        {
            TextBox_磁贴图片_Leave_2();
        }
        private void TextBox_磁贴图片_Leave_2()
        {
            if (TextBox_磁贴图片.Text == "")
            {
                TextBox_磁贴图片.SendToBack();
                if (PictureBox_磁贴图片预览.Image != null)
                {
                    PictureBox_磁贴图片预览.Image.Dispose();
                }
            }
            else
            {
                TextBox_磁贴图片.BringToFront();
            } 
            TextBox_磁贴图片.BackColor = Color.WhiteSmoke;
            Label_磁贴图片.BackColor = Color.WhiteSmoke;
        }
        private void TextBox_磁贴图标_Enter(object sender, EventArgs e)
        {
            TextBox_磁贴图标.BringToFront();
            TextBox_磁贴图标.BackColor = Color.Gainsboro;
            Label_磁贴图标.BackColor = Color.Gainsboro;
        }
        private void TextBox_磁贴图标_Leave(object sender, EventArgs e)
        {
            TextBox_磁贴图标_Leave_2();
        }
        private void TextBox_磁贴图标_Leave_2()
        {
            if (TextBox_磁贴图标.Text == "")
            {
                TextBox_磁贴图标.SendToBack();
                if (PictureBox_磁贴图标预览.Image != null)
                {
                    PictureBox_磁贴图标预览.Image.Dispose();
                }
            }
            else
            {
                TextBox_磁贴图标.BringToFront();
            }
            TextBox_磁贴图标.BackColor = Color.WhiteSmoke;
            Label_磁贴图标.BackColor = Color.WhiteSmoke;
        }
        
        private void Label_程序路径_MouseClick(object sender, MouseEventArgs e) // 点击标签后，置焦点为输入框，下同
        {
            TextBox_程序路径.Focus();
        }
        private void Label_磁贴名称_MouseClick(object sender, MouseEventArgs e)
        {
            TextBox_磁贴名称.Focus();
        }
        private void Label_磁贴图片_MouseClick(object sender, MouseEventArgs e)
        {
            TextBox_磁贴图片.Focus();
        }
        private void Label_磁贴图标_MouseClick(object sender, MouseEventArgs e)
        {
            TextBox_磁贴图标.Focus();
        }
        
        private void PictureBox_磁贴图片预览_MouseDown(object sender, MouseEventArgs e) // 图片预览框 鼠标按下开始取色
        {
            if (e.Button == MouseButtons.Left)
            {
                if (Button_旧版磁贴模式.Text == "旧版磁贴模式 [√]" || NewTileState == false) // 如果没有开启新版磁贴样式/开启了旧版磁贴模式，则允许取色
                {
                    PictureBox_磁贴图片预览.Cursor = Cursors.Cross;
                    ScreenColorFlag = true;
                }
            }
        }
        private void PictureBox_磁贴图片预览_MouseMove(object sender, MouseEventArgs e) // 图片预览框 鼠标移动 取色
        {
            if (ScreenColorFlag)
            {
                UserColor = "#" + Other_cs.Color.Get_ScreenColor(MousePosition.X, MousePosition.Y);
                PictureBox_磁贴图片预览.BackColor = ColorTranslator.FromHtml(UserColor);
            }
            
        }
        private void PictureBox_磁贴图片预览_MouseUp(object sender, MouseEventArgs e) // 图片预览框 鼠标放开结束取色
        {
            if (e.Button == MouseButtons.Left)
            {
                PictureBox_磁贴图片预览.Cursor = Cursors.Default;
                ScreenColorFlag = false;
            }
        }
        private void PictureBox_磁贴图片预览_MouseClick(object sender, MouseEventArgs e) // 图片预览框 鼠标右击恢复默认颜色
        {
            if (e.Button == MouseButtons.Right)
            {
                PictureBox_磁贴图片预览.BackColor = ColorTranslator.FromHtml(SystemColor);
            }
        }

        private void PictureBox_磁贴图片预览_BackColorChanged(object sender, EventArgs e) // 统一其他预览控件的背景颜色
        {
            PictureBox_磁贴图标预览.BackColor = Label_磁贴名称预览.BackColor = PictureBox_磁贴图片预览.BackColor;
            Recognize_Text_Color(PictureBox_磁贴图片预览.BackColor.R, PictureBox_磁贴图片预览.BackColor.G, PictureBox_磁贴图片预览.BackColor.B); // 智能识别文字颜色
        }
    }
}
