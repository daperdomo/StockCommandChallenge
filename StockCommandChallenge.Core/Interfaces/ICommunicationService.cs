using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StockCommandChallenge.Core.Interfaces
{
    public interface ICommunicationService
    {
        Task HandleMessage(string connectionId, string userName, string message);

        Task Initialize(string userName, string connectionId);
    }
}
