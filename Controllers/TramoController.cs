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
    public class TramoController : ControllerBase
    {
        ITramoServices tramoService;
        public TramoController(ITramoServices service)
        {
            tramoService = service;
        }

        [HttpGet("fechainicial={fechaInicial}/fechafinal={fechaFinal}")]
        public IActionResult GetTramos(string fechaInicial, string fechaFinal)
        {
            if (string.IsNullOrEmpty(fechaInicial) || string.IsNullOrEmpty(fechaFinal))
            {
                return NotFound();
            }
            return Ok(tramoService.GetTramos(fechaInicial, fechaFinal));
        }
        [HttpGet("perdidas/fechainicial={fechaInicial}/fechafinal={fechaFinal}/tramos={CountTramos}")]
        public IActionResult GetTramosPerdidas(string fechaInicial, string fechaFinal, string CountTramos)
        {
            if (string.IsNullOrEmpty(fechaInicial) || string.IsNullOrEmpty(fechaFinal) || string.IsNullOrEmpty(CountTramos))
            {
                return NotFound();
            }
            return Ok(tramoService.GetPeoresTramos(fechaInicial, fechaFinal, Convert.ToInt32(CountTramos)));
        }
    }
}