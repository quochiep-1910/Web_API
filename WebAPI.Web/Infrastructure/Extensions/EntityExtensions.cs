using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Model.Models;
using WebAPI.Web.Models;

namespace WebAPI.Web.Infrastructure.Extensions
{
    public static class EntityExtensions
    {
        public static void UpdatePostCategory(this PostCategory postCategory, PostCategoryViewModel postCategoryViewModel)
        {
            postCategory.ID = postCategoryViewModel.ID;
            postCategory.Name = postCategoryViewModel.Name;

            postCategory.Alias = postCategoryViewModel.Alias;
            postCategory.Description = postCategoryViewModel.Description;
            postCategory.ParentID = postCategoryViewModel.ParentID;

            postCategory.DisplayOrder = postCategoryViewModel.DisplayOrder;
            postCategory.Image = postCategoryViewModel.Image;
            postCategory.HomeFlag = postCategoryViewModel.HomeFlag;

            postCategory.CreatedDate = postCategoryViewModel.CreatedDate;
            postCategory.CreatedBy = postCategoryViewModel.CreatedBy;
            postCategory.UpdatedDate = postCategoryViewModel.UpdatedDate;
            postCategory.UpdatedBy = postCategoryViewModel.UpdatedBy;
            postCategory.MetaKeyword = postCategoryViewModel.MetaKeyword;

            postCategory.MetaDescription = postCategoryViewModel.MetaDescription;
            postCategory.Status = postCategoryViewModel.Status;
        }

        public static void UpdateProductCategory(this ProductCategory productCategory, ProductCategoryViewModel productCategoryViewModel)
        {
            productCategory.ID = productCategoryViewModel.ID;
            productCategory.Name = productCategoryViewModel.Name;

            productCategory.Alias = productCategoryViewModel.Alias;
            productCategory.Description = productCategoryViewModel.Description;
            productCategory.ParentID = productCategoryViewModel.ParentID;

            productCategory.DisplayOrder = productCategoryViewModel.DisplayOrder;
            productCategory.Image = productCategoryViewModel.Image;
            productCategory.HomeFlag = productCategoryViewModel.HomeFlag;

            productCategory.CreatedDate = productCategoryViewModel.CreatedDate;
            productCategory.CreatedBy = productCategoryViewModel.CreatedBy;
            productCategory.UpdatedDate = productCategoryViewModel.UpdatedDate;
            productCategory.UpdatedBy = productCategoryViewModel.UpdatedBy;
            productCategory.MetaKeyword = productCategoryViewModel.MetaKeyword;

            productCategory.MetaDescription = productCategoryViewModel.MetaDescription;
            productCategory.Status = productCategoryViewModel.Status;
        }

        public static void UpdatePost(this Post post, PostViewModel postViewModel)
        {
            post.ID = postViewModel.ID;
            post.Name = postViewModel.Name;

            post.Alias = postViewModel.Alias;
            post.Description = postViewModel.Description;
            post.CategoryID = postViewModel.CategoryID;

            post.Content = postViewModel.Content;
            post.HotFlag = postViewModel.HotFlag;
            post.ViewCount = postViewModel.ViewCount;
            post.Image = postViewModel.Image;
            post.HomeFlag = postViewModel.HomeFlag;

            post.CreatedDate = postViewModel.CreatedDate;
            post.CreatedBy = postViewModel.CreatedBy;
            post.UpdatedDate = postViewModel.UpdatedDate;
            post.UpdatedBy = postViewModel.UpdatedBy;

            post.MetaKeyword = postViewModel.MetaKeyword;
            post.MetaDescription = postViewModel.MetaDescription;
            post.Status = postViewModel.Status;
        }

