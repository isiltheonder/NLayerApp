using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Category> GetSingleCategoryByIdWithProductsAsync(int categoryId)
        {
            //FistOrDefaut dersek eğer bu id'den dbde birkaç tane varsa ilkini bulur. SingleOrDefault dersek bu id'de, yani bu koşulu karşılayan birden fazla satır varsa geriye hata döndürür. Id'den bir tane olması lazım çünkü primary key. First exception dönmez.
            return await _context.Categories.Include(x=>x.Products).Where(x=>x.Id== categoryId).SingleOrDefaultAsync(); 
        }
    }
}
