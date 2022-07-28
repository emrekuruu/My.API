using Enrolled.API.Entities;

namespace Enrolled.API.Repositories
{
    public interface IEnrolledRepository
    {
        Task<List<IEnumerable<Student>>> GetAllStudentsAsync(string ClassName);
        Task<bool> EnrollStudent(int id,string ClassName);

        Task<bool> TeachClass(string proffesor, string className);

    }
}
