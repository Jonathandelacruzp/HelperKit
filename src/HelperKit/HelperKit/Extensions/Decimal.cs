using System;

namespace HelperKit
{
    public static partial class Extensions
    {
        #region Decimal

        ///// <summary>
        ///// Calcula la cifra de recondeo para la moneda peruana con la regla .05 centimos
        ///// </summary>
        ///// <returns></returns>
        //public static Decimal GetPeruMoneyRound(this Decimal val)
        //{
        //    val = val.Round();
        //    var value = val.ToString().Split('.')[1].Substring(1).ToDecimal();
        //    if (value == 5 || value == 0)
        //        return new Decimal(0);
        //    else if (value < 5)
        //        return value / (new Decimal(100));
        //    else
        //        return (value - 5) / (new Decimal(100));
        //}

        /// <summary>
        /// Redondea decimal a n campos
        /// </summary>
        /// <param name="val"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public static Decimal Round(this Decimal val, int decimals = 2) => Decimal.Round(val, decimals, MidpointRounding.AwayFromZero);

        #endregion

        #region Decimal Convert Helper

        /// <summary>
        /// Covierte un objeto a un entero tipo Decimal
        /// </summary>
        /// <param name="val"></param>
        /// <param name="def"></param>
        /// <returns>Decimal</returns>
        public static decimal ToDecimal(this object val, decimal def)
        {
            if (decimal.TryParse(val?.ToString(), out var reval))
                return reval;

            return def;
        }

        /// <summary>
        /// Covierte un objeto a un entero tipo Decimal
        /// </summary>
        /// <param name="val"></param>
        /// <returns>Decimal</returns>
        public static decimal ToDecimal(this object val) => ToDecimal(val, 0);

        #endregion

        #region Long Convert Helper

        /// <summary>
        /// Covierte un decimal a un entero tipo long
        /// </summary>
        /// <param name="val">Decimal</param>
        /// <param name="def"></param>
        /// <returns>long</returns>
        public static long ToLong(this decimal val, long def)
        {
            if (long.TryParse(val.ToString(), out var reval))
                return reval;

            return def;
        }

        /// <summary>
        /// Covierte un decimal a un entero tipo long
        /// </summary>
        /// <param name="val">Decimal</param>
        /// <returns>long</returns>
        public static long ToLong(this decimal val) => ToLong(val, 0);

        #endregion
    }
}