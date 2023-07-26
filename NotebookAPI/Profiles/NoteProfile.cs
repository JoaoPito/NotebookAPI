using AutoMapper;
using NotebookAPI.Data.DTOs;
using NotebookAPI.Models;

namespace NotebookAPI.Profiles;

public class NoteProfile : Profile
{
    public NoteProfile()
    {
        CreateMap<CreateNoteDto, Note>();
        CreateMap<Note, ReadNoteDto>();
        CreateMap<UpdateNoteDto, Note>();
    }
}