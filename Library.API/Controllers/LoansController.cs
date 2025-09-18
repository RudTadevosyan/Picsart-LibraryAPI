using Library.Application.Interfaces;
using Library.Shared.CreationModels;
using Library.Shared.UpdateModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
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
        if (loan == null)
            return NotFound("Loan not found");
        
        return Ok(loan);
    }

    [HttpPost]
    public async Task<IActionResult> AddLoan([FromBody] CreateLoanModel loanModel)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var loan = await _service.AddLoan(loanModel);
            if(loan == null)
                return BadRequest("Could not add loan");
            
            return Ok(loan);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateLoan(int id, [FromBody] UpdateLoanModel loanModel)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var updated = await _service.UpdateLoan(id, loanModel);
            if(!updated)
                return BadRequest("Loan not updated");
        
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteLoan(int id)
    {
        var deleted = await _service.DeleteLoan(id);
        if(!deleted)
            return BadRequest("Loan not deleted");
        return NoContent();
    }

    [HttpPut("return_loan/{id:int}")]
    public async Task<IActionResult> ReturnLoan(int id)
    {
        var returned = await _service.ReturnLoan(id);
        if(!returned)
            return BadRequest("Loan not returned");
        return NoContent();
    }
}