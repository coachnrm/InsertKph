using InsertKph.Data;
using InsertKph.Dtos;
using InsertKph.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace InsertKph.Controllers 
{
    [ApiController]
    [Route("[controller]")]
    public class ProgressNoteController : ControllerBase
    {
        private readonly MyWorldDbContext _myWorldDbContext;
        private readonly IMapper _mapper;
        public ProgressNoteController(MyWorldDbContext myWorldDbContext, IMapper mapper)
        {
            _myWorldDbContext = myWorldDbContext;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(IpnDto payloadProgress)
        {
            var newProgress = _mapper.Map<IpdProgressNote>(payloadProgress);
            _myWorldDbContext.IpdProgressNote.Add(newProgress);
            await _myWorldDbContext.SaveChangesAsync();
            return Created($"/progressnote/{newProgress.progress_note_id}", newProgress);
        }
    }
}