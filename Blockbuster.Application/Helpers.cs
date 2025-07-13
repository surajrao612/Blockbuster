using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockbuster.Application
{
    public static class Helpers
    {
        public static decimal ConvertToDecimal(this string? price)
        {
            if (string.IsNullOrEmpty(price))
                return 0;

            else return decimal.Parse(price);
        }
    }
}
