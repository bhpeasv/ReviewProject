using System;
using System.Collections.Generic;
using System.Text;

namespace ReviewProject.Interfaces
{
    public interface IBankAccount
    {
        int AccountNumber { get; }
        double Balance { get; }
        double InterestRate { get; } 

        void Deposit(double amount);
        void Withdraw(double amount);
    }
}
