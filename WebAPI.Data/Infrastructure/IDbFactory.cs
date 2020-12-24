using System;

namespace WebAPI.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        GroceryDbContext Init();
    }
}