using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace waterskibaan
{
    class Program
    {
        static void Main(string[] args)
        {
            TestOpdracht2();
            TestOpdracht3();
            TestOpdracht8();
            TestOpdracht10();
            TestOpdracht11();
            TestOpdracht12();
        }

        private static void TestOpdracht2()
        {
            Console.WriteLine("\nOpdracht 2:");
            Kabel kabel = new Kabel();

            Lijn lijn1 = new Lijn();
            kabel.NeemLijnInGeruik(lijn1);
            Console.WriteLine(kabel.ToString());

            kabel.VerschuifLijnen();
            Console.WriteLine(kabel.ToString());

            Lijn lijn2 = new Lijn();
            kabel.NeemLijnInGeruik(lijn2);
            Lijn lijn3 = new Lijn(); //doet hij niet want 0 is al vol
            kabel.NeemLijnInGeruik(lijn3);
            Console.WriteLine(kabel.ToString());

            kabel.VerwijderLijnVanKabel(); //doet hij niet want 9 is leeg
            Console.WriteLine(kabel.ToString());
        }

        private static void TestOpdracht3()
        {
            Console.WriteLine("\nOpdracht 3:");
            Lijn lijn = new Lijn();
            LijnenVoorraad lijnVoorraad = new LijnenVoorraad();
            lijnVoorraad.LijnToevoegenAanRij(lijn);
            Console.WriteLine(lijnVoorraad.ToString());
            lijnVoorraad.verwijderEersteLijn();
            Console.WriteLine(lijnVoorraad.ToString());
        }

        private static void TestOpdracht8()
        {
            Console.WriteLine("\nOpdracht 8:");
            Waterskibaan waterskibaan = new Waterskibaan();
            waterskibaan.waterskibaan();
            Sporter henk = new Sporter(MoveCollection.GetWillekeurigeMoves(5));
            waterskibaan.SporterStart(henk);
        }

        private static void TestOpdracht10()
        {
            Console.WriteLine("\nOpdracht 10:");
            WachtrijInstructie instructierij = new WachtrijInstructie();
            WachtrijStarten startrij = new WachtrijStarten();
            InstructieGroep iGroep = new InstructieGroep();

            Console.WriteLine(instructierij.ToString());
            Console.WriteLine(startrij.ToString());
            Console.WriteLine(iGroep.ToString());
        }

        private static void TestOpdracht11()
        {
            Console.WriteLine("\nOpdracht 11:");
            //Game game = new Game();

            //game.initialize();
        }

        private static void TestOpdracht12()
        {
            Console.WriteLine("\nOpdracht 12:");
            Game game = new Game();

            game.initialize();
        }
    }
}
