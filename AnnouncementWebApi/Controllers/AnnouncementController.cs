﻿using AnnouncementWebApi.DB;
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

        [HttpPost]
        public IActionResult AddAnnouncement([FromBody] NewAnnouncement newAnnouncement)
        {
            Announcement newAnn = new() { Id = newAnnouncement.Id, Title = newAnnouncement.Title, Description = newAnnouncement.Description };
            _myDbContext.Add(newAnn);
            return Ok(newAnn);
        }

        [HttpPut]
        public IActionResult EditAnnouncement([FromBody]EditAnnouncement editAnnouncement)
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
                return Ok(editAnn);
            }
//            return Ok(editAnn);
        }

        // Delete item
        [HttpDelete]
        public IActionResult DeleteAnnouncement(int id)
        {
            var delAnnouncement = _myDbContext.Announcements.SingleOrDefault(a => a.Id == id);
            if (delAnnouncement == null)
            {
                return NotFound();
            }
            _myDbContext.Announcements.Remove(delAnnouncement);
            return Ok($"Announcement with id={id} was deleted!");
        }

        [HttpGet]
        public IActionResult GetAllAnnouncement()
        {
            //if ((_myDbContext.Announcements).Count > 0)
                return Ok(_myDbContext.Announcements);
            //return BadRequest();
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

        [HttpGet("TOP")]
        public IActionResult ShowTopThreeAnnouncements()
        {
            return Ok(_myDbContext.Announcements.Where(a => a.Title.Contains("announcement")).OrderBy(a => a.Id).Take(3));
        }
    }
}