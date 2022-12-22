using Grpc.Net.Client;
using System.ComponentModel;
using TMT.Share.CustomerManagement;

// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("http://localhost:20002");
var client = new CustomerGrpc.CustomerGrpcClient(channel);

var customerId = 0;
var receiverId = 0;

var reply = await client.CheckIfCustomerIsValidAsync(
    new CustomerCheckRequest
    {
        CustomerId = customerId,
        ReceiverId = receiverId,
    });
Console.WriteLine("Is Customer Valid? " + reply.IsCustomerValid);
Console.WriteLine("Is Receiver Valid? " + reply.IsReceiverValid);


var reply1 = await client.GetCustomerInfoAsync(
    new CustomerGetRequest { Id = customerId }
    );

foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(reply1))
{
    string name = descriptor.Name;
    object value = descriptor.GetValue(reply1);
    Console.WriteLine("{0}={1}", name, value);
}

var reply2 = await client.GetReceiverInfoAsync(
    new ReceiverGetRequest { Id = receiverId }
    );

foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(reply2))
{
    string name = descriptor.Name;
    object value = descriptor.GetValue(reply2);
    Console.WriteLine("{0}={1}", name, value);
}



Console.WriteLine("Press any key to exit...");
Console.ReadKey();