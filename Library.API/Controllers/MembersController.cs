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
        return Ok(member);
    }

    [HttpPost]
    public async Task<IActionResult> AddMember([FromBody] CreateMemberModel memberModel)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var member = await _service.AddMember(memberModel);
        return Ok(member);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateMember(int id, [FromBody] UpdateMemberModel memberModel)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        await _service.UpdateMember(id, memberModel);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteMember(int id)
    {
        await _service.DeleteMember(id);
        return Ok();
    }

    [HttpGet("active_loans/{id:int}")]
    public async Task<IActionResult> GetActiveLoans(int id)
    {
        var activeLoans = await _service.HasActiveLoans(id);
        return Ok(new {MemberId = id, ActiveLoans = activeLoans});
    }
}