using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MVC_Identity.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminUsersController(UserManager<IdentityUser> userManager) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            IQueryable<IdentityUser> users = userManager.Users;
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            IdentityUser? user = await userManager.FindByIdAsync(id);

            if (user is null)
            {
                ViewBag.ErrorMessage = $"Usuário com Id = {id} não foi encontrado.";
                return View("NotFound");
            }
            else
            {
                IdentityResult result = await userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View("Index");
            }
        }
    }
}
