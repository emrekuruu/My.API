using Enrolled.API.Entities;
using Enrolled.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Enrolled.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EnrolledController : ControllerBase
    {
        private readonly IEnrolledRepository _repository;
        public EnrolledController(IEnrolledRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("Get All Stuents",Name = "GetAllStudents")]
        [ProducesResponseType(typeof(List<Student>), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<List<Student>>> GetAllStudentsAsync(string ClassName)
        {
            var students = await _repository.GetAllStudentsAsync(ClassName);
            return Ok(students);
        }

        [HttpGet]
        [Route("Get Proffesors")]
        [ProducesResponseType(typeof(List<Proffesor>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Proffesor>>> GetProfessor(string ClassName)
        {
            var proffesors = await _repository.GetProffesor(ClassName);
            return Ok(proffesors);
        }

        [HttpGet]
        [Route("Get Lesson Plan")]
        [ProducesResponseType(typeof(List<Class>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Proffesor>>> GetLessonPlan(int id)
        {
            var classes = await _repository.GetLessonPlan(id);
            return Ok(classes);
        }




        [HttpPost("Enroll")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> EnrollStudent(int id, string className)
        {
            var affect = await _repository.EnrollStudent(id, className);
            return Ok(affect);
        }



        [HttpPost]
        [Route("Teach/")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> TeachClass(string proffesor, string className)
        {
            var affect = await _repository.TeachClass(proffesor, className);
            return Ok(affect);
        }



        [HttpPost]
        [Route("Create Student/")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> CreateStudent(string name,int id, int age)
        {
            var affect = await _repository.CreateStudent(name,id,age);
            return Ok(affect);
        }

        [HttpPost]
        [Route("Create Proffesor/")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> CreateProffesor(string name, int salary)
        {
            var affect = await _repository.CreateProf(name, salary);
            return Ok(affect);
        }


        [HttpPost]
        [Route("Create Class/")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> CreateProffesor(string name)
        {
            var affect = await _repository.CreateClass(name);
            return Ok(affect);
        }






        [HttpDelete("Delete Proffesor")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeleteProffesor(string proffesor)
        {
            var affect = await _repository.DeleteProffesor(proffesor);
            return Ok(affect);
        }


        [HttpDelete]
        [Route("Delete Student")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeleteStudent(int id)
        {
            var affect = await _repository.DeleteStudent(id);
            return Ok(affect);
        }


        [HttpDelete]
        [Route("Delete Class")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeleteClass(string name)
        {
            var affect = await _repository.DeleteClass(name);
            return Ok(affect);
        }




    }
}
