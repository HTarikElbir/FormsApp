using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FormsApp.Models;

namespace FormsApp.Controllers;

public class HomeController : Controller
{

    public HomeController()
    {

    }

    public IActionResult Index(string search)
    {
        var products = Repository.Products;

        // Search
        if (!String.IsNullOrEmpty(search))
        {
            ViewBag.Search = search;
            // Case-insensitive search
            products = products.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        
        return View(products);
    }

    public IActionResult Privacy()
    {
        return View();
    }

   
}
