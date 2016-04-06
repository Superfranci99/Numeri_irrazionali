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

        public Leibniz(int iterazioni) : base(iterazioni)
        {
        }

        public override BigDecimal Calcola()
        {
            BigDecimal pi = new BigDecimal(0);
            bool flag = true;

            for (int i = 0; i < Iterazioni; i++)
            {
                if(flag) pi += new BigDecimal(1) / ((new BigDecimal(2) * new BigDecimal(i)) + new BigDecimal(1));
                else pi -= new BigDecimal(1) / ((new BigDecimal(2) * new BigDecimal(i)) + new BigDecimal(1));
                flag = !flag;
            }

            return pi * new BigDecimal(4);
        }
    }
}
