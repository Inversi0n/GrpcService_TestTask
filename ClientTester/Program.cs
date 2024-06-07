using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Grpc.Net.Client;
using ClientTester;


// The port number must match the port of the gRPC server.
var hostPort = 5001;//32774;//7294;//5001;//7061

using var channel = GrpcChannel.ForAddress($"https://localhost:{hostPort}");
var dataClient = new Dater.DaterClient(channel);

var curState = RequestState.Add;
ChangeStatesNotify(curState);

while (true)
{
    var inputString = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(inputString))
        break;

    switch (inputString.ToLower())
    {
        case "add":
            {
                curState = RequestState.Add;

                ChangeStatesNotify(curState);
                break;
            }
        case "remove":
            {
                curState = RequestState.Remove;

                ChangeStatesNotify(curState);
                break;
            }
        case "get":
            {
                curState = RequestState.Get;

                ChangeStatesNotify(curState);
                break;
            }
        case "getall":
            {
                var response = await dataClient.GetAllAsync(new GetAllRequest());

                if (response == null)
                {
                    Console.WriteLine("Unable to get all elements");
                    break;
                }

                var sb = new StringBuilder();
                foreach (var i in response.Replies)
                {
                    sb.Append($"id = {i.Id} Data = {i.Data}");
                }

                Console.WriteLine(sb.ToString());
                break;
            }
        default:
            var strs = inputString.Split(" ");
            if (!int.TryParse(strs.FirstOrDefault(), out var id))
            {
                Console.WriteLine("Unable to parse id, try again");
            }

            switch (curState)
            {
                case RequestState.Add:
                    {
                        var sb = new StringBuilder();
                        for (int i = 1; i < strs.Length; i++)
                        {
                            sb.AppendLine(strs[i]);
                        }
                        var data = sb.ToString();
                        if (string.IsNullOrWhiteSpace(data))
                        {
                            Console.WriteLine("Data in the element requered");
                            break;
                        }
                        var response = await dataClient.AddAsync(new AddRequest() { Id = id, Data = sb.ToString() });
                        var wasAdded = response.IsAdded ? "was" : "wasn't";
                        Console.WriteLine($"Element {wasAdded} added");
                        break;
                    }
                case RequestState.Remove:
                    {
                        var response = await dataClient.RemoveAsync(new RemoveRequest() { Id = id });
                        var wasAdded = response.IsRemoved ? "was" : "wasn't";
                        Console.WriteLine($"Element {wasAdded} removed");
                        break;
                    }
                case RequestState.Get:
                    {
                        var sb = new StringBuilder();
                        for (int i = 1; i < strs.Length; i++)
                        {
                            sb.AppendLine(strs[i]);
                        }

                        var response = await dataClient.GetAsync(new GetRequest() { Id = id });
                        if (response == null || !response.IsSuccess)
                        {
                            Console.WriteLine("Unable to find element");
                        }
                        else
                        {
                            Console.WriteLine($"Returned element with id = {response.Id} and Data = {response.Data}");
                        }
                        break;
                    }
            }
            break;
    }
}
Console.WriteLine("Press any key to exit...");
Console.ReadKey();

void ChangeStatesNotify(RequestState state)
{
    Console.ForegroundColor = state switch
    {
        RequestState.Add => ConsoleColor.Green,
        RequestState.Remove => ConsoleColor.Red,
        RequestState.Get => ConsoleColor.Yellow,
        _ => ConsoleColor.White
    };

    Console.WriteLine($"You're currently on {curState.ToString()} mode");
    Console.ForegroundColor = ConsoleColor.White;
}

enum RequestState
{
    Add = 0,
    Remove = 1,
    Get = 2,
}