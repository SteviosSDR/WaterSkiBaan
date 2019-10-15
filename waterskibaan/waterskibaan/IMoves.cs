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
    }

    class Backflip : IMoves
    {
        public int Move()
        {
            return 20;
        }
    }

    class Frontflip : IMoves
    {
        public int Move()
        {
            return 30;
        }
    }

    class Tripplesalto : IMoves
    {
        public int Move()
        {
            return 60;
        }
    }

    class Jump : IMoves
    {
        public int Move()
        {
            return 5;
        }
    }



}
