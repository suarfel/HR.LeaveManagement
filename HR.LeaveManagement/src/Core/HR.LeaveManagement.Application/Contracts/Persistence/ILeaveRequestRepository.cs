


using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Persistence;

public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest> {
    Task<LeaveRequest> GetLeaveRequestWithDetails(int Id);
    Task<List<LeaveRequest>> GetLeaveRequestsWithDetails();
    Task ChangeApprovalStatus(LeaveRequest leaveRequest,bool? ApprovalStat);

}