using Grpc.Core;
using GrpcService;

namespace GrpcService1.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }

        private Func<HelloRequest, GreetingReply> getGreetReply = (request) =>
        {
            var reply = new GreetingReply();
            var hour = DateTime.Now.Hour;
            if (hour > 4 && hour < 12)
            {
                reply.GreetingOfDay = $"Good Morning, {request.Name}.";
                reply.ExtraMessage = "Good Day!!";
            }
            else if (hour > 12 && hour < 18)
            {
                reply.GreetingOfDay = $"Good Afternoon, {request.Name}.";
                reply.ExtraMessage = "Good Day Ahead!!";
            }
            else if (hour > 18 && hour < 21)
            {
                reply.GreetingOfDay = $"Good Evening, {request.Name}.";
                reply.ExtraMessage = "Hope you have awesome evening!!";
            }
            else
            {
                reply.GreetingOfDay = $"Good Night, {request.Name}";
                reply.ExtraMessage = "Sweet Dreams!!";
            }

            reply.Day = DateTime.Today.ToString("dddd");
            reply.Time = DateTime.Now.ToString("hh mm tt");
            return reply;
        };
        public override Task<GreetingReply> GreetWithTime(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(getGreetReply(request));
        }
    }
}