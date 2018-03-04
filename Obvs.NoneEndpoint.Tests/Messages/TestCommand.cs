using Obvs.Types;

namespace Obvs.NoneEndpoint.Tests.Messages
{
    public class TestCommand : ICommand, ITestMessage
    {
        public int Id { get; set; }
    }
}