using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace waterskibaan
{
    public class LijnenVoorraad
    {
        private Queue<Lijn> _lijnen { get; set; } = new Queue<Lijn>();

        public void LijnToevoegenAanRij(Lijn lijn)
        {
            if(lijn != null)
            {
                //Console.WriteLine("lijn toegevoegd aan rij");
                _lijnen.Enqueue(lijn);
            }
        }

        public Lijn verwijderEersteLijn()
        {
            //Console.WriteLine("Lijn verwijderd");
            return _lijnen.Dequeue();
        }

        public int GetAantalLijnen()
        {
            return _lijnen.Count;
        }

        override
        public string ToString()
        {
            return $"{GetAantalLijnen()} lijnen op voorraad.";
        }
    }
}
