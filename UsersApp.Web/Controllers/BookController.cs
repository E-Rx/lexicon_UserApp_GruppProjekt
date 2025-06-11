using Microsoft.AspNetCore.Mvc;

namespace UsersApp.Web.Controllers;

[Route("/book")]
public class BookController : Controller
{
    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }
}
