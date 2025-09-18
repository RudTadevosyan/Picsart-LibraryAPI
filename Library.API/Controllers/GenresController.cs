using Library.Application.Interfaces;
using Library.Shared.CreationModels;
using Library.Shared.UpdateModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenresController: ControllerBase
{
    private readonly IGenreService _service;

    public GenresController(IGenreService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllGenres()
    {
        return Ok(await _service.GetAllGenres());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetGenreById(int id)
    {
        var genre = await _service.GetGenreById(id);
        if(genre == null)
            return NotFound("Genre not found");
        return Ok(genre);
    }

    [HttpPost]
    public async Task<IActionResult> AddGenre([FromBody] CreateGenreModel genreModel)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var genre = await _service.AddGenre(genreModel);
        if(genre == null)
            return NotFound("Genre not found");
        return Ok(genre);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateGenre(int id, [FromBody] UpdateGenreModel genreModel)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var updated = await _service.UpdateGenre(id, genreModel);
        if(!updated)
            return BadRequest("Could not update genre");
        
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        var deleted = await _service.DeleteGenre(id);
        if(!deleted)
            return BadRequest("Could not delete genre");
        return NoContent();
    }
    
}