using HR.LeaveManagement.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly LeaveManagementDbContext _Dcontext;
    public GenericRepository(LeaveManagementDbContext  context)
    {
        _Dcontext = context;   
    }
    public async Task<T> Add(T entity)
    {
        await _Dcontext.AddAsync(entity);
        await _Dcontext.SaveChangesAsync();
        return entity;   
    }

    public async Task Delete(T entity)
    {
        _Dcontext.Set<T>().Remove(entity);
        await _Dcontext.SaveChangesAsync();
    }

    public async Task<bool> Exists(int id)
    {
        var entity = await Get(id);
        return entity != null;
         
    }

    public async Task<T> Get(int id)
    {
        return await _Dcontext.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> GetAll()
    {
         return await _Dcontext.Set<T>().ToListAsync();
    }

    public async Task Update(T entity)
    {
         _Dcontext.Entry(entity).State = EntityState.Modified;
         await _Dcontext.SaveChangesAsync();
    }
}
