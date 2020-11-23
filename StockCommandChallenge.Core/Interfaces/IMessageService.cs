using StockCommandChallenge.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockCommandChallenge.Core.Interfaces
{
    public interface IMessageService
    {
        void SaveMessage(string Username, string message);
        IEnumerable<Message> GetLast50Messages();
    }
}
