using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace waterskibaan
{
    public class Waterskibaan
    {
        Random aantalRondes = new Random();
        Kabel kabel { get; set; } = new Kabel();
        LijnenVoorraad lijnvoorraad { get; set; } = new LijnenVoorraad();
        
        public void SporterStart(Sporter sp)
        {
            if (sp.Zwemvest == null | sp.Skies == null)
            {
                throw new NullReferenceException("Deze sporter heeft zijn skies en/of zijn zemvest vergeten.");
            }

            else if(kabel.isStartPosietieLeeg())
            {
                Lijn lijn = lijnvoorraad.verwijderEersteLijn();
                lijn.huidigeSporter = sp;
                sp.AantalRondenNogTeGaan = aantalRondes.Next(1, 2);
                kabel.NeemLijnInGeruik(lijn);
            }
        }

        public void waterskibaan()
        {
            for (int i = 0; i < 15; i++)
            {
                Lijn lijn = new Lijn();
                lijnvoorraad.LijnToevoegenAanRij(lijn);
                //Console.WriteLine("Lijn toegevoegd");
            }
        }

        public void VerplaatsKabel()
        {
            kabel.VerschuifLijnen();
            lijnvoorraad.LijnToevoegenAanRij(kabel.VerwijderLijnVanKabel());
        }

        override
        public string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(lijnvoorraad.ToString());
            sb.Append(kabel.ToString());
            return sb.ToString();
        }
    }
}
