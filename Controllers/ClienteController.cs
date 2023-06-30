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
    public class ClienteController : ControllerBase
    {
        IClienteService clienteService;

        public ClienteController(IClienteService service)
        {
            clienteService = service;
        }

        [HttpGet("{fechaInicial}&{fechaFinal}")]
        public IActionResult Get(string fechaInicial, string fechaFinal)
        {
            if (string.IsNullOrEmpty(fechaInicial) || string.IsNullOrEmpty(fechaFinal))
            {
                return NotFound();
            }
            return Ok(clienteService.GetClientes(fechaInicial, fechaFinal));
        }
    }
}