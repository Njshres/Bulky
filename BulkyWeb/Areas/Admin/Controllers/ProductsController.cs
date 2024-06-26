using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
//using BulkyWeb.ViewModel;
//using System.Web.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductsController(IProductRepository context, ICategoryRepository db, IWebHostEnvironment webHostEnvironment)
        {
            _productRepo = context;
            _categoryRepo = db;
            _webHostEnvironment = webHostEnvironment;
        }
      

        //public ProductsController(IProductRepository context)
        //{
        //    _productRepo = context;
        //}
        // GET: Products
        public IActionResult Index()
        {
            List<Product> objProductList = _productRepo.GetAll(includeproperties: "Category").ToList();
            return View(objProductList);
        }

        // GET: Products/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productRepo.Get(u => u.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Upsert(int? id)
        {
            //var model = new ProductVM
            //{
            //    CategoryList = _categoryRepo.GetAll().Select(c => new SelectListItem
            //    {
            //        Text = c.Name,
            //        Value = c.Id.ToString()

            //    }).ToList(),

            //};

            ProductVM productVM = new()
            {
                CategoryList = _categoryRepo.GetAll()
               .Select(u => new SelectListItem
               {
                   Text = u.Name,
                   Value = u.Id.ToString(),
               }),
                Product = new Product()
            };
            if (id == null || id == 0)
            {
                //create
                return View(productVM);
            }
            else
            {
                //update
                productVM.Product = _productRepo.Get(u => u.Id == id);
                return View(productVM);
            }
        }

                // POST: Products/Create
                 // To protect from overposting attacks, enable the specific properties you want to bind to.
                // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
         [HttpPost]
         [ValidateAntiForgeryToken]
        //public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        //   {
        //   if (ModelState.IsValid)
        //   {
        //           if (productVM.Product.Id == 0)
        //           {
        //               _productRepo.Add(productVM.Product);
        //           }
        //           else
        //           {
        //              _productRepo.Update(productVM.Product);
        //           }

        //           string wwwRootPath = _webHostEnvironment.WebRootPath;
        //       if (file != null)
        //       {
        //           string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        //           string productPath =Path.Combine(wwwRootPath, @"images\product\");
        //           //string finalPath = Path.Combine(wwwRootPath, productPath);
        //           if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
        //           {
        //               var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));
        //               if (System.IO.File.Exists(oldImagePath))
        //               {
        //                   System.IO.File.Delete(oldImagePath);
        //               }
        //           }

        //           using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
        //           {
        //               file.CopyTo(fileStream);
        //           }
        //           productVM.Product.ImageUrl = @"\images\product\" + fileName;

        //       }
        //       if(productVM.Product.Id == 0)
        //       {
        //           _productRepo.Add(productVM.Product);
        //       }
        //       else
        //       {
        //           _productRepo.Update(productVM.Product);
        //       }

        //       _productRepo.Save();
        //       TempData["success"] = "Product added successfully";
        //       return RedirectToAction(nameof(Index));
        //   }
        //   return View();
        //}
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        //delete the old image
                        var oldImagePath =
                            Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                }

                if (productVM.Product.Id == 0)
                {
                    _productRepo.Add(productVM.Product);
                }
                else
                {
                    _productRepo.Update(productVM.Product);
                }

                _productRepo.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _categoryRepo.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(productVM);
            }
        }
        // GET: Products/Edit/5


        // GET: Products/Delete/5
        //  public IActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var product = _productRepo.Get(u => u.Id == id);

        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(product);
        //}

        //// POST: Products/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public IActionResult DeleteConfirmed(int id)
        //{
        //    var product = _productRepo.Get(u => u.Id == id);
        //    if (product != null)
        //    {
        //        _productRepo.Remove(product);
        //    }

        //    _productRepo.Save();
        //    TempData["success"] = "Product deleted successfully";
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool CategoryExists(int id)
        //{
        //    return _context.Categories.Any(e => e.Id == id);
        //}
        #region Api Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProductList = _productRepo.GetAll(includeproperties: "Category").ToList();
            return Json(new { data = objProductList } );


        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productToBeDeleted = _productRepo.Get(u => u.Id == id);
               if (productToBeDeleted == null)
                {
                    return Json(new { success = false, message = "Error while deleting" });
                }

                var oldImagePath =
                               Path.Combine(_webHostEnvironment.WebRootPath,
                               productToBeDeleted.ImageUrl.TrimStart('\\'));

                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }

                _productRepo.Remove(productToBeDeleted);
                _productRepo.Save();

                return Json(new { success = true, message = "Delete Successful" });
          }
        
        #endregion
    }
}
