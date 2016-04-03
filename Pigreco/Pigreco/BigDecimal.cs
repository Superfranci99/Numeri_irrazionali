using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pigreco
{
    class BigDecimal
    {
        public int ParteIntera { get; set; }  // 3 --> non deve essere infinitamente grande

        public List<int> ParteDecimale { get; set; } = new List<int>(); // lista di numeri a coppia

        private static char _separatore = '.';
        private static int _prec = 20;  //precisione nelle divisioni
        private static BigInteger _precisione = BigInteger.Pow(10, _prec);

        public BigDecimal(string numero)
        {
            // controlli iniziali
            if (string.IsNullOrWhiteSpace(numero)) throw new NullReferenceException("La stringa è nulla");
            numero = numero.Replace(',', _separatore);
            numero = numero.TrimEnd('0');
            string[] div = numero.Split(_separatore);
            if (div.Length > 2) throw new ArgumentException("Stringa non convertibile");

            // ottiene la parte intera del numero
            ParteIntera = int.Parse(div[0]);

            if (div.Length == 1) return; // esci se esiste solo la parte intera

            // ora la parte decimale...
            if (div[1].Length % 2 == 1) div[1] += "0"; // rende le cifre pari
            for(int i=0; i<div[1].Length; i += 2)
            {
                string num = div[1].Substring(i, 2);
                ParteDecimale.Add(int.Parse(num));
            }
        }

        public BigDecimal(int numero)    : this(numero.ToString()) { }
        public BigDecimal(byte numero)   : this(numero.ToString()) { }
        public BigDecimal(float numero)  : this(numero.ToString()) { }
        public BigDecimal(double numero) : this(numero.ToString()) { }

        public override string ToString()
        {
            string result = this.ParteIntera.ToString();
            foreach (int n in ParteDecimale)
                result += n.ToString();
            result = result.TrimEnd('0');
            return result;
        }

        public static BigDecimal operator +(BigDecimal n1, BigDecimal n2)
        {
            BigDecimal result = new BigDecimal(0);
            // n1 deve essere il numero con più cifre dopo la virgola
            if(n2.ParteDecimale.Count > n1.ParteDecimale.Count)
            {
                BigDecimal n3 = n1;
                n1 = n2;
                n2 = n3;
            }

            // mantieni la parte decimale più piccola di n1, fino ad arrivare alla precisione di n2
            if(n1.ParteDecimale.Count > n2.ParteDecimale.Count)
            {
                for (int i = n1.ParteDecimale.Count - 1; i > n2.ParteDecimale.Count - 1; i--)
                    result.ParteDecimale.Add(n1.ParteDecimale[i]);
            }

            int carry = 0; // resto
            for (int i = n2.ParteDecimale.Count - 1; i >= 0; i--)
            {
                int somma = n1.ParteDecimale[i] + n2.ParteDecimale[i] + carry;
                if (somma >= 100)
                {
                    result.ParteDecimale.Add(somma % 100);
                    carry = (somma - (somma % 100)) / 100;
                }
                else
                {
                    result.ParteDecimale.Add(somma);
                    carry = 0;
                }
            }

            result.ParteIntera = n1.ParteIntera + n2.ParteIntera + carry;
            result.ParteDecimale.Reverse();
            return result;

        }

        public static BigDecimal operator *(BigDecimal n1, BigDecimal n2)
        {
            BigInteger bg1, bg2;
            bg1 = BigInteger.Parse(n1.ToString().Replace(_separatore.ToString(), ""));
            bg2 = BigInteger.Parse(n2.ToString().Replace(_separatore.ToString(), ""));
            string result = (bg1 * bg2).ToString();
            int n = (n1.ParteDecimale.Count + n2.ParteDecimale.Count) * 2;
            result = result.Insert(result.Length - n +1, _separatore.ToString());
            return new BigDecimal(result);
        }

        public static BigDecimal operator /(BigDecimal n1, BigDecimal n2)
        {
            BigInteger bg1, bg2;
            bg1 = BigInteger.Parse(n1.ToString().Replace(_separatore.ToString(), "")) * _precisione;
            bg2 = BigInteger.Parse(n2.ToString().Replace(_separatore.ToString(), ""));
            string result = (bg1 / bg2).ToString();
            int n = _precisione.ToString().Length - 1;
            result = result.PadLeft(n + 1, '0');
            result = result.Insert(result.Length - n + 2, _separatore.ToString());
            return new BigDecimal(result);
        }
    }
}
