using StockCommandChallenge.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockCommandChallenge.Core.Helpers
{
    public class CommandHelper : ICommandHelper
    {
        private readonly List<string> AllowCommands = new List<string>
        {
            "HELP",
            "STOCK"
        };

        /// <summary>
        /// Validate is the current command is HELP
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool IsHelpCommand(string text)
        {
            return text.Trim().ToUpper() == "/HELP";
        }

        /// <summary>
        /// Evaluate if the text parameter is a stock command.
        /// </summary>
        /// <param name="text">Text to evaluate</param>
        /// <returns>returns the stock code</returns>
        public string GetStock(string text)
        {
            string[] stockSplitted = text.Split('=');
            if (stockSplitted.Length > 1 && stockSplitted[0].ToUpper().StartsWith("/STOCK"))
            {
                return stockSplitted[1];
            }
            else if (stockSplitted.Length == 1 && stockSplitted[0].ToUpper().StartsWith("/STOCK"))
            {
                throw new ArgumentException("Command error, use /HELP for more information.");
            }
            return string.Empty;
        }
    }
}
