using SIS.HTTP.Common;
using SIS.HTTP.Enums;
using SIS.HTTP.Exceptions;
using SIS.HTTP.Headers;
using SIS.HTTP.Headers.Contracts;
using SIS.HTTP.Requests.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SIS.HTTP.Requests
{
    public class HttpRequest : IHttpRequest
    {
        public HttpRequest(string requestString)
        {
            CoreValidator.ThrowIfNullOrEmpty(requestString, nameof(requestString));
            this.FormData = new Dictionary<string, object>();
            this.QueryData = new Dictionary<string, object>();
            this.Headers = new HttpHeaderCollection();

            this.ParseRequest(requestString);
        }

        public string Path { get; private set; }

        public string Url { get; private set; }

        public Dictionary<string, object> FormData { get; }

        public Dictionary<string, object> QueryData { get; }

        public IHttpHeaderCollection Headers { get; }

        public HttpRequestMethod RequestMethod { get; private set; }

        private bool IsValidRequestLine(string[] requestLineParams)
        {
            if (requestLineParams.Length != 3
                || requestLineParams[2] != GlobalConstants.HttpOneProtocolFragment)
            {
                return false;
            }

            return true;
        }

        private bool IsValidRequestQueryString(string queryString, string[] queryParameters)
        {
            CoreValidator.ThrowIfNullOrEmpty(queryString, nameof(queryString));

            return true;
            // TODO => Regex query string
        }

        private void ParseRequestMethod(string[] requestLineParams) 
        {
            string requestMethod = requestLineParams[0];
            HttpRequestMethod method;

            bool parseResult = HttpRequestMethod.TryParse(requestMethod, true, out method);
            if (!parseResult)
            {
                throw new BadRequestException(string.Format(GlobalConstants.UnsupportedHttpMethodExceptionMessage, requestMethod));
            }

            this.RequestMethod = method;
        }

        private void ParseRequestUrl(string[] requestLineParams)
        {
            this.Url = requestLineParams[1];
        }

        private void ParseRequestPath()
        {
            this.Path = this.Url.Split('?')[0];
        }

        private void ParseRequestHeader(string[] plainHeaders)
        {
            var splitted = plainHeaders
                .Select(plainHeader => plainHeader
                .Split(": ", StringSplitOptions.RemoveEmptyEntries))
                .ToList();

            foreach (var kvp in splitted)
            {
                if (kvp.Length == 2)
                {
                    this.Headers.AddHeader(new HttpHeader(kvp[0], kvp[1]));
                }
            }
        }

        private void ParseRequestParameters(string requestBody)
        {
            this.ParseRequestQueryParameters();
            this.ParseRequestFormDataParameters(requestBody);
        }

        private void ParseRequestQueryParameters()
        {
            if (this.HasQueryString())
            {
                var queryParams = this.Url
                    .Split('?', StringSplitOptions.RemoveEmptyEntries)[1]
                    .Split('&')
                    .Select(plainQueryParameter => plainQueryParameter.Split('='))
                    .ToList();

                foreach (var param in queryParams)
                {
                    if (!this.QueryData.ContainsKey(param[0]))
                    {
                        this.QueryData.Add(param[0], new HashSet<string>());
                        var collection = (ICollection<string>)QueryData[param[0]];
                        collection.Add(param[1]);
                    }
                    else
                    {
                        var collection = (ICollection<string>)QueryData[param[0]];
                        collection.Add(param[1]);
                    }
                }
            }
        }

        private bool HasQueryString()
        {
            return this.Url.Split('?').Length > 1;
        }

        private void ParseRequestFormDataParameters(string requestBody)
        {
            if (!string.IsNullOrEmpty(requestBody))
            {
                requestBody
                     .Split('&')
                     .Select(plainQueryParameter => plainQueryParameter.Split('='))
                     .ToList()
                     .ForEach(queryParameterKVP =>
                        this.FormData.Add(queryParameterKVP[0], queryParameterKVP[1]));
            }
        }

        private void ParseRequest(string requestString)
        {
            string[] splitRequestString = requestString.Split(new[] { GlobalConstants.HttpNewLine }, StringSplitOptions.None);

            string[] requestLineParams = splitRequestString[0].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (!this.IsValidRequestLine(requestLineParams))
            {
                throw new BadRequestException();
            }

            this.ParseRequestMethod(requestLineParams);
            this.ParseRequestUrl(requestLineParams);
            this.ParseRequestPath();

            this.ParseRequestHeader(splitRequestString.Skip(1).ToArray());
            //this.parseCookies();

            this.ParseRequestParameters(splitRequestString[splitRequestString.Length - 1]);
        }
    }
}
