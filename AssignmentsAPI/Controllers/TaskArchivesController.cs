using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Entities;
using Assignments.Data;

namespace AssignmentsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskArchivesController : ControllerBase
    {
        private readonly DataContext _context;

        public TaskArchivesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/TaskArchives
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskArchive>>> GetTaskArchives()
        {
          if (_context.TaskArchives == null)
          {
              return NotFound();
          }
            return await _context.TaskArchives.ToListAsync();
        }

        // GET: api/TaskArchives/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskArchive>> GetTaskArchive(int id)
        {
          if (_context.TaskArchives == null)
          {
              return NotFound();
          }
            var taskArchive = await _context.TaskArchives.FindAsync(id);

            if (taskArchive == null)
            {
                return NotFound();
            }

            return taskArchive;
        }

        // PUT: api/TaskArchives/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskArchive(int id, TaskArchive taskArchive)
        {
            if (id != taskArchive.Id)
            {
                return BadRequest();
            }

            _context.Entry(taskArchive).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskArchiveExists(id))
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

        // POST: api/TaskArchives
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaskArchive>> PostTaskArchive(TaskArchive taskArchive)
        {
          if (_context.TaskArchives == null)
          {
              return Problem("Entity set 'DataContext.TaskArchives'  is null.");
          }
            _context.TaskArchives.Add(taskArchive);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaskArchive", new { id = taskArchive.Id }, taskArchive);
        }

        // DELETE: api/TaskArchives/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskArchive(int id)
        {
            if (_context.TaskArchives == null)
            {
                return NotFound();
            }
            var taskArchive = await _context.TaskArchives.FindAsync(id);
            if (taskArchive == null)
            {
                return NotFound();
            }

            _context.TaskArchives.Remove(taskArchive);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskArchiveExists(int id)
        {
            return (_context.TaskArchives?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
