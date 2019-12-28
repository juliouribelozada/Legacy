// -----------------------------------------------------------------------
// <copyright file="NotifiableAndDependencyObject.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Atpc.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Windows;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class NotifiableAndDependencyObject : DependencyObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected internal void NotifyPropertyChanged(string info)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        protected virtual void NotifyPropertyChanged<T>(Expression<Func<T>> property)
        {
            var expression = property.Body as MemberExpression;
            if (expression != null)
            {
                var member = expression.Member;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs(member.Name));
                }
            }
        }
    }
}
