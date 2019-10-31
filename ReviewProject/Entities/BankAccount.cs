using ReviewProject.Interfaces;
using System;

namespace ReviewProject.Entities
{
    public class BankAccount : IBankAccount
    {
        public const double DefaultInterestRate = 0.01;

        private double _interestRate;

        public BankAccount(int accountNumber) : this(accountNumber, 0.0)
        {
        }

        public BankAccount(int accountNumber, double initialBalance)
        {
            if (accountNumber <= 0)
                throw new ArgumentException("Invalid Account Number");
            if (initialBalance < 0)
                throw new ArgumentException("Invalid Initial Balance");
            AccountNumber = accountNumber;
            Balance = initialBalance;
            InterestRate = DefaultInterestRate;
        }

        public int AccountNumber { get; private set; }

        public double Balance { get; private set; }

        public double InterestRate
        {
            get
            {
                return _interestRate;
            }
            set
            {
                if (value < 0 || value > 0.10)
                    throw new ArgumentException("Invalid Interest Rate");
                _interestRate = value;
            }
        }

        public void Deposit(double amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero");
            Balance += amount;
        }

        public void Withdraw(double amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero");
            if (amount > Balance)
                throw new ArgumentException("Amount to withdraw exceeds the balance");
            Balance -= amount;
        }
    }
}
