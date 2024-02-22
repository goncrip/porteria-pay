using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using PorteriaFunction.DTO;

namespace PorteriaFunction
{
    public class Functions
    {
        private readonly PorteriaContext porteriaContext;

        public Functions(PorteriaContext porteriaContext)
        {
            this.porteriaContext = porteriaContext;
        }

        [FunctionName("AddEmpresa")]
        public async Task<IActionResult> AddEmpresaAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            //string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var data = JsonConvert.DeserializeObject<EmpresaDTO>(requestBody);

            if (data != null && !string.IsNullOrEmpty(data.Nombre))
            {
                var empresa = new Empresa()
                {
                    Nombre = data.Nombre
                };

                porteriaContext.Empresas.Add(empresa);
                await porteriaContext.SaveChangesAsync();
            }
            else
            {
                return new BadRequestObjectResult("Faltan datos obligatorios");
            }

            return new OkObjectResult("OK");
        }

        [FunctionName("GetEmpresa")]
        public async Task<IActionResult> GetEmpresaAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            //string name = req.Query["name"];

            var empresas = await porteriaContext.Empresas.Select(x => new
            {
                x.IdEmpresa,
                x.Nombre
            }).ToListAsync();

            return new OkObjectResult(empresas);
        }

        [FunctionName("AddPersona")]
        public async Task<IActionResult> AddPersonaAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var data = JsonConvert.DeserializeObject<PersonaDTO>(requestBody);

            if (!string.IsNullOrEmpty(data.Documento) || 
                !string.IsNullOrEmpty(data.TipoDocumento) ||
                !string.IsNullOrEmpty(data.Documento) ||
                !string.IsNullOrEmpty(data.Nombres) ||
                !string.IsNullOrEmpty(data.Apellidos) ||
                !string.IsNullOrEmpty(data.Pais))
            {
                var persona = new Persona()
                {
                    Apellidos = data.Apellidos,
                    Celular = data.Celular,
                    Documento = data.Documento,
                    Nombres = data.Nombres,
                    Pais = data.Pais,
                    TipoDocumento = data.TipoDocumento
                };

                porteriaContext.Personas.Add(persona);
                await porteriaContext.SaveChangesAsync();
            }
            else
            {
                return new BadRequestObjectResult("Faltan datos obligatorios");
            }

            return new OkObjectResult("OK");
        }

        [FunctionName("GetPersona")]
        public async Task<IActionResult> GetPersonaAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string documento = req.Query["documento"];

            var personas = await porteriaContext.Personas.Where(x => x.Documento == documento).Select(x => new
            {
                x.IdPersona,
                x.Celular,
                x.Apellidos,
                x.Documento,
                x.TipoDocumento,
                x.Nombres,
                x.Pais
            }).ToListAsync();

            return new OkObjectResult(personas);
        }

        [FunctionName("AddTipoCarga")]
        public async Task<IActionResult> AddTipoCargaAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var data = JsonConvert.DeserializeObject<TipoCargaDTO>(requestBody);

            if (!string.IsNullOrEmpty(data.Descripcion))
            {
                var tipoCarga = new TipoCarga()
                {
                    Descripcion = data.Descripcion
                };

                porteriaContext.TipoCargas.Add(tipoCarga);
                await porteriaContext.SaveChangesAsync();
            }
            else
            {
                return new BadRequestObjectResult("Faltan datos obligatorios");
            }

            return new OkObjectResult("OK");
        }

        [FunctionName("GetTipoCarga")]
        public async Task<IActionResult> GetTipoCargaAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string documento = req.Query["documento"];

            var personas = await porteriaContext.TipoCargas.Select(x => new
            {
                x.IdTipoCarga,
                x.Descripcion
            }).ToListAsync();

            return new OkObjectResult(personas);
        }

        [FunctionName("AddVehiculo")]
        public async Task<IActionResult> AddVehiculoAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var data = JsonConvert.DeserializeObject<VehiculoDTO>(requestBody);

            if (!string.IsNullOrEmpty(data.Matricula) ||
                !string.IsNullOrEmpty(data.Pais))
            {
                var vechiculo = new Vehiculo()
                {
                    Matricula = data.Matricula,
                    Pais = data.Pais,
                };

                porteriaContext.Vehiculos.Add(vechiculo);
                await porteriaContext.SaveChangesAsync();
            }
            else
            {
                return new BadRequestObjectResult("Faltan datos obligatorios");
            }

            return new OkObjectResult("OK");
        }

        [FunctionName("GetVehiculo")]
        public async Task<IActionResult> GetVehiculoAsync(
           [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
           ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string matricula = req.Query["matricula"];

            var vehiculos = await porteriaContext.Vehiculos.Where(x => x.Matricula == matricula).Select(x => new
            {
                x.IdVehiculo,
                x.Matricula,
                x.Pais
            }).ToListAsync();

            return new OkObjectResult(vehiculos);
        }
    }
}
