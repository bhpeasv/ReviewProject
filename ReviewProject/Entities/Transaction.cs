using System;
using System.Collections.Generic;
using System.Text;
using ReviewProject.Interfaces;

namespace ReviewProject.Entities
{
    public class Transaction : ITransaction
    {
        public int Id  { get; }

        public DateTime TransactionTime  { get; }

        public string Message  { get; }
        public double Amount  { get; }

        public Transaction(int id, string message, double amount)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid Transaction Id");
            if (message == null || message == "")
                throw new ArgumentException("Transaction Message is missing or empty");
            Id = id;
            TransactionTime = DateTime.Now;
            Message = message;
            Amount = amount;
        }
    }
}
