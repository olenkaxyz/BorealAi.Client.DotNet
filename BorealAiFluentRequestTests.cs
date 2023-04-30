using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BorealAI.Client.Tests
{
    [TestClass]
    public class BorealAiFluentRequestTests
    {
        [TestMethod]
        public void CanGenerateRequestFromIntentName()
        {
            var intent = "test.intent";
            var request = BorealAiRequestFactory.FromIntent(intent);

            Assert.IsNotNull(request);
            Assert.AreEqual(intent, request.GetRequestBody().IntentName);
        }

        [TestMethod]
        public void CanGenerateRequestWithEntities()
        {
            var intent = "test.intent";
            var entityAName = "entityAName";
            var entityAValue = "entityAValue";
            var request = BorealAiRequestFactory
                .FromIntent(intent)
                .WithEntity(entityAName, entityAValue);

            Assert.IsNotNull(request);
            Assert.AreEqual(intent, request.GetRequestBody().IntentName);
            Assert.AreEqual(entityAName, request.GetRequestBody().Entities.First().EntityName);
            Assert.AreEqual(entityAValue, request.GetRequestBody().Entities.First().EntityValue);
        }
    }
}
