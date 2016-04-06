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
        public BigInteger ParteIntera { get; set; }  // 3 --> non deve essere infinitamente grande

        public List<int> ParteDecimale { get; set; } = new List<int>(); // lista di numeri a coppia

        private static char _separatore = '.';
        private static int _prec = 100;  //precisione nel calcolo di divisioni e radice
        private static BigInteger _precisione = BigInteger.Pow(10, _prec);

        public BigDecimal(string numero)
        {
            // controlli iniziali
            if (string.IsNullOrWhiteSpace(numero)) throw new NullReferenceException("La stringa è nulla");
            numero = numero.Replace(',', _separatore);
            //numero = numero.TrimEnd('0');
            string[] div = numero.Split(_separatore);
            if (div.Length > 2) throw new ArgumentException("Stringa non convertibile");

            // ottiene la parte intera del numero
            if (div[0] == "") ParteIntera = BigInteger.Zero;
            else ParteIntera = BigInteger.Parse(div[0]);


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
            string result = this.ParteIntera.ToString() + ".";
            foreach (int n in ParteDecimale)
            {
                if(n < 10) result += '0' + n.ToString();
                else result += n.ToString();
            }
                
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
            result.Clean();
            return (result);

        }

        public static BigDecimal operator *(BigDecimal n1, BigDecimal n2)
        {
            BigInteger bg1, bg2;
            bg1 = BigInteger.Parse(n1.ToString().Replace(_separatore.ToString(), ""));
            bg2 = BigInteger.Parse(n2.ToString().Replace(_separatore.ToString(), ""));
            string result = (bg1 * bg2).ToString();
            

            int n = 0;
            if(n1.ParteDecimale.Count > 0)
            {
                if (n1.ParteDecimale[n1.ParteDecimale.Count - 1] % 10 == 0) n += (n1.ParteDecimale.Count * 2) - 1;
                else n += n1.ParteDecimale.Count * 2;
            }

            if (n2.ParteDecimale.Count > 0)
            {
                if (n2.ParteDecimale[n2.ParteDecimale.Count - 1] % 10 == 0) n += (n2.ParteDecimale.Count * 2) - 1;
                else n += n2.ParteDecimale.Count * 2;
            }




            //int n = (n1.ParteDecimale.Count + n2.ParteDecimale.Count) * 2;
            //int n = _prec;
            int aaaa = result.Length;
            result = result.PadRight(n, '0');

            if(n > 0) result = result.Insert(result.Length - n, _separatore.ToString());
            BigDecimal risul = new BigDecimal(result);
            risul.Clean();
            return (risul);
        }

        public static BigDecimal operator /(BigDecimal n1, BigDecimal n2)
        {
            int oldPrec = _prec;
            BigInteger bg1, bg2;
            bg1 = BigInteger.Parse(n1.ToString().Replace(_separatore.ToString(), "")) * _precisione;
            bg2 = BigInteger.Parse(n2.ToString().Replace(_separatore.ToString(), ""));
            bool flag = false;
            while (bg2 > bg1)
            {
                flag = true;
                bg1 *= 10;
                _prec++;
            }
            for(int i=0; (i<oldPrec) && flag; i++)
            {
                bg1 *= 10;
                _prec++;
            }

            
            BigInteger numres = (bg1 / bg2);
            while (numres < _precisione)
            {
                bg1 *= 10;
                _prec++;
                numres = (bg1 / bg2);
            }
            _precisione = BigInteger.Pow(10, _prec);

            string result = numres.ToString();
            int aaa = result.Length;
            //int n = _precisione.ToString().Length - 1; //_prec
            int n = 0;
            //result = result.PadLeft(n + 1, '0');
            if (n2.ParteDecimale.Count > 0)
            {
                if (n2.ParteDecimale[n2.ParteDecimale.Count - 1] % 10 == 0) n += (n2.ParteDecimale.Count * 2) - 1;
                else n += n2.ParteDecimale.Count * 2;
            }



            //TOFIXXXXXXXXXXXXXXX




            int virgola = Math.Abs(n - _prec);
            result = result.PadLeft(_prec, '0');
            //int virgola = Math.Abs(n - _prec);
            //result = result.PadLeft(_prec, '0');

            result = result.Insert(result.Length - virgola, _separatore.ToString());
            BigDecimal risul = new BigDecimal(result);
            risul.Clean();
            _prec = oldPrec;
            _precisione = BigInteger.Pow(10, _prec);
            return (risul);
        }

        public static BigDecimal operator -(BigDecimal n1, BigDecimal n2)
        {
            BigDecimal result = new BigDecimal(0);
            
            if(n1.ParteDecimale.Count < n2.ParteDecimale.Count)
            {
                for (int i = n1.ParteDecimale.Count; i < n2.ParteDecimale.Count; i++)
                    n1.ParteDecimale.Add(0);
            }
            

            // mantieni la parte decimale più piccola di n1, fino ad arrivare alla precisione di n2
            if (n1.ParteDecimale.Count > n2.ParteDecimale.Count)
            {
                for (int i = n1.ParteDecimale.Count - 1; i > n2.ParteDecimale.Count - 1; i--)
                    result.ParteDecimale.Add(n1.ParteDecimale[i]);
            }

            //int carry = 0; // resto
            for (int i = n2.ParteDecimale.Count - 1; i >= 0; i--)
            {
                if(n1.ParteDecimale[i] >= n2.ParteDecimale[i]) result.ParteDecimale.Add(n1.ParteDecimale[i] - n2.ParteDecimale[i]);
                else
                {
                    if (i == 0) n1.ParteIntera--;
                    else n1.ParteDecimale[i - 1]--;
                    result.ParteDecimale.Add(100 + n1.ParteDecimale[i] - n2.ParteDecimale[i]);
                }
            
            }

            result.ParteIntera = n1.ParteIntera - n2.ParteIntera;
            result.ParteDecimale.Reverse();
            result.Clean();
            return (result);
        }

        public BigDecimal Sqrt()
        {
            string result = "";
            // separa la parte intera a coppie di due
            BigInteger parteIntera = ParteIntera;
            int n;
            List<int> listaInteri = new List<int>();
            while(parteIntera > 0)
            {
                n = int.Parse(BigInteger.Remainder(parteIntera, 100).ToString());
                listaInteri.Add(n);
                parteIntera = (parteIntera - n) / 100;
            }
            listaInteri.Reverse();

            //cerca la radice approssimata della prima coppia
            int x = SearchMinQuadrato(listaInteri[0]);
            result = x.ToString();

            BigInteger intermedio = listaInteri[0] - x * x;

            // cicla la parte intera
            for (int i = 1; i < listaInteri.Count; i++)
            {
                intermedio = intermedio * 100 + listaInteri[i];
                int val = SearchMaxProdotto(intermedio, BigInteger.Parse(result) * 20);
                intermedio -= ((BigInteger.Parse(result) * 20 + val) * val);
                result += val.ToString();
            }

            int virgola = result.Length;

            // cicla la parte con la virgola
            for(int i=0; i < ParteDecimale.Count; i++)
            {
                intermedio = intermedio * 100 + ParteDecimale[i];
                int val = SearchMaxProdotto(intermedio, BigInteger.Parse(result) * 20);
                intermedio -= ((BigInteger.Parse(result) * 20 + val) * val);
                result += val.ToString();
            }

            // se non c'è resto esci
            if(intermedio == 0)
            {
                result = result.Insert(virgola, ".");
                return new BigDecimal(result);
            }

            int maxcicli = _prec - (result.Length - virgola);

            for(int i=0; i< maxcicli; i++)
            {
                intermedio = intermedio * 100;
                int val = SearchMaxProdotto(intermedio, BigInteger.Parse(result) * 20);
                intermedio -= ((BigInteger.Parse(result) * 20 + val) * val);
                result += val.ToString();
            }

            result = result.Insert(virgola, ".");
            BigDecimal risul = new BigDecimal(result);
            risul.Clean();
            return (risul);
        }

        private int SearchMinQuadrato(int n)
        {
            if (n >= 100) throw new ArgumentException();
            int i = 1;
            while ((i * i) <= n) i++;
            return i -1;
        }

        private int SearchMaxProdotto(BigInteger max, BigInteger coeff)
        {
            int i = 1;
            while (((coeff + i) * i) < max) i++;
            return i -1 ;
        }

        public void Clean()
        {
            int i=ParteDecimale.Count-1;
            while ((i>=0) && (ParteDecimale[i] == 0))
            {
                ParteDecimale.RemoveAt(i);
                i--;
            }
        }
    }
}
