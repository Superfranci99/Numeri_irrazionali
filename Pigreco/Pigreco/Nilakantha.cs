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

        public override double Calcola()
        {
            double pi = 3;
            bool flag = true;
            for (int i = 2; i < Precisione; i += 2)
            {
                if (flag) pi += 4.0 / (i * (i + 1) * (i + 2));
                else pi -= 4.0 / (i * (i + 1) * (i + 2));
                flag = !flag;
            }

            return pi;
        }
    }
}
