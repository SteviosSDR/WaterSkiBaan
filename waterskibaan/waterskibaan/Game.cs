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

        //timers
        private Timer BezoekerTimer;

        private Timer InstructionTimer;

        private Timer MoveLinesTimer;

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

            Waterskibaan.waterskibaan();

            GameLoop();
        }

        public void GameLoop()
        {
            NewVisitor();
            NewInstruction();
            MoveLines();

            Console.ReadLine();
            BezoekerTimer.Stop();
            BezoekerTimer.Dispose();
            InstructionTimer.Stop();
            InstructionTimer.Dispose();
            MoveLinesTimer.Stop();
            MoveLinesTimer.Dispose();
        }

        private void NewVisitor()
        {
            BezoekerTimer = new Timer(3000);
            BezoekerTimer.Elapsed += NewSporter;
            BezoekerTimer.AutoReset = true;
            BezoekerTimer.Enabled = true;
        }

        private void NewInstruction()
        {
            InstructionTimer = new Timer(20000); //20000
            InstructionTimer.Elapsed += NewInstructieAfgelopen;
            InstructionTimer.AutoReset = true;
            InstructionTimer.Enabled = true;
        }

        private void MoveLines()
        {
            MoveLinesTimer = new Timer(4000);
            MoveLinesTimer.Elapsed += LijnenVerplaatst;
            MoveLinesTimer.AutoReset = true;
            MoveLinesTimer.Enabled = true;
        }

        private void LijnenVerplaatst(object source, ElapsedEventArgs e)
        {
            Waterskibaan.VerplaatsKabel();
            Waterskibaan.SporterStart(WachtrijStarten.SporterVerlaatRij());
            Console.WriteLine(Waterskibaan.ToString());
        }

        public void info()
        {
            Console.WriteLine(WachtrijInstructie.ToString());
            Console.WriteLine(InstructieGroep.ToString());
            Console.WriteLine(WachtrijStarten.ToString());
        }

        private void NewInstructieAfgelopen(Object source, ElapsedEventArgs e)
        {
            InstructieAfgelopen?.Invoke(new InstructieAfgelopenArgs(WachtrijInstructie.SportersVerlatenRij(5)));
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
            info();
            NieuweBezoeker?.Invoke(new NieuweBezoekerArgs(new Sporter(MoveCollection.GetWillekeurigeMoves(5))));
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
