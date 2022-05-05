// See https://aka.ms/new-console-template for more information
using NATS.Client;
using static NATS.Client.JetStream.PurgeOptions;

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
var jsmc = c.CreateJetStreamManagementContext();
var i = 0;
for (var j = 0; j < 100; j++)
{
    var subject = $"test.{i}-{j}";
    Task.Run(() =>
    {
        c.Publish(subject, new byte[0]);
        // Task.Delay(1500).Wait();
        // jsmc.PurgeStream("test", new PurgeOptionsBuilder().WithSubject(subject).Build());
    });
}
i++;
Console.ReadLine();
