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
        return Ok(author);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAuthor(int id, [FromBody] UpdateAuthorModel authorModel)
    {
        if(!ModelState.IsValid) 
            return BadRequest(ModelState);
        
        await _service.UpdateAuthor(id, authorModel);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        await _service.DeleteAuthor(id);
        return Ok();
    } 
    
}