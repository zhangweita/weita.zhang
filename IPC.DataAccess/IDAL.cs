using System.Linq.Expressions;

namespace IPC.DataAccess;

public interface IDAL<T>
{
    /// <summary>
    /// 新增实体
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    int Add(T t);
    /// <summary>
    /// 新增实体集合
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    int AddRange(List<T> entityCollection);
    /// <summary>
    /// 根据Id删除实体
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    int DeleteById(int id);
    /// <summary>
    /// 根据查询条件删除实体
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    int DeleteRange(Expression<Func<T, bool>> expression);
    /// <summary>
    /// 更新实体类
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    int Update(T t);
    /// <summary>
    /// 批量更新实体类
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    int UpdateRange(IEnumerable<T> entityCollection);
    /// <summary>
    /// 根据Id查询一条数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    T? GetById(int id);
    /// <summary>
    /// 根据条件查询一条数据
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    T? Get(Expression<Func<T, bool>> expression);
    /// <summary>
    /// 根据条件查询数据集合
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    List<T> GetList(Expression<Func<T, bool>> expression);
}

public interface IAsyncDAL<T>
{
    /// <summary>
    /// 新增实体
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    Task<int> AddAsync(T t);
    /// <summary>
    /// 新增实体集合
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    Task<int> AddRangeAsync(IEnumerable<T> entityCollection);
    /// <summary>
    /// 根据Id删除实体
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<int> DeleteAsync(int id);
    /// <summary>
    /// 根据查询条件删除实体
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<int> DeleteRangeAsync(Expression<Func<T, bool>> expression);
    /// <summary>
    /// 更新实体类
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    Task<int> UpdateAsync(T t);
    /// <summary>
    /// 批量更新实体类
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    Task<int> UpdateRangeAsync(IEnumerable<T> entityCollection);
    /// <summary>
    /// 根据Id查询一条数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<T?> GetByIdAsync(int id);
    /// <summary>
    /// 根据条件查询一条数据
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    Task<T?> GetAsync(Expression<Func<T, bool>> expression);
    /// <summary>
    /// 根据条件查询数据集合
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    Task<List<T>> GetListAsync(Expression<Func<T, bool>> expression);
}