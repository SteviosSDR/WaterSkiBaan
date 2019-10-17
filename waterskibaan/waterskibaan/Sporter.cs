using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace waterskibaan
{
    public class Zwemvest
    {

    }

    public class Skies
    {

    }

    public class Color
    {

    }

     public class Sporter
    {
        public Sporter(List<IMoves> Moves)
        {
            this.Moves = Moves;
            for(int i = 0; i < Moves.Count; i++)
            {
                aantalPunten = aantalPunten + (Moves[i].Move()); 
            }
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
    }
}
