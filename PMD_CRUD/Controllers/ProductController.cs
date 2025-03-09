using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PMD_CRUD.Data;
using PMD_CRUD.Models.VModel;
using PMD_CRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace PMD_CRUD.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductDbContext _context;
        private readonly IWebHostEnvironment _en;

        public ProductController(ProductDbContext context, IWebHostEnvironment en)
        {
            _context = context;
            _en = en;
        }
        public IActionResult Index()
        {
            return View(_context.Products.Include(d => d.Details)!.ThenInclude(c => c.Category).OrderByDescending(p => p.PId).ToList());
        }



        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Category = new SelectList(_context.Categories, "CId", "CName").ToList();
            return PartialView("Create"); //when call ajax 
            //return View("Create");
        }
        [HttpPost]
        public IActionResult Create(VModel vModel, int[] CId)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product()
                {
                    PName = vModel.PName,
                    PDate = vModel.PDate,
                    IsAvailable = vModel.IsAvailable,
                    Price = vModel.Price,
                    Quantity = vModel.Quantity,
                };
                if (vModel.ImageFile != null)
                {
                    var file = DateTime.Now.Ticks.ToString() +
                    Path.GetExtension(vModel.ImageFile.FileName);
                    var fileName = _en.WebRootPath + "/Images/" + file;
                    using (var strem = System.IO.File.Create(fileName))
                    {
                        vModel.ImageFile.CopyTo(strem);
                    }
                    product.PImage = "/Images/" + file;
                }
                foreach (var c in CId)
                {
                    Details details = new Details()
                    {
                        Product = product,
                        PId = product.PId,
                        CId = c
                    };
                    _context.Add(details);
                }
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vModel);
        }



        public IActionResult AddCategory(int? id)
        {
            ViewBag.Category = new SelectList(_context.Categories, "CId", "CName", id ??
        0).ToList();
            return PartialView("_addCategory");
        }


        public IActionResult Delete(int? id)
        {
            var product = _context.Products.Find(id);
            var details = _context.Details.Where(p => p.PId == product.PId).ToList();
            _context.Details.RemoveRange(details);
            _context.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }



        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ViewBag.Category = new SelectList(_context.Categories, "CId", "CName").ToList();
            var product = _context.Products.Find(id);
            var details = _context.Details.Where(p => p.PId == product!.PId).ToList();
            var pObj = new VModel()
            {
                PId = product.PId,
                PName = product.PName,
                Details = details,
                PImage = product.PImage,
                IsAvailable = product.IsAvailable,
                PDate = product.PDate,
                Price = product.Price,
                Quantity = product.Quantity,
            };
            return PartialView("Edit", pObj); //when call ajax 
            //return View(pObj);
        }
        [HttpPost]
        public IActionResult Edit(VModel vModel, int[] CId)
        {
            if (ModelState.IsValid)
            {
                var exProduct = _context.Products.Find(vModel.PId);
                var exDetails = _context.Details.Where(p => p.PId == exProduct.PId).ToList();

                exProduct.PName = vModel.PName;
                exProduct.PDate = vModel.PDate;
                exProduct.IsAvailable = vModel.IsAvailable;
                exProduct.Price = vModel.Price;
                exProduct.Quantity = vModel.Quantity;

                if (vModel.ImageFile != null)
                {
                    var file = DateTime.Now.Ticks.ToString() +
        Path.GetExtension(vModel.ImageFile.FileName);
                    var fileName = _en.WebRootPath + "/Images/" + file;
                    using (var strem = System.IO.File.Create(fileName))
                    {
                        vModel.ImageFile.CopyTo(strem);
                    }
                    exProduct.PImage = "/Images/" + file;
                }
                else
                {
                    exProduct.PImage = exProduct.PImage;
                }
                _context.Details.RemoveRange(exDetails);
                foreach (var c in CId)
                {
                    Details details = new Details()
                    {
                        Product = exProduct,
                        PId = exProduct.PId,
                        CId = c
                    };
                    _context.Add(details);
                }
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vModel);
        }
    }
}
