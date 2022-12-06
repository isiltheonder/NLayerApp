using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        // IQuearyable döndüğümüzde direk veri tabanına gitmez.
        // ToList ToListAsync gibi metotları çağırarsak direk o zaman veritabanına gider.
        // async var olan threadleri blocklamamk için kullanılır.
        Task<T> GetByIdAsync(int id);
        IQueryable<T> GetAll();
        IQueryable<T> Where(Expression<Func<T, bool>> expression); //veritabanına sorgu yapmıyoruz. veritabanına sorgu yapılmasını oluşturuyoruz.
        // Delegeler metotları işaret eden yapılardır.
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
