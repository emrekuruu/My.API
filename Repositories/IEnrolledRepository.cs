using Enrolled.API.Entities;

namespace Enrolled.API.Repositories
{
    public interface IEnrolledRepository
    {
        Task<List<IEnumerable<Student>>> GetAllStudentsAsync(string ClassName);

        Task<List<IEnumerable<Proffesor>>> GetProffesor(string proffesor);

        Task<List<IEnumerable<Class>>> GetLessonPlan(int id);
 

        Task<bool> EnrollStudent(int id,string ClassName);

        Task<bool> TeachClass(string proffesor, string className);

        Task<bool> DeleteProffesor(string className);
        Task<bool> DeleteStudent(int id);

        Task<bool> DeleteClass(string className);

        Task<bool> CreateStudent(string name, int id, int age);
        Task<bool> CreateProf(string name, int salary);

        Task<bool> CreateClass(string className);

    }
}
