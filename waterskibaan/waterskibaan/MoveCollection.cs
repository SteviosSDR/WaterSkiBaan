using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace waterskibaan
{
    static class MoveCollection
    {
        public static List<IMoves> GetWillekeurigeMoves(int maxAantalMoves)
        {
            Random random = new Random();

            List<IMoves> moves = new List<IMoves>();
            moves.Add(new Jump());
            moves.Add(new Tripplesalto());
            moves.Add(new Frontflip());
            moves.Add(new Backflip());


            List<IMoves> GetWillekeurigeMoves = new List<IMoves>();

            for(int i = 0; i < maxAantalMoves; i++)
            {
                if(random.Next(2) == 0)
                {
                    GetWillekeurigeMoves.Add(moves[random.Next(moves.Count())]);
                }
            }
            return GetWillekeurigeMoves;
        }
    }
}
