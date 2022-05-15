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

        public async Task<IBaseResponse<AnnouncementResponse>> AddAnnouncementAsync(NewAnnouncement request)
        {
            try
            {
                var newAnnouncement = new Announcement { Title = request.Title, Description = request.Description };

                _myDbContext.Announcements.Add(newAnnouncement);
                await _myDbContext.SaveChangesAsync();

                return await GetByIdAsync(newAnnouncement.Id);
            }
            catch (Exception ex)
            {
                return new BaseResponse<AnnouncementResponse>()
                {
                    IsError = true,
                    ErrorMessage = $"[AddAnnouncementAsync] : {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<AnnouncementResponse>> EditAnnouncementAsync(EditAnnouncement request)
        {
            try
            {
                var editAnnouncement = await _myDbContext.Announcements.SingleOrDefaultAsync(a => a.Id == request.Id);

                if (editAnnouncement == null)
                {
                    return new BaseResponse<AnnouncementResponse>()
                    {
                        IsError = true,
                        ErrorMessage = "Error! Edition announcement is not found!"
                    };
                }

                editAnnouncement.Title = request.Title;
                editAnnouncement.Description = request.Description;
                editAnnouncement.EditDate = DateTime.Now;

                await _myDbContext.SaveChangesAsync();
                return await GetByIdAsync(editAnnouncement.Id);
            }

            catch (Exception ex)
            {
                return new BaseResponse<AnnouncementResponse>()
                {
                    IsError = true,
                    ErrorMessage = $"[EditAnnouncementAsync] : {ex.Message}"
                };
            }
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
            int count = 0;
            var baseResponse = new BaseResponse<List<AnnouncementResponse>>();
            var compareAnnouncementList = new List<CompareAnnouncement>();
            var responceList = new List<AnnouncementResponse>();
            var returnAnnouncementList = new List<Announcement>();

            try
            {
                var announcementList = await _myDbContext.Announcements.ToDictionaryAsync(x => ++count, x => x);
                if (announcementList.Count == 0)
                {
                    baseResponse.IsError = true;
                    baseResponse.ErrorMessage = "Error! There are not such announcements";
                    return baseResponse;
                }

                for (int i = 1; i <= announcementList.Count; i++)
                {
                    var toCompare = $"{announcementList[i].Title} {announcementList[i].Description}".Split('.', ',', ' ', '?');
                    for (int j = i + 1; j <= announcementList.Count; j++)
                    {
                        var toCompareNext = $"{announcementList[j].Title} {announcementList[j].Description}".Split('.', ',', ' ', '?');
                        var wordCount = toCompare.Count(x => toCompareNext.Contains(x));
                        if (wordCount > 0)
                            compareAnnouncementList.Add(new CompareAnnouncement { First = announcementList[i], Second = announcementList[j], WordCount = wordCount });
                    }
                }

                var sortList = compareAnnouncementList.OrderByDescending(x => x.WordCount).Take(3);

                foreach (var item in sortList)
                {
                    returnAnnouncementList.Add(item.First);
                    returnAnnouncementList.Add(item.Second);
                }

                var returnAnnnouncement = returnAnnouncementList.Distinct().ToList();

                foreach (var item in returnAnnnouncement)
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