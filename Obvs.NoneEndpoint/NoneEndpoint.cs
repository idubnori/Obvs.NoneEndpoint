using Obvs.Types;

namespace Obvs.NoneEndpoint
{
    public class NoneEndpoint : ServiceEndpoint
    {
        public NoneEndpoint()
            : base(
                new DefaultMessageSource<IRequest>(),
                new DefaultMessageSource<ICommand>(),
                new DefaultMessagePublisher<IEvent>(),
                new DefaultMessagePublisher<IResponse>(),
                typeof(NoneEndpoint))
        {
        }
    }
}