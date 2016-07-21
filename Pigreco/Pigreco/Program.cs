using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace Pigreco
{
    class Program
    {
        public static BigDecimal piorig = new BigDecimal("3,1415926535897932384626433832795028841971693993751058209749445923078164062862089986280348253421170679");

        static void Main(string[] args)
        {
        }

        public static BigDecimal Prec(BigDecimal pical)
        {
            return (new BigDecimal(100) * pical) / piorig;
        }
    }
}
