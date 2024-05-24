using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using UrunSatisApp.Models;

namespace UrunSatisApp.Controllers
{
    public class HomeController : Controller
    {
        public List<Product> LoadProducts()
        {
            var products = new List<Product>();
            using StreamReader productReader = new StreamReader("App_Data/products.txt");
            string productsTxt = productReader.ReadToEnd();

            if (!string.IsNullOrEmpty(productsTxt))
            {
                var productList = productsTxt.Split('\n');

                foreach (var productLine in productList)
                {
                    var product = productLine.Split('|');
                    products.Add
                 (
                    new Product
                    {
                        Name = product[0],
                        Price = int.Parse(product[1]),
                        Image = product[2]
                    }
                 );
                }
            }
            return products;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var products = LoadProducts();
            return View(products);
        }

        [HttpPost]
        public IActionResult Index(Cart model)
        {
            var products = LoadProducts();

            var choosenProduct = new Product();
            foreach (var product in products)
            {
                if(product.Name == model.Name)
                {
                    choosenProduct = product;
                    break;
                }
            }

            var changeBack = model.PaidPrice - choosenProduct.Price;
            var incomplete = choosenProduct.Price - model.PaidPrice;

            if (changeBack > 0)
            {
                ViewData["Msg"] = $"<div class=\"alert alert-success\" role=\"alert\">\r\n  Satın alımınız için teşekkür ederiz! Para üstünüz : {changeBack} TL\r\n</div>\r\n";

            }
            else if (changeBack < 0)
            {
                ViewData["Msg"] = $"<div class=\"alert alert-warning\" role=\"alert\">\r\n  Eksik ödeme yaptınız! Eklenmesi gereken tutar : {incomplete} TL\r\n</div>\r\n";
            }
            else
            {
                ViewData["Msg"] = $"<div class=\"alert alert-success\" role=\"alert\">\r\n  Satın alımınız için teşekkür ederiz!\r\n</div>\r\n";
            }

            return View(products);
        }
    }
}
