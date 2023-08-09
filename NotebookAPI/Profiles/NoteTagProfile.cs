using AutoMapper;
using NotebookAPI.Data.DTOs;
using NotebookAPI.Models;

namespace NotebookAPI.Profiles;

public class NoteTagProfile : Profile
{
    public NoteTagProfile()
    {
        CreateMap<CreateNoteTagDto, NoteTag>();
        CreateMap<NoteTag, ReadNoteTagDto>();
        CreateMap<UpdateNoteTagDto, NoteTag>();
        CreateMap<NoteTag, UpdateNoteTagDto>();
    }
    
}