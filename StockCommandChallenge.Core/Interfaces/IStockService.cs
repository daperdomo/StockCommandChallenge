using System.Threading.Tasks;

namespace StockCommandChallenge.Core.Interfaces
{
    public interface IStockService
    {
        Task<string> GetStock(string stockCode);
        Task<string> GetStockFileContent(string stockCode);
        string GetStockFromFileContent(string fileContent);
    }
}
