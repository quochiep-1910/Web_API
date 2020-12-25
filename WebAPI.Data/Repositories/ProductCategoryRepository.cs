using System.Collections.Generic;
using System.Linq;
using WebAPI.Data.Infrastructure;
using WebAPI.Model.Models;

namespace WebAPI.Data.Repositories
{
    /// <summary>
    /// Phương thức tự định nghĩa
    /// </summary>
    public interface IProductCategoryRepository : IRepository<ProductCategory>
    {
        IEnumerable<ProductCategory> GetByAlias(string alias);
    }

    public class ProductCategoryRepository : RepositoryBase<ProductCategory>, IProductCategoryRepository
    {
        //tất cả phương thức update, delete, add đã tự động tạo bởi lớp kế thừa
        public ProductCategoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<ProductCategory> GetByAlias(string alias)
        {
            return this.DbContext.ProductCategories.Where(x => x.Alias == alias);
        }
    }
}