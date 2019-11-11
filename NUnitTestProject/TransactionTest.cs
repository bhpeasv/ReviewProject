using System;
using ReviewProject.Entities;
using ReviewProject.Interfaces;
using NUnit.Framework;

namespace NUnitTestProject
{
    public class TransactionTest
    {
        [Test]
        public void CreateTransaction()
        {
            int transId = 1;
            String message = "Some Message";
            double amount = 123.45;

            DateTime before = DateTime.Now;
            ITransaction t = new Transaction(transId, message, amount);
            DateTime after = DateTime.Now;

            Assert.AreEqual(transId, t.Id);
            Assert.True(before <= t.TransactionTime && t.TransactionTime <= after);
            Assert.AreEqual(message, t.Message);
            Assert.AreEqual(amount, t.Amount);
        }

        
        [TestCase(0)]
        [TestCase(-1)]
        public void CreateTransaction_InvalidId_ExpectArgumentException(int transId)
        {
            ITransaction t = null;

            var ex = Assert.Throws<ArgumentException>(() => t = new Transaction(transId, "Message", 123.45));

            Assert.Null(t);
            Assert.AreEqual("Invalid Transaction Id", ex.Message);
            Assert.Null(t);
            Assert.AreEqual("Invalid Transaction Id", ex.Message);
        }

        
        [TestCase(null)]
        [TestCase("")]
        public void CreateTransaction_MessageEmptyOrNull_ExpectArgumentException(string message)
        {
            ITransaction t = null;

            var ex = Assert.Throws<ArgumentException>(() => t = new Transaction(1, message, 123.45));

            Assert.Null(t);
            Assert.AreEqual("Transaction Message is missing or empty", ex.Message);
        }

    }
}
