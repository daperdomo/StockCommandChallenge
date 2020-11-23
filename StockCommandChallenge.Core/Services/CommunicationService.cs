using Microsoft.AspNetCore.SignalR;
using StockCommandChallenge.Core.Communication;
using StockCommandChallenge.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StockCommandChallenge.Core.Services
{
    public class CommunicationService : ICommunicationService
    {
        private readonly IMessageService _messageService;
        private readonly ICommandHelper _commandHelper;
        private readonly IHubContext<ChatHub, IClient> _chatHub;
        public CommunicationService(IMessageService messageService,
            IHubContext<ChatHub, IClient> hubContext,
            ICommandHelper commandHelper)
        {
            _messageService = messageService;
            _chatHub = hubContext;
            _commandHelper = commandHelper;
        }

        public async Task HandleMessage(string connectionId, string userName, string message)
        {
            try
            {
                string stockCode = _commandHelper.GetStock(message);
                if (!string.IsNullOrEmpty(stockCode))
                {
                    await _chatHub.Clients.All.SendMessage(userName, message, DateTime.Now);
                    //var result = await _stockApiService.GetStock(stockCode);
                    //await _chatHub.Clients.All.SendMessage("System", result, DateTime.Now);
                }
                else if (_commandHelper.IsHelpCommand(message))
                {
                    await _chatHub.Clients.Client(connectionId).SendMessage("System", "The correct command format is /stock={stock_code}", DateTime.Now);
                }
                else if (message.Trim().StartsWith("/"))
                {
                    await _chatHub.Clients.All.SendMessage("System", "Command error, use /HELP for more information.", DateTime.Now);
                }
                else
                {
                    _messageService.SaveMessage(userName, message);
                    await _chatHub.Clients.All.SendMessage(userName, message, DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                await _chatHub.Clients.Client(connectionId).SendMessage("System", ex.Message, DateTime.Now);
            }
        }

        public async Task Initialize(string userName, string connectionId)
        {
            var messages = _messageService.GetLast50Messages();
            await _chatHub.Clients.Client(connectionId).Initialize(messages);
        }
    }
}
