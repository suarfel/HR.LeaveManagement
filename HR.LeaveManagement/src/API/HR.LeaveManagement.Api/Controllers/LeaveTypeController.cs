using Microsoft.AspNetCore.Mvc;
using MediatR;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Queries;
using HR.LeaveManagement.Application.DTOs.LeaveType;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;

namespace HR.LeaveManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeaveTypeController : ControllerBase
{
    private readonly IMediator _mediator;
    public LeaveTypeController(IMediator mediatR)
    {
        _mediator = mediatR;     
    }

    [HttpGet]
    public async Task<ActionResult<List<LeaveTypeDto>>> Get(){

        var leaveTypes = await _mediator.Send(new GetLeaveTypeListRequest());
        return Ok(leaveTypes);
    }

    [HttpGet("{id}")]
     public async Task<ActionResult<LeaveTypeDto>> Get(int id){

        var leaveType = await _mediator.Send(new GetLeaveTypeDetailRequest{Id = id});
        return Ok(leaveType);
    }

    [HttpPost]
      public async Task<ActionResult> Post([FromBody] CreateLeaveTypeDto leaveTypeDto){
        var response = await _mediator.Send(new CreateLeaveTypeCommand{LeaveTypeDto = leaveTypeDto});
        return Ok(response);
        
    }

     [HttpPut]
      public async Task<ActionResult> Put([FromBody] LeaveTypeDto leaveTypeDto){

         await _mediator.Send(new UpdateLeaveTypeCommand{LeaveTypeDto = leaveTypeDto});
        return NoContent();
        
    }

     [HttpDelete]
      public async Task<ActionResult> Delete(int id){
         await _mediator.Send(new DeleteLeaveTypeCommand{Id = id});
        return NoContent();
        
    }


}
