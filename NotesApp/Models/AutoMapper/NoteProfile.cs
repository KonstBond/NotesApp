using AutoMapper;
using NotesApp.Models.DB.Entities;
using NotesApp.Models.DTO;
using System.Runtime;

namespace NotesApp.Models.AutoMapper
{
    public class NoteProfile : Profile
    {
        public NoteProfile()
        {
            CreateMap<NoteDto, Note>()
                .ReverseMap();
        }
    }
}
