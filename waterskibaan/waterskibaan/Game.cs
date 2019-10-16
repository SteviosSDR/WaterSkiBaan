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
        Waterskibaan waterskibaan = new Waterskibaan();
        WachtrijInstructie instructierij = new WachtrijInstructie();
        InstructieGroep instructieGroep = new InstructieGroep();
        WachtrijStarten startRij = new WachtrijStarten();

        public delegate void NieuweBezoekerHandler(NieuweBezoekerArgs args);
        public delegate void InstructieAfgelopenHandler(InstructieAfgelopenArgs args);

        public event InstructieAfgelopenHandler InstructieAfgelopen;
        public event NieuweBezoekerHandler NieuweBezoeker;

        //timers
        private static System.Timers.Timer gametimer;

        private int sec;

        public void initialize()
        {
            NieuweBezoeker += NewBezoeker;
            InstructieAfgelopen += VInstructieAfgelopen;
            waterskibaan.waterskibaan();

            GameLoop();
        }

        public void GameLoop()
        {
            gametimer = new Timer(1000);
            sec++;
            if(sec%2 == 0)
            {
                gametimer.Elapsed += NewSporter;
            }

            else if (sec%20 == 0)
            {
                gametimer.Elapsed += NewInstructieAfgelopen;
            }

            gametimer.AutoReset = true;
            gametimer.Enabled = true;

            Console.ReadLine();
            gametimer.Stop();
            gametimer.Dispose();
        }

        private void NewInstructieAfgelopen(Object source, ElapsedEventArgs e)
        {
            InstructieAfgelopen?.Invoke(new InstructieAfgelopenArgs(instructierij.SportersVerlatenRij(5)));
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

        private void NewBezoeker(NieuweBezoekerArgs args)
        {
            //Console.WriteLine("Sporter toegevoegd aan wachtrij");
            instructierij.SporterneemPlaatsInRij(args.Sporter);
        }

        private void VInstructieAfgelopen(InstructieAfgelopenArgs args)
        {
            foreach(Sporter sporter in args.sporterLijst)
            {
                instructieGroep.SporterneemPlaatsInRij(sporter);
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
