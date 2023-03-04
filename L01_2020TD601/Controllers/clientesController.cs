using L01_2020TD601.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2020TD601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class clientesController : ControllerBase
    {
        private readonly clientesContext _clientesContext;
        public clientesController(clientesContext clientesContext)
        {
            _clientesContext = clientesContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<clientes> lclientes = (from e in _clientesContext.clientes select e).ToList();
            if (lclientes != null)
            {
                return Ok(lclientes);
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] clientes clientes)
        {
            try
            {
                _clientesContext.clientes.Add(clientes);
                _clientesContext.SaveChanges();
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Route("Update/{id}")]

        public IActionResult update(int id, [FromBody] clientes cliente)
        {
            clientes? clienteActual = (from e in _clientesContext.clientes
                                      where e.clienteId == id
                                      select e).FirstOrDefault();

            if (clienteActual == null)
            {
                return BadRequest();
            }

            clienteActual.nombreCliente = cliente.nombreCliente;
            clienteActual.direccion = cliente.direccion;

            _clientesContext.Entry(clienteActual).State = EntityState.Modified;
            _clientesContext.SaveChanges();
            return Ok(clienteActual);
        }

        //delete
        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult delete(int id)
        {
            //? para decirle que puede ser nulleable
            clientes? clientes = (from e in _clientesContext.clientes
                                  where e.clienteId == id
                                  select e).FirstOrDefault();

            if (clientes == null)
            {
                return BadRequest();
            }

            //Para apuntar a uno en particular
            _clientesContext.clientes.Attach(clientes);
            _clientesContext.clientes.Remove(clientes);
            _clientesContext.SaveChanges();
            return Ok(clientes);
        }


    

        //pedidos filtrados por direccion
        [HttpGet]
        [Route("findDireccion")]
        public IActionResult BuscarMotorista(string filtro)
        {

            List<clientes>? lpedidos = (from e in _clientesContext.clientes where e.direccion.Contains(filtro) select e).ToList();

            if (lpedidos != null)
            {
                return Ok(lpedidos);
            }
            return NotFound();

        }

    }
}
