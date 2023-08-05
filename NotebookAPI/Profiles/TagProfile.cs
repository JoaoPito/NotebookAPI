using AutoMapper;
using NotebookAPI.Data.DTOs;
using NotebookAPI.Models;

namespace NotebookAPI.Profiles
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<CreateTagDto, Tag>();
            CreateMap<Tag, ReadTagDto>();
            CreateMap<UpdateTagDto, Tag>();
            CreateMap<Tag,UpdateTagDto>();
        }
    }
}