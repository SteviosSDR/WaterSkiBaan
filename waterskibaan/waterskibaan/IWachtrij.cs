using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

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
    }

    public class WachtrijInstructie : Wachtrij
    {
        public WachtrijInstructie() : base(100) { }

        public override string ToString() => $"WachtrijInstructie lengte: { queue.Count},max: {MAX_LENGTE_RIJ}]";
    }

    public class InstructieGroep : Wachtrij
    {
        public InstructieGroep() : base(5) { }

        public override string ToString() => $"InstructieGroep lengte: { queue.Count},max: {MAX_LENGTE_RIJ}";
    }

    public class WachtrijStarten : Wachtrij
    {
        public WachtrijStarten() : base(20) { }

        public override string ToString() => $"WachtrijStarten lengte: {queue.Count},max: {MAX_LENGTE_RIJ}";
    }
}
