using StockCommandChallenge.Core.Interfaces;
using StockCommandChallenge.Core.Models;
using StockCommandChallenge.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockCommandChallenge.Core.Services
{
    public class MessageService : IMessageService
    {
        private readonly IRepository<Message> _messageRepository;
        public MessageService(IRepository<Message> messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public IEnumerable<Message> GetLast50Messages()
        {
            var result = _messageRepository.Table.OrderByDescending(m => m.Created).ToList();
            return result;
        }

        public void SaveMessage(string Username, string message)
        {
            _messageRepository.Create(new Message
            {
               MessageText = message,
               Username = Username
            });
            _messageRepository.SaveChanges();
        }
    }
}
