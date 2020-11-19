using System;
using System.Globalization;

namespace HelperKit
{
    public static partial class Extensions
    {
        #region Decimal

        // /// <summary>
        // /// Calcula la cifra de recondeo para la moneda peruana con la regla .05 centimos
        // /// </summary>
        // /// <param name="val"></param>
        // /// <returns></returns>
        // public static decimal GetPeruMoneyRound(this decimal val)
        // {
        //     val = val.Round();
        //     var value = val.ToString(CultureInfo.InvariantCulture).Split('.')[1].Substring(1).ToDecimal();
        //     if (value == 5 || value == 0)
        //         return new decimal(0);
        //     if (value < 5)
        //         return value / (new decimal(100));
        //     return (value - 5) / (new decimal(100));
        // }

        /// <summary>
        /// Round a decimal value by a number of decimals
        /// </summary>
        /// <param name="val"></param>
        /// <param name="decimals">Number of decimals (default 2)</param>
        /// <returns></returns>
        public static decimal Round(this decimal val, int decimals = 2)
        {
            return decimal.Round(val, decimals, MidpointRounding.AwayFromZero);
        }

        #endregion

        #region Decimal Convert Helper

        /// <summary>
        /// Converts a value to decimal
        /// </summary>
        /// <param name="val"></param>
        /// <param name="def"></param>
        /// <returns>Decimal</returns>
        public static decimal ToDecimal(this object val, decimal def = 0)
        {
            return decimal.TryParse(val?.ToString(), out var result) ? result : def;
        }

        #endregion

        #region Long Convert Helper

        /// <summary>
        /// Converts a value to long
        /// </summary>
        /// <param name="val">Decimal</param>
        /// <param name="def"></param>
        /// <returns>long</returns>
        public static long ToLong(this decimal val, long def = 0)
        {
            return long.TryParse(val.ToString(CultureInfo.CurrentCulture), out var result) ? result : def;
        }

        #endregion
    }
}