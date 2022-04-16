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
        private readonly DatabaseContext _myDbContext;
        public AnnouncementService(DatabaseContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        public async Task<IBaseResponse<List<AnnouncementResponse>>> GetAnnouncementsAsync()
        {
            var baseResponse = new BaseResponse<List<AnnouncementResponse>>();
            try
            {
                var responseAnnouncementList = new List<AnnouncementResponse>();
                var announcementList = await _myDbContext.Announcements.ToListAsync();
                if (announcementList.Count == 0)
                {
                    baseResponse.IsError = true;
                    baseResponse.ErrorMessage = "Error! Database is empty";
                    return baseResponse;
                }

                foreach (var announcement in announcementList)
                {
                    var announcementResponse = new AnnouncementResponse() { Id = announcement.Id, Title = announcement.Title, Description = announcement.Description, CreatedDate = announcement.CreatedDate };
                    responseAnnouncementList.Add(announcementResponse);
                }

                baseResponse.Response = responseAnnouncementList;
                baseResponse.ErrorMessage = "Request completed!";
            }

            catch (Exception ex)
            {
                baseResponse.IsError = true;
                baseResponse.ErrorMessage = $"[GetAnnouncementAsync] : {ex.Message}";
            }
            return baseResponse;
        }

        public async Task<IBaseResponse<AnnouncementResponse>> GetByIdAsync(int id)
        {
            var baseResponse = new BaseResponse<AnnouncementResponse>();
            var announcementResponse = new AnnouncementResponse();
            try
            {
                var announcement = await _myDbContext.Announcements.FindAsync(id);
                if (announcement == null)
                {
                    baseResponse.IsError = true;
                    baseResponse.ErrorMessage = "Error! Announcement is not found!";
                    return baseResponse;
                }
                announcementResponse.Id = announcement.Id;
                announcementResponse.Title = announcement.Title;
                announcementResponse.Description = announcement.Description;
                announcementResponse.CreatedDate = announcement.CreatedDate;

                baseResponse.Response = announcementResponse;
                baseResponse.ErrorMessage = "Request completed!";
            }
            catch (Exception ex)
            {
                baseResponse.IsError = true;
                baseResponse.ErrorMessage = $"[GetByIdAsync] : {ex.Message}";
            }
            return baseResponse;
        }

        public async Task<IBaseResponse<AnnouncementResponse>> AddAnnouncementAsync(NewAnnouncement newAnnouncement)
        {
            var baseResponse = new BaseResponse<AnnouncementResponse>();
            var announcementResponse = new AnnouncementResponse();
            Announcement newAnn = new() { Title = newAnnouncement.Title, Description = newAnnouncement.Description };
            _myDbContext.Announcements.Add(newAnn);
            try
            {
                await _myDbContext.SaveChangesAsync();
                announcementResponse = new AnnouncementResponse() { Id = newAnn.Id, Title = newAnn.Title, Description = newAnn.Description, CreatedDate = newAnn.CreatedDate };
                baseResponse.Response = announcementResponse;
                baseResponse.ErrorMessage = "Request completed!";
            }
            catch (Exception ex)
            {
                baseResponse.IsError = true;
                baseResponse.ErrorMessage = $"[AddAnnouncementAsync] : {ex.Message}";
            }
            return baseResponse;
        }

        public async Task<IBaseResponse<AnnouncementResponse>> EditAnnouncementAsync(EditAnnouncement editAnnouncement)
        {
            var baseResponse = new BaseResponse<AnnouncementResponse>();
            try
            {
                var editAnn = await _myDbContext.Announcements.SingleOrDefaultAsync(a => a.Id == editAnnouncement.Id);
                AnnouncementResponse announcementResponse = new();
                if (editAnn == null)
                {
                    baseResponse.IsError = true;
                    baseResponse.ErrorMessage = "Error! Edition announcement is not found!";
                    return baseResponse;
                }
                editAnn.Title = editAnnouncement.Title;
                editAnn.Description = editAnnouncement.Description;
                editAnn.EditDate = DateTime.Now;

                await _myDbContext.SaveChangesAsync();

                announcementResponse.Id = editAnn.Id;
                announcementResponse.Title = editAnn.Title;
                announcementResponse.Description = editAnn.Description;
                announcementResponse.CreatedDate = editAnn.CreatedDate;

                baseResponse.Response = announcementResponse;
                baseResponse.ErrorMessage = "Request completed!";
            }
            catch (Exception ex)
            {
                baseResponse.IsError = true;
                baseResponse.ErrorMessage = $"[EditAnnouncementAsync] : {ex.Message}";
            }
            return baseResponse;
        }

        public async Task<IBaseResponse<string>> DeleteAnnouncementAsync(int id)
        {
            var baseResponse = new BaseResponse<string>();
            try
            {
                var delAnnouncement = _myDbContext.Announcements.SingleOrDefault(a => a.Id == id);
                if (delAnnouncement.Id != id)
                {
                    baseResponse.IsError = true;
                    baseResponse.ErrorMessage = "Error! Announcement is not found!";
                    return baseResponse;
                } 
                _myDbContext.Announcements.Remove(delAnnouncement);
                baseResponse.Response = $"Successful! Announcement with id={id} was deleted!";
                baseResponse.ErrorMessage = "Request completed!";
                await _myDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                baseResponse.IsError = true;
                baseResponse.ErrorMessage = $"[DeleteAnnouncementAsync] : {ex.Message}";
            }
            return baseResponse;
        }

        public async Task<IBaseResponse<List<AnnouncementResponse>>> ShowTopThreeAnnouncementsAsync()
        {
            var baseResponse = new BaseResponse<List<AnnouncementResponse>>();
            try
            {
                var announcementsList = await _myDbContext.Announcements.Where(a => a.Title.Contains("announcement")).Take(3).ToListAsync();
                if (announcementsList.Count == 0)
                {
                    baseResponse.IsError = true;
                    baseResponse.ErrorMessage = "Error! There are not such announcements";
                    return baseResponse;
                }
                var responceList = new List<AnnouncementResponse>();
                foreach (var item in announcementsList)
                {
                    var announcementResponse = new AnnouncementResponse() { Id = item.Id, Title = item.Title, Description = item.Description, CreatedDate = item.CreatedDate };
                    responceList.Add(announcementResponse);
                }
                baseResponse.Response = responceList;
                baseResponse.ErrorMessage = "Request completed!";
            }
            catch (Exception ex)
            {
                baseResponse.IsError = true;
                baseResponse.ErrorMessage = $"[ShowTopThreeAnnouncementsAsync] : {ex.Message}";
            }
            return baseResponse;
        }
    }
}