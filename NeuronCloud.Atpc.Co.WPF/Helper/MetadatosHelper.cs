// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MetadatosHelper.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.WPF.Helper
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;

    using NeuronCloud.Atpc.Co.Modelos.Helpers;
    
    public class MetadatosHelper
    {
        internal static Metadatos ParseMetadata(string bindingPath, object entity)
        {
            if (entity != null && !string.IsNullOrEmpty(bindingPath))
            {
                PropertyInfo property = GetProperty(entity.GetType(), bindingPath);
                if (property != null)
                {
                    Metadatos validationMetadata = new Metadatos();
                    foreach (object obj in property.GetCustomAttributes(false))
                    {
                        if (obj is RequiredAttribute)
                        {
                            validationMetadata.EsRequerido = true;
                        }
                        else
                        {
                            DisplayAttribute displayAttribute = obj as DisplayAttribute;
                            if (displayAttribute != null)
                            {
                                validationMetadata.Descripcion = displayAttribute.GetDescription();
                                validationMetadata.Nombre = displayAttribute.GetName();
                            }
                        }
                    }

                    if (validationMetadata.Nombre == null)
                    {
                        validationMetadata.Nombre = property.Name;
                    }

                    return validationMetadata;
                }
            }

            return null;
        }

        private static PropertyInfo GetProperty(Type entityType, string propertyPath)
        {
            Type type = entityType;
            string[] strArray = propertyPath.Split(new[] { '.' });

            if (strArray != null)
            {
                for (int index = 0; index < strArray.Length; ++index)
                {
                    PropertyInfo property = type.GetProperty(strArray[index]);
                    if (property == null || !property.CanRead)
                    {
                        return null;
                    }

                    if (index == strArray.Length - 1)
                    {
                        return property;
                    }

                    type = property.PropertyType;
                }
            }

            return null;
        }
    }
}
