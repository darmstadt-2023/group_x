using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace backend
{
    public class Student
    {
        public int idstudent { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string ? address { get; set; }

        internal Database Db { get; set; }

        public Student()
        {
        }

        internal Student(Database db)
        {
            Db = db;
        }

        public async Task<List<Student>> GetAllAsync()
        {
            Console.WriteLine("test1");
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM  Student ;";
            return await ReturnAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task<Student> FindOneAsync(int idstudent)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM  Student  WHERE  idstudent  = @idstudent";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@idstudent",
                DbType = DbType.Int32,
                Value = idstudent,
            });
            var result = await ReturnAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }


        public async Task<int> InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO  student  ( fname ,  lname ,  username,password, address ) VALUES (@fname, @lname, @username,@password,@address);";
            BindParams(cmd);
            try
            {
                await cmd.ExecuteNonQueryAsync();
                int id_user = (int)cmd.LastInsertedId;
                return id_user;
            }
            catch (System.Exception)
            {
                return 0;
            }
        }

        public async Task<int> UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE  student  SET  fname  = @fname,  lname  = @lname,  username  = @username, password=@password, address=@address WHERE  idstudent  = @idstudent;";
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
            cmd.CommandText = @"DELETE FROM  student  WHERE  idstudent  = @idstudent;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private async Task<List<Student>> ReturnAllAsync(DbDataReader reader)
        {
            var posts = new List<Student>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Student(Db)
                    {
                        idstudent = reader.GetInt32(0),
                        fname = reader.GetString(1),
                        lname = reader.GetString(2),
                        username = reader.GetString(3),
                        password = reader.GetString(4)
                    };
                    if (!reader.IsDBNull(5))
                        post.address = reader.GetString(5);

                    posts.Add(post);
                }
            }
            return posts;
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@idstudent",
                DbType = DbType.Int32,
                Value = idstudent,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@fname",
                DbType = DbType.String,
                Value = fname,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@lname",
                DbType = DbType.String,
                Value = lname,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@username",
                DbType = DbType.String,
                Value = username,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@password",
                DbType = DbType.String,
                Value = password,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@address",
                DbType = DbType.String,
                Value = address,
            });
        }
    }
}