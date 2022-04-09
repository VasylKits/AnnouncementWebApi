using AnnouncementWebApi.DB;
using AnnouncementWebApi.DTOs;
using AnnouncementWebApi.Models;
using AnnouncementWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnnouncementWebApi.Services.Implementations
{
    public class AnnouncementService : IAnnouncementService<AnnouncementResponse>
    {
        private readonly DatabaseContext _myDbContext = new DatabaseContext();

        public Task<AnnouncementResponse> AddAnnouncement(AnnouncementResponse item)
        {
            throw new NotImplementedException();
        }
        public Task<List<AnnouncementResponse>> GetAnnouncement()
        {
            throw new NotImplementedException();
        }
     
        public Task<AnnouncementResponse> EditAnnouncement(EditAnnouncement editAnnouncement)
        {
            throw new NotImplementedException();
        }

        public Task<AnnouncementResponse> GetById(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> DeleteAnnouncement(int id)
        {
            var delAnnouncement = _myDbContext.Announcements.SingleOrDefault(a => a.Id == id);
            if (delAnnouncement == null)
            {
                return false;
            }
            _myDbContext.Announcements.Remove(delAnnouncement);
            await _myDbContext.SaveChangesAsync();
            return true;
        }

        public Task<List<AnnouncementResponse>> ShowTopThreeAnnouncements()
        {
            throw new NotImplementedException();
        }
    }
}