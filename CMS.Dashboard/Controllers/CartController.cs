using System.Collections.Generic;
using System.Web.Mvc;
using CMS.Dashboard.Models;
using CMS.DataAccess.Core.Extension;
using CMS.DataAccess.Core.Repositories;
using CMS.DataAccess.Persistence;
using CMS.DataAccess.Persistence.Repositories;

namespace CMS.Dashboard.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository _productRepository = new ProductRepository(new WorkContext());

        [HttpPost, Route("add-to-cart")]
        public ActionResult AddToCart(int id)
        {
            List<CartItem> cartItems;
            var product = _productRepository.Get(id);

            if (product.Discount > 0)
            {
                if (!product.DiscountIsPercent)
                {
                    product.Price = product.Price - product.Discount;
                }
                else
                {
                    product.Price = product.Price - (product.Price*(product.Discount/100));
                }
            }

            if (Session["ShoppingCart"] == null)
            {
                cartItems = new List<CartItem>
                {
                    new CartItem {Quality = 1, Product = product}
                };
                Session["ShoppingCart"] = cartItems;
            }
            else
            {
                bool flag = false;
                cartItems = (List<CartItem>)Session["ShoppingCart"];
                foreach (CartItem item in cartItems)
                {
                    if (item.Product.Id == id)
                    {
                        item.Quality++;
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                    cartItems.Add(new CartItem { Quality = 1, Product = product });
                Session["ShoppingCart"] = cartItems;
            }
            
            return Json(new
            {
                Status = true,
                Items = (List<CartItem>)Session["ShoppingCart"]
            });
        }

        [HttpPost, Route("remove-from-cart")]
        public ActionResult RemoveFromCart(int id)
        {
            if (Session["ShoppingCart"] != null)
            {
                var cartItems = (List<CartItem>)Session["ShoppingCart"];
                cartItems.RemoveAll(s => s.Product.Id == id);

                Session["ShoppingCart"] = cartItems;
            }

            return Json(new
            {
                Status = true,
                Items = (List<CartItem>)Session["ShoppingCart"]
            });
        }

        [Route("get-cart-items")]
        public ActionResult GetCartItems()
        {
            if (Session["ShoppingCart"] != null)
            {
                var cartItems = (List<CartItem>)Session["ShoppingCart"];
                return Json(cartItems, JsonRequestBehavior.AllowGet);
            }

            return null;
        }
    }
}