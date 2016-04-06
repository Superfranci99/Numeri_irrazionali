using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pigreco
{
    abstract class PiAlgo
    {
        /// <summary>
        /// Numero di cifre da calcolare dopo la virgola
        /// </summary>
        public int Iterazioni { get; set; }

        public PiAlgo(int iterazioni)
        {
            this.Iterazioni = iterazioni;
        }

        /// <summary>
        /// Calcola il valore di pi greco
        /// </summary>
        /// <returns></returns>
        public abstract BigDecimal Calcola();

    }
}
