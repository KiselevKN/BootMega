using System;
using Service.Settings.Enums;

namespace Service.Settings.Extensions
{
    internal static class BaudRateExtensions
    {
        internal static int ToInt32(this BaudRate baudRate)
        {
            return (int)baudRate;
        }

        internal static string ToString(this BaudRate baudRate, bool asInt32)
        {
            return (asInt32) ? (baudRate.ToInt32()).ToString() : baudRate.ToString();
        }

        internal static BaudRate ToBaudRate(this int baudRate)
        {
            return (BaudRate)baudRate;
        }

        internal static BaudRate ToBaudRate(this string baudRate, bool asInt32)
        {
            return (asInt32) ? (BaudRate)Convert.ToInt32(baudRate) : (BaudRate)Enum.Parse(typeof(BaudRate), baudRate);
        }     
    }
}
