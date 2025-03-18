using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FormsApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FormsApp.Controllers;

public class HomeController : Controller
{

    public HomeController()
    {

    }

    public IActionResult Index(string search ,string category)
    {
        var products = Repository.Products;

        // Search
        if (!String.IsNullOrEmpty(search))
        {
            ViewBag.Search = search;
            // Case-insensitive search
            products = products.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        // Category
        if (!String.IsNullOrEmpty(category) && category !="0" )
        {
            products = products.Where(p => p.CategoryId == int.Parse(category)).ToList();
        }


        //ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");
        
        
        var model = new ProductViewModel
        {
            Products = products,
            Categories = Repository.Categories,
            SelectedCategory = category,
        };
        return View(model);
    }

    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product model, IFormFile imageFile)
    {

        var extension = ""; 

        // Check Extansion
        if (imageFile != null)
        {
            var allowedExtansions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            // Take File Extansion
            extension = Path.GetExtension(imageFile.FileName);
            if (!allowedExtansions.Contains(extension.ToLower()))
            {
                // Add Model State Error
                ModelState.AddModelError("", $"Please select a valid image file {string.Join(", " , allowedExtansions)}");
            }
        }


        // Validation
        if (ModelState.IsValid)
        {  
                // Create Random Image Name
                var randomImageName = string.Format($"{Guid.NewGuid()}{extension}");
                // Take Image Path
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomImageName);
                // Save Image
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                model.Image = randomImageName;
                Repository.CreateProduct(model);
                return RedirectToAction("Index");
        }
        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");
        return View(model);
    }
    [HttpGet]
    public IActionResult Edit(int id)
    {
        if (id == 0)
        {
            return NotFound();
        }

        var product = Repository.Products.FirstOrDefault(p => p.ProductId == id);

        if (product == null)
        {
            return NotFound();
        }

        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");
        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Product product , IFormFile? imageFile)
    {
        if (id != product.ProductId) 
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            if(imageFile != null)
            {
                // Take File Extansion
                var extansion = Path.GetExtension(imageFile.FileName);
                // Create Random Image Name
                var randomImageName = string.Format($"{Guid.NewGuid()}{extansion}");
                // Take Image Path
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomImageName);
                // Save Image
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                product.Image = randomImageName;
            }
            Repository.UpdateProduct(product);
            return RedirectToAction("Index");
        }
        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");
        return View(product);

    }

    public IActionResult Delete(int? id)
    {
        if (id == null) 
        {
            return NotFound();
        }
        var product = Repository.Products.FirstOrDefault(p => p.ProductId == id);
        if (product == null)
        {
            return NotFound();
        }
        Repository.DeleteProduct(product);

        return RedirectToAction("Index");
    }
}
