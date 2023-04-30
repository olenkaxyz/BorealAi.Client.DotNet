using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BorealAI.Client.Tests
{
    [TestClass]
    public class BorealAiRequestGreetingsTests
    {
        [TestMethod]
        public void CanCreateGreetingRequest()
        {
            var request = BorealAiRequestFactory.Greetings;
            Assert.IsNotNull(request);
            Assert.AreEqual(BorealAiConsts.GreetingsIntent, request.GetRequestBody().IntentName);
        }

        [TestMethod]
        public void CanCreateGreetingRequestWithUserName()
        {
            var request = BorealAiRequestFactory.Greetings.WithUserName("Bob");
            Assert.IsNotNull(request);
            Assert.AreEqual(BorealAiConsts.GreetingsIntent, request.GetRequestBody().IntentName);
            Assert.IsTrue(request.GetRequestBody().Entities.Count == 1);
            var entity = request.GetRequestBody().Entities.First();

            Assert.IsTrue(entity.EntityName == BorealAiConsts.GreetingsEntityUserName);
            Assert.IsTrue(entity.EntityValue == "Bob");
        }

        [TestMethod]
        public void CallingWithUserNameMultipleTimesUsesOnlyLastName()
        {
            var request = BorealAiRequestFactory
                .Greetings
                .WithUserName("Bob")
                .WithUserName("Rob");
            Assert.IsNotNull(request);
            Assert.AreEqual(BorealAiConsts.GreetingsIntent, request.GetRequestBody().IntentName);
            Assert.IsTrue(request.GetRequestBody().Entities.Count == 1);
            var entity = request.GetRequestBody().Entities.First();

            Assert.IsTrue(entity.EntityName == BorealAiConsts.GreetingsEntityUserName);
            Assert.IsTrue(entity.EntityValue == "Rob");
        }

        [TestMethod]
        public void CanAddContext()
        {
            var request = BorealAiRequestFactory
                .Greetings
                .WithContext("testContextName", "testContextValue1")
                .WithOptionalContext("optionalContext", "textContextValue3");

            var body = request.GetRequestBody();

            Assert.IsNotNull(request);
            Assert.IsNotNull(body);
            Assert.AreEqual(2, body.ContextData.Count);

            Assert.IsTrue(body.ContextData.Any(context => context.ContextName == "testContextName"));
            Assert.IsTrue(body.ContextData.Any(context => context.IsOptional));
            Assert.IsTrue(body.ContextData.Any(context => context.IsOptional == false));
        }
    }
}
