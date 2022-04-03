using AnnouncementWebApi.DB;
using AnnouncementWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnnouncementWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class AnnouncementController : ControllerBase
    {
        private readonly DatabaseContext _myDbContext = new DatabaseContext();

        [HttpGet]
        public async Task<IActionResult> GetAnnouncementsAsync()
        {
            return Ok(await _myDbContext.Announcements.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var announcement = await _myDbContext.Announcements.FindAsync(id);
            if (announcement == null)
            {
                return NotFound();
            }
            return Ok(announcement);
        }

        [HttpPost]
        public async Task<IActionResult> AddAnnouncementAsync([FromBody] NewAnnouncement newAnnouncement)
        {
            Announcement newAnn = new() { Title = newAnnouncement.Title, Description = newAnnouncement.Description };
            _myDbContext.Announcements.Add(newAnn);
            try
            {
                await _myDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok(newAnn);
        }

        [HttpPut]
        public async Task<IActionResult> EditAnnouncementAsync(EditAnnouncement editAnnouncement)
        {
            var editAnn = _myDbContext.Announcements.SingleOrDefault(a => a.Id == editAnnouncement.Id);
            if (editAnn == null)
            {
                return NotFound();
            }
            {
                editAnn.Title = editAnnouncement.Title;
                editAnn.Description = editAnnouncement.Description;
                editAnn.EditDate = DateTime.Now;
            }
            try
            {
                await _myDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok(editAnnouncement);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAnnouncementAsync(int id)
        {
            var delAnnouncement = _myDbContext.Announcements.SingleOrDefault(a => a.Id == id);
            if (delAnnouncement == null)
            {
                return NotFound();
            }
            _myDbContext.Announcements.Remove(delAnnouncement);
            await _myDbContext.SaveChangesAsync();
            return Ok($"Successful!\nAnnouncement with id={id} was deleted!");
        }

        [HttpGet("TOP")]
        public async Task<IActionResult> ShowTopThreeAnnouncementsAsync()
        {
            return Ok(await _myDbContext.Announcements.Where(a => a.Title.Contains("announcement")).OrderBy(a => a.Id).Take(3).ToListAsync());
        }
    }
}