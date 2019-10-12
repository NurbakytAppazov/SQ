using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SqApp.DbData;
using SqApp.Models;
using SqApp.ViewModels;

namespace SqApp.Controllers
{
    //[Authorize(Roles = "Oppo")]
    public class AdminController : Controller
    {
        private AppDbContext db;
        private IHostingEnvironment env;

        public AdminController(AppDbContext _db, IHostingEnvironment _env)
        {
            db = _db;
            env = _env;
        }

        //List Actions
        public IActionResult List()
        {
            return View();
        }
        public async Task<IActionResult> ListProduct()
        {
            ShopViewModel svm = new ShopViewModel
            {
                Products = await db.Products.Include(p => p.Pod_Category).ThenInclude(p => p.Category).ToListAsync(),
                Categories = await db.Categories.ToListAsync()
            };
            return View(svm);
        }
        public ActionResult ListRequest(string reqlist)
        {
            if (reqlist == "ListRequest")
            {
                IQueryable<Request> req = db.Requests;
                return PartialView(req);
            }
            return PartialView("error");
        }
        public ActionResult ListCategory(string ctlist)
        {
            if (ctlist == "ListCategory")
            {
                IQueryable<Category> ct = db.Categories;
                return PartialView(ct);
            }
            return PartialView("error");
        }
        public ActionResult ListPodCategory(string ctname)
        {
            int cid = db.Categories.FirstOrDefault(c => c.Name == ctname).Id;
            IQueryable<Pod_Category> pct = db.Pod_Categories.Where(p => p.CategoryId == cid);

            ViewBag.pctname = db.Categories.FirstOrDefault(c => c.Name == ctname).Name;

            return PartialView(pct);
        }
        public ActionResult PodCategory(string ctmodel)
        {
            int ct = db.Categories.FirstOrDefault(p => p.Name == ctmodel).Id;
            IQueryable<Pod_Category> pc = db.Pod_Categories.Where(p => p.CategoryId == ct);

            return PartialView(pc);
        }

        //Add Actions
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(Category model)
        {
            if (ModelState.IsValid)
            {
                Category category = new Category
                {
                    Name = model.Name
                };
                await db.Categories.AddAsync(category);
                await db.SaveChangesAsync();
                return RedirectToAction("List", "Admin");
            }
            return View(model);
        }

