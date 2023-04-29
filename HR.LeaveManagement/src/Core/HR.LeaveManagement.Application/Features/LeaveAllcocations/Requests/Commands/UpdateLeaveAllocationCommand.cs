using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllcocations.Requests.Commands;

public class UpdateLeaveAllocationCommand : IRequest<Unit>
{
    public UpdateLeaveAllocationDto LeaveAllocationDto  { get; set; }
}
