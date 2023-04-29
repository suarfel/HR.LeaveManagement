using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllcocations.Requests.Commands;

public class DeleteLeaveAllocationCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
