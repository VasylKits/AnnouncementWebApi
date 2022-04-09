using AnnouncementWebApi.DB;
using AnnouncementWebApi.DTOs;
using AnnouncementWebApi.Models;
using AnnouncementWebApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnnouncementWebApi.Services.Implementations
{
    public class AnnouncementService : IAnnouncementService
    {
        //заінжектити використовуючи констуктор класу 
        readonly DatabaseContext _myDbContext = null;
        public AnnouncementService(DatabaseContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        public async Task<AnnouncementResponse> AddAnnouncementAsync(NewAnnouncement newAnnouncement)
        {
            Announcement newAnn = new() { Title = newAnnouncement.Title, Description = newAnnouncement.Description };
            _myDbContext.Announcements.Add(newAnn);
            var announcementResponse = new AnnouncementResponse();
            try
            {
                await _myDbContext.SaveChangesAsync();
                announcementResponse = new AnnouncementResponse() { Title = newAnn.Title, Description = newAnn.Description };
            }
            catch (Exception ex)
            {
                throw new Exception(message: $"{ex.Message}");
            }
            return announcementResponse;
        }

        public async Task<List<AnnouncementResponse>> GetAnnouncementAsync()
        {
            var varList = await _myDbContext.Announcements.ToListAsync();
            var responceList = new List<AnnouncementResponse>();
            foreach (var item in varList)
            {
                var announcementResponse = new AnnouncementResponse() { Title = item.Title, Description = item.Description };
                responceList.Add(announcementResponse);
            }
            return responceList;
        }

        public async Task<AnnouncementResponse> EditAnnouncementAsync(EditAnnouncement editAnnouncement)
        {
            var editAnn = _myDbContext.Announcements.SingleOrDefault(a => a.Id == editAnnouncement.Id);
            if (editAnn == null)
            {
                throw new NotImplementedException();
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
                throw new NotImplementedException();
            }
            throw new NotImplementedException();
        }

        public async Task<AnnouncementResponse> GetByIdAsync(int id)
        {
            var announcement = await _myDbContext.Announcements.FindAsync(id);
            if (announcement == null)
            {
                throw new Exception();
            }
            return announcement;
            throw new NotImplementedException();
        }

        public async Task<int?> DeleteAnnouncementAsync(int id)
        {
            var delAnnouncement = _myDbContext.Announcements.SingleOrDefault(a => a.Id == id);
            if (delAnnouncement == null)
            {
                return null;
            }
            _myDbContext.Announcements.Remove(delAnnouncement);
            await _myDbContext.SaveChangesAsync();
            return id;
        }

        public async Task<List<AnnouncementResponse>> ShowTopThreeAnnouncementsAsync()
        {
            throw new NotImplementedException();
        }
    }
}