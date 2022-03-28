using AnnouncementWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnnouncementWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class AnnouncementController : ControllerBase
    {
        // в контролерах методи - прочитати в метаніті, які є типи повернень і атрибути для них
        // в сервісах логіка для екшенів контролера
        // в інтерфейсах створити інтерфейс і оголосити в ньому методи, які потрібні для реалізація завдання CRUD
        // в імплемент має бути реалізація методів() EditAnnoun, AddAn, DelAnn..
        // GitHub  і контролери

        private static List<Announcement> announcements = new(new[]
        {
            new Announcement() { Id = 1, Title = "First announcement", Description = "Something in announcement, ets....", CreatedDate = DateTime.Now },
            new Announcement() { Id = 2, Title = "Second announce", Description = "This is a different from other each", CreatedDate = DateTime.Now },
            new Announcement() { Id = 3, Title = "Third announcement", Description = "Something in announcement, ets....", CreatedDate = DateTime.Now },
            new Announcement() { Id = 4, Title = "Fourth announcement", Description = "Somet in announce, ets....", CreatedDate = DateTime.Now },
        });

        [HttpPost]
        public IActionResult AddAnnouncement([FromBody] NewAnnouncement newAnnouncement)
        {
            Announcement newAnn = new() { Id = newAnnouncement.Id, Title = newAnnouncement.Title, Description = newAnnouncement.Description };
            announcements.Add(newAnn);
            return Ok(newAnn);
        }

        [HttpPut]
        public IActionResult EditAnnouncement(EditAnnouncement editAnnouncement)
        {
            return Ok("edit");
        }

        // Delete item
        [HttpDelete]
        public IActionResult DeleteAnnouncement(int id)
        {
            var delAnnouncement = announcements.SingleOrDefault(a => a.Id == id);
            if (delAnnouncement == null)
            {
                return NotFound();
            }
            announcements.Remove(delAnnouncement);
            return Ok($"Announcement with id={id} was deleted!");
        }

        [HttpGet]
        public IActionResult GetAllAnnouncement()
        {
            if (announcements.Count > 0)
                return Ok(announcements);
            return BadRequest();
        }

        [HttpGet("{id}")]
        public IActionResult GetId(int id)
        {
            var announcement = announcements.SingleOrDefault(a => a.Id == id);
            if (announcement == null)
            {
                return NotFound();
            }
            return Ok(announcement);
        }

        [HttpGet("TOP")]
        public IActionResult ShopTopThreeAnnouncements()
        {
            var result = from a in announcements
                         where a.Title.Contains("announcement")
                         orderby a.Id
                         select a;
                return Ok(result);
        }
    }
}