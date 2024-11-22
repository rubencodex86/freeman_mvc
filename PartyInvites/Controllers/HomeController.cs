using Microsoft.AspNetCore.Mvc;
using PartyInvites.Models;

namespace PartyInvites.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
    
    [HttpGet]
    public ViewResult RsvpForm() => View();
    
    [HttpPost]
    public ViewResult RsvpForm(GuestResponse guestResponse) => View();
        // TODO: store response from guest
}
