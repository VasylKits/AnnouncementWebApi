using AnnouncementWebApi.Models;
using AnnouncementWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AnnouncementWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class AnnouncementController : ControllerBase
    {
        private readonly IAnnouncementService _announcementService;

        public AnnouncementController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAnnouncementsAsync()
        {
            return Ok(await _announcementService.GetAnnouncementsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await _announcementService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddAnnouncementAsync([FromBody] NewAnnouncement newAnnouncement)
        {
            return Ok(await _announcementService.AddAnnouncementAsync(newAnnouncement));
        }

        [HttpPut]
        public async Task<IActionResult> EditAnnouncementAsync([FromBody] EditAnnouncement editAnnouncement)
        {
            return Ok(await _announcementService.EditAnnouncementAsync(editAnnouncement));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAnnouncementAsync(int id)
        {
            return Ok(await _announcementService.DeleteAnnouncementAsync(id));
        }

        [HttpGet("TOP")]
        public async Task<IActionResult> ShowTopThreeAnnouncementsAsync()
        {
            return Ok(await _announcementService.ShowTopThreeAnnouncementsAsync());
        }
    }
}