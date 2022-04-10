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
        readonly DatabaseContext _myDbContext;
        public AnnouncementService(DatabaseContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        public async Task<List<AnnouncementResponse>> GetAnnouncementAsync()
        {
            var varList = await _myDbContext.Announcements.ToListAsync();
            var responceList = new List<AnnouncementResponse>();
            foreach (var item in varList)
            {
                var announcementResponse = new AnnouncementResponse() {Id = item.Id, Title = item.Title, Description = item.Description, CreatedDate = item.CreatedDate };
                responceList.Add(announcementResponse);
            }
            return responceList;
        }
        public async Task<AnnouncementResponse> GetByIdAsync(int id)
        {
            var announcementResponse = new AnnouncementResponse();
            var announcement = await _myDbContext.Announcements.FindAsync(id);
            if (announcement == null)
            {
                throw new Exception();
            }
            {
                announcementResponse.Id = announcement.Id;
                announcementResponse.Title = announcement.Title;
                announcementResponse.Description = announcement.Description;
                announcementResponse.CreatedDate = announcement.CreatedDate;
            }
            return announcementResponse;
        }
        public async Task<AnnouncementResponse> AddAnnouncementAsync(NewAnnouncement newAnnouncement)
        {
            Announcement newAnn = new() { Title = newAnnouncement.Title, Description = newAnnouncement.Description };
            _myDbContext.Announcements.Add(newAnn);
            try
            {
                await _myDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(message: $"{ex.Message}");
            }
            var announcementResponse = new AnnouncementResponse() { Id = newAnn.Id, Title = newAnn.Title, Description = newAnn.Description, CreatedDate = newAnn.CreatedDate };
            return announcementResponse;
        }

        public async Task<AnnouncementResponse> EditAnnouncementAsync(EditAnnouncement editAnnouncement)
        {
            AnnouncementResponse announcementResponse = new();
            var editAnn = await _myDbContext.Announcements.SingleOrDefaultAsync(a => a.Id == editAnnouncement.Id);

            if (editAnn == null)
            {
                throw new Exception();
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
                throw new Exception(message: $"{ex.Message}");
            }
            announcementResponse.Id = editAnn.Id;
            announcementResponse.Title = editAnn.Title;
            announcementResponse.Description = editAnn.Description;
            announcementResponse.CreatedDate = editAnn.CreatedDate;
            return announcementResponse;
        }

        public async Task<int?> DeleteAnnouncementAsync(int id)
        {
            var delAnnouncement = _myDbContext.Announcements.SingleOrDefault(a => a.Id == id);
            if (delAnnouncement == null)
            {
                throw new Exception();
            }
            _myDbContext.Announcements.Remove(delAnnouncement);
            await _myDbContext.SaveChangesAsync();
            return id;
        }

        public async Task<List<AnnouncementResponse>> ShowTopThreeAnnouncementsAsync()
        {
            var varList = await _myDbContext.Announcements.Where(a => a.Title.Contains("announcement")).OrderBy(a => a.Id).Take(3).ToListAsync();
            var responceList = new List<AnnouncementResponse>();
            foreach (var item in varList)
            {
                var announcementResponse = new AnnouncementResponse() { Id = item.Id, Title = item.Title, Description = item.Description, CreatedDate = item.CreatedDate };
                responceList.Add(announcementResponse);
            }
            return responceList;
        }
    }
}