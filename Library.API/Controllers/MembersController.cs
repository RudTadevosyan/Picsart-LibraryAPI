using Library.Application.Interfaces;
using Library.Shared.CreationModels;
using Library.Shared.UpdateModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController: ControllerBase
{
    private readonly IMemberService _service;

    public MembersController(IMemberService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMembers()
    {
        return Ok(await _service.GetAllMembers());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetMemberById(int id)
    {
        var member = await _service.GetMemberById(id);
        if(member == null)
            return NotFound("Member not found");
        return Ok(member);
    }

    [HttpPost]
    public async Task<IActionResult> AddMember([FromBody] CreateMemberModel memberModel)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var member = await _service.AddMember(memberModel);
        if(member == null)
            return BadRequest("Could not add member");
        
        return Ok(member);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateMember(int id, [FromBody] UpdateMemberModel memberModel)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var updated = await _service.UpdateMember(id, memberModel);
        if(!updated)
            return BadRequest("Could not update member");
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteMember(int id)
    {
        try
        {
            var deleted = await _service.DeleteMember(id);
            if(!deleted)
                return BadRequest("Could not delete member");
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("active_loans/{id:int}")]
    public async Task<IActionResult> GetActiveLoans(int id)
    {
        var activeLoans = await _service.HasActiveLoans(id);
        if(activeLoans == null) 
            return BadRequest("Member not found");
        
        return Ok(new {MemberId = id, ActiveLoans = activeLoans});
    }
}