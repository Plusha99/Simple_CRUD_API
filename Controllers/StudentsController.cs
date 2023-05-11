using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Simple_CRUD_API.Data;
using Simple_CRUD_API.Models;

namespace Simple_CRUD_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly DataContext _context;
        public StudentsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("Get all students")]
        public async Task<ActionResult> GetAllStudents()
        {
            var students = await _context.Students.ToListAsync();
            return Ok(students);
        }

        [HttpPost("Cteate student")]
        public async Task<ActionResult> CreateStudent(Students student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return Created($"/GetByStudentId/{student.StudentId}", student);
        }

        [HttpGet("GetByStudentId/{studentId}")]
        public async Task<ActionResult> GetByStudentId(int studentId)
        {
            var student = await _context.Students.FindAsync(studentId);
            return Ok(student);
        }

        [HttpPut("EditStudent")]
        public async Task<ActionResult> EditStudent(Students students, int studentId)
        {
            var dbStudent = await _context.Students.FindAsync(studentId);
            if(dbStudent == null)
                return NotFound();

            _context.Students.Update(students);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("DeleteStudent/{studentId}")]
        public async Task<ActionResult> DeleteStudent(int studentId)
        {
            var dbStudent = await _context.Students.FindAsync(studentId);
            if(dbStudent == null)
                return NotFound();
            
            _context.Students.Remove(dbStudent);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}