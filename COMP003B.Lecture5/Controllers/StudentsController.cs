using COMP003B.Lecture5.Models;
using Microsoft.AspNetCore.Mvc;

namespace COMP003B.Lecture5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : Controller
    {
        // in-memory list
        private List<Student> _students = new List<Student>();

        // default constructor to pre-fill
        public StudentsController()
        {
            _students.Add(new Student { Id = 1, FirstName = "Student", MiddleName = "Number", LastName = "One" });
            _students.Add(new Student { Id = 2, FirstName = "Student", MiddleName = "Number", LastName = "Two" });
            _students.Add(new Student { Id = 3, FirstName = "Student", MiddleName = "Number", LastName = "Three" });
            _students.Add(new Student { Id = 4, FirstName = "Student", MiddleName = "Number", LastName = "Four" });
            _students.Add(new Student { Id = 5, FirstName = "Student", MiddleName = "Number", LastName = "Five" });

        }

        // Read
        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetAllStudents()
        {
            return _students;
        }

        // Read
        [HttpGet("{id}")]
        public ActionResult<Student> GetStudentById(int id)
        {
            var student = _students.Find(s => s.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // Create
        [HttpPost]
        public ActionResult<Student> CreateStudent(Student student)
        {
            // overwrite student id by max + 1
            student.Id = _students.Max(s => s.Id) + 1;
            _students.Add(student);

            return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
        }

        // Update
        [HttpPut]
        public IActionResult UpdateStudent(int id, Student updatedStudent)
        {
            var student = _students.Find(s => s.Id == id);

            if (student == null)
            {
                return BadRequest();
            }

            student.FirstName = updatedStudent.FirstName;
            student.MiddleName = updatedStudent.MiddleName;
            student.FirstName = updatedStudent.LastName;

            return NoContent();
        }

        // Delete
        [HttpDelete]
        public IActionResult DeleteStudent(int id)
        {
            var student = _students.Find(s => s.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            _students.Remove(student);

            return NoContent();
        }
    }
}
