using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Obvs.Types;

namespace Obvs.NoneEndpoint
{
    public class NoneEndpointClient : IServiceEndpointClient
    {
        public IObservable<IEvent> Events { get; } = Observable.Empty<IEvent>();

        public Task SendAsync(ICommand command) => Task.FromResult(0);

        public IObservable<IResponse> GetResponses(IRequest request) => Observable.Empty<IResponse>();

        public bool CanHandle(IMessage message) => false;

        public string Name => nameof(NoneEndpointClient);

        public void Dispose()
        {
        }
    }
}