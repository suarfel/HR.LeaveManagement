using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories;

public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
{
    private  readonly LeaveManagementDbContext _DbContext;
    public LeaveRequestRepository(LeaveManagementDbContext context) : base(context)
    {
        _DbContext = context ;
    }

    public async Task ChangeApprovalStatus(LeaveRequest leaveRequest, bool? ApprovalStat)
    {
        leaveRequest.Approved = ApprovalStat;
        _DbContext.Entry(leaveRequest).State = EntityState.Modified;
        await _DbContext.SaveChangesAsync();

    }

    public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails()
    {
         var leaveRequests = await _DbContext.LeaveRequests.Include(p => p.LeaveType).ToListAsync();

         return leaveRequests;
    }

    public async Task<LeaveRequest> GetLeaveRequestWithDetails(int Id)
    {
        var leaveRequest = await _DbContext.LeaveRequests.Include(p => p.LeaveType).FirstOrDefaultAsync(p => p.Id == Id);

        return leaveRequest;
    }
}
