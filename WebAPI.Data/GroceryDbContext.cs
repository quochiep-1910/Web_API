﻿using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using WebAPI.Model.Models;

namespace WebAPI.Data
{
    public class GroceryDbContext : IdentityDbContext<ApplicationUser>
    {
        public GroceryDbContext() : base("GroceryStoreConnection")
        {
            //Tải chậm trong Khung thực thể
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Footer> Footers { set; get; }
        public DbSet<Menu> Menus { set; get; }
        public DbSet<MenuGroup> MenuGroups { set; get; }
        public DbSet<Order> Orders { set; get; }
        public DbSet<OrderDetail> OrderDetails { set; get; }
        public DbSet<Page> Pages { set; get; }
        public DbSet<Post> Posts { set; get; }
        public DbSet<PostCategory> PostCategories { set; get; }
        public DbSet<PostTag> PostTags { set; get; }
        public DbSet<Product> Products { set; get; }

        public DbSet<ProductCategory> ProductCategories { set; get; }
        public DbSet<ProductTag> ProductTags { set; get; }
        public DbSet<Slide> Slides { set; get; }
        public DbSet<SupportOnline> SupportOnlines { set; get; }
        public DbSet<SystemConfig> SystemConfigs { set; get; }

        public DbSet<Tag> Tags { set; get; }

        public DbSet<VisitorStatistic> VisitorStatistics { set; get; }
        public DbSet<Error> Errors { set; get; }
        public DbSet<ContactDetail> ContactDetails { set; get; }
        public DbSet<Feedback> Feedbacks { set; get; }

        //phân quyền
        public DbSet<ApplicationGroup> ApplicationGroups { set; get; }

        public DbSet<ApplicationRole> ApplicationRoles { set; get; }

        public DbSet<ApplicationRoleGroup> ApplicationRoleGroups { set; get; }

        public DbSet<ApplicationUserGroup> ApplicationUserGroups { set; get; }

        public static GroceryDbContext Create()
        {
            return new GroceryDbContext();
        }

        /// <summary>
        /// Ghi đè DbContext
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(DbModelBuilder builder)
        {
            //thêm key cho entity
            //đổi tên bảng
            builder.Entity<IdentityUserRole>().HasKey(i => new { i.UserId, i.RoleId }).ToTable("ApplicationUserRoles");
            builder.Entity<IdentityUserLogin>().HasKey(i => i.UserId).ToTable("ApplicationUserLogins");
            builder.Entity<IdentityRole>().ToTable("ApplicationRoles");
            builder.Entity<IdentityUserClaim>().HasKey(i => i.UserId).ToTable("ApplicationUserClaims");
        }
    }
}