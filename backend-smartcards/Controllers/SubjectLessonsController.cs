using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend_smartcards.Models;

namespace backend_smartcards.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectLessonsController : ControllerBase
    {
        private readonly SmartcardsdbContext _context;

        public SubjectLessonsController(SmartcardsdbContext context)
        {
            _context = context;
        }

        // GET: api/SubjectLessons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectLesson>>> GetSubjectLessons()
        {
            return await _context.SubjectLessons.ToListAsync();
        }

        // GET: api/SubjectLessons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubjectLesson>> GetSubjectLesson(int id)
        {
            var subjectLesson = await _context.SubjectLessons.FindAsync(id);

            if (subjectLesson == null)
            {
                return NotFound();
            }

            return subjectLesson;
        }

        // GET: api/SubjectLessons/5
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<IEnumerable<SubjectLesson>>> GetGrantedSubjectLessonByUserId(int id)
        {
            List<SubjectLesson> result;
            var user = await _context.Users.FindAsync(id);
            if (  user.RoleId == 1)
            {
                result = await _context.SubjectLessons.ToListAsync();
            }
            else
            {
                result = await _context.SubjectLessons.Where(sl => sl.GrantingAccesses.Any(ga => ga.UserId == id)).ToListAsync();
            }


            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        // GET: api/SubjectLessons/5
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<IEnumerable<SubjectLesson>>> GetGrantedSubjectLessonByCreatedById(int id)
        {

            var    result = await _context.SubjectLessons.Where(sl => sl.CreatedById == id).ToListAsync();

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        // PUT: api/SubjectLessons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubjectLesson(int id, SubjectLesson subjectLesson)
        {
            if (id != subjectLesson.Id)
            {
                return BadRequest();
            }

            _context.Entry(subjectLesson).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectLessonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SubjectLessons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SubjectLesson>> PostSubjectLesson(SubjectLesson subjectLesson)
        {
            _context.SubjectLessons.Add(subjectLesson);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubjectLesson", new { id = subjectLesson.Id }, subjectLesson);
        }

        // DELETE: api/SubjectLessons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubjectLesson(int id)
        {
            var subjectLesson = await _context.SubjectLessons.FindAsync(id);
            if (subjectLesson == null)
            {
                return NotFound();
            }

            _context.SubjectLessons.Remove(subjectLesson);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubjectLessonExists(int id)
        {
            return _context.SubjectLessons.Any(e => e.Id == id);
        }
    }
}
