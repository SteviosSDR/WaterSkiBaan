using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections;
using static waterskibaan.Game;

namespace waterskibaan
{
    public class Logger
    {
        public readonly List<Sporter> _bezoekers = new List<Sporter>();
        public readonly Kabel _kabel;
        public int TotaalAantalRondes { get; set; }
        public int BezoekersInRood { get; set; }
        public Logger(Kabel kabel)
        {
            _kabel = kabel;
        }

        public List<Sporter> GetLichsteKleuren()
        {
            var a = (from sporter in _bezoekers
                     orderby GetTotaleKleurWaarde(sporter.KledingKleur) descending
                     select sporter).Take(10);

            List<Sporter> kleuren = new List<Sporter>();
            kleuren = a.ToList();
            return kleuren;
        }

        public int GetHighScore()
        {
            int highscore = 0;
            for (int i = 0; i < _bezoekers.Count; i++)
            {
                highscore = _bezoekers.Max(sporter => sporter.aantalPunten);
            }
            return highscore;
        }

        public int GetTotaleRondes()
        {
            return TotaalAantalRondes;
        }

        public List<string> UniekeMoves(LinkedList<Lijn> lijnen)
        {
            List<string> m = new List<string>();

            foreach (Lijn lijn in lijnen)
            {
                if (lijn.huidigeSporter == null) continue;
                foreach (IMoves move in lijn.huidigeSporter.Moves)
                {
                    if (!m.Contains(move.Naam))
                    {
                        m.Add(move.Naam);
                    }
                }
            }
            return m;
        }


        public void VoegBezoekerToe(Sporter sp)
        {
            _bezoekers.Add(sp);
            if (ColorsAreClose(sp.KledingKleur, Color.Red))
            {
                BezoekersInRood++;
            }
            TotaalAantalRondes += sp.AantalRondenNogTeGaan;
        }


        public int GetTotaleBezoekers()
        {
            var totaalBezoekers = from bezoeker in _bezoekers
                                  select bezoeker;
            return totaalBezoekers.Count();
        }

        internal void OnNieuweBezoeker(NieuweBezoekerArgs args)
        {
            VoegBezoekerToe(args.Sporter);
        }

        public bool ColorsAreClose(Color a, Color z, int threshold = 100)
        {
            int r = (int)a.R - z.R,
                g = (int)a.G - z.G,
                b = (int)a.B - z.B;

            return (r * r + g * g + b * b) <= threshold * threshold;
        }
        public int GetTotaleKleurWaarde(Color c)
        {
            return c.R * c.R + c.G * c.G + c.B * c.B;
        }
    }
}
