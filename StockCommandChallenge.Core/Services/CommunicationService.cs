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
        private readonly IServiceBroker _serviceBroker;
        private readonly IStockService _stockService;
        public CommunicationService(IMessageService messageService,
            IHubContext<ChatHub, IClient> hubContext,
            ICommandHelper commandHelper,
            IServiceBroker serviceBroker,
            IStockService stockService)
        {
            _messageService = messageService;
            _chatHub = hubContext;
            _commandHelper = commandHelper;
            _serviceBroker = serviceBroker;
            _stockService = stockService;
        }

        public async Task HandleMessage(string connectionId, string userName, string message)
        {
            try
            {
                message = message.Trim();
                string stockCode = _commandHelper.GetStock(message);
                if (!string.IsNullOrEmpty(stockCode))
                {
                    _messageService.SaveMessage(userName, message);
                    await _chatHub.Clients.All.SendMessage(userName, message, DateTime.Now);
                    _stockService.GetStock(stockCode);
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
            _serviceBroker.DeleteConsumer(userName);
            _serviceBroker.AddConsumer(userName, (message) =>
            {
                _chatHub.Clients.Client(connectionId).SendMessage("System", message, DateTime.Now);
            });
        }
    }
}
