using System;
using ReviewProject.Entities;
using ReviewProject.Interfaces;
using Xunit;

namespace XUnitTestProject
{
    public class TransactionTest
    {
        [Fact]
        public void CreateTransaction()
        {
            int transId = 1;
            String message = "Some Message";
            double amount = 123.45;

            DateTime before = DateTime.Now;
            ITransaction t = new Transaction(transId, message, amount);
            DateTime after = DateTime.Now;

            Assert.Equal(transId, t.Id);
            Assert.True(before <= t.TransactionTime && t.TransactionTime <= after);
            Assert.Equal(message, t.Message);
            Assert.Equal(amount, t.Amount);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void CreateTransaction_InvalidId_ExpectArgumentException(int transId)
        {
            ITransaction t = null;

            var ex = Assert.Throws<ArgumentException>(() => t = new Transaction(transId, "Message", 123.45));

            Assert.Null(t);
            Assert.Equal("Invalid Transaction Id", ex.Message);
            Assert.Null(t);
            Assert.Equal("Invalid Transaction Id", ex.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void CreateTransaction_MessageEmptyOrNull_ExpectArgumentException(string message)
        {
            ITransaction t = null;

            var ex = Assert.Throws<ArgumentException>(() => new Transaction(1, message, 123.45));

            Assert.Null(t);
            Assert.Equal("Transaction Message is missing or empty", ex.Message);
        }

    }
}
