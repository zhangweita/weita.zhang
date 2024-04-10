﻿namespace IPC.Service;

/// <summary>
/// 服务抽象类
/// </summary>
public abstract class BaseService : IService
{
    protected IConfiguration Cfg { get; set; }

    protected ILogger Logger;
    protected BaseService(ILogger logger, IConfiguration cfg) => (Logger, Cfg) = (logger, cfg);
}