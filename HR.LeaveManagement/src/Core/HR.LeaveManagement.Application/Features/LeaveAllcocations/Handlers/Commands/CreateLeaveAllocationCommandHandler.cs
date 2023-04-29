using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllcocations.Requests.Commands;
using HR.LeaveManagement.Domain;

using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllcocations.Handlers.Commands;

public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, int>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IMapper _mapper;

    public  CreateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository,IMapper mapper,ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
    }
    public async Task<int> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveAllocationDtoValidator(_leaveTypeRepository);
        var validatorResult = await validator.ValidateAsync(request.LeaveAllocationDto);

        if (validatorResult.IsValid == false){
            throw new ValidationException(validatorResult); 
        }
        
        var leaveAllcocation = _mapper.Map<LeaveAllocation>(request.LeaveAllocationDto);
        leaveAllcocation = await _leaveAllocationRepository.Add(leaveAllcocation);

        return leaveAllcocation.Id;


    }
}
