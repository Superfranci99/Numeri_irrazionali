using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pigreco
{
    class Nilakantha : PiAlgo
    {
        // Precisione: numero di iterazioni, non corrisponde al numero di cifre dopo la virgola

        public Nilakantha(int precisione) : base(precisione)
        {
        }

        public override BigDecimal Calcola()
        {
            BigDecimal pi = new BigDecimal(3);
            bool flag = true;
            for (int i = 2; i < Precisione; i += 2)
            {
                if (flag) pi += new BigDecimal(4) / (new BigDecimal(i) * new BigDecimal(i + 1) * new BigDecimal(i + 2));
                else      pi -= new BigDecimal(4) / (new BigDecimal(i) * new BigDecimal(i + 1) * new BigDecimal(i + 2));
                flag = !flag;
            }

            return pi;
        }
    }
}
