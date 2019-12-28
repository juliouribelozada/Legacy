// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerialPortParameters.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the SerialPortParameters type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Atpc.UI.Controls.Configurators.SerialPortConfiguration
{
    using System.Collections.Generic;
    using System.IO.Ports;
    using System.Linq;

    public class SerialPortParameters
    {
        private static readonly IList<Port> availablePorts;
        private static readonly IList<HandShakeProtocol> handShakeProtocols;
        private static readonly IList<DataBits> dataBitsLengths;
        private static readonly IList<ParityValue> parityValues;
        private static readonly IList<StopBitsValue> stopBitsValues;
        private static readonly IList<BaudRate> baudRatesAvailables;

        static SerialPortParameters()
        {
            availablePorts = SerialPort.GetPortNames().ToList().ConvertAll(s => new Port(s)).OrderBy(p => p.Name).ToList();
            baudRatesAvailables = new List<BaudRate>
                {
                    new BaudRate(75),
                    new BaudRate(110),
                    new BaudRate(134),
                    new BaudRate(150),
                    new BaudRate(300),
                    new BaudRate(600),
                    new BaudRate(1200),
                    new BaudRate(1800),
                    new BaudRate(2400),
                    new BaudRate(4800),
                    new BaudRate(7200),
                    new BaudRate(9600),
                    new BaudRate(14400),
                    new BaudRate(19200),
                    new BaudRate(38400),
                    new BaudRate(57600),
                    new BaudRate(115200),
                    new BaudRate(128000)
                };

            dataBitsLengths = new List<DataBits>
                {
                    new DataBits(5),
                    new DataBits(6), 
                    new DataBits(7), 
                    new DataBits(8)
                };

            parityValues = new List<ParityValue>
                {
                    new ParityValue(Parity.Even, "Par"),
                    new ParityValue(Parity.Odd, "Impar"),
                    new ParityValue(Parity.None, "Ninguna"),
                    new ParityValue(Parity.Mark, "Marca"),
                    new ParityValue(Parity.Space, "Espacio")
                };

            stopBitsValues = new List<StopBitsValue>
                {
                    new StopBitsValue(StopBits.One, "1"),
                    new StopBitsValue(StopBits.Two, "2"),
                    new StopBitsValue(StopBits.OnePointFive, "1.5"),
                    new StopBitsValue(StopBits.None, "Ninguna")
                };

            handShakeProtocols = new List<HandShakeProtocol>
                {
                    new HandShakeProtocol(Handshake.None, "Ninguno"),
                    new HandShakeProtocol(Handshake.XOnXOff, "Xon / Xoff"),
                    new HandShakeProtocol(Handshake.RequestToSend, "Hardware")
                };
        }

        public static IList<HandShakeProtocol> HandShakeProtocols
        {
            get
            {
                return handShakeProtocols;
            }
        }

        public static IList<DataBits> DataBitsLengths
        {
            get
            {
                return dataBitsLengths;
            }
        }

        public static IList<ParityValue> ParityValues
        {
            get
            {
                return parityValues;
            }
        }

        public static IList<StopBitsValue> StopBitsValues
        {
            get
            {
                return stopBitsValues;
            }
        }

        public static IList<BaudRate> BaudRatesAvailables
        {
            get
            {
                return baudRatesAvailables;
            }
        }

        public static IList<Port> AvailablePorts
        {
            get
            {
                return availablePorts;
            }
        }
    }
}