using System;
using System.Diagnostics.Contracts;
using System.Windows;

// 来源：https://github.com/walterlv/Walterlv.Packages/blob/master/src/Frameworks/Walterlv.Windows.Framework/Windows/Navigating/NavigationItem.cs

namespace Xiu2.TileTool.UIFramework
{
    /// <summary>
    /// 为 Master-Detail 布局型导航提供通用的 ViewModel。
    /// </summary>
    public class NavigationItem : BindableObject
    {
        private readonly Func<UIElement> _viewCreator;
        private readonly Func<object> _viewModelCreator;
        private UIElement? _view;
        private object? _viewModel;

        /// <summary>
        /// 创建 <see cref="NavigationItem"/> 的新实例。
        /// </summary>
        /// <param name="viewCreator">创建 View 的方法。</param>
        /// <param name="viewModelCreator">创建 ViewModel 的方法。</param>
        /// <param name="title">导航的标题（仅提供通用属性，具体使用需在 MVVM 模式中自行绑定 Title 属性）。</param>
        /// <param name="description">导航的描述导航的标题（仅提供通用属性，具体使用需在 MVVM 模式中自行绑定 Title 属性）。</param>
        /// <param name="data">导航对象的额外属性（为避免额外编写继承自 <see cref="NavigationItem"/> 的类，这里提供一个通用的属性，用于在导航的 ViewModel 上下文中绑定使用）。</param>
        public NavigationItem(Func<UIElement> viewCreator, Func<object> viewModelCreator,
            string? title = null, string? description = null, object? data = null)
        {
            _viewCreator = viewCreator ?? throw new ArgumentNullException(nameof(viewCreator));
            _viewModelCreator = viewModelCreator ?? throw new ArgumentNullException(nameof(viewModelCreator));
            Title = title ?? "";
            Description = description;
            Data = data;
        }

        /// <summary>
        /// 导航上下文中的 View。
        /// </summary>
        public UIElement View => _view ??= _viewCreator();

        /// <summary>
        /// 导航上下文中的 ViewModel。
        /// </summary>
        public object ViewModel => _viewModel ??= _viewModelCreator();

        /// <summary>
        /// 导航标题。
        /// </summary>
        public string? Title { get; }

        /// <summary>
        /// 导航描述。
        /// </summary>
        public string? Description { get; }

        /// <summary>
        /// 导航中附带的额外数据。
        /// </summary>
        public object? Data { get; }

        /// <summary>
        /// 将一个 View 和一个 ViewModel 连接起来，组成一个适用于 Master-Detail 布局的通用导航 ViewModel。
        /// </summary>
        /// <typeparam name="TView">View 的类型。</typeparam>
        /// <typeparam name="TViewModel">ViewModel 的类型。</typeparam>
        /// <param name="title">导航的标题（仅提供通用属性，具体使用需在 MVVM 模式中自行绑定 Title 属性）。</param>
        /// <param name="description">导航的描述导航的标题（仅提供通用属性，具体使用需在 MVVM 模式中自行绑定 Title 属性）。</param>
        /// <param name="data">导航对象的额外属性（为避免额外编写继承自 <see cref="NavigationItem"/> 的类，这里提供一个通用的属性，用于在导航的 ViewModel 上下文中绑定使用）。</param>
        /// <returns>适用于 Master-Detail 布局的通用导航 ViewModel。</returns>
        [Pure]
        public static NavigationItem Combine<TView, TViewModel>(
            string? title = null, string? description = null, object? data = null)
            where TView : UIElement, new()
            where TViewModel : class, new()
            => new NavigationItem<TView, TViewModel>(() => new TView(), () => new TViewModel(), title, description, data);
    }

    /// <summary>
    /// 为 Master-Detail 布局型导航提供通用的 ViewModel。
    /// </summary>
    public class NavigationItem<TView, TViewModel> : NavigationItem
        where TView : UIElement, new()
        where TViewModel : class, new()
    {
        /// <summary>
        /// 创建 <see cref="NavigationItem"/> 的新实例。
        /// </summary>
        /// <param name="viewCreator">创建 View 的方法。</param>
        /// <param name="viewModelCreator">创建 ViewModel 的方法。</param>
        /// <param name="title">导航的标题（仅提供通用属性，具体使用需在 MVVM 模式中自行绑定 Title 属性）。</param>
        /// <param name="description">导航的描述导航的标题（仅提供通用属性，具体使用需在 MVVM 模式中自行绑定 Title 属性）。</param>
        /// <param name="data">导航对象的额外属性（为避免额外编写继承自 <see cref="NavigationItem"/> 的类，这里提供一个通用的属性，用于在导航的 ViewModel 上下文中绑定使用）。</param>
        public NavigationItem(Func<TView> viewCreator, Func<TViewModel> viewModelCreator,
            string? title = null, string? description = null, object? data = null)
            : base(viewCreator, viewModelCreator, title, description, data)
        {
        }

        /// <summary>
        /// 导航上下文中的 View。
        /// </summary>
        public new TView View => (TView)base.View;

        /// <summary>
        /// 导航上下文中的 ViewModel。
        /// </summary>
        public new TViewModel ViewModel => (TViewModel)base.ViewModel;

        /// <summary>
        /// 将一个 View 和一个 ViewModel 连接起来，组成一个适用于 Master-Detail 布局的通用导航 ViewModel。
        /// </summary>
        /// <param name="title">导航的标题（仅提供通用属性，具体使用需在 MVVM 模式中自行绑定 Title 属性）。</param>
        /// <param name="description">导航的描述导航的标题（仅提供通用属性，具体使用需在 MVVM 模式中自行绑定 Title 属性）。</param>
        /// <param name="data">导航对象的额外属性（为避免额外编写继承自 <see cref="NavigationItem"/> 的类，这里提供一个通用的属性，用于在导航的 ViewModel 上下文中绑定使用）。</param>
        /// <returns>适用于 Master-Detail 布局的通用导航 ViewModel。</returns>
        [Pure]
        public static NavigationItem Combine(string? title = null, string? description = null, object? data = null)
            => new NavigationItem<TView, TViewModel>(() => new TView(), () => new TViewModel(), title, description, data);
    }
}
