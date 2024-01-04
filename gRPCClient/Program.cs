using Grpc.Net.Client;
using GrpcService1;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var channel = GrpcChannel.ForAddress(new Uri("https://localhost:7217"));
        var gRPCclient = new Greeter.GreeterClient(channel);
        var reply = await gRPCclient.SayHelloAsync(new HelloRequest() { Name= "Simon"});
        var swaggerResponse = await gRPCclient.GenerateOpenApiJsonAsync(new GenerateOpenApiJsonRequest() { FileName =  "", ServerAddress=""});
        Console.WriteLine(swaggerResponse.Successful);
        Console.WriteLine(reply.Message);
        Console.ReadLine();
    }
}