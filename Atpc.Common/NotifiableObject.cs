// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotifiableObject.cs" company="ATPC.co">
//   © Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Atpc.Common
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;

    /// <summary>
    /// Instantiate a Notifiable Object.
    /// </summary>
    public class NotifiableObject : INotifyPropertyChanged
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
