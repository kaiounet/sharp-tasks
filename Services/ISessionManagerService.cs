using System;

namespace sharp_tasks.Services;

public interface ISessionManagerService
{
    public void Add(string key, Object value);
    public T Get<T>(string key);
    public void Remove(string key);
}
