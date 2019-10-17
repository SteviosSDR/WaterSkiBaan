using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace waterskibaan
{
    public class Zwemvest
    {

    }

    public class Skies
    {

    }

    public class Sporter
    {
        private static readonly Random rnd = new Random();

        public Sporter(List<IMoves> Moves)
        {
            this.Moves = Moves;
            for(int i = 0; i < Moves.Count; i++)
            {
                aantalPunten = aantalPunten + (Moves[i].Move()); 
            }
            KledingKleur = GetRandomKleur();

            AantalRondenNogTeGaan = aantalRondes();
        }

        public IMoves HuidigeMove
        {
            get;

            set;
        }

        public int aantalPunten
        {
            get;

            set;
        }

        public int AantalRondenNogTeGaan
        {
            get;

            set;
        }

        public Zwemvest Zwemvest
        {
            get;

            set;
        } = new Zwemvest();

        public Skies Skies
        {
            get;

            set;
        } = new Skies();

        public Color KledingKleur
        {
            get;

            set;
        }

        public List<IMoves> Moves
        {
            get;

            set;
        }
        private int aantalRondes()
        {
            return rnd.Next(2) + 1;
        }
        private static Color GetRandomKleur()
        {
            return Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
        }
    }
}
