// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerialPortConfigurationViewModel.cs" company="ATPC.co">
//   ATPC © 2012
// </copyright>
// <summary>
//   Defines the SerialPortConfigurationViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

 namespace Atpc.UI.Controls.Configurators.SerialPortConfiguration

{
    using System.Configuration;
    using System.IO.Ports;
    using System.Linq;
    using System.Windows;

    using Atpc.Common;

    internal class SerialPortConfigurationViewModel : NotifiableObject
    {
        private readonly Window window;

        private readonly SerialPortSettings settings = SerialPortConfigurator.Settings;
        private readonly SerialPortParameters parameters = new SerialPortParameters();

        private SettingsPropertyValueCollection defaultSettings;

        private int velocidadOriginal;
        private Parity paridadOriginal;
        private string nombrePuertoOriginal;
        private int bitsDeDatosOriginal;
        private StopBits bitsDeParadaOriginal;
        private Handshake controlDeFlujoOriginal;

        private int velocidad;
        private Parity paridad;
        private string nombrePuerto;
        private int bitsDeDatos;
        private StopBits bitsDeParada;
        private Handshake controlDeFlujo;

        private AtpcBindableCommand acceptChangesCommand;
        private AtpcBindableCommand resetChangesCommand;
        private AtpcBindableCommand resetToDefaultCommand;

        private SerialPort serialPortToConfigure;

        public SerialPortConfigurationViewModel()
        {
        }

        public SerialPortConfigurationViewModel(Window window)
        {
            this.window = window;
        }

        public AtpcBindableCommand AcceptChangesCommand
        {
            get
            {
                return this.acceptChangesCommand ?? (this.acceptChangesCommand = new AtpcBindableCommand(this.AcceptChanges, this.HasChange));
            }
        }

        public AtpcBindableCommand ResetToDefaultCommand
        {
            get
            {
                return this.resetToDefaultCommand ?? (this.resetToDefaultCommand = new AtpcBindableCommand(this.ResetToDefaults, o => true));
            }
        }

        public AtpcBindableCommand ResetChangesCommand
        {
            get
            {
                return this.resetChangesCommand ?? (this.resetChangesCommand = new AtpcBindableCommand(this.Reset, this.HasChange));
            }
        }

        public SerialPortParameters Parameters
        {
            get
            {
                return this.parameters;
            }
        }

        public SerialPortSettings Settings
        {
            get
            {
                return this.settings;
            }
        }

        public SerialPort SerialPortToConfigure
        {
            get
            {
                return this.serialPortToConfigure;
            }

            set
            {
                this.serialPortToConfigure = value;
                this.UpdateModel();
            }
        }

        public string NombrePuerto
        {
            get
            {
                return this.nombrePuerto;
            }

            set
            {
                if (value != this.nombrePuerto)
                {
                    this.nombrePuerto = value;
                    this.NotifyPropertyChanged("NombrePuerto");
                    this.NotifyPropertyChanged("NombrePuertoHasChanges");
                    this.NotifyPropertyChanged("HasChanges");
                }
            }
        }

        public int Velocidad
        {
            get
            {
                return this.velocidad;
            }

            set
            {
                if (value != this.velocidad)
                {
                    this.velocidad = value;
                    this.NotifyPropertyChanged("Velocidad");
                    this.NotifyPropertyChanged("VelocidadHasChanges");
                    this.NotifyPropertyChanged("HasChanges");
                }
            }
        }

        public int BitsDeDatos
        {
            get
            {
                return this.bitsDeDatos;
            }

            set
            {
                if (value != this.bitsDeDatos)
                {
                    this.bitsDeDatos = value;
                    this.NotifyPropertyChanged("BitsDeDatos");
                    this.NotifyPropertyChanged("BitsDeDatosHasChanges");
                    this.NotifyPropertyChanged("HasChanges");
                }
            }
        }

        public Parity Paridad
        {
            get
            {
                return this.paridad;
            }

            set
            {
                if (value != this.paridad)
                {
                    this.paridad = value;
                    this.NotifyPropertyChanged("Paridad");
                    this.NotifyPropertyChanged("ParidadHasChanges");
                    this.NotifyPropertyChanged("HasChanges");
                }
            }
        }

        public StopBits BitsDeParada
        {
            get
            {
                return this.bitsDeParada;
            }

            set
            {
                if (value != this.bitsDeParada)
                {
                    this.bitsDeParada = value;
                    this.NotifyPropertyChanged("BitsDeParada");
                    this.NotifyPropertyChanged("BitsDeParadaHasChanges");
                    this.NotifyPropertyChanged("HasChanges");
                }
            }
        }

        public Handshake ControlDeFlujo
        {
            get
            {
                return this.controlDeFlujo;
            }

            set
            {
                if (value != this.controlDeFlujo)
                {
                    this.controlDeFlujo = value;
                    this.NotifyPropertyChanged("ControlDeFlujo");
                    this.NotifyPropertyChanged("ControlDeFlujoHasChanges");
                    this.NotifyPropertyChanged("HasChanges");
                }
            }
        }

