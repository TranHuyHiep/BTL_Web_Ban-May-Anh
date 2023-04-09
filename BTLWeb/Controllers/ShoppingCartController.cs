using BTLWeb.Constants;
using BTLWeb.Models;
using BTLWeb.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using System.Text.Json;
using System.Web.Helpers;

namespace BTLWeb.Controllers
{


    public class ShoppingCartController : Controller
    {
        QlbanMayAnhContext db = new QlbanMayAnhContext();
        // GET: ShoppingCartController1
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Add(string productId)
        {
            var cart = HttpContext.Session.Get<List<ShoppingCartViewModel>>(CommonConstants.SessionCart);
            if (cart == null)
            {
                cart = new List<ShoppingCartViewModel>();
            }
            if (cart.Any(x => x.ProductId == productId))
            {
                foreach (var item in cart)
                {
                    if (item.ProductId == productId)
                    {
                        item.Quantity += 1;
                    }
                }
            }
            else
            {
                ShoppingCartViewModel newItem = new ShoppingCartViewModel();
                newItem.ProductId = productId;
                var product = db.TDanhMucSps.Find(productId);
                newItem.Product = product;
                newItem.Quantity = 1;
                cart.Add(newItem);
            }

            HttpContext.Session.Set<List<ShoppingCartViewModel>>(CommonConstants.SessionCart, cart);

            return Json(new
            {
                status = true
            });
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            var cart = HttpContext.Session.Get<List<ShoppingCartViewModel>>(CommonConstants.SessionCart);
            if (cart == null)
            {
                cart = new List<ShoppingCartViewModel>();
            }
            return Json(cart);
        }

        [HttpPost]
        public JsonResult Update(string cartData)
        {
            var cartViewModel = new JavaScriptSerializer().Deserialize<List<ShoppingCartViewModel>>(cartData);

            var cartSession = HttpContext.Session.Get<List<ShoppingCartViewModel>>(CommonConstants.SessionCart);
            foreach (var item in cartSession)
            {
                foreach (var jitem in cartViewModel)
                {
                    item.Quantity = jitem.Quantity;
                }
            }

            HttpContext.Session.Set<List<ShoppingCartViewModel>>(CommonConstants.SessionCart, cartSession);
            return Json(new
            {
                status = true
            });
        }

        [HttpPost]
        public JsonResult DeleteAll()
        {
            HttpContext.Session.Set<List<ShoppingCartViewModel>>(CommonConstants.SessionCart, new List<ShoppingCartViewModel>());
            return Json(new
            {
                status = true
            });
        }

        public ActionResult Checkout()
        {
            var cart = HttpContext.Session.Get<List<ShoppingCartViewModel>>(CommonConstants.SessionCart);

            if (cart == null)
            {
                return Redirect("/");
            }
            return View();
        }

        /*public JsonResult GetUser()
        {
            if (Request.IsAuthenticated)
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
        }*/


        /*public ActionResult CreateOrder(string orderViewModel)
        {
            var order = new JavaScriptSerializer().Deserialize<OrderViewModel>(orderViewModel);

            var orderNew = new THoaDonBan();

            orderNew.UpdateOrder(order);

            var cart = HttpContext.Session.Get<List<ShoppingCartViewModel>>(CommonConstants.SessionCart);
            
            List<TChiTietHdb> orderDetails = new List<TChiTietHdb>();

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

            var orderReturn = _orderService.Create(ref orderNew, orderDetails);
            _productService.Save();

            if (order.PaymentMethod == "CASH")
            {
                return Json(new
                {
                    status = true
                });
            }
            else
            {

                var currentLink = ConfigHelper.GetByKey("CurrentLink");
                RequestInfo info = new RequestInfo();
                info.Merchant_id = merchantId;
                info.Merchant_password = merchantPassword;
                info.Receiver_email = merchantEmail;



                info.cur_code = "vnd";
                info.bank_code = order.BankCode;

                info.Order_code = orderReturn.ID.ToString();
                info.Total_amount = orderDetails.Sum(x => x.Quantity * x.Price).ToString();
                info.fee_shipping = "0";
                info.Discount_amount = "0";
                info.order_description = "Thanh toán đơn hàng tại TeduShop";
                info.return_url = currentLink + "xac-nhan-don-hang.html";
                info.cancel_url = currentLink + "huy-don-hang.html";

                info.Buyer_fullname = order.CustomerName;
                info.Buyer_email = order.CustomerEmail;
                info.Buyer_mobile = order.CustomerMobile;

                APICheckoutV3 objNLChecout = new APICheckoutV3();
                ResponseInfo result = objNLChecout.GetUrlCheckout(info, order.PaymentMethod);
                if (result.Error_code == "00")
                {
                    return Json(new
                    {
                        status = true,
                        urlCheckout = result.Checkout_url,
                        message = result.Description
                    });
                }
                else
                    return Json(new
                    {
                        status = false,
                        message = result.Description
                    });
            }



        }*/
    }
}
