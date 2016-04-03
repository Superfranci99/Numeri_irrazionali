using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pigreco
{
    class Leibniz : PiAlgo
    {
        // Precisione: numero di iterazioni, non corrisponde al numero di cifre dopo la virgola

        public Leibniz(int precisione) : base(precisione)
        {
        }

        public override BigDecimal Calcola()
        {
            BigDecimal pi = new BigDecimal(0);

            for (int i = 1; i < Precisione; i += 4)
            {
                pi += new BigDecimal(2) / (new BigDecimal(i) * (new BigDecimal(i + 2)));
            }

            return pi * new BigDecimal(4);
        }
    }
}
