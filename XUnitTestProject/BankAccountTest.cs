using ReviewProject;
using ReviewProject.Entities;
using ReviewProject.Interfaces;
using System;
using Xunit;

namespace XUnitTestProject
{
    public class BankAccountTest
    {
        [Fact]
        public void CreateBankAccountWithZeroBalanceAndDefaultInterestRate()
        {
            int accountNumber = 1;
            IBankAccount acc = new BankAccount(accountNumber);

            Assert.NotNull(acc);
            Assert.Equal(accountNumber, acc.AccountNumber);
            Assert.Equal(0.0, acc.Balance);
            Assert.Equal(BankAccount.DEFAULT_INTEREST_RATE, acc.InterestRate);
        }

        [Fact]
        public void CreateBankAccountWithInitialBalanceAndDefaultInterestRate()
        {
            int accountNumber = 1;
            double initialBalance = 123.45;

            IBankAccount acc = new BankAccount(accountNumber, initialBalance);

            Assert.NotNull(acc);
            Assert.Equal(accountNumber, acc.AccountNumber);
            Assert.Equal(initialBalance, acc.Balance);
            Assert.Equal(BankAccount.DEFAULT_INTEREST_RATE, acc.InterestRate);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void CreateBankAccountInvalidAccountNumber_ExpectArgumentException(int accNumber)
        {
            IBankAccount acc = null;
            var ex = Assert.Throws<ArgumentException>(() => acc = new BankAccount(accNumber));
            Assert.Null(acc);
            Assert.Equal("Invalid Account Number", ex.Message);
        }

        [Fact]
        public void CreateBankAccountInvalidInitialBalance_ExpectArgumentException()
        {
            IBankAccount acc = null;
            double initialBalance = -0.01;
            var ex = Assert.Throws<ArgumentException>(() => acc = new BankAccount(1, initialBalance));
            Assert.Null(acc);
            Assert.Equal("Invalid Initial Balance", ex.Message);
        }

        [Theory]
        [InlineData(0.00)]
        [InlineData(0.10)]
        public void SetInterestRate(double interestRate)
        {
            IBankAccount acc = new BankAccount(1);
            acc.InterestRate = interestRate;

            Assert.Equal(interestRate, acc.InterestRate);
        }

        [Theory]
        [InlineData(-0.001)]
        [InlineData(0.101)]
        public void SetInterestRateInvalid_ExpectArgumentException(double interestRate)
        {
            IBankAccount acc = new BankAccount(1);
            double oldInterestRate = acc.InterestRate;

            var ex = Assert.Throws<ArgumentException>(() => acc.InterestRate = interestRate);

            Assert.Equal("Invalid Interest Rate", ex.Message);
            Assert.Equal(oldInterestRate, acc.InterestRate);
        }

        [Theory]
        [InlineData(0.01)]
        [InlineData(23.45)]
        public void DepositValidAmount(double amount)
        {
            double initialBalance = 123.45;
            IBankAccount acc = new BankAccount(1, initialBalance);

            acc.Deposit(amount);

            Assert.Equal(initialBalance + amount, acc.Balance);
        }

        [Theory]
        [InlineData(0.00)]
        [InlineData(-0.01)]
        public void DepositInvalidAmount_ExpectArgumentException(double amount)
        {
            double initialBalance = 123.45;
            IBankAccount acc = new BankAccount(1, initialBalance);

            var ex = Assert.Throws<ArgumentException>(() => acc.Deposit(amount));

            Assert.Equal("Amount must be greater than zero", ex.Message);
            Assert.Equal(initialBalance, acc.Balance);
        }

        [Theory]
        [InlineData(0.01)]
        [InlineData(23.45)]
        public void WithdrawValidAmount(double amount)
        {
            double initialBalance = 123.45;
            IBankAccount acc = new BankAccount(1, initialBalance);

            acc.Withdraw(amount);

            Assert.Equal(initialBalance - amount, acc.Balance);
        }

        [Theory]
        [InlineData(0.00)]
        [InlineData(-0.01)]
        public void WithdrawInvalidAmount_ExpectArgumentException(double amount)
        {
            double initialBalance = 123.45;
            IBankAccount acc = new BankAccount(1, initialBalance);

            var ex = Assert.Throws<ArgumentException>(() => acc.Withdraw(amount));

            Assert.Equal("Amount must be greater than zero", ex.Message);
            Assert.Equal(initialBalance, acc.Balance);
        }

        //[Fact]
        //public void WithdrawAmountExceedingBalance_ExpectArgumentException()
        //{
        //    double initialBalance = 123.45;
        //    IBankAccount acc = new BankAccount(1, initialBalance);

        //    var ex = Assert.Throws<ArgumentException>(() => acc.Withdraw(initialBalance + 0.01));

        //    Assert.Equal("Amount to withdraw exceeds the balance", ex.Message);
        //    Assert.Equal(initialBalance, acc.Balance);
        //}

    }
}
