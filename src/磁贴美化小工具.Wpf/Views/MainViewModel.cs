using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xiu2.TileTool.UIFramework;

namespace Xiu2.TileTool.Views
{
    public class MainViewModel : BindableObject
    {
        public ObservableCollection<NavigationItem> NavigationItems { get; } = new ObservableCollection<NavigationItem>
        {
            NavigationItem.Combine<EditTilePage, EditTileViewModel>("新建/编辑磁贴", null, ""),
            NavigationItem.Combine<AboutPage, AboutViewModel>("关于", null, ""),
            NavigationItem.Combine<SettingsPage, SettingsViewModel>("设置", null, ""),
        };
    }
}