        public static void UpdateProduct(this Product product, ProductViewModel productViewModel)
        {
            product.ID = productViewModel.ID;
            product.Name = productViewModel.Name;

            product.Alias = productViewModel.Alias;
            product.Description = productViewModel.Description;
            product.CategoryID = productViewModel.CategoryID;

            product.Content = productViewModel.Content;
            product.HotFlag = productViewModel.HotFlag;
            product.ViewCount = productViewModel.ViewCount;

            product.Image = productViewModel.Image;

            product.ImageHotTag = productViewModel.ImageHotTag;
            product.Price = productViewModel.Price;
            product.PromotionPrice = productViewModel.PromotionPrice;

            product.Warranty = productViewModel.Warranty;
            product.Tags = productViewModel.Tags;
            product.Quantity = productViewModel.Quantity;

            product.MoreImages = productViewModel.MoreImages;
            product.HomeFlag = productViewModel.HomeFlag;
            product.OriginalPrice = productViewModel.OriginalPrice;

            product.CreatedDate = productViewModel.CreatedDate;
            product.CreatedBy = productViewModel.CreatedBy;
            product.UpdatedDate = productViewModel.UpdatedDate;
            product.UpdatedBy = productViewModel.UpdatedBy;

            product.MetaKeyword = productViewModel.MetaKeyword;
            product.MetaDescription = productViewModel.MetaDescription;
            product.Status = productViewModel.Status;
        }

        public static void UpdatePage(this Page page, PageViewModel PageViewModel)
        {
            page.ID = PageViewModel.ID;
            page.Name = PageViewModel.Name;
            page.Alias = PageViewModel.Alias;

            page.Content = PageViewModel.Content;
            page.Image = PageViewModel.Image;

            page.CreatedDate = PageViewModel.CreatedDate;
            page.CreatedBy = PageViewModel.CreatedBy;
            page.UpdatedDate = PageViewModel.UpdatedDate;
            page.UpdatedBy = PageViewModel.UpdatedBy;

            page.MetaKeyword = PageViewModel.MetaKeyword;
            page.MetaDescription = PageViewModel.MetaDescription;
            page.Status = PageViewModel.Status;
        }

        public static void UpdateFeedback(this Feedback feedback, FeedbackViewModel feedbackViewModel)
        {
            feedback.Name = feedbackViewModel.Name;
            feedback.Email = feedbackViewModel.Email;
            feedback.Message = feedbackViewModel.Message;
            feedback.Status = feedbackViewModel.Status;
            feedback.CreateDate = DateTime.Now;
        }

        public static void UpdateSlide(this Slide slide, SlideViewModel slideViewModel)
        {
            slide.Name = slideViewModel.Name;
            slide.Description = slideViewModel.Description;
            slide.Image = slideViewModel.Image;
            slide.Url = slideViewModel.Url;
            slide.DisplayOrder = slideViewModel.DisplayOrder;

            slide.Status = slideViewModel.Status;
            slide.Content = slideViewModel.Content;
        }

        public static void UpdateFooter(this Footer footer, FooterViewModel footerViewModel)
        {
            footer.ID = footerViewModel.ID;
            footer.Content = footerViewModel.Content;
        }

        public static void UpdateOrder(this Order order, OrderViewModel orderViewModel)
        {
            order.CustomerName = orderViewModel.CustomerName;
            order.CustomerAddress = orderViewModel.CustomerAddress;
            order.CustomerEmail = orderViewModel.CustomerEmail;

            order.CustomerMobile = orderViewModel.CustomerMobile;
            order.CustomerMessage = orderViewModel.CustomerMessage;
            order.PaymentMethod = orderViewModel.PaymentMethod;

            order.CreatedDate = DateTime.Now;
            order.CreatedBy = orderViewModel.CreatedBy;
            order.Status = orderViewModel.Status;
            order.CustomerId = orderViewModel.CustomerId;
        }

        public static void UpdateApplicationGroup(this ApplicationGroup appGroup, ApplicationGroupViewModel appGroupViewModel)
        {
            appGroup.ID = appGroupViewModel.ID;
            appGroup.Name = appGroupViewModel.Name;
        }

        public static void UpdateApplicationRole(this ApplicationRole appRole, ApplicationRoleViewModel appRoleViewModel, string action = "add")
        {
            if (action == "update")
                appRole.Id = appRoleViewModel.Id;
            else
                appRole.Id = Guid.NewGuid().ToString();
            appRole.Name = appRoleViewModel.Name;
            appRole.Description = appRoleViewModel.Description;
        }

        public static void UpdateUser(this ApplicationUser appUser, ApplicationUserViewModel appUserViewModel, string action = "add")
        {
            appUser.Id = appUserViewModel.Id;
            appUser.FullName = appUserViewModel.FullName;
            appUser.BirthDay = appUserViewModel.BirthDay;
            appUser.Email = appUserViewModel.Email;
            appUser.UserName = appUserViewModel.UserName;
            appUser.PhoneNumber = appUserViewModel.PhoneNumber;
        }
    }
}