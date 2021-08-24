using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarShop.Controllers
{
    class IssuesController : Controller
    {
        public HttpResponse Add()
        {
            return this.View();
        }
    }
}
