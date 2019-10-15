using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using waterskibaan; 

namespace WaterskibaanTest
{
    [TestClass]
    public class UnitTest1
    {
        Kabel kabel;
        Lijn lijn;

        [TestInitialize]
        public void init()
        {
            kabel = new Kabel();
            lijn = new Lijn();
        }

        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsTrue(kabel.isStartPosietieLeeg());
        }

        [TestMethod]
        public void test2()
        {
            kabel.NeemLijnInGeruik(lijn);
            Assert.IsFalse(kabel.isStartPosietieLeeg());
        }

        [TestMethod]
        public void test3()
        {
            kabel.NeemLijnInGeruik(lijn);
        }

        [TestMethod]
        private static void TestOpdracht10()
        {
            WachtrijInstructie instructierij = new WachtrijInstructie();
            WachtrijStarten startrij = new WachtrijStarten();
            InstructieGroep iGroep = new InstructieGroep();
        }

    }
}
