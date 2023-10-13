using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Models;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using WebCli.Models;

namespace WebClient.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly HttpClient httpClient = null;
    private string ProductApiUrl = "";

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        httpClient = new HttpClient();
        var contentType = new MediaTypeWithQualityHeaderValue("application/json");
        httpClient.DefaultRequestHeaders.Accept.Add(contentType);
        ProductApiUrl = "http://localhost:5198/odata/Books";
    }

    public async Task<IActionResult> Index()
    {
        HttpResponseMessage responseMessage = await httpClient.GetAsync(ProductApiUrl);
        string strData = await responseMessage.Content.ReadAsStringAsync();
        dynamic temp = JObject.Parse(strData);
        var lst = temp.value;
        List<Book> items = ((JArray)temp.value).Select(x => new Book
        {
            Id = (int)x["Id"],
            Author = (string)x["Author"],
            ISBN = (string)x["ISBN"],
            Title = (string)x["Title"],
            Price = (decimal)x["Price"],
        }).ToList();
        foreach (var item in items)
        {
            Console.WriteLine(item.Author);
            Console.WriteLine(item.Title);
        }
        return View(items);
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(FormCollection formCollection)
    {
        if (ModelState.IsValid)
        {
            Book book = new Book { Id = 2, Author = "", Title = "", Price = 0, ISBN = "" };
            book.Title = formCollection["Title"];
            book.ISBN = formCollection["ISBN"];
            book.Price = Convert.ToDecimal(formCollection["Price"]);
            book.Author = formCollection["Author"];
            RedirectToAction("Index");
        }
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

