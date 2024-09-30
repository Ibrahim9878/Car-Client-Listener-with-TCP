using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Channels;
using TCP_Task_Manager_Client;

var ip = IPAddress.Loopback;
var port = 27001;
var client = new TcpClient();
client.Connect(ip, port);
var stream = client.GetStream();
var br = new BinaryReader(stream);
var bw = new BinaryWriter(stream);
Command command = null;
string responce = null;
string str = null;
while (true)
{
    Console.Write("Write any command or Help: ");
    str = Console.ReadLine().ToUpper();
    if (str == "HELP")
    {
        Console.WriteLine();
        Console.WriteLine("Command List: ");
        Console.WriteLine(Command.CarList);
        Console.WriteLine($"{Command.Put} <car_id> <car_name>");
        Console.WriteLine($"{Command.Post} <car_id> <post_property>");
        Console.WriteLine($"{Command.Delete} <car_id>");
        Console.WriteLine($"Help");
        Console.ReadLine();
        Console.Clear();
    }
    var input = str.Split(' ');
    switch (input[0].ToUpper())
    {
        case Command.CarList:
            command = new Command() { Text = input[0] };
            bw.Write(JsonSerializer.Serialize(command));
            responce = br.ReadString();
            var CarList = JsonSerializer.Deserialize<List<Car>>(responce);
            foreach (var item in CarList)
            {
                Console.WriteLine($"{item}");
            }
            break;
        case Command.Put:
            Car car = new Car() {Id = int.Parse(input[1]), Model = input[2] };
            command = new Command() { Text = input[0], Param = car };
            bw.Write(JsonSerializer.Serialize(command));
            responce = br.ReadString();
            var newCar = JsonSerializer.Deserialize<Car>(responce);
            Console.WriteLine($"{newCar.Model} Added");
            break;
        case Command.Delete:
            Car car2 = new Car() { Id = int.Parse(input[1]) };
            command = new Command() { Text = input[0], Param = car2 };
            bw.Write(JsonSerializer.Serialize(command));
            responce = br.ReadString();
            if(responce.Contains("No Car With this id"))
            {
                Console.WriteLine(responce);
                
            }
            else
            {
                var DeletedCar = JsonSerializer.Deserialize<Car>(responce);
                Console.WriteLine($"{DeletedCar.Id} Deleted");
            }
            break;
        case Command.Post:
            Car car3 = new Car() { Id = int.Parse(input[1]) };
            command = new Command() { Text = input[0], Param = car3, PostProperty = input[2] };
            bw.Write(JsonSerializer.Serialize(command));
            responce = br.ReadString() ;
            if (responce.Contains("No Car With this id"))
            {
                Console.WriteLine(responce);

            }
            else
            {
                var EditedCar = JsonSerializer.Deserialize<Car>(responce);
                Console.WriteLine($"{EditedCar.Id} Edited");
            }
            break;
        default:

            break;
    }
}