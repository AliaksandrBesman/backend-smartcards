using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend_smartcards.Models;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace backend_smartcards.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestAnswersController : ControllerBase
    {
        private readonly SmartcardsdbContext _context;

        public TestAnswersController(SmartcardsdbContext context)
        {
            _context = context;
        }

        // GET: api/TestAnswers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestAnswer>>> GetTestAnswers()
        {
            return await _context.TestAnswers.ToListAsync();
        }

        // GET: api/TestAnswers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TestAnswer>> GetTestAnswer(int id)
        {
            var testAnswer = await _context.TestAnswers.FindAsync(id);

            if (testAnswer == null)
            {
                return NotFound();
            }

            return testAnswer;
        }

        // PUT: api/TestAnswers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTestAnswer(int id, TestAnswer testAnswer)
        {
            if (id != testAnswer.Id)
            {
                return BadRequest();
            }

            _context.Entry(testAnswer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestAnswerExists(id))
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

        // POST: api/TestAnswers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TestAnswer>> PostTestAnswer(TestAnswer testAnswer)
        {
            _context.TestAnswers.Add(testAnswer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTestAnswer", new { id = testAnswer.Id }, testAnswer);
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("[action]")]
        public async Task<ActionResult<IEnumerable<TestAnswer>>> PostGetTestResultByUserAndSubId([FromBody] JObject data)
        {

            int userId = data["userId"].ToObject<int>();
            int subjectId = data["subjectId"].ToObject<int>();
            var finded_user = await _context.TestAnswers.Where(q => q.Question.SubjectLessonId == subjectId).Where(q => q.UserId == userId).ToListAsync();


            if (finded_user == null)
            {
                return NotFound();
            }

            return finded_user;
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersCOmpletedTesByModuleId(int id)
        {
            var finded_user = await _context.Users.Where( u=> _context.TestAnswers.Where(q => q.Question.SubjectLessonId == id).Any(t => t.UserId == u.Id)).ToListAsync();

    //        context.Users
    //.Where(u => context.Test.Any(t => t.Id == u.Id))
    //.ToList();


            if (finded_user == null)
            {
                return NotFound();
            }

            return finded_user;
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<IEnumerable<TestAnswer>>> GetUsersTestResultByModuleId(int id)
        {
            var finded_user = await _context.TestAnswers.Where(t => t.Question.SubjectLessonId == id).ToListAsync();

            if (finded_user == null)
            {
                return NotFound();
            }

            return finded_user;
        }

        // DELETE: api/TestAnswers/5
        [HttpDelete("[action]/{module_id}")]
        public async Task<IActionResult> DeleteTestAnswerForModule(int module_id)
        {
            var questionsToDelete = _context.QuestionAnswers.Where(q => q.SubjectLessonId == module_id).Select(q => q.Id).ToList();
            var answersToDelete = _context.TestAnswers.Where(a => questionsToDelete.Contains(a.QuestionId));

            

            if (answersToDelete == null || answersToDelete.Count() == 0)
            {
                return NotFound();
            }

            _context.TestAnswers.RemoveRange(answersToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/TestAnswers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTestAnswer(int id)
        {
            var testAnswer = await _context.TestAnswers.FindAsync(id);
            if (testAnswer == null)
            {
                return NotFound();
            }

            _context.TestAnswers.Remove(testAnswer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TestAnswerExists(int id)
        {
            return _context.TestAnswers.Any(e => e.Id == id);
        }
    }
}
