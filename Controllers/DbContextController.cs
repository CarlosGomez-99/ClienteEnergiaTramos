using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnergiaElectrica.Services;
using Microsoft.AspNetCore.Mvc;

namespace EnergiaElectrica.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DbContextController : ControllerBase
    {
        IDbService dbService;


        public DbContextController(IDbService service)
        {
            dbService = service;
        }
        [HttpGet]
        [Route("createdb")]
        public IActionResult CreateDataBase()
        {

            dbService.CreateDb();

            return Ok();
        }
    }
}