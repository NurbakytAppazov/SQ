using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SqApp.DbData;
using SqApp.ViewModels;

namespace SqApp.Controllers
{
    public class StoreController : Controller
    {
        private AppDbContext db;

        public StoreController(AppDbContext _db)
        {
            this.db = _db;
        }


        public async Task<IActionResult> Shop()
        {
            ShopViewModel svm = new ShopViewModel
            {
                Products = await db.Products.Include(p => p.Pod_Category).ThenInclude(p=>p.Category).ToListAsync(),
                Categories = await db.Categories.ToListAsync()
            };
            return View(svm);
        }

        public async Task<IActionResult> Details(int? Id, int? page)
        {
            if(Id != null)
            {
                Product product = await db.Products.FirstOrDefaultAsync(p => p.Id == Id);
                IEnumerable<Comment> cm = await db.Comments.Where(p => p.ProductId == product.Id).ToListAsync();
                IEnumerable<ProductImage> pi = await db.ProductImages.Where(p => p.ProductId == product.Id).ToListAsync();
                IEnumerable<ProductInfo> pinfo = await db.ProductInfos.Where(p => p.ProductId == product.Id).Include(p=>p.InProductInfos).ToListAsync();

                ViewBag.Id = db.Categories.FirstOrDefault(p => p.Id == product.CategoryId).Id;
                ViewBag.Name = db.Categories.FirstOrDefault(p => p.Id == product.CategoryId).Name;
                ViewBag.a = 0;
                ViewBag.b = 2;
                
                if (product != null)
                {
                    DetailsViewModel dm = new DetailsViewModel
                    {
                        Products = product,
                        Comments = cm,
                        ProductImages = pi,
                        ProductInfos = pinfo,
                        Productes = await db.Products.ToListAsync(),
                        Categories = await db.Categories.ToListAsync()
                    };
                    return View(dm);
                }
            }
            return View();
        }

        public async Task<IActionResult> ShopMenu(int? Id, int? page)
        {
            if(Id != null)
            {
                ViewBag.Id = Id;
                Category ct = await db.Categories.FirstOrDefaultAsync(p => p.Id == Id);
                if(ct == null)
                {
                    return RedirectToAction("Shop", "Store");
                }

                ViewBag.Name = ct.Name;
                ViewBag.Count = db.Products.Where(p => p.CategoryId == Id).Count();
                ViewBag.Page = page;

                var pager = new Pager(ViewBag.Count, page);

                ShopMenuViewModel smvm = new ShopMenuViewModel
                {
                    Products = await db.Products.Include(p => p.Pod_Category).ThenInclude(p => p.Category).Where(p => p.Pod_Category.CategoryId == Id).Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToListAsync(),
                    Categories = await db.Categories.ToListAsync(),
                    Pod_Categories = await db.Pod_Categories.ToListAsync(),
                    Pager = pager
                };
                
                //int c = 4;
                //int b = c * (page - 1);
                //int a = 0;
                //foreach (var t in smvm.Products)
                //for (int i = 0; i < ViewBag.Count; i++)
                //{
                //    //a++;
                //    if (i + 1 >= b && i + 1 <= c * page)
                //    {
                //        smvm.Products2[i] = smvm.Products[i];
                //    }
                //}

                return View(smvm);
            }
            return View();
        }





    }
}