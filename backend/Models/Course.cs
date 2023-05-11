using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace backend
{
    public class Course
    {
        public int idcourse { get; set; }
        public string name { get; set; }
        public int ects { get; set; }

        internal Database Db { get; set; }

        public Course()
        {
        }

        internal Course(Database db)
        {
            Db = db;
        }

        public async Task<List<Course>> GetAllAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM  course ;";
            return await ReturnAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task<Course> FindOneAsync(int idcourse)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM  course  WHERE  idcourse  = @idcourse";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@idcourse",
                DbType = DbType.Int32,
                Value = idcourse,
            });
            var result = await ReturnAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }


        public async Task<int> InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO  course  ( name ,  ects  ) VALUES (@name, @ects);";
            BindParams(cmd);
            try
            {
                await cmd.ExecuteNonQueryAsync();
                int idcourse = (int)cmd.LastInsertedId;
                return idcourse;
            }
            catch (System.Exception)
            {
                return 0;
            }
        }

        public async Task<int> UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE  course  SET  name  = @name,  ects  = @ects WHERE  idcourse  = @idcourse;";
            BindParams(cmd);
            BindId(cmd);
            int affectedRows=0;
            try
            {
                affectedRows=await cmd.ExecuteNonQueryAsync();
            }
            catch (System.Exception)
            {
                
                throw;
            }
            return affectedRows;
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM  course  WHERE  idcourse  = @idcourse;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private async Task<List<Course>> ReturnAllAsync(DbDataReader reader)
        {
            var posts = new List<Course>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Course(Db)
                    {
                        idcourse = reader.GetInt32(0),
                        name = reader.GetString(1),
                        ects = reader.GetInt32(2)
                    };

                    posts.Add(post);
                }
            }
            return posts;
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@idcourse",
                DbType = DbType.Int32,
                Value = idcourse,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@name",
                DbType = DbType.String,
                Value = name,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@ects",
                DbType = DbType.Int32,
                Value = ects,
            });
           
        }
    }
}