        public bool NombrePuertoHasChanges
        {
            get
            {
                return this.nombrePuerto != this.nombrePuertoOriginal;
            }
        }

        public bool VelocidadHasChanges
        {
            get
            {
                return this.velocidad != this.velocidadOriginal;
            }
        }

        public bool BitsDeDatosHasChanges
        {
            get
            {
                return this.bitsDeDatos != this.bitsDeDatosOriginal;
            }
        }

        public bool ParidadHasChanges
        {
            get
            {
                return this.paridad != this.paridadOriginal;
            }
        }

        public bool BitsDeParadaHasChanges
        {
            get
            {
                return this.bitsDeParada != this.bitsDeParadaOriginal;
            }
        }

        public bool ControlDeFlujoHasChanges
        {
            get
            {
                return this.controlDeFlujo != this.controlDeFlujoOriginal;
            }
        }

        public bool HasChanges
        {
            get
            {
                return this.VelocidadHasChanges || this.ParidadHasChanges || this.NombrePuertoHasChanges || this.BitsDeDatosHasChanges || this.BitsDeParadaHasChanges || this.ControlDeFlujoHasChanges;
            }
        }

        public void Reset(object parameter)
        {
            this.Velocidad = this.velocidadOriginal;
            this.Paridad = this.paridadOriginal;
            this.NombrePuerto = this.nombrePuertoOriginal;
            this.BitsDeDatos = this.bitsDeDatosOriginal;
            this.BitsDeParada = this.bitsDeParadaOriginal;
            this.ControlDeFlujo = this.controlDeFlujoOriginal;
        }

        private void ResetToDefaults(object obj)
        {
            if (this.defaultSettings == null)
            {
                this.defaultSettings = new SettingsPropertyValueCollection();
                foreach (SettingsProperty property in this.Settings.Properties)
                {
                    var propertyValue = new SettingsPropertyValue(property)
                        { SerializedValue = property.DefaultValue, IsDirty = false };
                    this.defaultSettings.Add(propertyValue);
                }
            }

            // this.NombrePuerto = (string)this.defaultSettings["PortNameSetting"].PropertyValue;
            this.NombrePuerto = SerialPortParameters.AvailablePorts.First().Name;
            this.Velocidad = (int)this.defaultSettings["BaudRateSetting"].PropertyValue;
            this.BitsDeDatos = (int)this.defaultSettings["DataBitsSetting"].PropertyValue;
            this.Paridad = (Parity)this.defaultSettings["ParitySetting"].PropertyValue;
            this.BitsDeParada = (StopBits)this.defaultSettings["StopBitsSetting"].PropertyValue;
            this.ControlDeFlujo = (Handshake)this.defaultSettings["HandshakeSetting"].PropertyValue;
        }

        private bool HasChange(object parameter)
        {
            return this.HasChanges;
        }

        private void AcceptChanges(object parameter)
        {
            this.serialPortToConfigure.BaudRate = this.Velocidad;
            this.serialPortToConfigure.Parity = this.Paridad;
            this.serialPortToConfigure.PortName = this.NombrePuerto;
            this.serialPortToConfigure.StopBits = this.BitsDeParada;
            this.serialPortToConfigure.DataBits = this.BitsDeDatos;
            this.serialPortToConfigure.Handshake = this.ControlDeFlujo;

            this.Settings.PortNameSetting = this.serialPortToConfigure.PortName;
            this.Settings.BaudRateSetting = this.serialPortToConfigure.BaudRate;
            this.Settings.DataBitsSetting = this.serialPortToConfigure.DataBits;
            this.Settings.ParitySetting = this.serialPortToConfigure.Parity;
            this.Settings.StopBitsSetting = this.serialPortToConfigure.StopBits;
            this.Settings.HandshakeSetting = this.serialPortToConfigure.Handshake;
            this.Settings.Save();
            if (this.window != null)
            {
                this.window.DialogResult = true;
            }
        }

        private void UpdateModel()
        {
            this.velocidadOriginal = this.serialPortToConfigure.BaudRate;
            this.paridadOriginal = this.serialPortToConfigure.Parity;
            this.nombrePuertoOriginal = this.serialPortToConfigure.PortName;
            this.bitsDeDatosOriginal = this.serialPortToConfigure.DataBits;
            this.bitsDeParadaOriginal = this.serialPortToConfigure.StopBits;
            this.controlDeFlujoOriginal = this.serialPortToConfigure.Handshake;

            this.velocidad = this.serialPortToConfigure.BaudRate;
            this.paridad = this.serialPortToConfigure.Parity;
            this.nombrePuerto = this.serialPortToConfigure.PortName;
            this.bitsDeDatos = this.serialPortToConfigure.DataBits;
            this.bitsDeParada = this.serialPortToConfigure.StopBits;
            this.controlDeFlujo = this.serialPortToConfigure.Handshake;
        }
    }
}
