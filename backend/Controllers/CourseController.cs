using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("[controller]")]
    public class CourseController : ControllerBase
    {
        public CourseController(Database db)
        {
            Db = db;
        }

        // GET api/Course
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            await Db.Connection.OpenAsync();
            var query = new Course(Db);
            var result = await query.GetAllAsync();
            return new OkObjectResult(result);
        }

        // GET api/Course/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            Console.WriteLine("test id="+id);
            await Db.Connection.OpenAsync();
            var query = new Course(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        // POST api/Course
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Course body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            int result=await body.InsertAsync();
            return new OkObjectResult(result);
        }

        // PUT api/Course/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody]Course body)
        {
            await Db.Connection.OpenAsync();
            var query = new Course(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            result.name = body.name;
            result.ects = body.ects;
            int res=await result.UpdateAsync();
            return new OkObjectResult(res);
        }

        // DELETE api/Course/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new Course(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            await result.DeleteAsync();
            return new OkObjectResult(result);
        }

        public Database Db { get; }
    }
}