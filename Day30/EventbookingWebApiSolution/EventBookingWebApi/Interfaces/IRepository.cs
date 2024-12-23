﻿namespace EventBookingWebApi.Interfaces
{
    public interface IRepository<K, T> where T : class
    {
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(K key);
        Task<T> Get(K key);
        Task<IEnumerable<T>> GetAll();

    }
}
