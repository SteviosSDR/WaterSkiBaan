using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace waterskibaan
{
    public class Kabel
    {
        private LinkedList<Lijn> _lijnen { get; set; } 

        public Kabel()
        {
            this._lijnen = new LinkedList<Lijn>();
        }

        public bool isStartPosietieLeeg()
        {
            foreach(Lijn lijn in _lijnen)
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
                _lijnen.AddFirst(lijn);
                lijn.PositieOpDeKabel = 0;
            }
        }

        public void VerschuifLijnen()
        {
            foreach(Lijn lijn in _lijnen)
            {
                if(lijn.PositieOpDeKabel < 10)
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
            foreach(Lijn lijn in _lijnen)
            {
                if (lijn.PositieOpDeKabel == 10 && lijn.huidigeSporter.AantalRondenNogTeGaan == 1)
                {
                    _lijnen.Remove(lijn);
                    return lijn;
                }
            }
            return null;
        }

        override
        public string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach(Lijn lijn in _lijnen)
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
