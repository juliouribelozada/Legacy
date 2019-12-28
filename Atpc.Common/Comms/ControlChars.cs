// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControlChars.cs" company="ATPC.co">
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Atpc.Common.Comms
{
    using System;
    using System.Globalization;

    // ToDo= Incluir otros ControlChars: http://en.wikipedia.org/wiki/ASCII
    public class ControlChars
    {
        /// <summary>
        /// Dec: 1 Hex:0x01
        /// </summary>
        public const byte SOH = 1;

        /// <summary>
        /// Dec: 2 Hex:0x02
        /// </summary>
        public const byte STX = 2;

        /// <summary>
        /// Dec: 3 Hex:0x03
        /// </summary>
        public const byte ETX = 3;

        /// <summary>
        /// Dec: 4 Hex:0x04
        /// </summary>
        public const byte EOT = 4;

        /// <summary>
        /// Dec: 5 Hex:0x05
        /// </summary>
        public const byte ENQ = 5;

        /// <summary>
        /// Dec: 6 Hex:0x06
        /// </summary>
        public const byte ACK = 6;

        /// <summary>
        /// Dec: 7 Hex:0x07
        /// </summary>
        public const byte BEL = 7;

        /// <summary>
        /// Dec: 9 Hex:0x09
        /// </summary>
        public const byte HT = 9;

        /// <summary>
        /// Dec: 10 Hex:0x0A
        /// </summary>
        public const byte LF = 10;

        /// <summary>
        /// Dec: 11 Hex:0x0B
        /// </summary>
        public const byte SB = 11;

        /// <summary>
        /// Dec: 13 Hex:0x0D
        /// </summary>
        public const byte CR = 13;

        /// <summary>
        /// Dec: 16 Hex:0x20
        /// </summary>
        public const byte DLE = 16;

        /// <summary>
        /// Dec: 21 Hex:0x15
        /// </summary>
        public const byte NAK = 21;

        /// <summary>
        /// Dec: 23 Hex:0x17
        /// </summary>
        public const byte ETB = 23;

        /// <summary>
        /// Dec: 28 Hex:0x1C
        /// </summary>
        public const byte EB = 28;

        /// <summary>
        /// Dec: 29 Hex:0x1D
        /// </summary>
        public const byte GS = 29;

        /// <summary>
        /// Dec: 30 Hex:0x1E
        /// </summary>
        public const byte RS = 30;

        public static string ControlCharsReadable(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                string output = input;
                output = output.Replace(Convert.ToChar(ACK).ToString(CultureInfo.InvariantCulture), "<ACK>");
                output = output.Replace(Convert.ToChar(BEL).ToString(CultureInfo.InvariantCulture), "<BEL>");
                output = output.Replace(Convert.ToChar(DLE).ToString(CultureInfo.InvariantCulture), "<DLE>");
                output = output.Replace(Convert.ToChar(EOT).ToString(CultureInfo.InvariantCulture), "<EOT>");
                output = output.Replace(Convert.ToChar(HT).ToString(CultureInfo.InvariantCulture), "<HT>");
                output = output.Replace(Convert.ToChar(SB).ToString(CultureInfo.InvariantCulture), "<SB>");
                output = output.Replace(Convert.ToChar(SOH).ToString(CultureInfo.InvariantCulture), "<SOH>");
                output = output.Replace(Convert.ToChar(STX).ToString(CultureInfo.InvariantCulture), "<STX>");
                output = output.Replace(Convert.ToChar(EB).ToString(CultureInfo.InvariantCulture), "<EB>");
                output = output.Replace(Convert.ToChar(ENQ).ToString(CultureInfo.InvariantCulture), "<ENQ>");
                output = output.Replace(Convert.ToChar(ETB).ToString(CultureInfo.InvariantCulture), "<ETB>");
                output = output.Replace(Convert.ToChar(ETX).ToString(CultureInfo.InvariantCulture), "<ETX>");
                output = output.Replace(Convert.ToChar(LF).ToString(CultureInfo.InvariantCulture), "<LF>");
                output = output.Replace(Convert.ToChar(CR).ToString(CultureInfo.InvariantCulture), "<CR>");
                output = output.Replace(Convert.ToChar(NAK).ToString(CultureInfo.InvariantCulture), "<NAK>");
                output = output.Replace(Convert.ToChar(GS).ToString(CultureInfo.InvariantCulture), "<GS>");
                output = output.Replace(Convert.ToChar(RS).ToString(CultureInfo.InvariantCulture), "<RS>");
                return output;
            }

            return string.Empty;
        }
    }
}
