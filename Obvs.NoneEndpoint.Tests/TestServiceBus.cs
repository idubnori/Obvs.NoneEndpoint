using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Obvs.Configuration;
using Obvs.NoneEndpoint.Tests.Messages;
using Obvs.Types;
using Xunit;
using Xunit.Abstractions;

namespace Obvs.NoneEndpoint.Tests
{
    public class TestServiceBus
    {
        private readonly ITestOutputHelper _output;

        public TestServiceBus(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task ShouldSendAndReceiveMessagesOverServiceBus_AnyMessagesWithNoEndpointClients()
        {
            IServiceBus serviceBus = ServiceBus.Configure()
                    .WithNoneEndpoints()
                    .PublishLocally().AnyMessagesWithNoEndpointClients()
                    .UsingConsoleLogging()
                    .Create();

            await ShouldSendAndReceiveMessagesOverServiceBus(serviceBus);
        }

        [Fact]
        public async Task ShouldSendAndReceiveMessagesOverServiceBus_OnlyMessagesWithNoEndpoints()
        {
            IServiceBus serviceBus = ServiceBus.Configure()
                .WithNoneEndpoints()
                .PublishLocally().OnlyMessagesWithNoEndpoints()
                .UsingConsoleLogging()
                .Create();

            await ShouldSendAndReceiveMessagesOverServiceBus(serviceBus);
        }

        private async Task ShouldSendAndReceiveMessagesOverServiceBus(IServiceBus serviceBus)
        {
            // create threadsafe collection to hold received messages in
            var messages = new ConcurrentBag<IMessage>();

            // create some actions that will act as a fake services acting on incoming commands and requests
            Action<TestCommand> fakeService1 = command => serviceBus.PublishAsync(new TestEvent {Id = command.Id});
            Action<TestRequest> fakeService2 = request => serviceBus.ReplyAsync(request, new TestResponse {Id = request.Id});
            var observer = new AnonymousObserver<IMessage>(msg =>
            {
                messages.Add(msg);
                _output.WriteLine(JsonConvert.SerializeObject(msg));
            }, exception => _output.WriteLine(exception.ToString()));

            // subscribe to all messages on the ServiceBus
            var sub1 = serviceBus.Events.Subscribe(observer);
            var sub2 = serviceBus.Commands.Subscribe(observer);
            var sub3 = serviceBus.Requests.Subscribe(observer);
            var sub4 = serviceBus.Commands.OfType<TestCommand>().Subscribe(fakeService1);
            var sub5 = serviceBus.Requests.OfType<TestRequest>().Subscribe(fakeService2);

            // send some messages
            await serviceBus.SendAsync(new TestCommand {Id = 123});
            var sub6 = serviceBus.GetResponses(new TestRequest {Id = 456}).Subscribe(observer);

            // wait some time until we think all messages have been sent and received from RabbitMQ
            await Task.Delay(TimeSpan.FromSeconds(1));

            // test we got everything we expected
            Assert.True(messages.OfType<TestCommand>().Count() == 1, "TestCommand not received");
            Assert.True(messages.OfType<TestEvent>().Count() == 1, "TestEvent not received");
            Assert.True(messages.OfType<TestRequest>().Count() == 1, "TestRequest not received");
            Assert.True(messages.OfType<TestResponse>().Count() == 1, "TestResponse not received");

            // dispose subscriptions
            sub1.Dispose();
            sub2.Dispose();
            sub3.Dispose();
            sub4.Dispose();
            sub5.Dispose();
            sub6.Dispose();

            // always call Dispose on serviceBus when exiting process
            ((IDisposable) serviceBus).Dispose();
        }
    }
}
