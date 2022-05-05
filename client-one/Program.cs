// See https://aka.ms/new-console-template for more information
using NATS.Client;
using NATS.Client.JetStream;
// Create a new connection factory to create
// a connection.
ConnectionFactory cf = new ConnectionFactory();

// Creates a live connection to the default
// NATS Server running locally
var options = ConnectionFactory.GetDefaultOptions();
options.Servers = new string[] { "nats://localhost:4222" };
options.AsyncErrorEventHandler += (obj, sender) =>
{
    Console.WriteLine("ERROR");
};
options.UnhandledStatusEventHandler += (obj, sender) =>
{
    Console.WriteLine("ERROR");
};
IConnection c = cf.CreateConnection(options);

var js = c.CreateJetStreamContext();
var i = 0;
var consConfig = new ConsumerConfiguration.ConsumerConfigurationBuilder()
    .WithReplayPolicy(ReplayPolicy.Instant)
    .WithDeliverPolicy(DeliverPolicy.LastPerSubject)
    .WithAckPolicy(AckPolicy.All)
    .WithMaxAckPending(1)
    .Build();
var pushSubOption = new PushSubscribeOptions.PushSubscribeOptionsBuilder()
    .WithConfiguration(consConfig)
    .Build();
js.PushSubscribeAsync("test.>", (obj, arg) =>
{
    Console.WriteLine($"subject: {arg.Message.Subject} - nth message: {i}");
    i++;
}, true, pushSubOption);

Console.ReadLine();