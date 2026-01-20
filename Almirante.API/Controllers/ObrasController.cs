using Almirante.Domain.Entities;
using Almirante.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Almirante.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ObrasController : ControllerBase
{
    private readonly AppDbContext _context;

    public ObrasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ObraDeArte>>> GetObras()
    {
        return await _context.Obras.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<ObraDeArte>> PostObra(ObraDeArte obra)
    {
        _context.Obras.Add(obra);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetObras), new { id = obra.Id }, obra);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteObra(int id)
    {
        var obra = await _context.Obras.FindAsync(id);
        if (obra == null)
        {
            return NotFound();
        }

        _context.Obras.Remove(obra);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutObra(int id, ObraDeArte obra)
    {
        if (id != obra.Id)
        {
            return BadRequest("O ID da obra não corresponde ao ID fornecido na URL.");
        }

        _context.Entry(obra).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ObraExists(id))
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

    private bool ObraExists(int id)
    {
        return _context.Obras.Any(e => e.Id == id);
    }
}