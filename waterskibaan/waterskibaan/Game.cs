using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace waterskibaan
{
    public class Game
    {
        //properties
        public Waterskibaan Waterskibaan { get; set; }
        public WachtrijInstructie WachtrijInstructie { get; set; }
        public InstructieGroep InstructieGroep { get; set; }
        public WachtrijStarten WachtrijStarten { get; set; }

        public int _ticks;

        public Logger logger;

        //delegates
        public delegate void NieuweBezoekerHandler(NieuweBezoekerArgs args);
        public delegate void InstructieAfgelopenHandler(InstructieAfgelopenArgs args);

        //events
        public event InstructieAfgelopenHandler InstructieAfgelopen;
        public event NieuweBezoekerHandler NieuweBezoeker;


        public void initialize()
        {
            Waterskibaan = new Waterskibaan();
            WachtrijInstructie = new WachtrijInstructie(this);
            InstructieGroep = new InstructieGroep(this);
            WachtrijStarten = new WachtrijStarten(this);
            logger = new Logger(Waterskibaan.kabel);
            NieuweBezoeker += logger.OnNieuweBezoeker;

            Waterskibaan.waterskibaan();
        }

        public void secondPassed(object sender, ElapsedEventArgs args)
        {
            _ticks++;
            if (_ticks % 2 == 0)
            {
                Console.WriteLine("Nieuwe bezoeker!");
                NewSporter();
            }

            if (_ticks % 20 == 0)
            {
                NewInstructieAfgelopen(); 
            }


            if (_ticks % 4 == 0)
            {
                LijnenVerplaatst();
            }
        }

        private void LijnenVerplaatst()
        {
            if(WachtrijStarten.queue.Count > 0)
            {
                Waterskibaan.VerplaatsKabel();
                Waterskibaan.SporterStart(WachtrijStarten.SporterVerlaatRij());
                Console.WriteLine(Waterskibaan.ToString());
            }
        }

        public void info()
        {
            Console.WriteLine(WachtrijInstructie.ToString());
            Console.WriteLine(InstructieGroep.ToString());
            Console.WriteLine(WachtrijStarten.ToString());
        }

        private void NewInstructieAfgelopen()
        {
            InstructieAfgelopen?.Invoke(new InstructieAfgelopenArgs(WachtrijInstructie.SportersVerlatenRij(5)));
        }

        private void NewSporter()
        {
            //opdracht 11
            /* 
            Sporter henk = new Sporter(MoveCollection.GetWillekeurigeMoves(5));
            waterskibaan.VerplaatsKabel();
            waterskibaan.SporterStart(henk);
            Console.WriteLine(waterskibaan.ToString());
            */
            Sporter sporter = new Sporter(MoveCollection.GetWillekeurigeMoves(5));
            NieuweBezoeker?.Invoke(new NieuweBezoekerArgs(sporter));

        }

        public class NieuweBezoekerArgs : EventArgs
        {
            public Sporter Sporter { get;}

            public NieuweBezoekerArgs(Sporter sporter)
            {
                Sporter = sporter;
            }
        }

        public class InstructieAfgelopenArgs : EventArgs
        {
            public List<Sporter> sporterLijst { get; }

            public InstructieAfgelopenArgs(List<Sporter> sporterlist)
            {
                sporterLijst = sporterlist;
            }
        }
    }
}
