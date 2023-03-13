namespace 磁贴美化小工具
{
    partial class Form
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form));
            this.TextBox_程序路径 = new System.Windows.Forms.TextBox();
            this.Label_程序路径 = new System.Windows.Forms.Label();
            this.PictureBox_磁贴图片预览 = new System.Windows.Forms.PictureBox();
            this.TextBox_磁贴名称 = new System.Windows.Forms.TextBox();
            this.Label_磁贴名称 = new System.Windows.Forms.Label();
            this.TextBox_磁贴图片 = new System.Windows.Forms.TextBox();
            this.Label_磁贴图片 = new System.Windows.Forms.Label();
            this.TextBox_磁贴图标 = new System.Windows.Forms.TextBox();
            this.Label_磁贴图标 = new System.Windows.Forms.Label();
            this.Button_添加磁贴 = new System.Windows.Forms.Button();
            this.Button_查看 = new System.Windows.Forms.Button();
            this.Button_初始化 = new System.Windows.Forms.Button();
            this.Button_显示文字 = new System.Windows.Forms.Button();
            this.Button_添加右键菜单 = new System.Windows.Forms.Button();
            this.Button_查看磁贴目录 = new System.Windows.Forms.Button();
            this.Button_自动检查更新 = new System.Windows.Forms.Button();
            this.Button_检查更新 = new System.Windows.Forms.Button();
            this.PictureBox_磁贴图标预览 = new System.Windows.Forms.PictureBox();
            this.Label_磁贴名称预览 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.Button_旧版磁贴模式 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_磁贴图片预览)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_磁贴图标预览)).BeginInit();
            this.SuspendLayout();
            // 
            // TextBox_程序路径
            // 
            this.TextBox_程序路径.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TextBox_程序路径.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextBox_程序路径.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.TextBox_程序路径.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.TextBox_程序路径.Location = new System.Drawing.Point(16, 11);
            this.TextBox_程序路径.Name = "TextBox_程序路径";
            this.TextBox_程序路径.ReadOnly = true;
            this.TextBox_程序路径.Size = new System.Drawing.Size(390, 18);
            this.TextBox_程序路径.TabIndex = 0;
            this.TextBox_程序路径.TabStop = false;
            this.toolTip.SetToolTip(this.TextBox_程序路径, "拖入 [应用程序、快捷方式、开始菜单磁贴(非UWP)]\r\n按下 [退格键(Backspace)] 可清空。");
            this.TextBox_程序路径.TextChanged += new System.EventHandler(this.TextBox_程序路径_TextChanged);
            this.TextBox_程序路径.Enter += new System.EventHandler(this.TextBox_程序路径_Enter);
            this.TextBox_程序路径.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_程序路径_KeyPress);
            this.TextBox_程序路径.Leave += new System.EventHandler(this.TextBox_程序路径_Leave);
            // 
            // Label_程序路径
            // 
            this.Label_程序路径.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Label_程序路径.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Label_程序路径.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label_程序路径.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.Label_程序路径.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.Label_程序路径.Location = new System.Drawing.Point(-8, 0);
            this.Label_程序路径.Name = "Label_程序路径";
            this.Label_程序路径.Size = new System.Drawing.Size(430, 40);
            this.Label_程序路径.TabIndex = 1;
            this.Label_程序路径.Text = "     程序路径";
            this.Label_程序路径.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip.SetToolTip(this.Label_程序路径, "拖入 [应用程序、快捷方式、开始菜单磁贴(非UWP)]\r\n按下 [退格键(Backspace)] 可清空。");
            this.Label_程序路径.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Label_程序路径_MouseClick);
            // 
            // PictureBox_磁贴图片预览
            // 
            this.PictureBox_磁贴图片预览.BackColor = System.Drawing.Color.WhiteSmoke;
            this.PictureBox_磁贴图片预览.Location = new System.Drawing.Point(423, 41);
            this.PictureBox_磁贴图片预览.Name = "PictureBox_磁贴图片预览";
            this.PictureBox_磁贴图片预览.Size = new System.Drawing.Size(100, 100);
            this.PictureBox_磁贴图片预览.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox_磁贴图片预览.TabIndex = 2;
            this.PictureBox_磁贴图片预览.TabStop = false;
            this.toolTip.SetToolTip(this.PictureBox_磁贴图片预览, "磁贴预览框：\r\n按下 [鼠标左键] 开始屏幕取色，松开 [鼠标左键] 停止取色。\r\n单击 [鼠标右键] 重置为系统主题色。\r\n\r\n注意：系统 2004 版本及以上" +
        "/开启新版磁贴样式后，请开启下方的 [旧版磁贴模式] 才能使用 [屏幕取色] 功能。\r\n注意：图标预览时可能会出现锯齿等显示问题，但实际磁贴显示正常！");
            this.PictureBox_磁贴图片预览.BackColorChanged += new System.EventHandler(this.PictureBox_磁贴图片预览_BackColorChanged);
            this.PictureBox_磁贴图片预览.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PictureBox_磁贴图片预览_MouseClick);
            this.PictureBox_磁贴图片预览.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox_磁贴图片预览_MouseDown);
            this.PictureBox_磁贴图片预览.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox_磁贴图片预览_MouseMove);
            this.PictureBox_磁贴图片预览.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBox_磁贴图片预览_MouseUp);
            // 
            // TextBox_磁贴名称
            // 
            this.TextBox_磁贴名称.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TextBox_磁贴名称.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextBox_磁贴名称.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.TextBox_磁贴名称.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.TextBox_磁贴名称.Location = new System.Drawing.Point(16, 52);
            this.TextBox_磁贴名称.Name = "TextBox_磁贴名称";
            this.TextBox_磁贴名称.Size = new System.Drawing.Size(390, 18);
            this.TextBox_磁贴名称.TabIndex = 1;
            this.TextBox_磁贴名称.TabStop = false;
            this.toolTip.SetToolTip(this.TextBox_磁贴名称, "磁贴显示的文字");
            this.TextBox_磁贴名称.TextChanged += new System.EventHandler(this.TextBox_磁贴名称_TextChanged);
            this.TextBox_磁贴名称.Enter += new System.EventHandler(this.TextBox_磁贴名称_Enter);
            this.TextBox_磁贴名称.Leave += new System.EventHandler(this.TextBox_磁贴名称_Leave);
            // 
            // Label_磁贴名称
            // 
            this.Label_磁贴名称.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Label_磁贴名称.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Label_磁贴名称.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label_磁贴名称.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.Label_磁贴名称.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.Label_磁贴名称.Location = new System.Drawing.Point(-8, 41);
            this.Label_磁贴名称.Name = "Label_磁贴名称";
            this.Label_磁贴名称.Size = new System.Drawing.Size(430, 40);
            this.Label_磁贴名称.TabIndex = 4;
            this.Label_磁贴名称.Text = "     磁贴名称";
            this.Label_磁贴名称.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip.SetToolTip(this.Label_磁贴名称, "磁贴显示的文字");
            this.Label_磁贴名称.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Label_磁贴名称_MouseClick);
            // 
            // TextBox_磁贴图片
            // 
            this.TextBox_磁贴图片.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TextBox_磁贴图片.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextBox_磁贴图片.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.TextBox_磁贴图片.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.TextBox_磁贴图片.Location = new System.Drawing.Point(16, 93);
            this.TextBox_磁贴图片.Name = "TextBox_磁贴图片";
            this.TextBox_磁贴图片.ReadOnly = true;
            this.TextBox_磁贴图片.Size = new System.Drawing.Size(390, 18);
            this.TextBox_磁贴图片.TabIndex = 2;
            this.TextBox_磁贴图片.TabStop = false;
            this.toolTip.SetToolTip(this.TextBox_磁贴图片, "拖入 [图片文件( .png .jpg .jpeg )]，与磁贴图标二选一\r\n按下 [退格键(Backspace)] 可清空。\r\n");
            this.TextBox_磁贴图片.TextChanged += new System.EventHandler(this.TextBox_磁贴图片_TextChanged);
            this.TextBox_磁贴图片.Enter += new System.EventHandler(this.TextBox_磁贴图片_Enter);
            this.TextBox_磁贴图片.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_磁贴图片_KeyPress);
            this.TextBox_磁贴图片.Leave += new System.EventHandler(this.TextBox_磁贴图片_Leave);
            // 
            // Label_磁贴图片
            // 
            this.Label_磁贴图片.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Label_磁贴图片.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Label_磁贴图片.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label_磁贴图片.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.Label_磁贴图片.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.Label_磁贴图片.Location = new System.Drawing.Point(-8, 82);
            this.Label_磁贴图片.Name = "Label_磁贴图片";
            this.Label_磁贴图片.Size = new System.Drawing.Size(430, 40);
            this.Label_磁贴图片.TabIndex = 6;
            this.Label_磁贴图片.Text = "     磁贴图片";
            this.Label_磁贴图片.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip.SetToolTip(this.Label_磁贴图片, "拖入 [图片文件( .png .jpg .jpeg )]，与磁贴图标二选一\r\n按下 [退格键(Backspace)] 可清空。");
            this.Label_磁贴图片.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Label_磁贴图片_MouseClick);
            // 
            // TextBox_磁贴图标
            // 
            this.TextBox_磁贴图标.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TextBox_磁贴图标.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextBox_磁贴图标.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.TextBox_磁贴图标.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.TextBox_磁贴图标.Location = new System.Drawing.Point(16, 134);
            this.TextBox_磁贴图标.Name = "TextBox_磁贴图标";
            this.TextBox_磁贴图标.ReadOnly = true;
            this.TextBox_磁贴图标.Size = new System.Drawing.Size(390, 18);
            this.TextBox_磁贴图标.TabIndex = 3;
            this.TextBox_磁贴图标.TabStop = false;
            this.toolTip.SetToolTip(this.TextBox_磁贴图标, "拖入 [图标文件( .ico )]，与磁贴图片二选一\r\n按下 [退格键(Backspace)] 可清空。\r\n");
            this.TextBox_磁贴图标.TextChanged += new System.EventHandler(this.TextBox_磁贴图标_TextChanged);
            this.TextBox_磁贴图标.Enter += new System.EventHandler(this.TextBox_磁贴图标_Enter);
            this.TextBox_磁贴图标.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_磁贴图标_KeyPress);
            this.TextBox_磁贴图标.Leave += new System.EventHandler(this.TextBox_磁贴图标_Leave);
            // 
            // Label_磁贴图标
            // 
            this.Label_磁贴图标.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Label_磁贴图标.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Label_磁贴图标.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label_磁贴图标.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.Label_磁贴图标.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.Label_磁贴图标.Location = new System.Drawing.Point(-8, 123);
            this.Label_磁贴图标.Name = "Label_磁贴图标";
            this.Label_磁贴图标.Size = new System.Drawing.Size(430, 40);
            this.Label_磁贴图标.TabIndex = 8;
            this.Label_磁贴图标.Text = "     磁贴图标  与磁贴图片二选一";
            this.Label_磁贴图标.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip.SetToolTip(this.Label_磁贴图标, "拖入 [图标文件( .ico )]，与磁贴图片二选一\r\n按下 [退格键(Backspace)] 可清空。");
            this.Label_磁贴图标.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Label_磁贴图标_MouseClick);
            // 
            // Button_添加磁贴
            // 
            this.Button_添加磁贴.BackColor = System.Drawing.Color.LightGray;
            this.Button_添加磁贴.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Button_添加磁贴.Enabled = false;
            this.Button_添加磁贴.FlatAppearance.BorderSize = 0;
            this.Button_添加磁贴.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_添加磁贴.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.Button_添加磁贴.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.Button_添加磁贴.Location = new System.Drawing.Point(-8, 164);
            this.Button_添加磁贴.Name = "Button_添加磁贴";
            this.Button_添加磁贴.Size = new System.Drawing.Size(536, 40);
            this.Button_添加磁贴.TabIndex = 9;
            this.Button_添加磁贴.TabStop = false;
            this.Button_添加磁贴.Text = "添加/更新磁贴";
            this.toolTip.SetToolTip(this.Button_添加磁贴, "点击后添加或更新磁贴\r\n\r\n注意：C:\\Program Files 等权限敏感目录下的程序，需要 [以管理员身份运行] 本软件才能添加/更新磁贴！");
            this.Button_添加磁贴.UseVisualStyleBackColor = false;
            this.Button_添加磁贴.Click += new System.EventHandler(this.Button_添加磁贴_Click);
            // 
            // Button_查看
            // 
            this.Button_查看.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Button_查看.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Button_查看.FlatAppearance.BorderSize = 0;
            this.Button_查看.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_查看.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.Button_查看.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.Button_查看.Location = new System.Drawing.Point(423, 0);
            this.Button_查看.Name = "Button_查看";
            this.Button_查看.Size = new System.Drawing.Size(43, 40);
            this.Button_查看.TabIndex = 17;
            this.Button_查看.TabStop = false;
            this.Button_查看.Text = "查看";
            this.Button_查看.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip.SetToolTip(this.Button_查看, "打开程序所在文件夹");
            this.Button_查看.UseVisualStyleBackColor = false;
            this.Button_查看.Click += new System.EventHandler(this.Button_查看_Click);
            // 
            // Button_初始化
            // 
            this.Button_初始化.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Button_初始化.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Button_初始化.FlatAppearance.BorderSize = 0;
            this.Button_初始化.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_初始化.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.Button_初始化.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.Button_初始化.Location = new System.Drawing.Point(467, 0);
            this.Button_初始化.Name = "Button_初始化";
            this.Button_初始化.Size = new System.Drawing.Size(57, 40);
            this.Button_初始化.TabIndex = 18;
            this.Button_初始化.TabStop = false;
            this.Button_初始化.Text = "初始化";
            this.toolTip.SetToolTip(this.Button_初始化, "初始化磁贴配置文件");
            this.Button_初始化.UseVisualStyleBackColor = false;
            this.Button_初始化.Click += new System.EventHandler(this.Button_初始化_Click);
            // 
            // Button_显示文字
            // 
            this.Button_显示文字.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Button_显示文字.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Button_显示文字.FlatAppearance.BorderSize = 0;
            this.Button_显示文字.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_显示文字.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.Button_显示文字.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.Button_显示文字.Location = new System.Drawing.Point(423, 142);
            this.Button_显示文字.Name = "Button_显示文字";
            this.Button_显示文字.Size = new System.Drawing.Size(101, 21);
            this.Button_显示文字.TabIndex = 19;
            this.Button_显示文字.TabStop = false;
            this.Button_显示文字.Text = "隐藏文字";
            this.toolTip.SetToolTip(this.Button_显示文字, "显示或隐藏磁贴文字");
            this.Button_显示文字.UseVisualStyleBackColor = false;
            this.Button_显示文字.TextChanged += new System.EventHandler(this.Button_显示文字_TextChanged);
            this.Button_显示文字.Click += new System.EventHandler(this.Button_显示文字_Click);
            this.Button_显示文字.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox_磁贴图片预览_MouseDown);
            // 
            // Button_添加右键菜单
            // 
            this.Button_添加右键菜单.BackColor = System.Drawing.Color.Gainsboro;
            this.Button_添加右键菜单.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Button_添加右键菜单.FlatAppearance.BorderSize = 0;
            this.Button_添加右键菜单.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_添加右键菜单.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.Button_添加右键菜单.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.Button_添加右键菜单.Location = new System.Drawing.Point(0, 205);
            this.Button_添加右键菜单.Name = "Button_添加右键菜单";
            this.Button_添加右键菜单.Size = new System.Drawing.Size(115, 22);
            this.Button_添加右键菜单.TabIndex = 20;
            this.Button_添加右键菜单.TabStop = false;
            this.Button_添加右键菜单.Text = "添加右键菜单 [√]";
            this.toolTip.SetToolTip(this.Button_添加右键菜单, "开启后，右键任意 应用程序(.exe)、快捷方式(.lnk) 可看到 [自定义并固定到\"开始\"屏幕]\r\n[√]：添加右键菜单\r\n[×]：取消右键菜单（即未添加）");
            this.Button_添加右键菜单.UseVisualStyleBackColor = false;
            this.Button_添加右键菜单.Click += new System.EventHandler(this.Button_添加右键菜单_Click);
            // 
            // Button_查看磁贴目录
            // 
            this.Button_查看磁贴目录.BackColor = System.Drawing.Color.Gainsboro;
            this.Button_查看磁贴目录.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Button_查看磁贴目录.FlatAppearance.BorderSize = 0;
            this.Button_查看磁贴目录.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_查看磁贴目录.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.Button_查看磁贴目录.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.Button_查看磁贴目录.Location = new System.Drawing.Point(348, 205);
            this.Button_查看磁贴目录.Name = "Button_查看磁贴目录";
            this.Button_查看磁贴目录.Size = new System.Drawing.Size(95, 22);
            this.Button_查看磁贴目录.TabIndex = 21;
            this.Button_查看磁贴目录.TabStop = false;
            this.Button_查看磁贴目录.Text = "查看磁贴目录";
            this.toolTip.SetToolTip(this.Button_查看磁贴目录, "打开磁贴对应的快捷方式所在文件夹");
            this.Button_查看磁贴目录.UseVisualStyleBackColor = false;
            this.Button_查看磁贴目录.Click += new System.EventHandler(this.Button_查看磁贴目录_Click);
            // 
            // Button_自动检查更新
            // 
            this.Button_自动检查更新.BackColor = System.Drawing.Color.Gainsboro;
            this.Button_自动检查更新.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Button_自动检查更新.FlatAppearance.BorderSize = 0;
            this.Button_自动检查更新.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_自动检查更新.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.Button_自动检查更新.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.Button_自动检查更新.Location = new System.Drawing.Point(116, 205);
            this.Button_自动检查更新.Name = "Button_自动检查更新";
            this.Button_自动检查更新.Size = new System.Drawing.Size(115, 22);
            this.Button_自动检查更新.TabIndex = 22;
            this.Button_自动检查更新.TabStop = false;
            this.Button_自动检查更新.Text = "自动检查更新 [×]";
            this.toolTip.SetToolTip(this.Button_自动检查更新, "[√]：启动软件后检查更新\r\n[×]：启动软件后不检查更新");
            this.Button_自动检查更新.UseVisualStyleBackColor = false;
            this.Button_自动检查更新.Click += new System.EventHandler(this.Button_自动检查更新_Click);
            // 
            // Button_检查更新
            // 
            this.Button_检查更新.BackColor = System.Drawing.Color.Gainsboro;
            this.Button_检查更新.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Button_检查更新.FlatAppearance.BorderSize = 0;
            this.Button_检查更新.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_检查更新.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.Button_检查更新.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.Button_检查更新.Location = new System.Drawing.Point(444, 205);
            this.Button_检查更新.Name = "Button_检查更新";
            this.Button_检查更新.Size = new System.Drawing.Size(80, 22);
            this.Button_检查更新.TabIndex = 23;
            this.Button_检查更新.TabStop = false;
            this.Button_检查更新.Text = "检查更新";
            this.toolTip.SetToolTip(this.Button_检查更新, "立即检查更新");
            this.Button_检查更新.UseVisualStyleBackColor = false;
            this.Button_检查更新.Click += new System.EventHandler(this.Button_检查更新_Click);
            // 
            // PictureBox_磁贴图标预览
            // 
            this.PictureBox_磁贴图标预览.BackColor = System.Drawing.Color.WhiteSmoke;
            this.PictureBox_磁贴图标预览.Location = new System.Drawing.Point(457, 75);
            this.PictureBox_磁贴图标预览.Name = "PictureBox_磁贴图标预览";
            this.PictureBox_磁贴图标预览.Size = new System.Drawing.Size(32, 32);
            this.PictureBox_磁贴图标预览.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox_磁贴图标预览.TabIndex = 24;
            this.PictureBox_磁贴图标预览.TabStop = false;
            this.toolTip.SetToolTip(this.PictureBox_磁贴图标预览, "磁贴预览框：\r\n按下 [鼠标左键] 开始屏幕取色，松开 [鼠标左键] 停止取色。\r\n单击 [鼠标右键] 重置为系统主题色。\r\n\r\n注意：系统 2004 版本及以上" +
        "/开启新版磁贴样式后，请开启下方的 [旧版磁贴模式] 才能使用 [屏幕取色] 功能。\r\n注意：图标预览时可能会出现锯齿等显示问题，但实际磁贴显示正常！");
            this.PictureBox_磁贴图标预览.Visible = false;
            this.PictureBox_磁贴图标预览.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PictureBox_磁贴图片预览_MouseClick);
            this.PictureBox_磁贴图标预览.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox_磁贴图片预览_MouseDown);
            this.PictureBox_磁贴图标预览.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox_磁贴图片预览_MouseMove);
            this.PictureBox_磁贴图标预览.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBox_磁贴图片预览_MouseUp);
            // 
            // Label_磁贴名称预览
            // 
            this.Label_磁贴名称预览.AutoEllipsis = true;
            this.Label_磁贴名称预览.BackColor = System.Drawing.Color.Transparent;
            this.Label_磁贴名称预览.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Label_磁贴名称预览.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label_磁贴名称预览.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.Label_磁贴名称预览.ForeColor = System.Drawing.SystemColors.Window;
            this.Label_磁贴名称预览.Location = new System.Drawing.Point(428, 120);
            this.Label_磁贴名称预览.Margin = new System.Windows.Forms.Padding(0);
            this.Label_磁贴名称预览.Name = "Label_磁贴名称预览";
            this.Label_磁贴名称预览.Size = new System.Drawing.Size(92, 16);
            this.Label_磁贴名称预览.TabIndex = 25;
            this.Label_磁贴名称预览.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip.SetToolTip(this.Label_磁贴名称预览, "磁贴文字预览：\r\n点击下面的 [显示文字/隐藏文字] 按钮即可切换。\r\n文字颜色会智能识别磁贴图片/背景颜色。");
            this.Label_磁贴名称预览.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PictureBox_磁贴图片预览_MouseClick);
            this.Label_磁贴名称预览.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox_磁贴图片预览_MouseDown);
            this.Label_磁贴名称预览.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox_磁贴图片预览_MouseMove);
            this.Label_磁贴名称预览.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBox_磁贴图片预览_MouseUp);
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 10000;
            this.toolTip.BackColor = System.Drawing.SystemColors.HighlightText;
            this.toolTip.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.toolTip.InitialDelay = 500;
            this.toolTip.ReshowDelay = 100;
            this.toolTip.ShowAlways = true;
            // 
            // Button_旧版磁贴模式
            // 
            this.Button_旧版磁贴模式.BackColor = System.Drawing.Color.Gainsboro;
            this.Button_旧版磁贴模式.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Button_旧版磁贴模式.FlatAppearance.BorderSize = 0;
            this.Button_旧版磁贴模式.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_旧版磁贴模式.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.Button_旧版磁贴模式.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.Button_旧版磁贴模式.Location = new System.Drawing.Point(232, 205);
            this.Button_旧版磁贴模式.Name = "Button_旧版磁贴模式";
            this.Button_旧版磁贴模式.Size = new System.Drawing.Size(115, 22);
            this.Button_旧版磁贴模式.TabIndex = 26;
            this.Button_旧版磁贴模式.TabStop = false;
            this.Button_旧版磁贴模式.Text = "旧版磁贴模式 [×]";
            this.toolTip.SetToolTip(this.Button_旧版磁贴模式, "[√]：适合正在使用旧版磁贴的电脑 (支持自定义磁贴背景颜色、屏幕取色等)\r\n[×]：适合正在使用新版磁贴/系统 2004 版本以上的电脑");
            this.Button_旧版磁贴模式.UseVisualStyleBackColor = false;
            this.Button_旧版磁贴模式.Click += new System.EventHandler(this.Button_旧版磁贴模式_Click);
            // 
            // Form
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(523, 227);
            this.Controls.Add(this.Button_旧版磁贴模式);
            this.Controls.Add(this.Label_磁贴名称预览);
            this.Controls.Add(this.PictureBox_磁贴图标预览);
            this.Controls.Add(this.Button_初始化);
            this.Controls.Add(this.Button_查看);
            this.Controls.Add(this.Button_添加磁贴);
            this.Controls.Add(this.Label_磁贴图标);
            this.Controls.Add(this.Label_磁贴图片);
            this.Controls.Add(this.Label_磁贴名称);
            this.Controls.Add(this.PictureBox_磁贴图片预览);
            this.Controls.Add(this.Label_程序路径);
            this.Controls.Add(this.TextBox_磁贴图标);
            this.Controls.Add(this.TextBox_磁贴图片);
            this.Controls.Add(this.TextBox_磁贴名称);
            this.Controls.Add(this.TextBox_程序路径);
            this.Controls.Add(this.Button_显示文字);
            this.Controls.Add(this.Button_检查更新);
            this.Controls.Add(this.Button_自动检查更新);
            this.Controls.Add(this.Button_查看磁贴目录);
            this.Controls.Add(this.Button_添加右键菜单);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "磁贴美化小工具";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_FormClosing);
            this.Load += new System.EventHandler(this.Form_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_磁贴图片预览)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_磁贴图标预览)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextBox_程序路径;
        private System.Windows.Forms.Label Label_程序路径;
        private System.Windows.Forms.PictureBox PictureBox_磁贴图片预览;
        private System.Windows.Forms.TextBox TextBox_磁贴名称;
        private System.Windows.Forms.Label Label_磁贴名称;
        private System.Windows.Forms.TextBox TextBox_磁贴图片;
        private System.Windows.Forms.Label Label_磁贴图片;
        private System.Windows.Forms.TextBox TextBox_磁贴图标;
        private System.Windows.Forms.Label Label_磁贴图标;
        private System.Windows.Forms.Button Button_添加磁贴;
        private System.Windows.Forms.Button Button_查看;
        private System.Windows.Forms.Button Button_初始化;
        private System.Windows.Forms.Button Button_显示文字;
        private System.Windows.Forms.Button Button_添加右键菜单;
        private System.Windows.Forms.Button Button_查看磁贴目录;
        private System.Windows.Forms.Button Button_自动检查更新;
        private System.Windows.Forms.Button Button_检查更新;
        private System.Windows.Forms.PictureBox PictureBox_磁贴图标预览;
        private System.Windows.Forms.Label Label_磁贴名称预览;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button Button_旧版磁贴模式;
    }
}

