﻿namespace EFCoreWebAPI.Interfaces
{
    public interface IRepository <K, T> where T : class
    {
        Task<T> Get(K key);
        Task<IEnumerable<T>> GetAll();
        Task<T> Add(T entity);
        Task<T> Delete(int key);
        Task <T> Update(T entity, int pid);

    }
}
