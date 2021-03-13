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

        bool ChangeStatus(int id);
    }

    public class ProductCategoryRepository : RepositoryBase<ProductCategory>, IProductCategoryRepository
    {
        //tất cả phương thức update, delete, add đã tự động tạo bởi lớp kế thừa
        public ProductCategoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public bool ChangeStatus(int id)
        {
            var productCategory = DbContext.ProductCategories.Find(id);
            productCategory.Status = !productCategory.Status; //cứ giá trị nào trong status là true thì chuyển thành false (đảo ngược giá trị)
            return productCategory.Status;
        }

        public IEnumerable<ProductCategory> GetByAlias(string alias)
        {
            return this.DbContext.ProductCategories.Where(x => x.Alias == alias);
        }
    }
}