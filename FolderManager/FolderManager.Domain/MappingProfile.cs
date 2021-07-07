using AutoMapper;
using FolderManager.Domain.Models;
using FolderManager.Db.DomainModels;

namespace FolderManager.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Folder, FolderViewModel>();
            CreateMap<Folder, FolderEditModel>();
            CreateMap<FolderViewModel, Folder>();
            CreateMap<FolderCreateModel, Folder>();
            CreateMap<FolderEditModel, Folder>();
        }
    }
}
