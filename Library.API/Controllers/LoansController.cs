using Library.Application.Interfaces;
using Library.Shared.DTOs.Loan;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LoansController: ControllerBase
{
    private readonly ILoanService _service;

    public LoansController(ILoanService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllLoans()
    {
        return Ok(await _service.GetAllLoans());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetLoanById(int id)
    {
        var loan = await _service.GetLoanById(id);
        return Ok(loan);
    }

    [HttpPost]
    public async Task<IActionResult> AddLoan([FromBody] CreateLoanModel loanModel)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var loan = await _service.AddLoan(loanModel);
        return Ok(loan);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateLoan(int id, [FromBody] UpdateLoanModel loanModel)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        await _service.UpdateLoan(id, loanModel);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteLoan(int id)
    {
        await _service.DeleteLoan(id);
        return Ok();
    }

    [HttpPut("{id:int}/return")]
    public async Task<IActionResult> ReturnLoan(int id)
    {
        await _service.ReturnLoan(id);
        return Ok();
    }
}