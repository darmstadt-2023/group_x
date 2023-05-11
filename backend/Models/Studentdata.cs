using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace backend
{
    public class StudentData
    {
        public string name { get; set; }
        public int ects { get; set; }
        public int grade { get; set; }

        internal Database Db { get; set; }

        public StudentData()
        {
        }

        internal StudentData(Database db)
        {
            Db = db;
        }

        public async Task<List<StudentData>> GetAllAsync()
        {
            Console.WriteLine("test1");
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"select name,ects, grade
            from course inner join grade on course.idcourse=grade.idcourse
            inner join student on student.idstudent=grade.idstudent";
            return await ReturnAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task<List<StudentData>> FindByUsername(String username)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"select name,ects, grade
            from course inner join grade on course.idcourse=grade.idcourse
            inner join student on student.idstudent=grade.idstudent
            where username=@username;";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@username",
                DbType = DbType.String,
                Value = username,
            });
            return await ReturnAllAsync(await cmd.ExecuteReaderAsync());
        }

        private async Task<List<StudentData>> ReturnAllAsync(DbDataReader reader)
        {
            var posts = new List<StudentData>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new StudentData(Db)
                    {
                        name = reader.GetString(0),
                        ects = reader.GetInt32(1),
                        grade = reader.GetInt32(2)
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }

    }
}