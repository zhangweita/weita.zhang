using IPC.DataAccess;

namespace IPC.Service;

/// <summary>
/// 服务抽象类
/// </summary>
public abstract class BaseService<T> : IService
{
    protected IConfiguration Cfg;
    protected ILogger Logger;
    protected IDAL<T> Dal;
    protected BaseService(ILogger logger, IConfiguration cfg) => (Logger, Cfg) = (logger, cfg);
}
