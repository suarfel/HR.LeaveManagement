using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories;

public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
{
    private readonly LeaveManagementDbContext _DbContext;
    public LeaveAllocationRepository(LeaveManagementDbContext context) : base(context)
    {
        _DbContext = context ;
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
    {
        var leaveAllocations = await _DbContext.LeaveAllocations.Include(p => p.LeaveType).ToListAsync();
        return leaveAllocations;
    }

    public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int Id)
    {
         var leaveAllocation = await _DbContext.LeaveAllocations.Include(p => p.LeaveType).FirstOrDefaultAsync(p => p.Id == Id);
         return leaveAllocation;
    }
}
