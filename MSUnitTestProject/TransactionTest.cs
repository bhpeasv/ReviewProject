using System;
using ReviewProject.Entities;
using ReviewProject.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSUnitTestProject
{
    public class TransactionTest
    {
        [TestMethod]
        public void CreateTransaction()
        {
            int transId = 1;
            String message = "Some Message";
            double amount = 123.45;

            DateTime before = DateTime.Now;
            ITransaction t = new Transaction(transId, message, amount);
            DateTime after = DateTime.Now;

            Assert.Equals(transId, t.Id);
            Assert.IsTrue(before <= t.TransactionTime && t.TransactionTime <= after);
            Assert.Equals(message, t.Message);
            Assert.Equals(amount, t.Amount);
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        public void CreateTransaction_InvalidId_ExpectArgumentException(int transId)
        {
            ITransaction t = null;

            var ex = Assert.ThrowsException<ArgumentException>(() => t = new Transaction(transId, "Message", 123.45));

            Assert.IsNull(t);
            Assert.Equals("Invalid Transaction Id", ex.Message);
            Assert.IsNull(t);
            Assert.Equals("Invalid Transaction Id", ex.Message);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        public void CreateTransaction_MessageEmptyOrNull_ExpectArgumentException(string message)
        {
            ITransaction t = null;

            var ex = Assert.ThrowsException<ArgumentException>(() => t = new Transaction(1, message, 123.45));

            Assert.IsNull(t);
            Assert.Equals("Transaction Message is missing or empty", ex.Message);
        }

    }
}
