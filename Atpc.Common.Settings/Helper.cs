using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATPC.Controls.Settings
{
    using System.Configuration;
    using System.Linq.Expressions;

    public static class Helper
    {
        public static U GetDefaulValue<U>(Expression<Func<U>> property)
        {
            var expression = property.Body as MemberExpression;
            if (expression != null)
            {
                var member = expression.Member;
                var att = member.GetCustomAttributes(typeof(DefaultSettingValueAttribute), true).FirstOrDefault();
                if (att != null)
                {
                    return (U)Convert.ChangeType(((DefaultSettingValueAttribute)att).Value, typeof(U));
                }
                else
                {
                    return default(U);
                }
            }
            else
            {
                return default(U);
            }

        }
    }
}
