using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Queries;

public class GetLeaveRequestRequestHandler : IRequestHandler<GetLeaveRequestRequest, LeaveRequestDto>
{
    
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IMapper _mapper;

    public GetLeaveRequestRequestHandler( ILeaveRequestRepository leaveRequestRepository,IMapper mapper)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;  
    }
    public async Task<LeaveRequestDto> Handle(GetLeaveRequestRequest request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepository.GetLeaveRequestWithDetails(request.Id);
        return _mapper.Map<LeaveRequestDto>(leaveRequest);    
    }
}
