using Obvs.Configuration;
using Obvs.Types;

namespace Obvs.NoneEndpoint
{
    public static class Extensions
    {
        public static ICanAddEndpointOrLoggingOrCorrelationOrCreate<IMessage, ICommand, IEvent, IRequest, IResponse>
            WithNoneEndpoints(this ICanAddEndpoint<IMessage, ICommand, IEvent, IRequest, IResponse> canAddEndpoint)
        {
            return canAddEndpoint.WithEndpoints(new NoneEndPointProvider());
        }
    }
}