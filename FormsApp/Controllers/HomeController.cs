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
        var allowedExtansions = new[] {".jpg", ".jpeg", ".png", ".gif" };
        // Take File Extansion
        var extansion = Path.GetExtension(imageFile.FileName);
        // Create Random Image Name
        var randomImageName = string.Format($"{Guid.NewGuid()}{extansion}");
        // Take Image Path
        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomImageName);

        // Check Extansion
        if (imageFile != null)
        {
            if (!allowedExtansions.Contains(extansion.ToLower()))
            {
                // Add Model State Error
                ModelState.AddModelError("", $"Please select a valid image file {string.Join(", " , allowedExtansions)}");
            }
        }


        // Validation
        if (ModelState.IsValid)
        {
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

}
