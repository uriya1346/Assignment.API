using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Entities;
using Assignments.Data;

namespace AssignmentsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppAssignmentsController : ControllerBase
    {
        private readonly DataContext _context;

        public AppAssignmentsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/AppAssignments
        // All tasks without archived tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppAssignment>>> GetAssignments()
        {
          if (_context.Assignments == null)
          {
              return NotFound();
          }

            return await _context.Assignments.Where(item => item.TaskArchiveId == 2).OrderBy(item => item.Start).ToListAsync();
        }
        // GET: api/AppAssignments
        [HttpGet]
        [Route("withArchive")]
        public async Task<ActionResult<IEnumerable<AppAssignment>>> GetAssignmentsWithArchive()
        {
            if (_context.Assignments == null)
            {
                return NotFound();
            }

            return await _context.Assignments.OrderBy(item => item.Start).ToListAsync();
        }

        // Refresh archive tasks
        [HttpGet]
        [Route("updateArchive")]
        public async Task<ActionResult<IEnumerable<AppAssignment>>> RefreshArchiveTasks()
        {
            if (_context.Assignments == null)
            {
                return NotFound();
            }
            var updateTask = await _context.Assignments.FromSqlRaw("SELECT * FROM [dbo].[Assignments] where DATEDIFF(DAY,[End],GETDATE()) > 7 and [End]  <> '0001-01-01'").ToListAsync();

          //  var updateTask = await _context.Assignments.Where(item => DateTime.Compare(item.End,DateTime.Now)
 

            updateTask.ForEach(task =>
            {
                task.TaskArchiveId = 1;
            });
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) { 
                    throw;
            }
            return Ok();
        }

        // GET: api/AppAssignments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppAssignment>> GetAppAssignment(int id)
        {
          if (_context.Assignments == null)
          {
              return NotFound();
          }
            var appAssignment = await _context.Assignments.FindAsync(id);

            if (appAssignment == null)
            {
                return NotFound();
            }

            return appAssignment;
        }

        // PUT: api/AppAssignments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppAssignment(int id, AppAssignment appAssignment)
        {
            if (id != appAssignment.Id)
            {
                return BadRequest();
            }

            _context.Entry(appAssignment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppAssignmentExists(id))
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

        // POST: api/AppAssignments
        [HttpPost]
        public async Task<ActionResult<AppAssignment>> PostAppAssignment(AppAssignment appAssignment)
        {
          if (_context.Assignments == null)
          {
              return Problem("Entity set 'DataContext.Assignments'  is null.");
          }
            _context.Assignments.Add(appAssignment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppAssignment", new { id = appAssignment.Id }, appAssignment);
        }

        // DELETE: api/AppAssignments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppAssignment(int id)
        {
            if (_context.Assignments == null)
            {
                return NotFound();
            }
            var appAssignment = await _context.Assignments.FindAsync(id);
            if (appAssignment == null)
            {
                return NotFound();
            }

            _context.Assignments.Remove(appAssignment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppAssignmentExists(int id)
        {
            return (_context.Assignments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
