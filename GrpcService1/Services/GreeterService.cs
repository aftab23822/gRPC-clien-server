using System.Net;
using Grpc.Core;
using GrpcService1;

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
        public override Task<GenerateOpenApiJsonResponse> GenerateOpenApiJson(GenerateOpenApiJsonRequest request, ServerCallContext context)
        {
            var isSuccessful = DownloadFile(request);
            return Task.FromResult(
                DownloadFile(request)
            );
        }

        private GenerateOpenApiJsonResponse DownloadFile(GenerateOpenApiJsonRequest request)
        {
            var generateOpenApiJsonResponse = new GenerateOpenApiJsonResponse() { Successful = false };
            using var client = new WebClient();
            ServicePointManager.ServerCertificateValidationCallback =
               delegate { return true; };
            try
            {
                client.DownloadFile(request.ServerAddress, request.FileName);
                generateOpenApiJsonResponse.Successful = true;
                generateOpenApiJsonResponse.Message = "Successful";
            }
            catch (Exception ex)
            {
                generateOpenApiJsonResponse.Successful = false;
                generateOpenApiJsonResponse.Message = ex.Message;
            }
            return generateOpenApiJsonResponse;
        }
    }
}