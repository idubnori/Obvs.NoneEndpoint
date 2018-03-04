using Obvs.Types;

namespace Obvs.NoneEndpoint.Tests.Messages
{
    public class TestResponse : IResponse, ITestMessage
    {
        public string RequestId { get; set; }
        public string RequesterId { get; set; }
        public int Id { get; set; }
    }
}