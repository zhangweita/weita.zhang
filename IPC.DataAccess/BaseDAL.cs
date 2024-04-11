using IPC.DataAccess.Oracle.Factory;
using IPC.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Linq.Expressions;

namespace IPC.DataAccess;

public class BaseDAL<T> : IDAL<T>, IAsyncDAL<T> where T : BaseModel
{
    private DbContext Db { get; init; }
    public BaseDAL(IDbContextFactory DbFactory, IConfiguration configuration)
    {
        Db = DbFactory.CreateContext(DatabaseType.OracleTest);
    }

    #region 增    
    public virtual int Add(T t)
    {
        Db.Set<T>().Add(t);
        return Db.SaveChanges();
    }
    public virtual Task<int> AddAsync(T t)
    {
        Db.Set<T>().Add(t);
        return Db.SaveChangesAsync();
    }
    public int AddRange(List<T> entityCollection)
    {
        Db.Set<T>().AddRange(entityCollection);
        return Db.SaveChanges();
    }
    public Task<int> AddRangeAsync(IEnumerable<T> entityCollection)
    {
        Db.Set<T>().AddRange(entityCollection);
        return Db.SaveChangesAsync();
    }
    #endregion

    #region 删
    public virtual int DeleteById(int id)
    {

        T? t = Db.Set<T>().Find(id);
        if (t == null) return 0;

        Db.Set<T>().Remove(t);
        return Db.SaveChanges();
    }
    public virtual async Task<int> DeleteAsync(int id)
    {
        T? t = await Db.Set<T>().FindAsync(id);
        if (t == null) return 0;

        Db.Set<T>().Remove(t);
        return await Db.SaveChangesAsync();
    }
    public int DeleteRange(Expression<Func<T, bool>> expression)
    {
        Db.RemoveRange(Db.Set<T>().Where(expression));
        return Db.SaveChanges();
    }
    public Task<int> DeleteRangeAsync(Expression<Func<T, bool>> expression)
    {
        Db.RemoveRange(Db.Set<T>().Where(expression));
        return Db.SaveChangesAsync();
    }
    #endregion

    #region 改
    public virtual int Update(T t)
    {
        Db.Set<T>().Attach(t);
        Db.Set<T>().Entry(t).State = EntityState.Modified;
        Db.Set<T>().Update(t);
        return Db.SaveChanges();
    }
    public Task<int> UpdateAsync(T t)
    {
        Db.Set<T>().Attach(t);
        Db.Set<T>().Entry(t).State = EntityState.Modified;
        Db.Set<T>().Update(t);
        return Db.SaveChangesAsync();
    }
    public int UpdateRange(IEnumerable<T> entityCollection)
    {
        foreach (var t in entityCollection)
        {
            Db.Set<T>().Attach(t);
            Db.Set<T>().Entry(t).State = EntityState.Modified;
        }
        Db.Set<T>().UpdateRange(entityCollection);
        return Db.SaveChanges();
    }
    public Task<int> UpdateRangeAsync(IEnumerable<T> entityCollection)
    {
        foreach (var t in entityCollection)
        {
            Db.Set<T>().Attach(t);
            Db.Set<T>().Entry(t).State = EntityState.Modified;
        }
        Db.Set<T>().UpdateRange(entityCollection);
        return Db.SaveChangesAsync();
    }
    #endregion

    #region 查
    public virtual T? GetById(int id)
    {
        return Db.Set<T>().Find(id);
    }
    public virtual Task<T?> GetByIdAsync(int id)
    {
        return Db.Set<T>().FindAsync(id).AsTask();
    }
    public virtual T? Get(Expression<Func<T, bool>> expression)
    {
        return Db.Set<T>().SingleOrDefault(expression);
    }
    public virtual Task<T?> GetAsync(Expression<Func<T, bool>> expression)
    {
        return Db.Set<T>().SingleOrDefaultAsync(expression);
    }
    public virtual List<T> GetList(Expression<Func<T, bool>> expression)
    {
        return Db.Set<T>().Where(expression).ToList();
    }
    public virtual Task<List<T>> GetListAsync(Expression<Func<T, bool>> expression)
    {
        return Db.Set<T>().Where(expression).ToListAsync();
    }
    #endregion
}
