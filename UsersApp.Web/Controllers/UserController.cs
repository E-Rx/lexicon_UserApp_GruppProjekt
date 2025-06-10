using Microsoft.AspNetCore.Mvc;

namespace UsersApp.Web.Controllers;

public class UserController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
