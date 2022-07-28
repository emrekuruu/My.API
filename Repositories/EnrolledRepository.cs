using Dapper;
using Enrolled.API.Entities;
using Npgsql;

namespace Enrolled.API.Repositories
{
    public class EnrolledRepository : IEnrolledRepository
    {
        private readonly IConfiguration _configuration;

        public EnrolledRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<List<IEnumerable<Student>>> GetAllStudentsAsync(string className)
        {

            var AllStudents = new List<IEnumerable<Student>>();


            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var students = await connection.QueryAsync<int>
                ("SELECT DISTINCT student_id FROM ENROLL WHERE class_name = @ClassName ", new {ClassName = className});

            foreach (int id in students)
            {
                var temp =  await connection.QueryAsync<Student>("SELECT * FROM student WHERE id = @ID",new{Id = id} );
                AllStudents.Add(temp);
            }

            return AllStudents;
        }

        public async Task<bool> EnrollStudent(int id, string className)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
           var affected =  await connection.ExecuteAsync("INSERT INTO ENROLL(student_id, class_name) VALUES(@Id,@className)", new { ClassName = className, Id = id});

           if (affected == 0)
           {
               return false;
           }
           return true;

        }

        public async Task<bool> TeachClass(string proffesor, string className)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var affected = await connection.ExecuteAsync("INSERT INTO teach(prof, class) VALUES(@Prof,@ClassName)", new { ClassName = className, Prof = proffesor });

            if (affected == 0)
            {
                return false;
            }
            return true;
        }
    }
}
