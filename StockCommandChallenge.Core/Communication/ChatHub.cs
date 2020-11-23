using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StockCommandChallenge.Core.Communication
{
    public class ChatHub : Hub<IClient>
    {
        //private readonly IStockApiService _stockApiService;
        public ChatHub(/*IStockApiService stockApiService*/)
        {
            //_stockApiService = stockApiService;
        }

        public async Task SendMessage(string userName, string message)
        {

            //try
            //{
            //    string stockCode = CommandHelper.StockCommand(message);
            //    if (!string.IsNullOrEmpty(stockCode))
            //    {
            //        await Clients.All.SendMessage(userName, message, DateTime.Now);
            //        var result = await _stockApiService.GetStock(stockCode);
            //        await Clients.All.SendMessage("System", result, DateTime.Now);
            //    }
            //    else if (CommandHelper.IsHelpCommand(message))
            //    {
            //        await Clients.Client(Context.ConnectionId).SendMessage("System", "The correct command format is /stock={stock_code}", DateTime.Now);
            //    }
            //    else if (message.Trim().StartsWith("/"))
            //    {
            //        await Clients.All.SendMessage("System", "Command error, use /HELP for more information.", DateTime.Now);
            //    }
            //    else
            //    {
            //        await Clients.All.SendMessage(userName, message, DateTime.Now);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    await Clients.Client(Context.ConnectionId).SendMessage("System", ex.Message, DateTime.Now);
            //}
        }
    }

    public interface IClient
    {
        Task SendMessage(string userName, string message, DateTime date);
    }
}
