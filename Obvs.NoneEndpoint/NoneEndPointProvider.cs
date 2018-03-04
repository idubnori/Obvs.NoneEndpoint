using Obvs.Configuration;
using Obvs.Types;

namespace Obvs.NoneEndpoint
{
    public class NoneEndPointProvider : IServiceEndpointProvider
    {
        private readonly NoneEndpoint _noneEndpoint;
        private readonly NoneEndpointClient _noneEndpointClient;

        public NoneEndPointProvider()
        {
            _noneEndpoint = new NoneEndpoint();
            _noneEndpointClient = new NoneEndpointClient();
        }

        public IServiceEndpoint<IMessage, ICommand, IEvent, IRequest, IResponse> CreateEndpoint() => _noneEndpoint;

        public IServiceEndpointClient<IMessage, ICommand, IEvent, IRequest, IResponse> CreateEndpointClient() => _noneEndpointClient;
    }
}