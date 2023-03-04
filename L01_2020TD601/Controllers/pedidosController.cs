using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2020TD601.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2020TD601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class pedidosController : ControllerBase
    {
        private readonly pedidosContext _pedidosContext;
        public pedidosController(pedidosContext pedidosContext)
        {
            _pedidosContext = pedidosContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<pedidos> lpedidos = (from e in _pedidosContext.pedidos select e).ToList();
            if (lpedidos != null)
            {
                return Ok(lpedidos);
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] pedidos pediddos)
        {
            try
            {
                _pedidosContext.pedidos.Add(pediddos);
                _pedidosContext.SaveChanges();
                return Ok(pediddos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Route("Update/{id}")]

        public IActionResult update(int id, [FromBody] pedidos pedidos)
        {
            pedidos? pedidosActual = (from e in _pedidosContext.pedidos
                                           where e.pedidoId == id
                                           select e).FirstOrDefault();

            if (pedidosActual == null)
            {
                return BadRequest();
            }

            pedidosActual.motoristaId = pedidos.motoristaId;
            pedidosActual.clienteId = pedidos.clienteId;
            pedidosActual.platoID = pedidos.platoID;
            pedidosActual.cantidad = pedidos.cantidad;
            pedidosActual.precio = pedidos.precio;

            _pedidosContext.Entry(pedidosActual).State = EntityState.Modified;
            _pedidosContext.SaveChanges();
            return Ok(pedidosActual);
        }

        //delete
        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult delete(int id)
        {
            //? para decirle que puede ser nulleable
            pedidos? pedidos = (from e in _pedidosContext.pedidos
                                where e.pedidoId == id
                                select e).FirstOrDefault();

            if (pedidos == null)
            {
                return BadRequest();
            }

            //Para apuntar a uno en particular
            _pedidosContext.pedidos.Attach(pedidos);
            _pedidosContext.pedidos.Remove(pedidos);
            _pedidosContext.SaveChanges();
            return Ok(pedidos);
        }


        //Pedidos filtratados por id pedido
        [HttpGet]
        [Route("findCliente")]

        public IActionResult BuscarPedido(int id)
        {

            List<pedidos>? lpedidos = (from e in _pedidosContext.pedidos where e.clienteId == id select e).ToList();

            if (lpedidos != null) 
            {
                return Ok(lpedidos);
            }
            return NotFound();

        }

        //pedidos filtrados por id motorista
        [HttpGet]
        [Route("findMotorista")]
        public IActionResult BuscarMotorista(int id)
        {

            List<pedidos>? lpedidos = (from e in _pedidosContext.pedidos where e.motoristaId == id select e).ToList();

            if (lpedidos != null)
            {
                return Ok(lpedidos);
            }
            return NotFound();

        }
    }
}
