using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using ExampleAPI.Services;

namespace ExampleAPI.Controllers
{
    [Route("api/values")]
    public class ValuesController : ControllerBase
    {
        /// <summary>
        /// We can now use the Foo Service in the Controller :)
        /// </summary>
        /// <param name="fooService">The one we created</param>
        public ValuesController(FooService fooService) { }
    }
}
