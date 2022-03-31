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
        private readonly MyDbContext _myDbContext = new MyDbContext();
        // в контролерах методи - прочитати в метаніті, які є типи повернень і атрибути для них
        // в сервісах логіка для екшенів контролера
        // в інтерфейсах створити інтерфейс і оголосити в ньому методи, які потрібні для реалізація завдання CRUD
        // в імплемент має бути реалізація методів() EditAnnoun, AddAn, DelAnn..
        // GitHub  і контролери

        //private static List<Announcement> announcements = new(new[]
        //{
        //    new Announcement() { Id = 1, Title = "First announcement", Description = "Something in announcement, ets....", CreatedDate = DateTime.Now },
        //    new Announcement() { Id = 2, Title = "Second announce", Description = "This is a different from other each", CreatedDate = DateTime.Now },
        //    new Announcement() { Id = 3, Title = "Third announcement", Description = "Something in announcement, ets....", CreatedDate = DateTime.Now },
        //    new Announcement() { Id = 4, Title = "Fourth announcement", Description = "Somet in announce, ets....", CreatedDate = DateTime.Now },
        //});

        [HttpGet]
        public IActionResult GetAllAnnouncement()
        {
            return Ok(_myDbContext.Announcements.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var announcement = _myDbContext.Announcements.SingleOrDefault(a => a.Id == id);
            if (announcement == null)
            {
                return NotFound();
            }
            return Ok(announcement);
        }

        [HttpPost]
        public IActionResult AddAnnouncement([FromBody] NewAnnouncement newAnnouncement)
        {
            Announcement newAnn = new() { Id = newAnnouncement.Id, Title = newAnnouncement.Title, Description = newAnnouncement.Description };
            _myDbContext.Announcements.Add(newAnn);

            try
            {
                _myDbContext.SaveChanges();
            }
            catch
            {
                throw new Exception();
            }
            //return CreatedAtAction("GetAllAnnouncement", newAnn);
            return Ok(newAnn);
        }

        [HttpPut]
        public IActionResult EditAnnouncement([FromBody] EditAnnouncement editAnnouncement)
        {
            var editAnn = _myDbContext.Announcements.SingleOrDefault(a => a.Id == editAnnouncement.Id);
            if (editAnn == null)
            {
                return BadRequest();
            }

            _myDbContext.Entry(editAnn).State = EntityState.Modified;

            try
            {
                editAnn.Title = editAnnouncement.Title;
                editAnn.Description = editAnnouncement.Description;
                editAnn.EditDate = DateTime.Now;
                _myDbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return Ok(editAnn);
        }

        [HttpDelete]
        public IActionResult DeleteAnnouncement(int id)
        {
            var delAnnouncement = _myDbContext.Announcements.Find(id);

            if (delAnnouncement == null)
            {
                return NotFound();
            }

            _myDbContext.Announcements.Remove(delAnnouncement);

            try
            {
                _myDbContext.SaveChanges();
            }
            catch
            {
                throw new Exception();
            }
            return Ok($"Announcement with id={id} was deleted!");
        }

        [HttpGet("TOP")]
        public IActionResult ShowTopThreeAnnouncements()
        {
            return Ok(_myDbContext.Announcements.Where(a => a.Title.Contains("announcement")).OrderBy(a => a.Id).Take(3));
        }
    }
}