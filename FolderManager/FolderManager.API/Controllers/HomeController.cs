using AutoMapper;
using FolderManager.Api.Models;
using FolderManager.Db.DomainModels;
using FolderManager.Domain.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FolderManager.Api.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFolderService _folderService;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger,
            IFolderService folderService,
            IMapper mapper)
        {
            _logger = logger;
            _folderService = folderService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var folders = await _folderService.GetAllAsync();
            var foldersDto = _mapper.Map<List<FolderViewModel>>(folders);

            return View(foldersDto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var folder = await _folderService.GetByIdAsync(id);
            var folderDto = _mapper.Map<FolderViewModel>(folder);

            return PartialView("../Shared/_DeleteDialog", folderDto);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(FolderViewModel folderDto)
        {
            var folder = _mapper.Map<Folder>(folderDto);
            await _folderService.DeleteAsync(folder.Id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Add()
        {
            FolderViewModel folder = new FolderViewModel();

            return PartialView("../Shared/_CreateDialog", folder);
        }

        [HttpPost]
        public async Task<IActionResult> Add(FolderViewModel folderDto)
        {
            var parentId = "1cc20c31-aebb-4b1c-b077-1570a54ad2ff";
            var parent = await _folderService.GetByIdAsync(parentId);

            var folder = _mapper.Map<Folder>(folderDto);
            folder.Parent = parent;

            await _folderService.AddAsync(folder);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            var folder = await _folderService.GetByIdAsync(Id);
            var folderDto = _mapper.Map<FolderViewModel>(folder);


            return PartialView("../Shared/_EditDialog", folderDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(FolderViewModel folderDto)
        {
            var folder = _mapper.Map<Folder>(folderDto);

            await _folderService.UpdateAsync(folder);

            return RedirectToAction(nameof(Index));
        }
    }
}
