using System;
using System.Collections;
using System.Collections.Generic;
using WebAPI.Data.Infrastructure;
using WebAPI.Model.Models;
using System.Linq;

namespace WebAPI.Data.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
    }

    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}