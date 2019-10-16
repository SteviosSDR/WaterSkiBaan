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
        public delegate void NieuweBezoekerHandler(NieuweBezoekerArgs args);
        public delegate void InstructieAfgelopenHandler(InstructieAfgelopenArgs args);

        public event InstructieAfgelopenHandler InstructieAfgelopen;
        public event NieuweBezoekerHandler NieuweBezoeker;

        public Waterskibaan Waterskibaan { get; set; }
        public WachtrijInstructie WachtrijInstructie { get; set; }
        public InstructieGroep InstructieGroep { get; set; }
        public WachtrijStarten WachtrijStarten { get; set; }
        //timers
        private static System.Timers.Timer gametimer;

        private int sec;

        public void initialize()
        {
            Waterskibaan = new Waterskibaan();
            WachtrijInstructie = new WachtrijInstructie(this);
            InstructieGroep = new InstructieGroep();
            WachtrijStarten = new WachtrijStarten();

            InstructieAfgelopen += VInstructieAfgelopen;
            InstructieAfgelopen += InstructieNaarWachtrij;
            Waterskibaan.waterskibaan();

            GameLoop();
        }

        public void GameLoop()
        {
            gametimer = new Timer(1000);
            gametimer.Elapsed += TimerModulo;
            if (sec % 2 == 0)
            {
                Console.WriteLine("nieuwe bezoeker gevonden");
                gametimer.Elapsed += NewSporter;
            }

            else if (sec % 20 == 0)
            {
                Console.WriteLine("instructie afgelopen");
                gametimer.Elapsed += NewInstructieAfgelopen;
            }

            else if(sec % 4 == 0)
            {
                Console.WriteLine("Lijnen verplaatst");
                gametimer.Elapsed += LijnenVerplaatstPV;
            }
            gametimer.AutoReset = true;
            gametimer.Enabled = true;

            Console.ReadLine();
            gametimer.Stop();
            gametimer.Dispose();
        }

        private void TimerModulo(object source, ElapsedEventArgs e)
        {
            sec++;
        }

        private void LijnenVerplaatstPV(object source, ElapsedEventArgs e)
        {
            Waterskibaan.VerplaatsKabel();
            Sporter sporter = WachtrijStarten.SporterVerlaatRij();
            sporter.Skies = new Skies();
            sporter.Zwemvest = new Zwemvest();
            Waterskibaan.SporterStart(sporter);
            Console.WriteLine(Waterskibaan.ToString());
        }

        private void NewInstructieAfgelopen(Object source, ElapsedEventArgs e)
        {
            InstructieAfgelopen?.Invoke(new InstructieAfgelopenArgs(InstructieGroep.SportersVerlatenRij(5)));
        }

        private void NewSporter(Object source, ElapsedEventArgs e)
        {
            //opdracht 11
            /* 
            Sporter henk = new Sporter(MoveCollection.GetWillekeurigeMoves(5));
            waterskibaan.VerplaatsKabel();
            waterskibaan.SporterStart(henk);
            Console.WriteLine(waterskibaan.ToString());
            */

            NieuweBezoeker?.Invoke(new NieuweBezoekerArgs(new Sporter(MoveCollection.GetWillekeurigeMoves(5))));

        }

        private void VInstructieAfgelopen(InstructieAfgelopenArgs args)
        {
            foreach(Sporter sporter in args.sporterLijst)
            {
                Console.WriteLine("Sporter toegevoegd aan Instructiegroep");
                InstructieGroep.SporterneemPlaatsInRij(sporter);
            }
        }
        private void InstructieNaarWachtrij(InstructieAfgelopenArgs args)
        {
            foreach (Sporter sporter in args.sporterLijst)
            {
                Console.WriteLine("Sporter toegevoegd aan startrij");
                WachtrijStarten.SporterneemPlaatsInRij(sporter);
            }
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
