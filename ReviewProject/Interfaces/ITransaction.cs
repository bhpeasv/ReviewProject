using System;
using System.Collections.Generic;
using System.Text;

namespace ReviewProject.Interfaces
{
    public interface ITransaction
    {
        int Id { get; }
        DateTime TransactionTime  { get; }
        string Message  { get; }
        double Amount  { get; }
    }
}
