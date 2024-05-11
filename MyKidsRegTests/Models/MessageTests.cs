using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyKidsReg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyKidsReg.Models.Tests
{
    [TestClass()]
    public class MessageTests
    {
        private Message _InvalidMessage = new Message { Description = "" };
        private Message _ValidMessage = new Message { Description = "This is a valid description" };

        [TestMethod()]
        public void DescriptionValidateTest_EmptyDescription()
        {
            _ValidMessage.DescriptionValidate();
            Assert.ThrowsException<ArgumentNullException>(() => _InvalidMessage.DescriptionValidate());
        }

        [TestMethod()]
        public void DescriptionValidateTest_ValidDescription()
        {
            _ValidMessage.DescriptionValidate(); 
        }
    }
}