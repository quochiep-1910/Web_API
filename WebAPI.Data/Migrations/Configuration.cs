namespace WebAPI.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebAPI.Common;
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
            CreateProductCategorySample(context);
            CreateSlide(context);
        }

        private void CreateUser(GroceryDbContext context)
        {
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

        private void CreateProductCategorySample(WebAPI.Data.GroceryDbContext context)
        {
            if (context.ProductCategories.Count() == 0)
            {
                List<ProductCategory> listProductCategory = new List<ProductCategory>()
                {
                    new ProductCategory() { Name = "Thực Phẩm", Alias = "thuc-pham", Status = true },
                    new ProductCategory() { Name = "Gia dụng", Alias = "gia-dung", Status = true },
                    new ProductCategory() { Name = "Thức Ăn", Alias = "thuc-an", Status = true },
                    new ProductCategory() { Name = "Đồ chơi", Alias = "do-choi", Status = true },
                    new ProductCategory() { Name = "Mỹ Phẩm", Alias = "my-pham", Status = false }
                };
                context.ProductCategories.AddRange(listProductCategory);
                context.SaveChanges();
            }
        }

        private void CreateFooter(GroceryDbContext context)
        {
            if (context.Footers.Count(x => x.ID == CommonConstants.DefaultFooterId) == 0)
            {
                string content = "";
            }
        }

        private void CreateSlide(GroceryDbContext context)
        {
            if (context.Slides.Count() == 0)
            {
                List<Slide> listSlide = new List<Slide>()
                {
                    new Slide(){Name="Slide 1", DisplayOrder=1,Status=true,Url="#",Image="/Assets/client/img/slider/slide_1.png",Content=@"<h1>Women's Fashion</h1><p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. </p><a href=""#"">shop now</a>"},
                    new Slide(){Name="Slide 2", DisplayOrder=2,Status=true,Url="#",Image="/Assets/client/img/slider/slide_2.png",Content=@"<h1>New Collection</h1><p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. </p><a href=""#"">shop now</a>"},
                    new Slide(){Name="Slide 2", DisplayOrder=3,Status=true,Url="#",Image="/Assets/client/img/slider/slide_3.png",Content=@"<h1>Best Collection</h1><p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. </p><a href=""#"">shop now</a>"}
                };
                context.Slides.AddRange(listSlide);
                context.SaveChanges();
            }
        }
    }
}