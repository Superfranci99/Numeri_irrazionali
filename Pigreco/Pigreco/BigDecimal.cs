using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pigreco
{
    class BigDecimal
    {
        public int ParteIntera { get; set; }  // 3 --> non deve essere infinitamente grande

        public List<int> ParteDecimale { get; set; } = new List<int>(); // lista di numeri a coppia

        private char _separatore = '.';

        public BigDecimal(string numero)
        {
            // controlli iniziali
            if (string.IsNullOrWhiteSpace(numero)) throw new NullReferenceException("La stringa è nulla");
            numero = numero.Replace(',', _separatore);
            string[] div = numero.Split(_separatore);
            if (div.Length > 2) throw new ArgumentException("Stringa non convertibile");

            // ottiene la parte intera del numero
            this.ParteIntera = int.Parse(div[0]); 

            // ora la parte decimale...
            if (div[1].Length % 2 == 1) div[1] += "0"; // rende le cifre pari
            for(int i=0; i<div[1].Length; i += 2)
            {
                string num = div[1].Substring(i, 2);
                this.ParteDecimale.Add(int.Parse(num));
            }
        }
    }
}
