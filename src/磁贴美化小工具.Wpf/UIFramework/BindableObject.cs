using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

// 来源：https://github.com/walterlv/Walterlv.Packages/blob/master/src/Frameworks/Walterlv.Windows.Framework/ComponentModel/BindableObject.cs

namespace Xiu2.TileTool.UIFramework
{
    /// <summary>
    /// 表示可绑定的对象，在此类型的派生类中按约定定义的属性支持绑定。
    /// </summary>
    public abstract class BindableObject : INotifyPropertyChanged
    {
        /// <summary>
        /// 当此实例中的任何一个具有更改通知的属性值改变时发生。
        /// 派生类可以通过调用 <see cref="SetValue{T}"/> 或 <see cref="OnPropertyChanged"/> 来引发此事件。
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// 当具有更改通知的属性值改变时发生。
        /// </summary>
        /// <param name="propertyName">属性名称。不需要手动传入，会自动根据所在属性的方法名设置此参数值。</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 修改一个具有更改通知的属性值，并对外报告值的改变。
        /// </summary>
        /// <typeparam name="T">值的类型。</typeparam>
        /// <param name="field">要修改的字段引用。</param>
        /// <param name="value">要修改的字段的新值。</param>
        /// <param name="propertyName">属性名称。不需要手动传入，会自动根据所在属性的方法名设置此参数值。</param>
        /// <returns>如果值发生了更改，则返回 true；否则返回 false。</returns>
        protected bool SetValue<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (!Equals(field, value))
            {
                field = value;
                OnPropertyChanged(propertyName);
                return true;
            }

            return false;
        }
    }
}