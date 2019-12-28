// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotifiableObject.cs" company="Luis Soler">
//   Copyright © 2012-2014
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.Satelite.CapturaManual.Data.Model
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;

    using JetBrains.Annotations;

    public class NotifiableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected internal void NotifyPropertyChanged(string info)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        [NotifyPropertyChangedInvocator]
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