        public IActionResult AddPodCategory()
        {
            ViewBag.Category = db.Categories;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddPodCategory(Pod_Category model, string ctmodel)
        {
            if (ModelState.IsValid)
            {
                int cid = db.Categories.FirstOrDefault(p => p.Name == ctmodel).Id;

                Pod_Category podcat = new Pod_Category
                {
                    Name = model.Name,
                    CategoryId = cid
                };
                await db.Pod_Categories.AddAsync(podcat);
                await db.SaveChangesAsync();
                return RedirectToAction("List", "Admin");
            }
            return View(model);
        }

        public IActionResult AddProduct()
        {
            ViewBag.Category = db.Categories;
            ViewBag.pc = db.Pod_Categories.First();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductModel model, IFormFile PublicImg, string ctmodel, string pctmodel)
        {
            if (ModelState.IsValid)
            {
                if (PublicImg != null)
                {
                    string pimg = "/PublicImages/" + PublicImg.FileName;
                    // сохраняем файл в папку Files в каталоге wwwroot
                    using (var fileStream = new FileStream(env.WebRootPath + pimg, FileMode.Create))
                    {
                        await PublicImg.CopyToAsync(fileStream);
                    }
                }
                int cid = db.Categories.FirstOrDefault(p => p.Name == ctmodel).Id;
                int pcid = db.Pod_Categories.FirstOrDefault(p => p.Name == pctmodel).Id;

                Product product = new Product
                {
                    Name = model.Name,
                    FirstInfo = model.FirstInfo,
                    Price = model.Price,
                    MinPay = model.MinPay,
                    Info1 = model.Info1,
                    Info2 = model.Info2,
                    Info3 = model.Info3,
                    Info4 = model.Info4,
                    Info5 = model.Info5,
                    PublicImage = PublicImg.FileName,
                    CategoryId = cid,
                    Pod_CategoryId = pcid
                };

                await db.Products.AddAsync(product);
                await db.SaveChangesAsync();
                return RedirectToAction("EditProduct", "Admin", new { product.Id });
            }
            return View(model);
        }

        public async Task<IActionResult> AddOnInfo(int? Id, string InName)
        {
            if (Id != null)
            {
                Product pr = await db.Products.FirstOrDefaultAsync(p => p.Id == Id);
                if (pr != null)
                {
                    if (InName != null)
                    {
                        ProductInfo productInfo = new ProductInfo { InfoName = InName, ProductId = pr.Id };

                        await db.ProductInfos.AddAsync(productInfo);
                        await db.SaveChangesAsync();
                        return RedirectToAction("EditProduct", "Admin", new { Id });
                    }
                    return RedirectToAction("EditProduct", "Admin");
                }

            }
            return RedirectToAction("List", "Admin");
        }
        [HttpPost]
        public async Task<IActionResult> AddInInfo(int? Id, string InName, string InVal)
        {
            if (Id != null)
            {
                ProductInfo pr = await db.ProductInfos.FirstOrDefaultAsync(p => p.Id == Id);
                if (pr != null)
                {
                    Id = pr.ProductId;
                    if ((InName != null) && (InVal != null))
                    {
                        InProductInfo productInfo = new InProductInfo { InfoName = InName, InfoValue = InVal, ProductInfoId = pr.Id };

                        await db.InProductInfos.AddAsync(productInfo);
                        await db.SaveChangesAsync();

                        return RedirectToAction("EditProduct", "Admin", new { Id });
                    }
                    return RedirectToAction("EditProduct", "Admin", new { Id });
                }

            }
            return RedirectToAction("List", "Admin");
        }
        [HttpPost]
        public async Task<IActionResult> AddPhoto(int? Id, IFormFile addphoto)
        {
            Product pr = await db.Products.FirstOrDefaultAsync(p => p.Id == Id);
            if ((addphoto != null) && (pr != null))
            {
                string path = "/ProductImages/" + addphoto.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(env.WebRootPath + path, FileMode.Create))
                {
                    await addphoto.CopyToAsync(fileStream);
                }

                ProductImage pm = new ProductImage { ProductId = pr.Id, Url = addphoto.FileName };
                await db.ProductImages.AddAsync(pm);
                await db.SaveChangesAsync();

                return RedirectToAction("EditProduct", "Admin", new { pr.Id });
            }

            return RedirectToAction("EditProduct", "Admin", new { Id });
        }

