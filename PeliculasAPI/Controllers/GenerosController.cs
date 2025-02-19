﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using PeliculasAPI.Entidades;

namespace PeliculasAPI.Controllers
{
    [Route("api/generos")]
    [ApiController]
    public class GenerosController : ControllerBase
    {
        private readonly IRepositorio repositorio;
        private readonly ServiciosTransient transient1;
        private readonly ServiciosTransient transient2;
        private readonly ServiciosScoped scoped1;
        private readonly ServiciosScoped scoped2;
        private readonly ServiciosSingleton singleton;
        private readonly IOutputCacheStore outputCacheStore;
        private readonly IConfiguration configuration;
        private const string cacheTag = "generos";

        public GenerosController(IRepositorio repositorio,
            ServiciosTransient transient1,
            ServiciosTransient transient2,
            ServiciosScoped scoped1,
            ServiciosScoped scoped2,
            ServiciosSingleton singleton,
            IOutputCacheStore outputCacheStore,
            IConfiguration configuration) 
        {
            this.repositorio = repositorio;
            this.transient1 = transient1;
            this.transient2 = transient2;
            this.scoped1 = scoped1;
            this.scoped2 = scoped2;
            this.singleton = singleton;
            this.outputCacheStore = outputCacheStore;
            this.configuration = configuration;
        }

        [HttpGet("ejemplo-proveedor-configuracion")]
        public string GetEjemploProveedorConfiguracion()
        {
            return configuration.GetValue<string>("CadenaDeConexion")!;
        }


        [HttpGet("servicios-tiempo-de-vida")]
        public IActionResult GetServiciosTiemposDeVida()
        {
            return Ok(new
            {
                Transient = new { transient1 = transient1.ObntenerId, transient2 = transient2.ObntenerId },
                Scopeds = new { scoped1 = scoped1.ObntenerId, scoped2 = scoped2.ObntenerId },
                Singleton = singleton.ObntenerId
            });
        }

        [HttpGet] // api/generos
        [HttpGet("listado")] // api/generos/listado
        [HttpGet("/listado/generos")] // listado-generos  
        [OutputCache]
        public List<Genero> Get()
        {
            // var repositorio = new RepositorioEnMemoria();
            var generos = repositorio.ObtenerTodosLosGeneros();
            return generos;
        }

        //[HttpGet("{id:int}/{nombre?}")] //api/generos/2/Marco
        //public Genero? Get(int id, string nombre = "Marco")  
        //{
        //    var repositorio = new RepositorioEnMemoria();
        //    var genero = repositorio.ObtenerPorId(id);
        //    return genero;
        //}    

        [HttpGet("{id:int}")] // api/generos/500
        [OutputCache(Tags = [cacheTag])]
        public async Task<ActionResult<Genero>> Get(int id)
        {
            
            var genero = await repositorio.ObtenerPorId(id);

            if (genero is null)
            {
                return NotFound();
            }

            return genero;
        }

        [HttpGet("{nombre}")] // api/generos/Marco
        public async Task<Genero?> Get(string nombre)
        {
            var repositorio = new RepositorioEnMemoria();
            var genero = await repositorio.ObtenerPorId(1);
            return genero;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Genero genero)
        {
            
            var yaExisteUnGeneroConDichoNombre = repositorio.Existe(genero.Nombre);
            
            if (yaExisteUnGeneroConDichoNombre) 
            {
                return BadRequest($" Ya existe un género con el nombre {genero.Nombre}");
            }

            repositorio.Crear(genero);
            await outputCacheStore.EvictByTagAsync(cacheTag, default);

            return Ok();

        }

        [HttpPut]
        public void Put() 
        { 

        }

        [HttpDelete]
        public void Delete() 
        {

        }
    }
}
