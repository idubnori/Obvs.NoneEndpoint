using Obvs.Types;

namespace Obvs.NoneEndpoint
{
    public class NoneEndpointClient : ServiceEndpointClient
    {
        public NoneEndpointClient()
            : base(
                new DefaultMessageSource<IEvent>(),
                new DefaultMessageSource<IResponse>(),
                new DefaultMessagePublisher<IRequest>(),
                new DefaultMessagePublisher<ICommand>(),
                typeof(NoneEndpointClient))
        {
        }
    }
}