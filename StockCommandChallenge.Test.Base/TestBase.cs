using NUnit.Framework;
using System;

namespace StockCommandChallenge.Test.Base
{
    [TestFixture]
    public class TestBase
    {
        protected IServiceProvider _services;

        public virtual void SetUp()
        {
            _services = Program.CreateHostBuilder(new string[] { }).Build().Services;
        }
    }
}
