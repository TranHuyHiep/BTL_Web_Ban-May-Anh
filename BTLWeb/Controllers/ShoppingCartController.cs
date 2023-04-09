using BTLWeb.Constants;
using BTLWeb.Models;
using BTLWeb.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using System.Text.Json;
using System.Web.Helpers;

namespace BTLWeb.Controllers
{
    

    public class ShoppingCartController : Controller
    {
        QlbanManhContext db = new QlbanManhContext();
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
                foreach(var item in cart)
                {
                    if(item.ProductId == productId)
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
            foreach(var item in cartSession)
            {
                foreach(var jitem in cartViewModel)
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
    }
}
