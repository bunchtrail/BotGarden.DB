using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        // Получение всех сущностей
        Task<IEnumerable<T>> GetAllAsync();
        
        // Получение сущностей по условию
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        
        // Получение одной сущности по условию
        Task<T> GetByIdAsync(int id);
        
        // Добавление сущности
        Task AddAsync(T entity);
        
        // Добавление нескольких сущностей
        Task AddRangeAsync(IEnumerable<T> entities);
        
        // Обновление сущности
        void Update(T entity);
        
        // Удаление сущности
        void Remove(T entity);
        
        // Удаление нескольких сущностей
        void RemoveRange(IEnumerable<T> entities);
        
        // Сохранение изменений
        Task<int> SaveChangesAsync();
    }
} 