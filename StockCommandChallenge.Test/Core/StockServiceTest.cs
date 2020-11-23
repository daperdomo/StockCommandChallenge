using NUnit.Framework;
using StockCommandChallenge.Core.Interfaces;
using StockCommandChallenge.Test.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockCommandChallenge.Test.Core
{
    public class StockServiceTest : TestBase
    {
        private IStockService _stockService;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            _stockService = (IStockService)_services.GetService(typeof(IStockService));
        }

        [Test]
        [TestCase("Symbol,Date,Time,Open,High,Low,Close,Volume \n AAPL.US, 2020 - 11 - 20, 22:00:01, 118.65, 118.77, 117.29, 117.34, 73604287")]
        public void get_stock_from_file_content_not_null(string fileContent)
        {
            string stock = _stockService.GetStockFromFileContent(fileContent);
            Assert.IsNotNull(stock);
        }

        [Test]
        [TestCase("Symbol,Date,Time,Open,High,Low,Close,Volume \n AAPL.US, 2020 - 11 - 20, 22:00:01, 118.65, 118.77, 117.29, 117.34, 73604287")]
        public void get_stock_from_file_content_equal(string fileContent)
        {
            string stock = _stockService.GetStockFromFileContent(fileContent);
            Assert.AreEqual(stock, "AAPL.US quote is 117.34 per share");
        }
    }
}
