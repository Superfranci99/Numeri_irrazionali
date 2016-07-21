using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pigreco
{
    class Cusano : PiAlgo
    {
        public Cusano(int iterazioni) : base(iterazioni) { }

        public override BigDecimal Calcola()
        {
            BigDecimal i = new BigDecimal(2) * new BigDecimal(2).Sqrt();
            BigDecimal c = new BigDecimal(4);

            for (int x = 1; x < Iterazioni; x++)
            {
                c = new BigDecimal(0.5) * ((new BigDecimal(1) / i) + (new BigDecimal(1) / c));
                c = new BigDecimal(1) / c;
                i = (i * c);
                i = i.Sqrt();
            }

            return (i + c) / new BigDecimal(2);
        }
    }
}
