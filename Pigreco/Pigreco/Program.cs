using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pigreco
{
    class Program
    {
        static void Main(string[] args)
        {
            PiAlgo p = new Nilakantha(100);
            Console.WriteLine(p.Calcola());
            Console.Read();
            
        }
    }
}
