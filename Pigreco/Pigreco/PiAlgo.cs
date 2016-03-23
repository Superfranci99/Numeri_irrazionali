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
        public int Precisione
        {
            get { return Precisione; }
            set { if (value > 0) Precisione = value; else Precisione = 1; }
        }

        public PiAlgo(int precisione)
        {
            this.Precisione = precisione;
        }

        /// <summary>
        /// Calcola il valore di pi greco
        /// </summary>
        /// <returns></returns>
        public abstract double Calcola();

    }
}
