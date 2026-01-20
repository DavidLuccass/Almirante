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
}