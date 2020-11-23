using System;
using System.Collections.Generic;
using System.Text;

namespace StockCommandChallenge.Core.Interfaces
{
    public interface ICommandHelper
    {
        bool IsHelpCommand(string text);
        string GetStock(string text);
    }
}
