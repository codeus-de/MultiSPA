using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiSPA.Data.Models;

namespace MultiSPA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class App1DemoDataController : ControllerBase
    {
        // GET: api/App1DemoData
        [HttpGet]
        public IEnumerable<App1DemoData> Get()
        {
            return new App1DemoData[] { 
                new App1DemoData() { Id = 10, Name = "Henry", Bam = DateTime.Now },
                new App1DemoData() { Id = 20, Name = "Hund", Bam = DateTime.Now.AddDays(1) }
            };
        }
    }
}
