using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Obvs.Types;

namespace Obvs.NoneEndpoint
{
    public class NoneEndpoint : IServiceEndpoint
    {
        public bool CanHandle(IMessage message) => false;

        public string Name => nameof(NoneEndpoint);
        public IObservable<IRequest> Requests { get; } = Observable.Empty<IRequest>();

        public IObservable<ICommand> Commands { get; } = Observable.Empty<ICommand>();

        public Task PublishAsync(IEvent ev) => Task.FromResult(0);

        public Task ReplyAsync(IRequest request, IResponse response) => Task.FromResult(0);

        public void Dispose()
        {
        }
    }
}
