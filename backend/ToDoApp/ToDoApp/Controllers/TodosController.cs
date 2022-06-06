using Microsoft.AspNetCore.Mvc;
using ToDoApp.Models.Payloads.Requests;
using ToDoApp.Models.Payloads.Responses;
using ToDoApp.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoApp.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodosService _todosService;

        public TodosController(ITodosService todosService)
        {
            this._todosService = todosService ?? throw new ArgumentNullException(nameof(todosService));
        }

        // GET: api/<TodosController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await this._todosService.getAllAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(
                    new Response<Exception>()
                    {
                        Success = false,
                        Message = ex.Message,
                        //Errors = (IEnumerable<string>)ex.Data,
                    }
                );
            }
        }

        // GET api/<TodosController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                return Ok(await this._todosService.getByIdAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(
                    new Response<Exception>()
                    {
                        Success = false,
                        Message = ex.Message,
                        //Errors = (IEnumerable<string>)ex.Data,
                    }
                );
            }
        }

        // GET api/<TodosController>/GetByName/name=learn
        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                return Ok(await this._todosService.getByNameAsync(name));
            }
            catch (Exception ex)
            {
                return BadRequest(
                    new Response<Exception>()
                    {
                        Success = false,
                        Message = ex.Message,
                    }
                );
            }
        }

        // POST api/<TodosController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TodoCreateRequest todo)
        {
            try
            {
                var result = await this._todosService.createAsync(todo);
                if (result.Success)
                {
                    return StatusCode(StatusCodes.Status201Created, result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(
                    new Response<Exception>()
                    {
                        Success = false,
                        Message = ex.Message,
                    }
                );
            }
        }

        // PUT api/<TodosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TodosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
