using System;

namespace Shopping_Cart.Helpers
{
    class TruncateDecimals
    {
        /// <summary>
        /// Truncates a decimal without rounding it
        /// </summary>
        /// <param name="d">decimal</param>
        /// <param name="decimals">number of decimal places</param>
        /// <returns></returns>
        public static decimal Truncate(decimal d, byte decimals)
        {
            decimal r = Math.Round(d, decimals);
            if (d > 0 && r > d)
            {
                return r - new decimal(1, 0, 0, false, decimals);
            }
            else if (d < 0 && r < d)
            {
                return r + new decimal(1, 0, 0, false, decimals);
            }
            return r;
        }
    }
}
