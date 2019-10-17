using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace waterskibaan
{
    public class Kabel
    {
        public LinkedList<Lijn> lijnen = new LinkedList<Lijn>();
        public Kabel()
        {

        }

        public bool isStartPosietieLeeg()
        {
            foreach(Lijn lijn in lijnen)
            {
                if(lijn.PositieOpDeKabel == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public void NeemLijnInGeruik(Lijn lijn)
        {
            if(isStartPosietieLeeg())
            {
                lijnen.AddFirst(lijn);
            }
            else
            {
                return;
            }
        }

        public void VerschuifLijnen()
        {
            foreach(Lijn lijn in lijnen)
            {
                if (lijn.PositieOpDeKabel < 10)
                {
                    lijn.PositieOpDeKabel++;
                }

                else
                {
                    lijn.PositieOpDeKabel = 0;
                    lijn.huidigeSporter.AantalRondenNogTeGaan--;
                }
            }
        }

        public Lijn VerwijderLijnVanKabel()
        {
            foreach(Lijn lijn in lijnen)
            {
                if (lijn.PositieOpDeKabel == 10 && lijn.huidigeSporter.AantalRondenNogTeGaan == 1)
                {
                    //Console.WriteLine("sporter is klaar");
                    lijnen.Remove(lijn);
                    return lijn;
                }
            }
            return null;
        }

        override
        public string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach(Lijn lijn in lijnen)
            {
                sb.Append($"{lijn.PositieOpDeKabel}|");
            }

            if(sb.Length > 1)
            {
                sb.Remove(sb.Length - 1, 1);
            }

            return sb.ToString();
        }
    }
}
