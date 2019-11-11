using ReviewProject.Entities;
using ReviewProject.Interfaces;
using System;
using System.Linq;
using NUnit.Framework;

namespace NUnitTestProject
{
    public class BankAccountTest
    {
        [Test]
        public void CreateBankAccountWithZeroBalanceAndDefaultInterestRate()
        {
            int accountNumber = 1;

            DateTime before = DateTime.Now;
            IBankAccount acc = new BankAccount(accountNumber);
            DateTime after = DateTime.Now;

            Assert.NotNull(acc);
            Assert.AreEqual(accountNumber, acc.AccountNumber);
            Assert.AreEqual(0.0, acc.Balance);
            Assert.AreEqual(BankAccount.DefaultInterestRate, acc.InterestRate);
            Assert.NotNull(acc.Transactions);

            Assert.True(acc.Transactions.Count == 1);
            ITransaction t = acc.Transactions[0];
            Assert.AreEqual(1, t.Id);
            Assert.True(before <= t.TransactionTime && t.TransactionTime <= after);
            Assert.AreEqual("Bank Account Created", t.Message);
            Assert.AreEqual(acc.Balance, t.Amount);
        }

        [Test]
        public void CreateBankAccountWithInitialBalanceAndDefaultInterestRate()
        {
            int accountNumber = 1;
            double initialBalance = 123.45;

            DateTime before = DateTime.Now;
            IBankAccount acc = new BankAccount(accountNumber, initialBalance);
            DateTime after = DateTime.Now;
            
            Assert.NotNull(acc);
            Assert.AreEqual(accountNumber, acc.AccountNumber);
            Assert.AreEqual(initialBalance, acc.Balance);
            Assert.AreEqual(BankAccount.DefaultInterestRate, acc.InterestRate);
            Assert.NotNull(acc.Transactions);

            Assert.True(acc.Transactions.Count == 1);
            ITransaction t = acc.Transactions[0];
            Assert.AreEqual(1, t.Id);
            Assert.True(before <= t.TransactionTime && t.TransactionTime <= after);
            Assert.AreEqual("Bank Account Created", t.Message);
            Assert.AreEqual(acc.Balance, t.Amount);
        }

        
        [TestCase(0)]
        [TestCase(-1)]
        public void CreateBankAccountInvalidAccountNumber_ExpectArgumentException(int accNumber)
        {
            IBankAccount acc = null;
            var ex = Assert.Throws<ArgumentException>(() => acc = new BankAccount(accNumber));
            Assert.Null(acc);
            Assert.AreEqual("Invalid Account Number", ex.Message);
        }

        [Test]
        public void CreateBankAccountInvalidInitialBalance_ExpectArgumentException()
        {
            IBankAccount acc = null;
            double initialBalance = -0.01;
            var ex = Assert.Throws<ArgumentException>(() => acc = new BankAccount(1, initialBalance));
            Assert.Null(acc);
            Assert.AreEqual("Invalid Initial Balance", ex.Message);
        }

        [TestCase(0.00)]
        [TestCase(0.10)]
        public void SetInterestRate(double interestRate)
        {
            IBankAccount acc = new BankAccount(1);
            int lastTransId = acc.Transactions.Max(tr => tr.Id);
            
            DateTime before = DateTime.Now;
            acc.InterestRate = interestRate;
            DateTime after = DateTime.Now;

            Assert.AreEqual(interestRate, acc.InterestRate);
            ITransaction t = acc.Transactions[acc.Transactions.Count - 1];
            Assert.AreEqual(lastTransId + 1, t.Id);
            Assert.True(before <= t.TransactionTime && t.TransactionTime <= after);
            Assert.AreEqual("Interest Rate changed", t.Message);
            Assert.AreEqual(acc.InterestRate, t.Amount);
        }

        
        [TestCase(-0.001)]
        [TestCase(0.101)]
        public void SetInterestRateInvalid_ExpectArgumentException(double interestRate)
        {
            IBankAccount acc = new BankAccount(1);
            double oldInterestRate = acc.InterestRate;

            var ex = Assert.Throws<ArgumentException>(() => acc.InterestRate = interestRate);

            Assert.AreEqual("Invalid Interest Rate", ex.Message);
            Assert.AreEqual(oldInterestRate, acc.InterestRate);
        }

        
        [TestCase(0.01)]
        [TestCase(23.45)]
        public void DepositValidAmount(double amount)
        {
            double initialBalance = 123.45;
            IBankAccount acc = new BankAccount(1, initialBalance);

            DateTime before = DateTime.Now;
            acc.Deposit(amount);
            DateTime after = DateTime.Now;

            Assert.AreEqual(initialBalance + amount, acc.Balance);
            ITransaction t = acc.Transactions[acc.Transactions.Count - 1];
            Assert.AreEqual(acc.Transactions.Count, t.Id);
            Assert.True(before <= t.TransactionTime && t.TransactionTime <= after);
            Assert.AreEqual("Deposit", t.Message);
            Assert.AreEqual(amount, t.Amount);
        }

        
        [TestCase(0.00)]
        [TestCase(-0.01)]
        public void DepositInvalidAmount_ExpectArgumentException(double amount)
        {
            double initialBalance = 123.45;
            IBankAccount acc = new BankAccount(1, initialBalance);

            var ex = Assert.Throws<ArgumentException>(() => acc.Deposit(amount));

            Assert.AreEqual("Amount must be greater than zero", ex.Message);
            Assert.AreEqual(initialBalance, acc.Balance);
        }

        
        [TestCase(0.01)]
        [TestCase(23.45)]
        public void WithdrawValidAmount(double amount)
        {
            double initialBalance = 123.45;
            IBankAccount acc = new BankAccount(1, initialBalance);

            DateTime before = DateTime.Now;
            acc.Withdraw(amount);
            DateTime after = DateTime.Now;

            Assert.AreEqual(initialBalance - amount, acc.Balance);
            ITransaction t = acc.Transactions[acc.Transactions.Count - 1];
            Assert.AreEqual(acc.Transactions.Count, t.Id);
            Assert.True(before <= t.TransactionTime && t.TransactionTime <= after);
            Assert.AreEqual("Withdraw", t.Message);
            Assert.AreEqual(-amount, t.Amount);
        }

        
        [TestCase(0.00)]
        [TestCase(-0.01)]
        public void WithdrawInvalidAmount_ExpectArgumentException(double amount)
        {
            double initialBalance = 123.45;
            IBankAccount acc = new BankAccount(1, initialBalance);

            var ex = Assert.Throws<ArgumentException>(() => acc.Withdraw(amount));

            Assert.AreEqual("Amount must be greater than zero", ex.Message);
            Assert.AreEqual(initialBalance, acc.Balance);
        }

        [Test]
        public void WithdrawAmountExceedingBalance_ExpectArgumentException()
        {
            double initialBalance = 123.45;
            IBankAccount acc = new BankAccount(1, initialBalance);

            var ex = Assert.Throws<ArgumentException>(() => acc.Withdraw(initialBalance + 0.01));

            Assert.AreEqual("Amount to withdraw exceeds the balance", ex.Message);
            Assert.AreEqual(initialBalance, acc.Balance);
        }
    }
}
