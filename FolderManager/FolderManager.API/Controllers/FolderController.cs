using AutoMapper;
using FolderManager.Domain.Models;
using FolderManager.Db.DomainModels;
using FolderManager.Domain.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace FolderManager.Api.Controllers
{
    public class FolderController : Controller
    {
        private readonly IFolderService _folderService;
        private readonly IMapper _mapper;

        public FolderController(IFolderService folderService, IMapper mapper)
        {
            _folderService = folderService;
            _mapper = mapper;
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

            return View("../Home/Index");
        }

        [HttpGet]
        public IActionResult Add(string id)
        {
            ViewBag.ParentId = id;
            return PartialView("../Shared/_CreateDialog", new FolderCreateModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(FolderCreateModel folderDto, string parentId)
        {
            var parent = await _folderService.GetByIdAsync(parentId);

            var folder = _mapper.Map<Folder>(folderDto);
            folder.Parent = parent;

            await _folderService.AddAsync(folder);

            return View("../Home/Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var folder = await _folderService.GetByIdAsync(id);
            var folderDto = _mapper.Map<FolderEditModel>(folder);
            ViewBag.ParentId = folder.Parent.Id;


            return PartialView("../Shared/_EditDialog", folderDto);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(FolderEditModel folderDto)
        {
            await _folderService.UpdateAsync(folderDto);

            return View("../Home/Index");
        }

        [HttpGet]
        public async Task<IActionResult> Move(string id)
        {
            var folders = await _folderService.GetAllAsync();

            ViewBag.Folders = new SelectList(folders, "Id", "Name");

            return PartialView("../Shared/_MoveDialog", new FolderMoveModel() { FolderId = id });
        }

        [HttpPatch]
        public async Task<IActionResult> Move(FolderMoveModel folderDto)
        {
            await _folderService.MoveAsync(folderDto.FolderId, folderDto.NewParent);

            return View("../Home/Index");
        }
    }
}
