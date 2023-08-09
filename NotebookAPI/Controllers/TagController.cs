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
        public async Task<IActionResult> GetAllTags()
        {
            var tagsList = await _context.Tags.ToListAsync();
            var tagsListDto = _mapper.Map<List<ReadTagDto>>(tagsList);
            return Ok(tagsListDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTag(int id)
        {
            var tag = await _context.Tags.FirstOrDefaultAsync(tag => tag.Id == id);
            if(tag == null) return NotFound();

            var tagDto = _mapper.Map<ReadTagDto>(tag);

            return Ok(tagDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTag([FromBody] CreateTagDto tagDto)
        {
            var tag = _mapper.Map<Tag>(tagDto);
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTag), new { Id = tag.Id }, tag);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTag(int id, [FromBody] UpdateTagDto tagDto) 
        {
            var tag = await _context.Tags.FirstOrDefaultAsync(tag => tag.Id == id);
            if(tag == null) return NotFound();

            _mapper.Map(tagDto, tag);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateTagField(int id, [FromBody]JsonPatchDocument<UpdateTagDto> patch)
        {
            var tag = await _context.Tags.FirstOrDefaultAsync(t => t.Id == id);
            if(tag == null) return NotFound();

            var tagDto = _mapper.Map<UpdateTagDto>(tag);
            patch.ApplyTo(tagDto, ModelState);

            if(!TryValidateModel(tagDto))
                return ValidationProblem(ModelState);

            _mapper.Map(tagDto, tag);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            var tag = await _context.Tags.FirstOrDefaultAsync(t => t.Id == id);
            if(tag == null)
                return NotFound();

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}