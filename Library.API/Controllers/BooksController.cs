using Library.Application.Interfaces;
using Library.Shared.DTOs.Book;
using Library.Shared.DTOs.BookDetail;
using Library.Shared.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
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

    [HttpGet("Specification")]
    public async Task<IActionResult> GetAllBooksBySpec([FromQuery] BookFilterDto filter)
    {
        var validator = new BookFilterDtoValidator();
        var validationResult = await validator.ValidateAsync(filter);
        
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        
        var books = await _service.GetAllBooksBySpec(filter);
        return Ok(books);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetBookById(int id)
    {
        var book = await _service.GetBookById(id);
        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> AddBook([FromBody] CreateBookModel bookModel)
    {
        if(!ModelState.IsValid) 
            return BadRequest(ModelState);
        
        var book = await _service.AddBook(bookModel);
        return Ok(book);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookModel bookModel)
    {
        if(!ModelState.IsValid) 
            return BadRequest(ModelState);
        
        await _service.UpdateBook(id, bookModel);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        await _service.DeleteBook(id);
        return Ok();
    }

    [HttpPost("{bookId:int}/genre/{genreId:int}")]
    public async Task<IActionResult> AddGenreToBook(int bookId, int genreId)
    {
        await _service.AddGenreToBook(bookId, genreId);
        return Ok();
    }

    [HttpDelete("{bookId:int}/genre/{genreId:int}")]
    public async Task<IActionResult> DeleteGenreFromBook(int bookId, int genreId)
    {
        await _service.DeleteGenreFromBook(bookId, genreId);
        return Ok();
    }

    [HttpGet("availability/{id:int}")]
    public async Task<IActionResult> CheckBookAvailability(int id)
    {
        var availability = await _service.CheckBookAvailability(id);
        return Ok(new {BookId = id, Availability = availability});
    }

    [HttpPost("{bookId:int}/details")]
    public async Task<IActionResult> AddBookDetails(int bookId, [FromBody] CreateBookDetailModel bookDetailModel)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var detail = await _service.AddBookDetail(bookId, bookDetailModel);
        return Ok(detail);
    }

    [HttpPut("{bookId:int}/details")]
    public async Task<IActionResult> UpdateBookDetails([FromBody] UpdateBookDetailModel bookDetailModel)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        await _service.UpdateBookDetail(bookDetailModel);
        return Ok();
    }

    [HttpDelete("{bookId:int}/details")]
    public async Task<IActionResult> DeleteBookDetails(int detailId)
    {
        await _service.DeleteBookDetail(detailId);
        return Ok();
    }
}