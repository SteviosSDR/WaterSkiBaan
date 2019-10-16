using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using static waterskibaan.Game;

namespace waterskibaan
{
    public abstract class Wachtrij
    {
        public int MAX_LENGTE_RIJ
        {
            get;

            private set;
        }

        protected Wachtrij(int lengte) => MAX_LENGTE_RIJ = lengte;

        protected Queue<Sporter> queue = new Queue<Sporter>();

        public void SporterneemPlaatsInRij(Sporter sporter)
        {
            if (queue.Count >= MAX_LENGTE_RIJ) return;
            queue.Enqueue(sporter);
        }

        public List<Sporter> GetAlleSporters() => new List<Sporter>(queue.ToArray());

        public List<Sporter> SportersVerlatenRij(int aantal)
        {
            List<Sporter> sporters = new List<Sporter>();
            for (int i = 0; i < Math.Min(aantal, queue.Count); i++) sporters.Add(queue.Dequeue());
            return sporters;
        }

        public Sporter SporterVerlaatRij()
        {
            return queue.Dequeue();
        }
    }

    public class WachtrijInstructie : Wachtrij
    {
        public WachtrijInstructie() : base(100) { }

        public WachtrijInstructie(Game game) : base(100)
        {
            game.NieuweBezoeker += PlaatsBezoekerInRij;
        }

        public void PlaatsBezoekerInRij(NieuweBezoekerArgs args)
        {
            Console.WriteLine("bezoeker geplaatst in wachtrij");
            SporterneemPlaatsInRij(args.Sporter);
        }

        public override string ToString() => $"WachtrijInstructie lengte: { queue.Count},max: {MAX_LENGTE_RIJ}]";
    }

    public class InstructieGroep : Wachtrij
    {
        public InstructieGroep() : base(5) { }

        public InstructieGroep(Game game) : base(100)
        {
            game.InstructieAfgelopen += InstructieAfgelopen;
        }

        public void InstructieAfgelopen(InstructieAfgelopenArgs args)
        {
            Console.WriteLine("Instructie afgelopen");
            Console.WriteLine($"{args.sporterLijst.Count} sporters toegevoegd aan de rij");
            foreach (Sporter sporter in args.sporterLijst)
            {
                SporterneemPlaatsInRij(sporter);
            }
        }

        public override string ToString() => $"InstructieGroep lengte: { queue.Count},max: {MAX_LENGTE_RIJ}";
    }

    public class WachtrijStarten : Wachtrij
    {
        public WachtrijStarten() : base(20) { }

        public WachtrijStarten(Game game, Waterskibaan waterskibaan) : base(20)
        {

        }
        public override string ToString() => $"WachtrijStarten lengte: {queue.Count},max: {MAX_LENGTE_RIJ}";
    }
}
