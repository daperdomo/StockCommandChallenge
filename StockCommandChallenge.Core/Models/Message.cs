using System;
using System.Collections.Generic;

#nullable disable

namespace StockCommandChallenge.Core.Models
{
    public partial class Message
    {
        public string Username { get; set; }
        public string MessageText { get; set; }
        public byte[] SentTime { get; set; }
        public DateTime Created { get; set; }
    }
}
