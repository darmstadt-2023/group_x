using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class StudentDataController : ControllerBase
    {
        public StudentDataController(Database db)
        {
            Db = db;
        }

        // GET api/Student
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            await Db.Connection.OpenAsync();
            var query = new StudentData(Db);
            var result = await query.GetAllAsync();
            return new OkObjectResult(result);
        }

        // GET api/Student/5
        [HttpGet("{username}")]
        public async Task<IActionResult> GetOne(String username)
        {
            Console.WriteLine("test username="+username);
            await Db.Connection.OpenAsync();
            var query = new StudentData(Db);
            var result = await query.FindByUsername(username);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }


        public Database Db { get; }
    }
}