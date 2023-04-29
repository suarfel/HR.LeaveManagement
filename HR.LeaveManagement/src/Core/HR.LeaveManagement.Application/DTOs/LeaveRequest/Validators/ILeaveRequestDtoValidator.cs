using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators;

public class ILeaveRequestDtoValidator : AbstractValidator<ILeaveRequestDto>
{
     private readonly ILeaveTypeRepository _leaveTypeRepository;
    public ILeaveRequestDtoValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;

        RuleFor(p => p.StartDate)
        .LessThan(p => p.EndDate).WithMessage("{PropertyName} must be before {ComparisonValue}");
        
        RuleFor(p => p.EndDate)
        .GreaterThan(p => p.StartDate).WithMessage("{PropertyName} must be after {ComparisonValue}");

        RuleFor(p => p.LeaveTypeId)
        .MustAsync(async (id,token) => {
            var leaveTypeIdExists = await _leaveTypeRepository.Exists(id);
            return !leaveTypeIdExists;
        }).WithMessage("{PropertyName} does not exist");
    }
}
