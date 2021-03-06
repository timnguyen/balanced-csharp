﻿using Balanced;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;


namespace BalancedTest
{
    [TestClass]
    public class MarketplaceTest : Test
    {
        [TestMethod]
        public void TestCreate()
        {
            Settings.configure(null);

            var key = new APIKey();
            key.Save();
            Settings.configure(key.Secret);

            var mp = new Marketplace();
            Assert.IsNull(mp.id);
            mp.Save();

            Assert.IsNotNull(mp.id);
        }

        [TestMethod]
        [ExpectedException(typeof(Error))]
        public void TestDoubleCreate()
        {
            var mp = new Marketplace();
            mp.Save();
        }

        [TestMethod]
        public void TestMine()
        {
            Settings.configure(null);

            var key = new APIKey();
            key.Save();
            Settings.configure(key.Secret);

            var mp = new Marketplace();
            Assert.IsNull(mp.id);
            mp.Save();

            Assert.AreEqual(mp.id, Marketplace.Mine.id);
        }

        [TestMethod]
        [ExpectedException(typeof(NoResultsFound))]
        public void TestNoMine()
        {
            Settings.configure(null);

            var key = new APIKey();
            key.Save();
            Settings.configure(key.Secret);

            var mp = Marketplace.Mine;
        }

        [TestMethod]
        public void TestTokenizeBankAccount()
        {
            var ba = Mine.TokenizeBankAccount(
                "Homer Jay",
                "112233a",
                "121042882"
                );

            Assert.AreEqual(ba.name, "Homer Jay");
            Assert.AreEqual(ba.account_number, "xxx233a");
            Assert.AreEqual(ba.routing_number, "121042882");
        }

        [TestMethod]
        public void TestTokenizeCard() 
        {
            var card = Mine.TokenizeCard(
                    "123 Fake Street",
                    "Jollywood",
                    null,
                    "90210",
                    "Homer Jay",
                    "4112344112344113",
                    "123",
                    12,
                    2013);
            Assert.AreEqual(card.name, "Homer Jay");
            Assert.AreEqual(card.last_four, "4113");
            Assert.AreEqual(card.expiration_year, 2013);
            Assert.AreEqual(card.expiration_month, 12);
        }
    }
}
