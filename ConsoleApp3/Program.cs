using System.Net;
using System.Net.Sockets;

//var client = new TcpClient();
//client.Connect("10.2.14.1", 27001);
//var stream = client.GetStream();
//var bw = new BinaryWriter(stream);
//var br = new BinaryReader(stream);

//var message = string.Empty;
//var answer = string.Empty;
//while (true)
//{
//    message = Console.ReadLine();
//    bw.Write(message);
//    answer = br.ReadString();
//    Console.WriteLine(answer);
//}

using System.Text;

var listener = new UdpClient(27001);
var remoteEP = new IPEndPoint(IPAddress.Any, 0);
var message = string.Empty;

while (true)
{
    var bytes = listener.Receive(ref remoteEP);
    message = Encoding.Default.GetString(bytes);
    Console.WriteLine(message);
    Thread.Sleep(100);
    Console.Clear();
}

