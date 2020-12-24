namespace WebAPI.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private GroceryDbContext dbContext;

        public GroceryDbContext Init()
        {
            //nếu dbContext null thì khởi tạo 1 cái
            return dbContext ?? (dbContext = new GroceryDbContext());
        }

        protected override void DisposeCore()
        {
            //Nếu khác null thì sẽ loại bỏ dbContext *
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}