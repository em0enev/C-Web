using SIS.HTTP.Common;
using SIS.HTTP.Enums;
using SIS.HTTP.Exceptions;
using SIS.HTTP.Requests;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses.Contracts;
using SIS.WebServer.Result;
using SIS.WebServer.Routing.Contracts;
using System;
using System.Net.Sockets;
using System.Text;

namespace SIS.WebServer
{
    public class ConnectionHandler
    {
        private readonly Socket client;

        private readonly IServerRoutingTable serverRoutingTable;

        public ConnectionHandler(Socket client, IServerRoutingTable serverRoutingTable)
        {
            CoreValidator.ThrowIfNull(client, nameof(client));
            CoreValidator.ThrowIfNull(serverRoutingTable, nameof(serverRoutingTable));

            this.client = client;
            this.serverRoutingTable = serverRoutingTable;
        }

        public void ProcessRequest()
        {
            try
            {
                var httpRequest = this.ReadRequest();

                if (httpRequest != null)
                {
                    Console.WriteLine($"Processing: {httpRequest.RequestMethod} {httpRequest.Path}...");

                    var httpResponse = this.HandlerRequest(httpRequest);

                    this.PrepareResponse(httpResponse);
                }
            }
            catch (BadRequestException e)
            {
                this.PrepareResponse(new TextResult(e.ToString(), HttpResponseStatusCode.BadRequest));
            }
            catch (Exception e)
            {
                this.PrepareResponse(new TextResult(e.ToString(), HttpResponseStatusCode.InternalServerError));
            }

            this.client.Shutdown(SocketShutdown.Both);
        }

        private IHttpRequest ReadRequest()
        {
            var result = new StringBuilder();
            var data = new ArraySegment<byte>(new byte[1024]);

            while (true)
            {
                int numberOfBytesRead = this.client.Receive(data.Array, SocketFlags.None);

                if (numberOfBytesRead == 0)
                {
                    break;
                }

                var bytesAsString = Encoding.UTF8.GetString(data.Array, 0, numberOfBytesRead);
                result.Append(bytesAsString);

                if (numberOfBytesRead < 1023)
                {
                    break;
                }
            }
            if (result.Length == 0)
            {
                return null;
            }
            return new HttpRequest(result.ToString());
        }

        private IHttpResponse HandlerRequest(IHttpRequest request)
        {
            // execute function from current request => return- response
            if (!this.serverRoutingTable.Contains(request.RequestMethod, request.Path))
            {
                return new TextResult($"Route with method{request.RequestMethod} and path {request.Path} not found.", HttpResponseStatusCode.NotFound);
            }

            return this.serverRoutingTable.Get(request.RequestMethod, request.Path).Invoke(request);
        }

        private void PrepareResponse(IHttpResponse response)
        {
            // prepares response -> maps it to byte data
            byte[] byteSegments = response.GetBytes();

            this.client.Send(byteSegments, SocketFlags.None);
        }
    }
}