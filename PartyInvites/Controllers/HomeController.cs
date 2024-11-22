using Microsoft.AspNetCore.Mvc;
using PartyInvites.Models;

namespace PartyInvites.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
    
    [HttpGet]
    public ViewResult RsvpForm() => View();

    [HttpPost]
    public ViewResult RsvpForm(GuestResponse guestResponse)
    {
        Repository.AddResponse(guestResponse);
        return View("Thanks", guestResponse);
    }
    
    public ViewResult ListResponses() => View(Repository.Responses.Where(r => r.WillAttend == true));
}
