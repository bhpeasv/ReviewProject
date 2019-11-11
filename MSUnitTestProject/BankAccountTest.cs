using ReviewProject.Entities;
using ReviewProject.Interfaces;
using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSUnitTestProject
{
    [TestClass]
    public class BankAccountTest
    {
        [TestMethod]
        public void CreateBankAccountWithZeroBalanceAndDefaultInterestRate()
        {
            int accountNumber = 1;

            DateTime before = DateTime.Now;
            IBankAccount acc = new BankAccount(accountNumber);
            DateTime after = DateTime.Now;

            Assert.IsNotNull(acc);
            Assert.AreEqual(accountNumber, acc.AccountNumber);
            Assert.AreEqual(0.0, acc.Balance);
            Assert.AreEqual(BankAccount.DefaultInterestRate, acc.InterestRate);
            Assert.IsNotNull(acc.Transactions);

            Assert.IsTrue(acc.Transactions.Count == 1);
            ITransaction t = acc.Transactions[0];
            Assert.AreEqual(1, t.Id);
            Assert.IsTrue(before <= t.TransactionTime && t.TransactionTime <= after);
            Assert.AreEqual("Bank Account Created", t.Message);
            Assert.AreEqual(acc.Balance, t.Amount);
        }

        [TestMethod]
        public void CreateBankAccountWithInitialBalanceAndDefaultInterestRate()
        {
            int accountNumber = 1;
            double initialBalance = 123.45;

            DateTime before = DateTime.Now;
            IBankAccount acc = new BankAccount(accountNumber, initialBalance);
            DateTime after = DateTime.Now;
            
            Assert.IsNotNull(acc);
            Assert.AreEqual(accountNumber, acc.AccountNumber);
            Assert.AreEqual(initialBalance, acc.Balance);
            Assert.AreEqual(BankAccount.DefaultInterestRate, acc.InterestRate);
            Assert.IsNotNull(acc.Transactions);

            Assert.IsTrue(acc.Transactions.Count == 1);
            ITransaction t = acc.Transactions[0];
            Assert.AreEqual(1, t.Id);
            Assert.IsTrue(before <= t.TransactionTime && t.TransactionTime <= after);
            Assert.AreEqual("Bank Account Created", t.Message);
            Assert.AreEqual(acc.Balance, t.Amount);
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        public void CreateBankAccountInvalidAccountNumber_ExpectArgumentException(int accNumber)
        {
            IBankAccount acc = null;
            var ex = Assert.ThrowsException<ArgumentException>(() => acc = new BankAccount(accNumber));
            Assert.IsNull(acc);
            Assert.AreEqual("Invalid Account Number", ex.Message);
        }

        [TestMethod]
        public void CreateBankAccountInvalidInitialBalance_ExpectArgumentException()
        {
            IBankAccount acc = null;
            double initialBalance = -0.01;
            var ex = Assert.ThrowsException<ArgumentException>(() => acc = new BankAccount(1, initialBalance));
            Assert.IsNull(acc);
            Assert.AreEqual("Invalid Initial Balance", ex.Message);
        }

        [DataTestMethod]
        [DataRow(0.00)]
        [DataRow(0.10)]
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
            Assert.IsTrue(before <= t.TransactionTime && t.TransactionTime <= after);
            Assert.AreEqual("Interest Rate changed", t.Message);
            Assert.AreEqual(acc.InterestRate, t.Amount);
        }

        [DataTestMethod]
        [DataRow(-0.001)]
        [DataRow(0.101)]
        public void SetInterestRateInvalid_ExpectArgumentException(double interestRate)
        {
            IBankAccount acc = new BankAccount(1);
            double oldInterestRate = acc.InterestRate;

            var ex = Assert.ThrowsException<ArgumentException>(() => acc.InterestRate = interestRate);

            Assert.AreEqual("Invalid Interest Rate", ex.Message);
            Assert.AreEqual(oldInterestRate, acc.InterestRate);
        }

        [DataTestMethod]
        [DataRow(0.01)]
        [DataRow(23.45)]
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
            Assert.IsTrue(before <= t.TransactionTime && t.TransactionTime <= after);
            Assert.AreEqual("Deposit", t.Message);
            Assert.AreEqual(amount, t.Amount);
        }

        [DataTestMethod]
        [DataRow(0.00)]
        [DataRow(-0.01)]
        public void DepositInvalidAmount_ExpectArgumentException(double amount)
        {
            double initialBalance = 123.45;
            IBankAccount acc = new BankAccount(1, initialBalance);

            var ex = Assert.ThrowsException<ArgumentException>(() => acc.Deposit(amount));

            Assert.AreEqual("Amount must be greater than zero", ex.Message);
            Assert.AreEqual(initialBalance, acc.Balance);
        }

        [DataTestMethod]
        [DataRow(0.01)]
        [DataRow(23.45)]
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
            Assert.IsTrue(before <= t.TransactionTime && t.TransactionTime <= after);
            Assert.AreEqual("Withdraw", t.Message);
            Assert.AreEqual(-amount, t.Amount);
        }

        [DataTestMethod]
        [DataRow(0.00)]
        [DataRow(-0.01)]
        public void WithdrawInvalidAmount_ExpectArgumentException(double amount)
        {
            double initialBalance = 123.45;
            IBankAccount acc = new BankAccount(1, initialBalance);

            var ex = Assert.ThrowsException<ArgumentException>(() => acc.Withdraw(amount));

            Assert.AreEqual("Amount must be greater than zero", ex.Message);
            Assert.AreEqual(initialBalance, acc.Balance);
        }

        [TestMethod]
        public void WithdrawAmountExceedingBalance_ExpectArgumentException()
        {
            double initialBalance = 123.45;
            IBankAccount acc = new BankAccount(1, initialBalance);

            var ex = Assert.ThrowsException<ArgumentException>(() => acc.Withdraw(initialBalance + 0.01));

            Assert.AreEqual("Amount to withdraw exceeds the balance", ex.Message);
            Assert.AreEqual(initialBalance, acc.Balance);
        }
    }
}
