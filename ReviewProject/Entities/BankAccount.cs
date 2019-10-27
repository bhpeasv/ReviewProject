using ReviewProject.Interfaces;
using System;
using System.Collections.Generic;

namespace ReviewProject.Entities
{
    public class BankAccount : IBankAccount
    {
        public const double DEFAULT_INTEREST_RATE = 0.01;

        public BankAccount(int accountNumber) : this(accountNumber, 0.0)
        {
        }

        public BankAccount(int accountNumber, double initialBalance)
        {
            AccountNumber = accountNumber;
            Balance = initialBalance;
            InterestRate = DEFAULT_INTEREST_RATE;
        }

        public int AccountNumber { get; private set;}

        public double Balance { get; private set;}

        public double InterestRate { get; private set;}

        public void Deposit(double amount)
        {
            throw new NotImplementedException();
        }

        public void Withdraw(double amount)
        {
            throw new NotImplementedException();
        }
    }
}
