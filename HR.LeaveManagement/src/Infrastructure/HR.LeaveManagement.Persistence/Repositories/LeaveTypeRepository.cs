using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Persistence.Repositories;

public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
{
    private readonly LeaveManagementDbContext _DbContext;
    public LeaveTypeRepository(LeaveManagementDbContext context) : base(context)
    {
        _DbContext = context;
    }
}
