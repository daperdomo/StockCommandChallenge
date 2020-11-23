using System.Threading.Tasks;

namespace StockCommandChallenge.Core.Interfaces
{
    public interface IStockService
    {
        void GetStock(string stockCode);
        Task<string> GetStockFileContent(string stockCode);
        string GetStockFromFileContent(string fileContent);
    }
}
