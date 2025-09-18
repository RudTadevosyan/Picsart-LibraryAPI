using Library.Application.Interfaces;
using Library.Shared.CreationModels;
using Library.Shared.UpdateModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthorsController : ControllerBase
{
    private readonly IAuthorService _service;

    public AuthorsController(IAuthorService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAuthors()
    {
        var authors = await _service.GetAllAuthors();
        return Ok(authors);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAuthorById(int id)
    {
        var author = await _service.GetAuthorById(id);
        if(author == null) 
            return NotFound("Author not found");
        
        return Ok(author);
    }

    [HttpGet("search/{name}")]
    public async Task<IActionResult> GetAuthorByName(string name)
    {
        var authors = await _service.GetAllAuthorsByName(name);
        return Ok(authors);
    }

    [HttpPost]
    public async Task<IActionResult> AddAuthor([FromBody] CreateAuthorModel authorModel)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        
        var author = await _service.AddAuthor(authorModel);
        
        if(author == null) 
            return BadRequest("Could not create author");
        
        return Ok(author);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAuthor(int id, [FromBody] UpdateAuthorModel authorModel)
    {
        if(!ModelState.IsValid) 
            return BadRequest(ModelState);
        
        
        var updated = await _service.UpdateAuthor(id, authorModel);
        if(!updated) 
            return NotFound("Author not found");
        
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        try
        {
            var deleted = await _service.DeleteAuthor(id);
            if(!deleted)
                return NotFound("Author not found");
            return NoContent();
        }
        catch(InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    } 
    
}