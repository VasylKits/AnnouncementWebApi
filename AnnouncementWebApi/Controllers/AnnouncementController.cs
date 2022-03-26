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

        private static List<Announcement> announcements = new List<Announcement>(new[]
        {
            new Announcement() { Id = 1, Title = "First announcement", Description = "Something in announcement, ets....", CreatedDate = DateTime.Now },
            new Announcement() { Id = 2, Title = "Second announce", Description = "This is a different from other each", CreatedDate = DateTime.Now },
            new Announcement() { Id = 3, Title = "Third announcement", Description = "Something in announcement, ets....", CreatedDate = DateTime.Now },
            new Announcement() { Id = 4, Title = "Fourth announcement", Description = "Somet in announce, ets....", CreatedDate = DateTime.Now },
        });

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var announcement = announcements.SingleOrDefault(a => a.Id == id);
            if(announcement == null)
            {
                return NotFound();
            }
            return Ok(announcement);
        }
    }
}