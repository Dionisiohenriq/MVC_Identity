using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Identity.Context;
using MVC_Identity.Entities;

namespace MVC_Identity.Controllers;

[Authorize]
public class LionsController(AppDbContext context) : Controller
{

    // GET: Lions
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        return View(await context.Lions.ToListAsync());
    }

    // GET: Lions/Details/5
    //[Authorize(Roles = "Admin, Gerente, User")]
    [Authorize(Policy = "RequireUserAdminGerenteRole")]
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null) return NotFound();

        var lion = await context.Lions
            .FirstOrDefaultAsync(m => m.Id == id);
        if (lion == null) return NotFound();

        return View(lion);
    }

    // GET: Lions/Create
    //[Authorize(Roles = "Admin, Gerente, User")]
    [Authorize(Policy = "RequireUserAdminGerenteRole")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: Lions/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    //[Authorize(Roles = "Admin, Gerente, User")]
    [Authorize(Policy = "RequireUserAdminGerenteRole")]
    public async Task<IActionResult> Create([Bind("BirthPlace,Id,Name,Specie,Age")] Lion lion)
    {
        if (ModelState.IsValid)
        {
            context.Add(lion);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(lion);
    }

    // GET: Lions/Edit/5
    [Authorize(Roles = "Admin, Gerente")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var lion = await context.Lions.FindAsync(id);
        if (lion == null) return NotFound();
        return View(lion);
    }

    // POST: Lions/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin, Gerente")]
    public async Task<IActionResult> Edit(Guid id, [Bind("BirthPlace,Id,Name,Specie,Age")] Lion lion)
    {
        if (id != lion.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(lion);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LionExists(lion.Id))
                    return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(lion);
    }

    // GET: Lions/Delete/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null) return NotFound();

        var lion = await context.Lions
            .FirstOrDefaultAsync(m => m.Id == id);
        if (lion == null) return NotFound();

        return View(lion);
    }

    // POST: Lions/Delete/5
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var lion = await context.Lions.FindAsync(id);
        if (lion != null) context.Lions.Remove(lion);

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool LionExists(Guid id)
    {
        return context.Lions.Any(e => e.Id == id);
    }
}