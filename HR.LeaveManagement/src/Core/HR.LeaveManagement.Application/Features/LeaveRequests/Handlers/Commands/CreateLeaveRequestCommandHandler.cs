using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Models;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Domain;
using MediatR;
using HR.LeaveManagement.Application.Contracts.Infrastructure;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, BaseCommonResponse>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IMapper _mapper;
    private readonly IEmailSender _emailSender;

    public CreateLeaveRequestCommandHandler( ILeaveRequestRepository leaveRequestRepository,IMapper mapper,ILeaveTypeRepository leaveTypeRepository,IEmailSender emailSender)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
        _emailSender = emailSender;  
        
    }
    public async Task<BaseCommonResponse> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseCommonResponse();
        var validator = new CreateLeaveRequestDtoValidator(_leaveTypeRepository);
        var validatorResult = await validator.ValidateAsync(request.LeaveRequestDto);
        
        if (validatorResult.IsValid == false){
            response.Success = false;
            response.Message = "Creation Failed";
            response.Errors = validatorResult.Errors.Select(p => p.ErrorMessage).ToList();
    // throw new ValidationException(validatorResult); 
        }
        
        var leaveRequest = _mapper.Map<LeaveRequest>(request.LeaveRequestDto);
        leaveRequest = await _leaveRequestRepository.Add(leaveRequest);

        response.Success = true;
        response.Message = "Creation Successul";
        response.Id =  leaveRequest.Id;

        var email = new Email
        {
            To = "employeee@gamil.com",
            Subject = $"Your leave request for {leaveRequest.StartDate : D} to {leaveRequest.EndDate : D}" ,
            Body = "Leave Request Submitted"
        };

        try{
            await _emailSender.SendEmail(email);
        }catch{

        }

        return response;
    }
}
