using System;
using System.Collections.Generic;
using System.Text;

namespace StockCommandChallenge.Core.Interfaces
{
    public interface IServiceBroker
    {
        void AddConsumer(string queueName, Action<string> onReceived);
        void DeleteConsumer(string queueName);

        void SendBroadcast(string message);

        void Close();
    }
}
