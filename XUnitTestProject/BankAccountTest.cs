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
    }
}
