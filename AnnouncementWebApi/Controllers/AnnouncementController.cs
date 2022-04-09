using AnnouncementWebApi.Models;
using AnnouncementWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AnnouncementWebApi.Controllers
{
    //заінжектити сервіси через конструктор
    [ApiController]
    [Route("[controller]")]

    public class AnnouncementController : ControllerBase
    {
        //private readonly DatabaseContext _myDbContext = new();

        readonly IAnnouncementService _announcementService = null;

        public AnnouncementController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAnnouncementsAsync()
        {
            return Ok(await _announcementService.GetAnnouncementAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await _announcementService.GetByIdAsync(id));
        //    var announcement = await _myDbContext.Announcements.FindAsync(id);
        //    if (announcement == null)
        //    {
        //        return NotFound();
        //    }
        //   return Ok(announcement);
        }

        [HttpPost]
        public async Task<IActionResult> AddAnnouncementAsync([FromBody] NewAnnouncement newAnnouncement)
        {
            return Ok(await _announcementService.AddAnnouncementAsync(newAnnouncement));
        }

        [HttpPut]
        public async Task<IActionResult> EditAnnouncementAsync(EditAnnouncement editAnnouncement)
        {
            return Ok(await _announcementService.EditAnnouncementAsync(editAnnouncement));
            //    var editAnn = _myDbContext.Announcements.SingleOrDefault(a => a.Id == editAnnouncement.Id);
            //    if (editAnn == null)
            //    {
            //        return NotFound();
            //    }
            //    {
            //        editAnn.Title = editAnnouncement.Title;
            //        editAnn.Description = editAnnouncement.Description;
            //        editAnn.EditDate = DateTime.Now;
            //    }
            //    try
            //    {
            //        await _myDbContext.SaveChangesAsync();
            //    }
            //    catch (Exception ex)
            //    {
            //        return BadRequest($"{ex.Message}");
            //    }
            //    return Ok(editAnnouncement);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAnnouncementAsync(int id)
        {
            await _announcementService.DeleteAnnouncementAsync(id);
            return Ok($"Successful!\nAnnouncement with id={id} was deleted!");

            //var delAnnouncement = _myDbContext.Announcements.SingleOrDefault(a => a.Id == id);
            //if (delAnnouncement == null)
            //{
            //    return NotFound();
            //}
            //_myDbContext.Announcements.Remove(delAnnouncement);
            //await _myDbContext.SaveChangesAsync();
            //return Ok($"Successful!\nAnnouncement with id={id} was deleted!");
        }

        [HttpGet("TOP")]
        public async Task<IActionResult> ShowTopThreeAnnouncementsAsync()
        {
            return Ok(await _announcementService.ShowTopThreeAnnouncementsAsync());
            //return Ok(await _myDbContext.Announcements.Where(a => a.Title.Contains("announcement")).OrderBy(a => a.Id).Take(3).ToListAsync());
        }
    }
}