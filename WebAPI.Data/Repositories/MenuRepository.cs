using System.Collections.Generic;
using System.Linq;
using WebAPI.Data.Infrastructure;
using WebAPI.Model.Models;

namespace WebAPI.Data.Repositories
{
    public interface IMenuRepository : IRepository<Menu>
    {
        List<Menu> ListByGroupID(int group);
    }

    public class MenuRepository : RepositoryBase<Menu>, IMenuRepository
    {
        public MenuRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public List<Menu> ListByGroupID(int group)
        {
            return DbContext.Menus.Where(x => x.GroupID == group && x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }
    }
}