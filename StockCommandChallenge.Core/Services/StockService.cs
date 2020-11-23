using Microsoft.Extensions.Options;
using StockCommandChallenge.Core.Interfaces;
using StockCommandChallenge.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace StockCommandChallenge.Core.Services
{
    public class StockService : IStockService
    {

        private readonly AppSettings settings;
        HttpClient _client;
        public StockService(IOptions<AppSettings> appSettings)
        {
            settings = appSettings.Value;
            _client = new HttpClient();
        }

        public string GetStockFromFileContent(string fileContent)
        {
            List<string[]> csvRows = new List<string[]>();

            string[] Rows = fileContent.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (string row in Rows)
            {
                csvRows.Add(row.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
            }
            if (csvRows.Count > 1)
            {
                if (csvRows[1][6].Trim() == "N/D")
                {
                    return "Invalid stock code.";
                }
                return $"{csvRows[1][0]} quote is {csvRows[1][6].Trim()} per share".Trim();
            }

            return null;
        }

        public async Task<string> GetStockFileContent(string stockCode)
        {
            string fileContent = "";
            try
            {
                var response = await _client.GetAsync(string.Format(settings.StockApiUrl, stockCode));

                using (StreamReader sr = new StreamReader(await response.Content.ReadAsStreamAsync()))
                {
                    fileContent = sr.ReadToEnd();
                }
            }
            catch (Exception)
            {
                throw new ArgumentException("Invalid stock code.");
            }

            return fileContent;
        }

        public async Task<string> GetStock(string stockCode)
        {
            var fileContent = await this.GetStockFileContent(stockCode);
            return GetStockFromFileContent(fileContent);
        }
    }
}
