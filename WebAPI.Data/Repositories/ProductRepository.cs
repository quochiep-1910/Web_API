using System.Collections.Generic;
using WebAPI.Data.Infrastructure;
using WebAPI.Model.Models;
using System.Linq;

namespace WebAPI.Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetListProductByTag(string tagId, int page, int pageSize, out int totalRow);
    }

    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        //tất cả phương thức update, delete, add đã tự động tạo bởi lớp kế thừa
        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Product> GetListProductByTag(string tagId, int page, int pageSize, out int totalRow)
        {
            var query = from p in DbContext.Products
                        join pt in DbContext.ProductTags
                        on p.ID equals pt.ProductID
                        where pt.TagID == tagId
                        select p;
            totalRow = query.Count();
            return query.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize);
        }

        ////test
        //public IEnumerable<Tag> GetNameForTag()
        //{
        //    var query = from Name in DbContext.Tags select Name;
        //    return query;
        //}
    }
}