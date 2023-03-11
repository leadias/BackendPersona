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
        [Route("getList/{idList:int}")]

        public IActionResult GetLists(int idList)
        {

            DataTable lists = new DataTable();

            lists = _dbcontext.getList(idList);
            var listsItem = (from row in lists.AsEnumerable()
                         select new List()
                         {
                             IdList = int.Parse(row["idList"].ToString()),
                             IdItem = int.Parse(row["IdItem"].ToString()),
                             name = row["Name"].ToString()
                         }).ToList();

            try
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = listsItem });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = listsItem });


            }
        }

        [HttpPost]
        [Route("Store")]
        public IActionResult Store([FromBody] List objeto) {

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
        public IActionResult Edit([FromBody] List objeto)
        {
            List list = _dbcontext.list.Find(objeto.IdItem);

            if (list == null)
            {
                return BadRequest("item a editar no encontrado");

            }

            try
            {
                list.name = objeto.name is null ? list.name : objeto.name;




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

            List list = _dbcontext.list.Find(idItem);

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
