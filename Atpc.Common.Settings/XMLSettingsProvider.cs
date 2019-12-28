// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XMLSettingsProvider.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the XMLSettingsProvider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Atpc.Common.Settings
{
    using System;
    using System.Configuration;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Xml.Linq;

    public class XMLSettingsProvider : SettingsProvider
    {
        private const string SETTINGSROOT = "AtpcSettings";
        private XDocument settingsFile;

        public override string ApplicationName
        {
            get { return Assembly.GetEntryAssembly().GetName().Name; }
            set { }
        }

        private XDocument SettingsFile
        {
            get
            {
                if (this.settingsFile == null)
                {
                    try
                    {
                        this.settingsFile = XDocument.Load(Path.Combine(this.GetAppSettingsPath(), this.GetAppSettingsFilename()));
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("No se pudo Abrir el archivo de Configuración:" + e.Message);
                        this.settingsFile = new XDocument();
                        this.settingsFile.Add(new XElement(SETTINGSROOT));
                    }
                }

                return this.settingsFile;
            }
        }

        private XElement RootNode
        {
            get
            {
                if (this.SettingsFile.Root == null)
                {
                    this.settingsFile.Add(new XElement(SETTINGSROOT));
                }

                return this.SettingsFile.Root;
            }
        }

        public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection collection)
        {
            string sectionName = string.Empty;
            var values = new SettingsPropertyValueCollection();
            if (context.ContainsKey("GroupName"))
            {
                sectionName = context["GroupName"] as string;
            }

            foreach (SettingsProperty setting in collection)
            {
                SettingsPropertyValue value = new SettingsPropertyValue(setting)
                    {
                        IsDirty = false,
                        SerializedValue = this.GetValue(setting, sectionName)
                    };
                values.Add(value);
            }

            return values;
        }

        public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection collection)
        {
            string sectionName = string.Empty;

            if (context.ContainsKey("GroupName"))
            {
                sectionName = context["GroupName"] as string;
            }

            foreach (SettingsPropertyValue propval in collection)
            {
                this.SetValue(propval, sectionName);
            }

            try
            {
                this.SettingsFile.Save(Path.Combine(this.GetAppSettingsPath(), this.GetAppSettingsFilename()));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("No se puede guardar El Archivo de Configuracion: " + ex.Message);
            }
        }

        private void SetValue(SettingsPropertyValue propval, string sectionName)
        {
            string section = sectionName ?? string.Empty;

            XElement baseNode;
            XElement settingNode;

            if (string.IsNullOrWhiteSpace(section))
            {
                baseNode = this.RootNode;
            }
            else
            {
                var node = this.RootNode.Element(section);
                if (node == null)
                {
                    this.RootNode.Add(new XElement(section));
                }

                baseNode = this.RootNode.Element(section);
            }

            try
            {
                settingNode = baseNode != null ? baseNode.Element(propval.Name) : null;
            }
            catch (Exception)
            {
                settingNode = null;
            }

            if (settingNode != null)
            {
                settingNode.Value = propval.SerializedValue.ToString();
            }
            else
            {
                if (baseNode != null)
                {
                    baseNode.Add(new XElement(propval.Name, propval.SerializedValue.ToString()));
                }
                else
                {
                    Debug.WriteLine("SetValue Fallo porque no hay referenca al archivo");
                }
            }
        }

        private string GetValue(SettingsProperty setting, string sectionName)
        {
            string section = sectionName ?? string.Empty;

            XElement baseNode;

            if (string.IsNullOrWhiteSpace(section))
            {
                baseNode = this.RootNode;
            }
            else
            {
                var node = this.RootNode.Element(section);
                if (node == null)
                {
                    this.RootNode.Add(new XElement(section));
                }

                baseNode = this.RootNode.Element(section);
            }

            string ret;

            if (baseNode != null)
            {
                var settingNode = baseNode.Element(setting.Name);
                if (settingNode != null)
                {
                    ret = settingNode.Value;
                }
                else
                {
                    Debug.WriteLine("GetValue Cargando Valor por defecto...");
                    ret = setting.DefaultValue != null ? setting.DefaultValue.ToString() : string.Empty;
                }
            }
            else
            {
                Debug.WriteLine("GetValue Cargando Valor por defecto...");
                ret = setting.DefaultValue != null ? setting.DefaultValue.ToString() : string.Empty;
            }

            return ret;
        }

        private string GetAppSettingsPath()
        {
            FileInfo fileInfo = new FileInfo(Assembly.GetEntryAssembly().Location);
            return fileInfo.DirectoryName;
        }

        private string GetAppSettingsFilename()
        {
            return this.ApplicationName + ".options";
        }
    }
}
