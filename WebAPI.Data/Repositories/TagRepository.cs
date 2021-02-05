using System.Collections.Generic;
using WebAPI.Data.Infrastructure;
using WebAPI.Model.Models;
using System.Linq;
using System;

namespace WebAPI.Data.Repositories
{
    public interface ITagRepository : IRepository<Tag>
    {
        IEnumerable<Tag> GetNameForTag(int top);
    }

    public class TagRepository : RepositoryBase<Tag>, ITagRepository
    {
        public TagRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Tag> GetNameForTag(int top)
        {
            //SELECT TOP 5 *
            //FROM dbo.Tags
            //ORDER BY NEWID()
            var query = from Name in DbContext.Tags
                        .OrderBy(emp => Guid.NewGuid())
                        .Take(top).ToList()
                        select Name;
            return query;
        }
    }
}