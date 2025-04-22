using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using NotesProject.Models;
using NotesProject.Context;
using NotesProject.ViewModel;
using Microsoft.AspNetCore.Http.HttpResults;

namespace NotesProject.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class NotesController : ControllerBase
  {
    private readonly DatabaseContext _context;

    public NotesController(DatabaseContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<APIResponseModel<NoContent>>> GetAllNotes()
    {
      try
      {
        var notes = await _context.Notes.ToListAsync();
        return Ok(new APIResponseModel<IEnumerable<NotesModel>>(true, "Notes retrieved successfully", notes, 200));
      }
      catch (Exception ex)
      {
        return StatusCode(500, new APIResponseModel<NoContent>(false, $"An error occurred: {ex.Message}", null, 500));
      }
    }



    [HttpGet("{id}")]
    public async Task<ActionResult<APIResponseModel<NotesModel>>> GetById(int id)
    {
      var note = await _context.Notes.FindAsync(id);

      if (note == null)
      {
        // Return error response if note not found
        var response = new APIResponseModel<NotesModel>(false, "Note not found", null, 404);
        return NotFound(response);
      }

      // Return success response with the found note
      var successResponse = new APIResponseModel<NotesModel>(true, "Note retrieved successfully", note, 200);
      return Ok(successResponse);
    }


    [HttpPost]
    public async Task<ActionResult<APIResponseModel<NotesModel>>> Create(NotesModel note)
    {
      _context.Notes.Add(note);
      await _context.SaveChangesAsync();

      // Return success response with the created note
      var response = new APIResponseModel<NotesModel>(true, "Note created successfully", note, 200);
      return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<APIResponseModel<NotesModel>>> Update(int id, NotesModel note)
    {
      if (id != note.Id)
      {
        // Return BadRequest with an error message if ID mismatch
        var response = new APIResponseModel<NotesModel>(false, "ID mismatch", null, 400);
        return BadRequest(response);
      }

      _context.Entry(note).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!NoteExists(note.Id))
        {
          // Return NotFound if the note does not exist
          var response = new APIResponseModel<NotesModel>(false, "Note not found", null, 404);
          return NotFound(response);
        }
        throw;
      }

      // Return success message after the note is updated
      var successResponse = new APIResponseModel<NotesModel>(true, "Note updated successfully", note, 200);
      return Ok(successResponse);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      var note = await _context.Notes.FindAsync(id);
      if (note == null)
      {
        // Return not found with a message using the APIResponseModel
        var response = new APIResponseModel<NoContent>(false, "Note not found", null, 404);
        return NotFound(response);
      }

      _context.Notes.Remove(note);
      await _context.SaveChangesAsync();

      // Return success message using the APIResponseModel
      var successResponse = new APIResponseModel<NoContent>(true, "Note deleted successfully", null, 200);
      return Ok(successResponse);
    }



    private bool NoteExists(int id)
    {
      return _context.Notes.Any(e => e.Id == id);
    }
  }
}
