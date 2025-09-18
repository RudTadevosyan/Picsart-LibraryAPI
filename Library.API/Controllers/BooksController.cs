using Library.Application.Interfaces;
using Library.Shared.CreationModels;
using Library.Shared.UpdateModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class BooksController: ControllerBase
{
    private readonly IBookService _service;

    public BooksController(IBookService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        return Ok(await _service.GetAllBooks());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetBookById(int id)
    {
        var book = await _service.GetBookById(id);
        if(book == null)
            return NotFound("Book not found");
        
        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> AddBook([FromBody] CreateBookModel bookModel)
    {
        if(!ModelState.IsValid) 
            return BadRequest(ModelState);
        
        var book = await _service.AddBook(bookModel);
        if(book == null)
            return BadRequest("Could not add book");
        
        return Ok(book);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookModel bookModel)
    {
        if(!ModelState.IsValid) 
            return BadRequest(ModelState);
        
        var updated = await _service.UpdateBook(id, bookModel);
        if(!updated)
            return BadRequest("Could not update book");

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var deleted = await _service.DeleteBook(id);
        if(!deleted)
            return BadRequest("Could not delete book");
        
        return NoContent();
    }

    [HttpPost("{bookId:int}/genre/{genreId:int}")]
    public async Task<IActionResult> AddGenreToBook(int bookId, int genreId)
    {
        var result = await _service.AddGenreToBook(bookId, genreId);
        if(!result)
            return BadRequest("Could not add genre");
        
        return NoContent();
    }

    [HttpDelete("{bookId:int}/genre/{genreId:int}")]
    public async Task<IActionResult> DeleteGenreFromBook(int bookId, int genreId)
    {
        var result = await _service.DeleteGenreFromBook(bookId, genreId);
        if(!result)
            return BadRequest("Could not delete genre");
        return NoContent();
    }

    [HttpGet("availability/{id:int}")]
    public async Task<IActionResult> CheckBookAvailability(int id)
    {
        var availability = await _service.CheckBookAvailability(id);
        return Ok(new {BookId = id, Availability = availability});
    }

    [HttpPost("details/{bookId:int}")]
    public async Task<IActionResult> AddBookDetails(int bookId, [FromBody] CreateBookDetailModel bookDetailModel)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var detail = await _service.AddBookDetail(bookId, bookDetailModel);
        if(detail == null)
            return BadRequest("Could not add book details");
        
        return Ok(detail);
    }

    [HttpPut("details")]
    public async Task<IActionResult> UpdateBookDetails([FromBody] UpdateBookDetailModel bookDetailModel)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var updated = await _service.UpdateBookDetail(bookDetailModel);
        if(!updated)
            return BadRequest("Could not update book details");
        
        return NoContent();
    }

    [HttpDelete("details/{detailId:int}")]
    public async Task<IActionResult> DeleteBookDetails(int detailId)
    {
        var deleted = await _service.DeleteBookDetail(detailId);
        if(!deleted)
            return BadRequest("Could not delete book details");
        return NoContent();
    }
}