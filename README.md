# XIU2/TileTool

[![Release Version](https://img.shields.io/github/v/release/XIU2/TileTool.svg?style=flat-square&label=Release&color=1784ff)](https://github.com/XIU2/TileTool/releases/latest)
[![GitHub license](https://img.shields.io/github/license/XIU2/TileTool.svg?style=flat-square&label=License&color=3cb371&logo=github)](https://github.com/XIU2/TileTool/)
[![GitHub Star](https://img.shields.io/github/stars/XIU2/TileTool.svg?style=flat-square&label=Star&color=3cb371&logo=github)](https://github.com/XIU2/TileTool/)
[![GitHub Fork](https://img.shields.io/github/forks/XIU2/TileTool.svg?style=flat-square&label=Fork&color=3cb371&logo=github)](https://github.com/XIU2/TileTool/)

**Windows10 磁贴美化小工具**

最近体验了下 Win10 2004 的新版磁贴（开启方法见网盘），统一背景颜色为主题色、磁贴半透明，感觉还不错。  
于是我闲的没事用 C# 重写并开源了磁贴美化小工具（虽然也没人看）~ **如果觉得好用请点个⭐鼓励一下下~**  

- 详细介绍、使用说明：https://zhuanlan.zhihu.com/p/79630122  
- 磁贴辅助小工具开源：https://github.com/XIU2/TileAssistTool  

****

## 软件界面

![软件界面](https://raw.githubusercontent.com/XIU2/TileTool/master/img/01.png)  
> 界面折腾半天发现还是好丑，我可能真的没有设计天赋 -.-...  

![右键菜单](https://raw.githubusercontent.com/XIU2/TileTool/master/img/02.png)
> 右键菜单示例

****

## 效果示例

![](https://raw.githubusercontent.com/XIU2/TileTool/master/img/09.png)  
![](https://raw.githubusercontent.com/XIU2/TileTool/master/img/10.png)  
![](https://raw.githubusercontent.com/XIU2/TileTool/master/img/03.png)  
![](https://raw.githubusercontent.com/XIU2/TileTool/master/img/04.png)  
![](https://raw.githubusercontent.com/XIU2/TileTool/master/img/05.png)  
![](https://raw.githubusercontent.com/XIU2/TileTool/master/img/06.png)  
![](https://raw.githubusercontent.com/XIU2/TileTool/master/img/07.png)  
![](https://raw.githubusercontent.com/XIU2/TileTool/master/img/08.png)  

> 磁贴图标包(400+)我放在蓝奏云网盘中了，需要自取。  

****

## 下载地址

* 蓝奏云 ：[https://pan.lanzouq.com/b0sp46eh](https://pan.lanzouq.com/b0sp46eh)
* Github：[https://github.com/XIU2/TileTool/releases](https://github.com/XIU2/TileTool/releases)

****

## 使用说明

> 使用前请先鼠标指向各个组件查看 **相关提示说明！**  

* 方式一：拖放 **应用程序、快捷方式、开始菜单磁贴(非UWP)** 到软件内，即可编辑磁贴样式  
* 方式二：右键 **应用程序、快捷方式** 选择 [自定义并固定到"开始"屏幕]，即可编辑磁贴样式  
—— 方式二需要勾选软件中的 **[添加右键菜单]** 。

> **注意：** C:\Program Files 等权限目录下的程序，请 **[以管理员身份运行]** 本软件操作！  
> ——  
> 如果你已经以普通用户权限编辑磁贴，那么是无效的，需要删除 **[C:\Users\用户名\AppData\Local\VirtualStore]** 文件夹下的文件，然后 **[以管理员身份运行]** 本软件时编辑磁贴样式才能生效。  

> **另外，** 在程序路径、磁贴图片、磁贴图标输入框中按下 **[退格键(BackSpace)]** 即可清除内容。

### 我已经添加并排版好磁贴了，我还要重新添加吗？

不，你只需要：鼠标拖拽开始菜单的 [磁贴] 到软件中就可以自定义了。  

****

## 其他说明

* 图片最佳尺寸：100x100(像素)  
* 图标最佳尺寸：32x32(像素，仅ICO图标)  

### 运行提示 .NET 错误？

本软件最低依赖是 .NET Framework 4.6，报错说明你系统的该依赖版本低于 4.6（Win10 默认满足该依赖），请安装更高版本的 [.NET Framework](https://dotnet.microsoft.com/download/dotnet-framework) ！

****

### 拖入图片文件没反应？

如果可以正常拖入程序和快捷方式文件（.exe / .lnk），却发现**无法拖入图片文件**（.png / .jpg），那可能实际上图片文件已经拖入软件，但是因为**权限问题**导致软件无法读入图片文件，请**复制图片文件到其他目录下**再试试行不行。

****

## 赞赏支持

![微信赞赏](https://cdn.staticaly.com/gh/XIU2/XIU2/master/img/zs-01.png)![支付宝赞赏](https://cdn.staticaly.com/gh/XIU2/XIU2/master/img/zs-02.png)

****

## License

The GPL-3.0 License.