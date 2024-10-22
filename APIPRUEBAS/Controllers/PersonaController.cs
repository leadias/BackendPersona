using Microsoft.AspNetCore.Mvc;
using APIPRUEBAS.Models;
using Microsoft.AspNetCore.Cors;
using System.Data;



namespace APIPRUEBAS.Controllers
{

    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        public readonly DBAPIContext _dbcontext;


        public PersonaController(DBAPIContext _context) {
            _dbcontext = _context;
        }


        [HttpGet]
        [Route("getList")]

        public IActionResult GetLists()
        {
            try
            {
                DataTable lists = new DataTable();

                lists = _dbcontext.getList();
                var listsItem = (from row in lists.AsEnumerable()
                                 select new Persona()
                                 {
                                     Cedula = row["cedula"].ToString(),
                                     Nombre = row["nombre"].ToString(),
                                     Apellido = row["apellido"].ToString(),
                                     Celular = row["celular"].ToString()

                                 }).ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = listsItem });
               

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = "error obteniendo lista" });


            }
        }

        [HttpPost]
        [Route("Store")]
        public IActionResult Store([FromBody] Persona objeto) {

            try
            {
                _dbcontext.persona.Add(objeto);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "persona creada" });
            }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }

        }


        [HttpPut]
        [Route("Edit")]
        public IActionResult Edit([FromBody] Persona objeto)
        {
            Persona persona = _dbcontext.persona.Find(objeto.Cedula);

            if (persona == null)
            {
                return BadRequest("Persona a editar no encontrada");

            }

            try
            {
                persona.Nombre = objeto.Nombre is null ? persona.Nombre : objeto.Nombre;
                persona.Apellido = objeto.Apellido is null ? persona.Apellido : objeto.Apellido;
                persona.Celular = objeto.Celular is null ? persona.Celular : objeto.Celular;




                _dbcontext.persona.Update(persona);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status201Created, new { mensaje = "Persona editada" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }




        }

        [HttpDelete]
        [Route("Delete/{idItem}")]
        public IActionResult Delete(string idItem) {

            Persona list = _dbcontext.persona.Find(idItem);

            if (list == null)
            {
                return BadRequest("persona no encontrada");

            }

            try
            {
                _dbcontext.persona.Remove(list);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "persona eliminada" });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }


        }



    }
}
