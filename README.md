Run start-nats-server.ps1
This will start a nats server and configure a stream (nats.exe and nats-server.exe should be available in the Path variable to run this).

After that, run `dotnet run` inside ./client-one
And run `dotnet run` inside ./client-two with line 30 and 31 commented out (as is commited)

You will see 100 messages being received by client-one (nth message: 99)
Stop both client-one and client-two
Uncomment line 31. Rerun both clients in the same order as before (first client-one, then client-two)

Probably depending on the performance of your machine, you'll see around 85 messages being received.