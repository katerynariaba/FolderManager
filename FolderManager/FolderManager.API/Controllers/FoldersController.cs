using FolderManager.Db.DomainModels;
using FolderManager.Domain.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FolderManager.Api.Controllers
{
    public class FoldersController : Controller
    {
        private readonly IFolderService _folderService;

        public FoldersController(IFolderService folderService)
        {
            _folderService = folderService;
        }

        //// GET: Folders
        //public async Task<IActionResult> Index()
        //{
        //    var folders = await _folderService.GetAllAsync();

        //    return View(folders);
        //}

        //// GET: Folders
        //public async Task<IActionResult> Index()
        //{
        //    var root = await _folderService.GetRootAsync();

        //    return View(root);
        //}

        // GET: Folders
        public async Task<IActionResult> Index()
        {
            List<Folder> nodes = new List<Folder>();

            var roots = await _folderService.GetRootAsync();

            //Loop and add the Parent Nodes.
            foreach (var root in roots)
            {
                nodes.Add(new Folder
                {
                    Id = root.Id.ToString(),
                    Name = root.Name.ToString()
                });
            }


            var children = await _folderService.GetChildrenAsync("1");


            //Loop and add the Child Nodes.
            foreach (var child in children)
            {
                nodes.Add(new Folder
                {
                    Id = child.Id.ToString(),
                    Name = child.Name.ToString()
                });
            }

            return View(nodes);
        }

        //// GET: Folders/Details/5
        //public async Task<IActionResult> Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var folder = await _context.Folders
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (folder == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(folder);
        //}

        //// GET: Folders/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Folders/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Name,Path")] Folder folder)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(folder);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(folder);
        //}

        //// GET: Folders/Edit/5
        //public async Task<IActionResult> Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var folder = await _context.Folders.FindAsync(id);
        //    if (folder == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(folder);
        //}

        //// POST: Folders/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Path")] Folder folder)
        //{
        //    if (id != folder.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(folder);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!FolderExists(folder.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(folder);
        //}

        //// GET: Folders/Delete/5
        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var folder = await _context.Folders
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (folder == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(folder);
        //}

        //// POST: Folders/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string id)
        //{
        //    var folder = await _context.Folders.FindAsync(id);
        //    _context.Folders.Remove(folder);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool FolderExists(string id)
        //{
        //    return _context.Folders.Any(e => e.Id == id);
        //}
    }
}
