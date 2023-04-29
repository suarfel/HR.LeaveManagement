using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllcocations.Requests.Commands;

public class CreateLeaveAllocationCommand : IRequest<int>
{
    public CreateLeaveAllocationDto LeaveAllocationDto { get; set; }
}
