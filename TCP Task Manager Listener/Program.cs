using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using TCP_Task_Manager_Listener;

List<Car> Cars = new List<Car>()
{ 
    new Car(){ Id = 1,Model = "BMW"},
    new Car(){ Id = 2,Model = "Mercedes"},
    new Car(){ Id = 3,Model = "Hyundai"}
};
var ip = IPAddress.Loopback;
var port = 27001;
var listener = new TcpListener(ip, port);
listener.Start();
while (true)
{
    var client = listener.AcceptTcpClient();
    var stream = client.GetStream();
    var br = new BinaryReader(stream);
    var bw = new BinaryWriter(stream);
    while (true)
    {
        var input = br.ReadString();
        var command = JsonSerializer.Deserialize<Command>(input);
        Console.WriteLine(command.Text);
        Console.WriteLine(command.Param);
        Console.WriteLine(command.PostProperty);
        switch (command.Text.ToUpper())
        {
            case Command.CarList:
                var CarNames = JsonSerializer.Serialize(Cars);
                bw.Write(CarNames);
                break;
            case Command.Put:
                Cars.Add(command.Param);
                var name = JsonSerializer.Serialize(command.Param);
                bw.Write(name);
                break;
            case Command.Delete:
                var car = GetCarByID(command.Param.Id);
                if(car is null)
                {
                    bw.Write("No Car With this id");
                }
                else
                {
                    Cars.Remove(car);
                    var DeleteCar = JsonSerializer.Serialize(command.Param);
                    bw.Write(DeleteCar);
                }
                break;
            case Command.Post:
                var car1 = GetCarByID(command.Param.Id);
                if (car1 is null)
                {
                    bw.Write("No Car With this id");
                }
                else
                {
                    foreach (var item in Cars)
                    {
                        if(car1.Id == item.Id)
                        {
                            item.Model = command.PostProperty;
                            break;
                        }
                    }
                    var EditedCar = JsonSerializer.Serialize(command.Param);
                    bw.Write(EditedCar);
                }
                break;
            default:
                break;
        }
    }
}
Car GetCarByID(int id)
{
    foreach (var item in Cars)
    {
        if (item.Id == id) return item;
    }
    return null;
}