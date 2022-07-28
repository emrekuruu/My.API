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

        public async Task<List<IEnumerable<Class>>> GetLessonPlan(int id)
        {
            var AllClasses = new List<IEnumerable<Class>>();


            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var classes = await connection.QueryAsync<string>
                ("SELECT DISTINCT class_name FROM ENROLL WHERE student_id = @Id ", new { Id = id });

            foreach (string clas in classes)
            {
                var temp = await connection.QueryAsync<Class>("SELECT * FROM classes WHERE name = @Name", new { Name = clas });
                AllClasses.Add(temp);
            }

            return AllClasses;
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

        public async Task<List<IEnumerable<Proffesor>>> GetProffesor(string className)
        {
            var AllProffesors = new List<IEnumerable<Proffesor>>();


            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var proffesors = await connection.QueryAsync<string>
                ("SELECT DISTINCT prof FROM teach WHERE class = @ClassName", new { ClassName = className});

            foreach (string name in proffesors)
            {
                var temp = await connection.QueryAsync<Proffesor>("SELECT * FROM proffesor WHERE name = @Name", new { Name = name });
                AllProffesors.Add(temp);
            }

            return AllProffesors;
        }

        public async Task<bool> DeleteProffesor(string proffesor)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var affected = await connection.ExecuteAsync("DELETE FROM teach WHERE prof = @Prof ", new { Prof = proffesor });
            var affectedSecond = await connection.ExecuteAsync("DELETE FROM proffesor WHERE name = @Prof ", new { Prof = proffesor });

            if (affected != 0 && affectedSecond != 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteStudent(int id)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var affected = await connection.ExecuteAsync("DELETE FROM enroll WHERE student_id = @Id ", new { Id = id });
            var affectedSecond = await connection.ExecuteAsync("DELETE FROM student WHERE id = @Id ", new { Id = id });

            if (affected != 0 || affectedSecond != 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteClass(string className)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var affected = await connection.ExecuteAsync("DELETE FROM enroll WHERE class = @ClassName ", new { ClassName= className });
            var affectedSecond = await connection.ExecuteAsync("DELETE FROM classes WHERE name = @Name ", new { Name = className });
            var affectedThird = await connection.ExecuteAsync("DELETE FROM Teach WHERE class = @ClassName ", new { ClassName = className });

            if (affected != 0 || affectedSecond != 0 || affectedThird != 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> CreateStudent(string name, int id, int age)
        {
           
            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var affected = await connection.ExecuteAsync("INSERT INTO student(name,id,age) VALUES(@Name,@Id,@Age)", new { Name = name, Id = id, Age = age });

            if (affected == 0)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> CreateProf(string name, int salary)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var affected = await connection.ExecuteAsync("INSERT INTO proffesor(name,salary) VALUES(@Name,@Salary)", new { Name = name, Salary = salary });

            if (affected == 0)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> CreateClass(string className)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var affected = await connection.ExecuteAsync("INSERT INTO classes(name) VALUES(@Name)", new { Name = className });

            if (affected == 0)
            {
                return false;
            }
            return true;
        }
    }
}
