using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FormsApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

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

    public IActionResult Privacy()
    {
        return View();
    }

   
}
