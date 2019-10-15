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
        public void initialize()
        {
            WachtrijInstructie instructierij = new WachtrijInstructie();
            InstructieGroep instructieGroep = new InstructieGroep();
            WachtrijStarten startRij = new WachtrijStarten();
            waterskibaan.waterskibaan();

            Timer gametimer = new Timer();
            gametimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            gametimer.Interval = 1000;
            gametimer.AutoReset = true;
            gametimer.Enabled = true;
            Console.ReadLine();
            gametimer.Stop();
            gametimer.Dispose();
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Sporter henk = new Sporter(MoveCollection.GetWillekeurigeMoves(5));
            waterskibaan.VerplaatsKabel();
            waterskibaan.SporterStart(henk);
            Console.WriteLine(waterskibaan.ToString());
        }
    }
}
