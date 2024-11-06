using InsertKph.Data;
using InsertKph.Dtos;
using InsertKph.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Post([FromBody] IpnDto request)
        {
            // Validate the request
            if (request == null || request.Ipnis == null || !request.Ipnis.Any())
            {
                return BadRequest("Invalid progress note");
            }

            // create a new Progress note entity
            var progressnote = new IpdProgressNote
            {
                an = request.an,
                progress_note_date = request.progress_note_date,
                progress_note_time = request.progress_note_time,
                progress_note_owner_type = request.progress_note_owner_type,
                progress_note_doctor = request.progress_note_doctor,
                progress_note_enter_datetime = request.progress_note_enter_datetime,
                create_user = request.create_user,
                create_datetime = request.create_datetime,
                update_user = request.update_user,
                update_datetime = request.update_datetime,
                version = request.version,
                pre_order_progress_note_id = request.pre_order_progress_note_id,
                pre_order_progress_note_date = request.pre_order_progress_note_date,
                pre_order_progress_note_time = request.pre_order_progress_note_time
            };

            // Add a Progress note to the database
            _myWorldDbContext.IpdProgressNote.Add(progressnote);
            await _myWorldDbContext.SaveChangesAsync();

            // Create progress note items for progress note
            var progressitems = request.Ipnis.Select(i => new IpdProgressNoteItem
            {
                progress_note_id = progressnote.progress_note_id,
                an = request.an,
                progress_note_item_type = i.progress_note_item_type,
                progress_note_item_detail = i.progress_note_item_detail,
                progress_note_item_detail2 = i.progress_note_item_detail2, 
                create_user = request.create_user,
                create_datetime = DateTime.Now.ToString(),
                update_user = request.update_user,
                update_datetime = DateTime.Now.ToString(),
            }).ToList();

            // Add progress note items to the database
            _myWorldDbContext.IpdProgressNoteItems.AddRange(progressitems);
            await _myWorldDbContext.SaveChangesAsync();

            // Return the customer with associated orders
            return Ok(progressnote);
        }

        // get progress note by id
        [HttpGet("{id}")]
        public async Task<ActionResult<IpdProgressNote>> GetProgressNote(int id)
        {
            var progressnote = await  (from a in _myWorldDbContext.IpdProgressNote
                                      join b in _myWorldDbContext.IpdProgressNoteItems
                                      on a.progress_note_id equals b.progress_note_id into items
                                      where a.progress_note_id == id
                                      select new
                                      {
                                        a.progress_note_id,
                                        an = a.an, 
                                        a.progress_note_time, 
                                        a.progress_note_owner_type, 
                                        a.progress_note_doctor,
                                        a.create_user,
                                        a.create_datetime,
                                        a.update_user,
                                        a.update_datetime,
                                        progress_note_items = items.Select(b => new
                                        {
                                                b.progress_note_item_id,
                                                a.progress_note_id,
                                                a.an,
                                                b.progress_note_item_type,
                                                b.progress_note_item_detail,
                                                b.progress_note_item_detail2,
                                                a.create_user,
                                                a.create_datetime,
                                                a.update_user,
                                                a.update_datetime
                                        }).ToList()
                                      })
                                      .FirstOrDefaultAsync();

            if (progressnote == null)
            {
                return NotFound();
            }

            return Ok(progressnote);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIpdProgressNoteItems(int id, [FromBody] List<IpdProgressNoteItem> updatedItems)
        {
            // Step 1: Retrieve the IpdProgressNote based on the given id (although we're not updating it)
            var progressNote = await _myWorldDbContext.IpdProgressNote
                .FirstOrDefaultAsync(pn => pn.progress_note_id == id);

            if (progressNote == null)
            {
                return NotFound();  // If the progress note is not found, return 404
            }

            // Step 2: Iterate over the updated items and update each one
            foreach (var updatedItem in updatedItems)
            {
                var existingItem = await _myWorldDbContext.IpdProgressNoteItems
                    .FirstOrDefaultAsync(item => item.progress_note_item_id == updatedItem.progress_note_item_id 
                                                && item.progress_note_id == id); // Match by item ID and progress note ID

                if (existingItem == null)
                {
                    return NotFound($"Progress note item with ID {updatedItem.progress_note_item_id} not found.");
                }

                // Update the fields of the existing item based on the incoming data
                existingItem.progress_note_item_type = updatedItem.progress_note_item_type;
                existingItem.progress_note_item_detail = updatedItem.progress_note_item_detail;
                existingItem.progress_note_item_detail2 = updatedItem.progress_note_item_detail2;
                existingItem.update_user = updatedItem.update_user;
                existingItem.update_datetime = DateTime.UtcNow.ToString();  // Or any relevant update timestamp
            }

            // Step 3: Save changes to the database
            await _myWorldDbContext.SaveChangesAsync();

            // Return a successful response
            return NoContent();  // 204 No Content indicates that the update was successful
        }

        [HttpDelete("items/{itemId}")]
        public async Task<IActionResult> DeleteIpdProgressNoteItem(int itemId)
        {
            // Step 1: Retrieve the IpdProgressNoteItem based on the given itemId
            var itemToDelete = await _myWorldDbContext.IpdProgressNoteItems
                .FirstOrDefaultAsync(item => item.progress_note_item_id == itemId);

            if (itemToDelete == null)
            {
                return NotFound("Progress note item not found.");
            }

            // Step 2: Retrieve the progress_note_id from the item to check if it's the last item
            int progressNoteId = itemToDelete.progress_note_id;

            // Step 3: Delete the IpdProgressNoteItem
            _myWorldDbContext.IpdProgressNoteItems.Remove(itemToDelete);
            await _myWorldDbContext.SaveChangesAsync();

            // Step 4: Check if there are any remaining IpdProgressNoteItems for this progress_note_id
            var remainingItems = await _myWorldDbContext.IpdProgressNoteItems
                .Where(item => item.progress_note_id == progressNoteId)
                .CountAsync();

            // Step 5: If no items remain, delete the associated IpdProgressNote
            if (remainingItems == 0)
            {
                var progressNote = await _myWorldDbContext.IpdProgressNote
                    .FirstOrDefaultAsync(pn => pn.progress_note_id == progressNoteId);

                if (progressNote != null)
                {
                    _myWorldDbContext.IpdProgressNote.Remove(progressNote);
                    await _myWorldDbContext.SaveChangesAsync();
                }
            }

            // Step 6: Return a response indicating successful deletion
            return NoContent(); // 204 No Content indicates the deletion was successful
        }
    }
}