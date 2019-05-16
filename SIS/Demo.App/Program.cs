using SIS.HTTP.Requests;
using System;

namespace Demo.App
{
    class Program
    {
        static void Main(string[] args)
        {
            string request =
                $@"POST /url/asd?name=jhon&id=1#fragment HTTP/1.1{Environment.NewLine}"
                + "Authorization: Basic 321321321\r\n"
                + "Date: " + DateTime.Now + "\r\n"
                + "Host: localhost:5000\r\n"
                + "\r\n"
                + "username=jhondoe&password=123";

            string requestFromSite = $@"POST /cgi-bin/process.cgi?licenseID=string&content=string&/paramsXML=string HTTP/1.1
 User-Agent: Mozilla/4.0 (compatible; MSIE5.01; Windows NT)
 Host: www.tutorialspoint.com
 Content-Type: application/x-www-form-urlencoded
 Content-Length: length
 Accept-Language: en-us
 Accept-Encoding: gzip, deflate
 Connection: Keep-Alive
    
 licenseID=string&content=string&/paramsXML=string";


            HttpRequest httpRequest = new HttpRequest(request);

            ;
        }
    }
}
