using AutoMapper;
using FolderManager.Api.Models;
using FolderManager.Db.DomainModels;

namespace FolderManager.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Folder, FolderViewModel>();
            CreateMap<FolderViewModel, Folder>();
            CreateMap<FolderCreateModel, Folder>();
        }
    }
}
