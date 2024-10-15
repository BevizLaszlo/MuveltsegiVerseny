using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuveltsegiVerseny
{
    internal class Kerdes
    {
        public string KerdesString { get; set; }
        public int Valasz { get; set; }
        public int Pont { get; set; }
        public string Tema { get; set; }

        public Kerdes(string sor)
        {
            string[] adatok = sor.Split(';');

            KerdesString = adatok[0];
            Valasz = int.Parse(adatok[1]);
            Pont = int.Parse(adatok[2]);
            Tema = adatok[3];
        }
    }
}
