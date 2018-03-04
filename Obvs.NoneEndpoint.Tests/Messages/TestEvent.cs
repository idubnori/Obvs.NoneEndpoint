using Obvs.Types;

namespace Obvs.NoneEndpoint.Tests.Messages
{
    public class TestEvent : IEvent, ITestMessage
    {
        public int Id { get; set; }
    }
}