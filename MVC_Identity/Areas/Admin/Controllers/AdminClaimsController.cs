using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Identity.Areas.Admin.Models;
using System.Security.Claims;

namespace MVC_Identity.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminClaimsController(UserManager<IdentityUser> userManager) : Controller
    {
        public IActionResult Index()
        {
            var users = userManager.Users;

            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                ModelState.AddModelError("", "Usuário não encontrado.");
                return View();
            }

            var userClaims = await userManager.GetClaimsAsync(user);

            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Claims = userClaims.ToList(),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                ModelState.AddModelError("", "Usuário não encontrado.");
                return View(model);
            }
            else
            {
                user.Email = model.Email;
                user.UserName = model.UserName;

                var result = await userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                Errors(result);

                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> Create_Post(string claimType, string claimValue, string userId)
        {
            if (claimType is null || claimValue is null)
            {
                ModelState.AddModelError("", "Tipo/Valor da Claim deve ser informado.");
                return View();
            }

            IdentityUser? user = await userManager.FindByIdAsync(userId);

            if (user is not null)
            {
                var userClaims = await userManager.GetClaimsAsync(user);

                Claim? claim = userClaims.FirstOrDefault(t => t.Type.Equals(claimType) && t.Value.Equals(claimValue));

                if (claim is null)
                {
                    Claim newClaim = new Claim(claimType, claimValue);
                    IdentityResult result = await userManager.AddClaimAsync(user, newClaim);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Errors(result);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Claim já existente.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Usuário não encontrado.");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteClaim(string claimValues)
        {
            string[] claimValuesArray = claimValues.Split(';');
            string claimType = claimValuesArray[0];
            string claimValue = claimValuesArray[1];
            string userId = claimValuesArray[2];

            IdentityUser user = await userManager.FindByIdAsync(userId);

            if (user is not null)
            {
                var userClaims = await userManager.GetClaimsAsync(user);

                Claim? claim = userClaims.FirstOrDefault(c => c.Type.Equals(claimType) && c.Value.Equals(claimValue));

                IdentityResult result = await userManager.RemoveClaimAsync(user, claim);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    Errors(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "Usuário não encontrado");
            }

            return View("Index");
        }

        private void Errors(IdentityResult result)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
    }
}
