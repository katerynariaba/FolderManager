using AutoMapper;
using FolderManager.Api.Models;
using FolderManager.Db.DomainModels;
using FolderManager.Domain.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FolderManager.Api.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFolderService _folderService;
        private readonly IMapper _mapper;

        public HomeController(IFolderService folderService, IMapper mapper)
        {
            _folderService = folderService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var folders = await _folderService.GetAllAsync();
            var foldersDto = _mapper.Map<List<FolderViewModel>>(folders);

            ViewBag.folders = new SelectList(folders, "Id", "Name");

            return View(foldersDto);
        }
    }
}
