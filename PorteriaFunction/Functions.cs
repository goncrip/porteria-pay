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

            var personas = await porteriaContext.Personas.Select(x => new
            {
                x.IdPersona,
                x.Celular,
                x.Apellidos,
                x.Documento,
                x.TipoDocumento,
                Nombres = $"{x.Pais} {x.Documento} {x.Nombres} {x.Apellidos}",
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

            var vehiculos = await porteriaContext.Vehiculos.Select(x => new
            {
                x.IdVehiculo,
                x.Matricula,
                x.Pais
            }).ToListAsync();

            return new OkObjectResult(vehiculos);
        }

        [FunctionName("AddIngreso")]
        public async Task<IActionResult> AddIngresoAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var data = JsonConvert.DeserializeObject<IngresoDTO>(requestBody);
           
            var ingreso = new Ingreso()
            {
                FechaIngreso = DateTime.Now.ToUniversalTime(),
                IdEmpresa = data.IdEmpresa,
                IdVehiculo = data.IdVehiculo,
                IdPersona = data.IdPersona,
                IdTipoCarga = data.IdTipoCarga,
                Peso = data.Peso
            };

            porteriaContext.Ingresos.Add(ingreso);
            await porteriaContext.SaveChangesAsync();
            
            return new OkObjectResult("OK");
        }

        [FunctionName("UpdateEgreso")]
        public async Task<IActionResult> UpdateEgresoAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var idIngreso = Convert.ToInt32(req.Query["idIngreso"]);
            
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var ingreso = await porteriaContext
                            .Ingresos
                            .Where(x => x.IdIngreso == idIngreso && x.FechaEgreso == null)
                            .FirstOrDefaultAsync();

            if (ingreso != null)
            {
                ingreso.FechaEgreso = DateTime.Now.ToUniversalTime();

                await porteriaContext.SaveChangesAsync();
            }
            else
            {
                return new BadRequestObjectResult("No se encontró un ingreso sin fecha de egreso.");
            }

            return new OkObjectResult("OK");
        }

        [FunctionName("GetEgresosPendientes")]
        public async Task<IActionResult> GetEgresosPendientesAsync(
           [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
           ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string matricula = req.Query["matricula"];
            string pais = req.Query["pais"];

            var vehiculo = await porteriaContext
                                  .Vehiculos
                                  .Where(x => x.Matricula == matricula && 
                                              x.Pais == pais)
                                  .FirstOrDefaultAsync();

            if (vehiculo != null)
            {
                var ingresos = await porteriaContext.Ingresos.Where(x => x.IdVehiculo == vehiculo.IdVehiculo && x.FechaEgreso == null).Select(x => new
                {
                    x.IdIngreso,
                    x.FechaIngreso
                }).ToListAsync();

                return new OkObjectResult(ingresos);
            }

            return new BadRequestObjectResult($"No se encontró un ingreso pendiente para la matricula {pais} - {matricula}.");
        }

        [FunctionName("GetIngresos")]
        public async Task<IActionResult> GetIngresosAsync(
           [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
           ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            DateTime from = Convert.ToDateTime(req.Query["from"]);
            DateTime to = Convert.ToDateTime(req.Query["to"]);
            int idEmpresa = Convert.ToInt32(req.Query["idempresa"]);
            int idVehiculo = Convert.ToInt32(req.Query["idvehiculo"]);
            int idPersona = Convert.ToInt32(req.Query["idpersona"]);
            int idTipoCarga = Convert.ToInt32(req.Query["idtipocarga"]);

            var q = porteriaContext
                    .Ingresos
                    .Where(x => x.FechaIngreso.Date >= from.Date && 
                        x.FechaIngreso.Date <= to.Date)
                    .AsQueryable();

            if (idEmpresa > 0)
            {
                q = q.Where(x => x.IdEmpresa == idEmpresa);
            }

            if (idVehiculo > 0)
            {
                q = q.Where(x => x.IdVehiculo == idVehiculo);
            }

            if (idPersona > 0)
            {
                q = q.Where(x => x.IdPersona == idPersona);
            }

            if (idTipoCarga > 0)
            {
                q = q.Where(x => x.IdTipoCarga == idTipoCarga);
            }

            const string format = "dd/MM/yyyy HH:mm:ss";

            var query = from q1 in q
                        join e in porteriaContext.Empresas
                        on q1.IdEmpresa equals e.IdEmpresa
                        join v in porteriaContext.Vehiculos
                        on q1.IdVehiculo equals v.IdVehiculo
                        join p in porteriaContext.Personas
                        on q1.IdPersona equals p.IdPersona
                        join t in porteriaContext.TipoCargas
                        on q1.IdTipoCarga equals t.IdTipoCarga
                        select new ConsultaIngresoDTO()
                        {
                            Apellidos = p.Apellidos,
                            Documento = p.Documento,
                            Empresa = e.Nombre,
                            FechaEgreso = q1.FechaEgreso.HasValue ? q1.FechaEgreso.Value.AddHours(-3).ToString(format) : null,
                            FechaIngreso = q1.FechaIngreso.AddHours(-3).ToString(format),
                            Matricula = v.Matricula,
                            Nombres = p.Nombres,
                            PaisMatricula = v.Pais,
                            Peso = q1.Peso ?? 0,
                            TipoCarga = t.Descripcion
                        };

            return new OkObjectResult(await query.ToListAsync());
        }
    }
}