        //Edit Actions
        public async Task<IActionResult> EditProductMain(int? Id)
        {
            if(Id != null)
            {
                ViewBag.Category = db.Categories;
                ViewBag.pc = db.Pod_Categories.Take(1);

                Product pr = await db.Products.FirstOrDefaultAsync(p => p.Id == Id);

                return View(pr);
            }
            return RedirectToAction("ListProduct", "Admin");
        }
        [HttpPost]
        public async Task<IActionResult> EditProductMain(int? Id, ProductModel model, IFormFile PublicImg, string ctmodel, string pctmodel)
        {
            if (Id != null)
            {
                Product pr = await db.Products.FirstOrDefaultAsync(p => p.Id == Id);
                if (PublicImg != null)
                {
                    string pimg = "/PublicImages/" + PublicImg.FileName;
                    // сохраняем файл в папку Files в каталоге wwwroot
                    using (var fileStream = new FileStream(env.WebRootPath + pimg, FileMode.Create))
                    {
                        await PublicImg.CopyToAsync(fileStream);
                    }
                }
                int cid = db.Categories.FirstOrDefault(p => p.Name == ctmodel).Id;
                int pcid = db.Pod_Categories.FirstOrDefault(p => p.Name == pctmodel).Id;

                if(pr != null)
                {
                    pr.Name = model.Name;
                    pr.FirstInfo = model.FirstInfo;
                    pr.Price = model.Price;
                    pr.MinPay = model.MinPay;
                    pr.Info1 = model.Info1;
                    pr.Info2 = model.Info2;
                    pr.Info3 = model.Info3;
                    pr.Info4 = model.Info4;
                    pr.Info5 = model.Info5;
                    pr.PublicImage = PublicImg.FileName;
                    pr.CategoryId = cid;
                    pr.Pod_CategoryId = pcid;

                    await db.SaveChangesAsync();
                    return RedirectToAction("EditProduct", "Admin", new { pr.Id });
                }
                
            }
            return RedirectToAction("ListProduct", "Admin");
        }
        public async Task<IActionResult> EditProduct(int? Id)
        {
            if (Id != null)
            {
                Product product = await db.Products.Include(p => p.Pod_Category).ThenInclude(p => p.Category).FirstOrDefaultAsync(p => p.Id == Id);
                IEnumerable<Comment> cm = await db.Comments.Where(p => p.ProductId == product.Id).ToListAsync();
                IEnumerable<ProductImage> pi = await db.ProductImages.Where(p => p.ProductId == product.Id).ToListAsync();
                IEnumerable<ProductInfo> pinfo = await db.ProductInfos.Where(p => p.ProductId == product.Id).Include(p => p.InProductInfos).ToListAsync();

                if (product != null)
                {
                    DetailsViewModel dm = new DetailsViewModel
                    {
                        Products = product,
                        Comments = cm,
                        ProductImages = pi,
                        ProductInfos = pinfo
                    };
                    return View(dm);
                }
            }
            return RedirectToAction("ListProduct", "Admin");
        }

        public IActionResult EditCategory(int? Id)
        {
            ViewBag.Id = db.Categories.FirstOrDefault(x => x.Id == Id).Id;
            ViewBag.Name = db.Categories.FirstOrDefault(x => x.Id == Id).Name;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditCategory(Category model)
        {
            var ct = db.Categories.FirstOrDefault(p => p.Id == model.Id);

            if (ct.Id == model.Id)
            {
                ct.Name = model.Name;
                await db.SaveChangesAsync();
            }
            return RedirectToAction("List", "Admin");
        }

        public IActionResult EditPodCategory(int? Id)
        {
            int pcid = db.Pod_Categories.FirstOrDefault(p => p.Id == Id).CategoryId;
            ViewBag.Id = db.Categories.FirstOrDefault(x => x.Id == Id).Id;
            ViewBag.Name = db.Pod_Categories.FirstOrDefault(x => x.Id == Id).Name;
            ViewBag.ctname = db.Categories.FirstOrDefault(x => x.Id == pcid).Name;
            ViewBag.Category = db.Categories.ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditPodCategory(Pod_Category model, string ctmodel)
        {
            var pct = db.Pod_Categories.FirstOrDefault(p => p.Id == model.Id);
            int cid = db.Categories.FirstOrDefault(c => c.Name == ctmodel).Id;
            if (pct.Id == model.Id)
            {
                pct.Name = model.Name;
                pct.CategoryId = cid;
                await db.SaveChangesAsync();
            }
            return RedirectToAction("List", "Admin");
        }

        //Delete Actions
        public async Task<IActionResult> DeleteCategory(int? id)
        {
            if (id != null)
            {
                Category ct = db.Categories.FirstOrDefault(c => c.Id == id);

                db.Categories.Remove(ct);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("List", "Admin");
        }
        public async Task<IActionResult> DeletePodCategory(int? id)
        {
            if (id != null)
            {
                Pod_Category pct = db.Pod_Categories.FirstOrDefault(c => c.Id == id);

                db.Pod_Categories.Remove(pct);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("List", "Admin");
        }

        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if (id != null)
            {
                Product product = db.Products.FirstOrDefault(p => p.Id == id);

                db.Products.Remove(product);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("ListProduct", "Admin");
        }

    }
}