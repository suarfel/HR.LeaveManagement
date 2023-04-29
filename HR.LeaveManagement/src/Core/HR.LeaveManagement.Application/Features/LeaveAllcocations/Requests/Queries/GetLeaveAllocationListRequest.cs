using HR.LeaveManagement.Application.DTOs;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllcocations.Requests.Queries;

public class GetLeaveAllocationListRequest : IRequest<List<LeaveAllocationDto>>
{
}
