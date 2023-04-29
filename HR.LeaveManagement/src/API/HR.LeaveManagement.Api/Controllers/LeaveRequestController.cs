using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeaveRequestController : ControllerBase
{
    private readonly IMediator _mediator;
    public LeaveRequestController(IMediator mediatR)
    {
        _mediator = mediatR;     
    }

    [HttpGet]
    public async Task<ActionResult<List<LeaveRequestDto>>> Get(){

        var leaveRequests= await _mediator.Send(new GetLeaveRequestListRequest());
        return Ok(leaveRequests);
    }

    [HttpGet("{id}")]
     public async Task<ActionResult<LeaveRequestDto>> Get(int id){

        var leaveRequest = await _mediator.Send(new GetLeaveRequestRequest{Id = id});
        return Ok(leaveRequest);
    }

    [HttpPost]
      public async Task<ActionResult> Post([FromBody] CreateLeaveRequestDto leaveRequestDto){
        var response = await _mediator.Send(new CreateLeaveRequestCommand{LeaveRequestDto = leaveRequestDto});
        return Ok(response);
    }

     [HttpPut]
      public async Task<ActionResult> Put(int id,[FromBody] UpdateLeaveRequestDto leaveRequestDto){

         await _mediator.Send(new UpdateLeaveRequestCommand{Id = id,LeaveRequestDto = leaveRequestDto});
        return NoContent(); 
    }
    [HttpPut("{id}")]
      public async Task<ActionResult> Put([FromBody] ChangeLeaveRequestApprovalDto  changeLeaveRequestApprovalDto){

         await _mediator.Send(new UpdateLeaveRequestCommand{ChangeLeaveRequestApprovalDto  =  changeLeaveRequestApprovalDto});
        return NoContent(); 
    }


     [HttpDelete]
      public async Task<ActionResult> Delete(int id){
         await _mediator.Send(new DeleteLeaveRequestCommand{Id = id});
        return NoContent();
        
    }
}
