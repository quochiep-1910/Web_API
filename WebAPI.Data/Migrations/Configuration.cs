namespace WebAPI.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebAPI.Model.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebAPI.Data.GroceryDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        //phương thức Seed này khởi tạo dữ liệu mẫu khi chúng ta chạy khởi tạo ứng dựng
        protected override void Seed(WebAPI.Data.GroceryDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new GroceryDbContext()));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new GroceryDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "Grocery",
                Email = "hiepgakute1111@gmail.com",
                EmailConfirmed = true,
                BirthDay = DateTime.Now,
                FullName = "Nguyen Quoc Hiep"
            };
            //password User
            manager.Create(user, "quochieps@2");

            //nếu role chưa tồn tại thì tạo 2 role Admin và user
            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            //tìm user admin thông qua Email
            var adminUser = manager.FindByEmail("hiepgakute1111@gmail.com");

            //nếu thành công thì có giá trị và add admin và user
            manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
        }
    }
}