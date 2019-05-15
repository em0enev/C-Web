using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace HttpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            const string newLine = "\r\n";

            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 12345);

            tcpListener.Start();

            while (true)
            {
                var client = tcpListener.AcceptTcpClient();

                using (var stream = client.GetStream())
                {
                    var requestBytes = new byte[100000];
                    var readBytes = stream.Read(requestBytes, 0, requestBytes.Length);
                    var stringRequest = Encoding.UTF8.GetString(requestBytes, 0, readBytes);
                    Console.WriteLine(new string('=', 20));
                    Console.WriteLine(stringRequest);

                    string responseBody = "<form method='post'><input type='text' name='tweet' placeholder='Enter tweet...' /><input name='name' /><input type='submit' /></form>";

                    string response = "HTTP/1.0 200 OK" + newLine +
                                      "Content-Type: text/html" + newLine +
                                      // "Location: https://google.com" + NewLine +
                                      // "Content-Disposition: attachment; filename=index.html" + NewLine +
                                      "Server: MyCustomServer/1.0" + newLine +
                                      $"Content-Length: {responseBody.Length}" + newLine + newLine +
                                       responseBody;

                    var responseBytes = Encoding.UTF8.GetBytes(response);
                    stream.Write(responseBytes, 0, responseBytes.Length);
                }
            }
        }
    }
}
