using NUnit.Framework;
using StockCommandChallenge.Core.Interfaces;
using StockCommandChallenge.Test.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockCommandChallenge.Test.Core
{
    public class MessageServiceTest : TestBase
    {
        private IMessageService _messageService;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            _messageService = (IMessageService)_services.GetService(typeof(IMessageService));
        }

        [Test]
        public void get_last_50_messages_not_null()
        {
            var result = _messageService.GetLast50Messages();
            Assert.IsNotNull(result);
        }
    }
}
