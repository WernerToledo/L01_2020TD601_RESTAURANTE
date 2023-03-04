using L01_2020TD601.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2020TD601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class platosController : ControllerBase
    {
        private readonly platosContext _platosContext;
        public platosController(platosContext platosContext)
        {
            _platosContext = platosContext;
        }

        //CRUD
        //MOSTRAR TODO
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<platos> lplatos = (from e in _platosContext.platos select e).ToList();
            if (lplatos != null)
            {
                return Ok(lplatos);
            }
            return BadRequest();
        }

        //INSERT A LA TABLA
        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] platos platos)
        {
            try
            {
                _platosContext.platos.Add(platos);
                _platosContext.SaveChanges();
                return Ok(platos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        //ACTUALIZAR
        [HttpPut]
        [Route("Update/{id}")]

        public IActionResult update(int id, [FromBody] platos platos)
        {
            platos? platosActual = (from e in _platosContext.platos
                                      where e.platoId == id
                                      select e).FirstOrDefault();

            if (platosActual == null)
            {
                return BadRequest();
            }
            platosActual.nombrePlato = platos.nombrePlato;
            platosActual.precio = platos.precio;


            _platosContext.Entry(platosActual).State = EntityState.Modified;
            _platosContext.SaveChanges();
            return Ok(platosActual);
        }

        //delete
        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult delete(int id)
        {
            //? para decirle que puede ser nulleable
            platos? platos = (from e in _platosContext.platos
                                where e.platoId == id
                                select e).FirstOrDefault();

            if (platos == null)
            {
                return BadRequest();
            }

            //Para apuntar a uno en particular
            _platosContext.platos.Attach(platos);
            _platosContext.platos.Remove(platos);
            _platosContext.SaveChanges();
            return Ok(platos);
        }

        //CRUD

        //Pedidos filtratados por nombre de plato
        [HttpGet]
        [Route("findNombre")]

        public IActionResult BuscarPedido(String nombre)
        {

            List<platos>? lplatos = (from e in _platosContext.platos where e.nombrePlato.Contains(nombre) select e).ToList();

            if (lplatos != null)
            {
                return Ok(lplatos);
            }
            return NotFound();

        }
    }
}
