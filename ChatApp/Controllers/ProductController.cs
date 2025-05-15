using ChatApp.Hubs;
using ChatApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ITIContext context;
        private readonly IHubContext<ProductHub> productHub;

        public ProductController(ITIContext _context,IHubContext<ProductHub> productHub)
        {
            context = _context;
            this.productHub = productHub;
        }
        public IActionResult Index()
        {
            return View(context.Products.ToList());
        }


        //public IActionResult getjson()
        //{
        //    Product p=context.Products.FirstOrDefault();
        //    return Json(p);
        //}

        public IActionResult New()
        {
            return View("New");
        }

        [HttpPost]
        public IActionResult New(Product productFromReq)
        {
            if (ModelState.IsValid)
            {
                context.Products.Add(productFromReq);
                context.SaveChanges();
                //notification usingg hub
                productHub.Clients.All.SendAsync("NewProductHandel", productFromReq);
                //procust .Net {name="product1",Price=11}
                //==> serialization to json{"Name":"Prodiuct1",'Price":11}
                return RedirectToAction("Index");
            }
            return View("New",productFromReq);
        }
    }
}
