using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotebookAPI.Data;
using NotebookAPI.Data.DTOs;
using NotebookAPI.Models;

namespace NotebookAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TagController : ControllerBase
    {
        private readonly NoteContext _context;
        private readonly IMapper _mapper;
        public TagController(NoteContext context, IMapper mapper)
        {
            this._mapper = mapper;
            this._context = context;
        }

        [HttpGet]
        public IActionResult GetAllTags()
        {
            var tagsListDto = _mapper.Map<List<ReadTagDto>>(_context.Tags.ToList());
            return Ok(tagsListDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetTag(int id)
        {
            var tag = _context.Tags.FirstOrDefault(tag => tag.Id == id);
            if(tag == null) return NotFound();

            var tagDto = _mapper.Map<ReadTagDto>(tag);

            return Ok(tagDto);
        }

        [HttpPost]
        public IActionResult CreateTag([FromBody] CreateTagDto tagDto)
        {
            var tag = _mapper.Map<Tag>(tagDto);
            _context.Tags.Add(tag);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetTag), new { Id = tag.Id }, tag);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTag(int id, [FromBody] UpdateTagDto tagDto) 
        {
            var tag = _context.Tags.FirstOrDefault(tag => tag.Id == id);
            if(tag == null) return NotFound();

            _mapper.Map(tagDto, tag);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateTagField(int id, [FromBody]JsonPatchDocument<UpdateTagDto> patch)
        {
            var tag = _context.Tags.FirstOrDefault(t => t.Id == id);
            if(tag == null) return NotFound();

            var tagDto = _mapper.Map<UpdateTagDto>(tag);
            patch.ApplyTo(tagDto, ModelState);

            if(!TryValidateModel(tagDto))
                return ValidationProblem(ModelState);

            _mapper.Map(tagDto, tag);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTag(int id)
        {
            var tag = _context.Tags.FirstOrDefault(t => t.Id == id);
            if(tag == null)
                return NotFound();

            _context.Tags.Remove(tag);
            _context.SaveChanges();
            return NoContent();
        }
    }
}