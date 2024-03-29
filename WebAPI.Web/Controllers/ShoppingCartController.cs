﻿using AutoMapper;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebAPI.Common;
using WebAPI.Model.Models;
using WebAPI.Service;
using WebAPI.Web.App_Start;
using WebAPI.Web.Infrastructure.Extensions;
using WebAPI.Web.Models;

namespace WebAPI.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private IProductService _productService;
        private IOrderService _orderService;
        private ApplicationUserManager _userManager;

        public ShoppingCartController(IProductService productService, ApplicationUserManager userManager,
            IOrderService orderService)

        {
            this._productService = productService;
            this._userManager = userManager;
            this._orderService = orderService;
        }

        // GET: ShoppingCart
        public ActionResult Index()
        {
            if (Session[CommonConstants.SessionCart] == null)
            {
                Session[CommonConstants.SessionCart] = new List<ShoppingCartViewModel>(); //khởi tạo giá trị rỗng, đảm bảo giỏ hàng ko null
            }

            return View();
        }

        public ActionResult CheckOut()
        {
            if (Session[CommonConstants.SessionCart] == null)
            {
                return Redirect("/gio-hang.html");
            }
            return View();
        }

        //lấy thông tin đăng nhập
        public JsonResult GetUser()
        {
            if (Request.IsAuthenticated)//đã đăng nhập
            {
                var userId = User.Identity.GetUserId();
                var user = _userManager.FindById(userId);
                return Json(new
                {
                    data = user,
                    status = true
                });
            }
            return Json(new
            {
                status = false
            });
        }

        public JsonResult CreateOrder(string orderViewModel)
        {
            var order = new JavaScriptSerializer().Deserialize<OrderViewModel>(orderViewModel);
            var orderNew = new Order();
            orderNew.UpdateOrder(order);

            if (Request.IsAuthenticated)
            {
                orderNew.CustomerId = User.Identity.GetUserId();
                orderNew.CreatedBy = User.Identity.GetUserName();
            }
            var cart = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            List<OrderDetail> orderDetails = new List<OrderDetail>();

            bool isEnough = true;
            foreach (var item in cart)
            {
                var detail = new OrderDetail();
                detail.ProductID = item.ProductId;
                detail.Quantity = item.Quantity;
                detail.Price = item.Product.Price;

                orderDetails.Add(detail);
                isEnough = _productService.SellProduct(item.ProductId, item.Quantity);
                break;
            }
            if (isEnough)
            {
                _orderService.Create(orderNew, orderDetails);
                _productService.Save();

                return Json(new
                {
                    status = true
                });
            }
            else
            {
                return Json(new
                {
                    status = false,
                    message = "Không đủ hàng."
                });
            }
        }

        public JsonResult GetAll()
        {
            if (Session[CommonConstants.SessionCart] == null)
            {
                Session[CommonConstants.SessionCart] = new List<ShoppingCartViewModel>(); //khởi tạo giá trị rỗng, đảm bảo giỏ hàng ko null
            }
            var cart = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            return Json(new
            {
                data = cart,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add(int productId)
        {
            var cart = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];

            var product = _productService.GetById(productId);
            if (cart == null)
            {
                cart = new List<ShoppingCartViewModel>();
            }
            if (product.Quantity == 0)
            {
                return Json(new
                {
                    status = false,
                    messages = "Sản phẩm này hiện đang hết hàng"
                });
            }

            if (cart.Any(x => x.ProductId == productId))
            //Any: có chứa sản phẩm
            {
                foreach (var item in cart)
                {
                    if (item.ProductId == productId)//đã tồn tại thì +1
                    {
                        item.Quantity += 1;
                    }
                }
            }
            else //chưa có thì khởi tạo
            {
                ShoppingCartViewModel newItem = new ShoppingCartViewModel();
                newItem.ProductId = productId;

                newItem.Product = Mapper.Map<Product, ProductViewModel>(product);
                newItem.Quantity = 1;

                cart.Add(newItem);
            }
            Session[CommonConstants.SessionCart] = cart;
            return Json(new
            {
                status = true
            });
        }

        [HttpPost]
        public JsonResult DeleteItem(int productId)
        {
            var cartSession = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            if (cartSession != null)
            {
                cartSession.RemoveAll(x => x.ProductId == productId);
                Session[CommonConstants.SessionCart] = cartSession;
                return Json(new
                {
                    status = true
                });
            }
            return Json(new
            {
                status = false
            });
        }

        [HttpPost]
        public JsonResult Update(string cartData)
        {
            var cartViewModel = new JavaScriptSerializer().Deserialize<List<ShoppingCartViewModel>>(cartData);
            var cartSession = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            foreach (var item in cartSession)
            {
                foreach (var jitem in cartViewModel)
                {
                    if (item.ProductId == jitem.ProductId)
                    {
                        item.Quantity = jitem.Quantity;
                    }
                }
            }
            Session[CommonConstants.SessionCart] = cartSession;
            return Json(new
            {
                status = true
            });
        }

        [HttpPost]
        public JsonResult DeleteAll()
        {
            Session[CommonConstants.SessionCart] = new List<ShoppingCartViewModel>();
            return Json(new
            {
                status = true
            });
        }
    }
}