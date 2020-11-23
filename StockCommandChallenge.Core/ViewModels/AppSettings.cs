using System;
using System.Collections.Generic;
using System.Text;

namespace StockCommandChallenge.Core.ViewModels
{
    public class AppSettings
    {
        public string StockApiUrl { get; set; }
        public string ServiceBrokerHostName { get; set; }
        public string ServiceBrokerUsername { get; set; }
        public string ServiceBrokerPassword { get; set; }
    }
}
