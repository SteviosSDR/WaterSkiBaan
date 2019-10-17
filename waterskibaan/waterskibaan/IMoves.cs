using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace waterskibaan
{
    public interface IMoves
    {
        int Move();
        string Naam { get; }
    }

    class Backflip : IMoves
    {
        public int Move()
        {
            return 20;
        }

        string IMoves.Naam => "Backflip";
    }

    class Frontflip : IMoves
    {
        public int Move()
        {
            return 30;
        }

        string IMoves.Naam => "Frontflip";
    }

    class Tripplesalto : IMoves
    {
        public int Move()
        {
            return 60;
        }

        string IMoves.Naam => "Tripplesalto";
    }

    class Jump : IMoves
    {
        public int Move()
        {
            return 5;
        }

        string IMoves.Naam => "Jump";
    }



}
