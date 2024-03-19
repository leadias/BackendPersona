using Microsoft.AspNetCore.Mvc;
using APIPRUEBAS.Models;
using Microsoft.AspNetCore.Cors;
using System.Data;



namespace APIPRUEBAS.Controllers
{

    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListController : ControllerBase
    {
        public readonly DBAPIContext _dbcontext;


        public TodoListController(DBAPIContext _context) {
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
                                 select new List()
                                 {
                                     IdCargo = int.Parse(row["id_cargo"].ToString()),
                                     name = row["nombre"].ToString()
                                 }).ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = listsItem });


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = "error obteniendo lista" });


            }
        }


        [HttpGet]
        [Route("getListEmployee")]

        public IActionResult GetListEmployees()
        {
            try
            {
                DataTable lists = new DataTable();

                lists = _dbcontext.getListEmployees();
                var listsItem = (from row in lists.AsEnumerable()
                                 select new Employee()
                                 {
                                     Id = int.Parse(row["ID"].ToString()),
                                     identification = int.Parse(row["identification"].ToString()),
                                     date = DateTime.Parse(row["date"].ToString()),
                                     name = row["Name"].ToString(),
                                     position = int.Parse(row["position"].ToString()),
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
        public IActionResult Store([FromBody] Employee objeto) {

            try
            {
                _dbcontext.list.Add(objeto);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "item creado" });
            }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }

        }


        [HttpPut]
        [Route("Edit")]
        public IActionResult Edit([FromBody] Employee objeto)
        {
            Employee list = _dbcontext.list.Find(objeto.Id);

            if (list == null)
            {
                return BadRequest("item a editar no encontrado");

            }

            try
            {
                list.name = objeto.name is null ? list.name : objeto.name;
                list.identification = objeto.identification is 0 ? list.identification : objeto.identification;
                list.date = objeto.date;
                list.position = objeto.position is 0 ? list.identification : objeto.identification;

                _dbcontext.list.Update(list);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status201Created, new { mensaje = "item editado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }




        }

        [HttpDelete]
        [Route("Delete/{idItem:int}")]
        public IActionResult Delete(int idItem) {

            Employee list = _dbcontext.list.Find(idItem);

            if (list == null)
            {
                return BadRequest("item no encontrado");

            }

            try
            {
                _dbcontext.list.Remove(list);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "item borrado" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }


        }



    }
}
