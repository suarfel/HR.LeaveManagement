using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands;

public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IMapper _mapper;

    public  UpdateLeaveRequestCommandHandler( ILeaveRequestRepository leaveRequestRepository,IMapper mapper,ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;  
    }
    public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateLeaveRequestDtoValidator(_leaveTypeRepository);
        var validatorResult = await validator.ValidateAsync(request.LeaveRequestDto);

         if (validatorResult.IsValid == false){
            throw new ValidationException(validatorResult); 
        }
        var leaveRequest = await _leaveRequestRepository.Get(request.Id);

        if (request.LeaveRequestDto != null){
            _mapper.Map(request.LeaveRequestDto,leaveRequest);
            await _leaveRequestRepository.Update(leaveRequest);
        }
        else if(request.ChangeLeaveRequestApprovalDto != null){
            await _leaveRequestRepository.ChangeApprovalStatus(leaveRequest,request.ChangeLeaveRequestApprovalDto.Approved);
        }

        return Unit.Value;
        
    }
}
