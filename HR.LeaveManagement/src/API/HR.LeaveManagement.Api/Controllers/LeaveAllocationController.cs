using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllcocations.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveAllcocations.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeaveAllocationController : ControllerBase
{
     
    private readonly IMediator _mediator;
    public LeaveAllocationController(IMediator mediatR)
    {
        _mediator = mediatR;     
    }

    [HttpGet]
    public async Task<ActionResult<List<LeaveAllocationDto>>> Get(){

        var leaveAllcocations= await _mediator.Send(new GetLeaveAllocationListRequest());
        return Ok(leaveAllcocations);
    }

    [HttpGet("{id}")]
     public async Task<ActionResult<LeaveAllocationDto>> Get(int id){

        var leaveAllocation = await _mediator.Send(new GetLeaveAllocationDetailRequest{Id = id});
        return Ok(leaveAllocation);
    }

    [HttpPost]
      public async Task<ActionResult> Post([FromBody] CreateLeaveAllocationDto leaveAllocationDto){
        var response = await _mediator.Send(new CreateLeaveAllocationCommand{LeaveAllocationDto = leaveAllocationDto});
        return Ok(response);
    }

     [HttpPut]
      public async Task<ActionResult> Put([FromBody] UpdateLeaveAllocationDto leaveAllocationDto){

         await _mediator.Send(new UpdateLeaveAllocationCommand{LeaveAllocationDto = leaveAllocationDto});
        return NoContent(); 
    }

     [HttpDelete]
      public async Task<ActionResult> Delete(int id){
         await _mediator.Send(new DeleteLeaveAllocationCommand{Id = id});
        return NoContent();
        
    }

 

}
