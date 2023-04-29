

using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Persistence;

public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation> {

    Task<LeaveAllocation> GetLeaveAllocationWithDetails(int Id);
    Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails();

}