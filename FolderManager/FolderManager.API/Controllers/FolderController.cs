using AutoMapper;
using FolderManager.Api.Models;
using FolderManager.Db.DomainModels;
using FolderManager.Domain.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
            var folderDto = _mapper.Map<FolderViewModel>(folder);
            ViewBag.ParentId = folderDto.Parent.Id;


            return PartialView("../Shared/_EditDialog", folderDto);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(FolderViewModel folderDto, string parentId)
        {
            var folder = _mapper.Map<Folder>(folderDto);

            await _folderService.UpdateAsync(folder, parentId);

            return View("../Home/Index");
        }
    }
}
