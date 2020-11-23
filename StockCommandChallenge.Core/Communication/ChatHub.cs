using Microsoft.AspNetCore.SignalR;
using StockCommandChallenge.Core.Interfaces;
using StockCommandChallenge.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StockCommandChallenge.Core.Communication
{
    public class ChatHub : Hub<IClient>
    {
        private readonly ICommunicationService _communicationService;
        public ChatHub(ICommunicationService communicationService)
        {
            _communicationService = communicationService;
        }
        public async Task Initialize(string userName)
        {
            await _communicationService.Initialize(userName, Context.ConnectionId);
        }
        public void SendMessage(string userName, string message)
        {
            _communicationService.HandleMessage(Context.ConnectionId, userName, message);
        }
    }

    public interface IClient
    {
        Task Initialize(IEnumerable<Message> messages);
        Task SendMessage(string userName, string message, DateTime date);
    }
}
