using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace waterskibaan
{
    public class Lijn
    {
        public Sporter huidigeSporter
        {
            get;

            set;
        }

        private int lijnPositie;
        public int PositieOpDeKabel
        {
            get
            {
                return lijnPositie;
            }


            set
            {
                lijnPositie = value;
            }
        }
    }
}
