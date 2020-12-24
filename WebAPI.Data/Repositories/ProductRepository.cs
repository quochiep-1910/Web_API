using WebAPI.Data.Infrastructure;
using WebAPI.Model.Models;

namespace WebAPI.Data.Repositories
{
    public interface IProductRepository
    {
    }

    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        //tất cả phương thức update, delete, add đã tự động tạo bởi lớp kế thừa
        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}