// -----------------------------------------------------------------------
// <copyright file="Validator.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Atpc.Common.Utils
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
   public static class Validator
    {
        //public static IEnumerable<ErrorInfo> GetErrors(object instance)
        //{
        //    var metadataAttrib =
        //        instance.GetType().GetCustomAttributes(typeof (MetadataTypeAttribute), true).OfType
        //            <MetadataTypeAttribute>().FirstOrDefault();
        //    var buddyClassOrModelClass = metadataAttrib != null ? metadataAttrib.MetadataClassType : instance.GetType();
        //    var buddyClassProperties = TypeDescriptor.GetProperties(buddyClassOrModelClass).Cast<PropertyDescriptor>();
        //    var modelClassProperties = TypeDescriptor.GetProperties(instance.GetType()).Cast<PropertyDescriptor>();

        //    return from buddyProp in buddyClassProperties
        //           join modelProp in modelClassProperties on buddyProp.Name equals modelProp.Name
        //           from attribute in buddyProp.Attributes.OfType<ValidationAttribute>()
        //           where !attribute.IsValid(modelProp.GetValue(instance))
        //           select new ErrorInfo(buddyProp.Name, attribute.FormatErrorMessage(string.Empty), instance);
        }

}
