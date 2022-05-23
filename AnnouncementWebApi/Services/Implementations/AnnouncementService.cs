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
            try
            {
                var announcementList = await _myDbContext.Announcements.ToListAsync();

                if (announcementList.Count == 0)
                    return new BaseResponse<List<AnnouncementResponse>>
                    {
                        IsError = true,
                        ErrorMessage = "Error! Database is empty"
                    };

                var responseAnnouncementList = new List<AnnouncementResponse>();

                foreach (var announcement in announcementList)
                {
                    responseAnnouncementList.Add(new AnnouncementResponse()
                    {
                        Id = announcement.Id,
                        Title = announcement.Title,
                        Description = announcement.Description,
                        CreatedDate = announcement.CreatedDate
                    });
                }

                return new BaseResponse<List<AnnouncementResponse>>
                {
                    Response = responseAnnouncementList,
                    ErrorMessage = "Request completed!"
                };
            }

            catch (Exception ex)
            {
                return new BaseResponse<List<AnnouncementResponse>>
                {
                    IsError = true,
                    ErrorMessage = $"[GetAnnouncementAsync] : {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<AnnouncementResponse>> GetByIdAsync(int id)
        {
            try
            {
                var announcement = await _myDbContext.Announcements.FindAsync(id);
                if (announcement is null)
                    return new BaseResponse<AnnouncementResponse>() 
                    {
                        IsError = true, 
                        ErrorMessage = "Error! Announcement is not found!"
                    };

                return new BaseResponse<AnnouncementResponse>()
                {
                    Response = new AnnouncementResponse()
                    {
                        Id = announcement.Id,
                        Title = announcement.Title,
                        Description = announcement.Description,
                        CreatedDate = announcement.CreatedDate
                    },
                    ErrorMessage = "Request completed!"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<AnnouncementResponse>()
                {
                    IsError = true,
                    ErrorMessage = $"[GetByIdAsync] : {ex.Message}"
                };
            }
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

                if (editAnnouncement is null)
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
            try
            {
                var delAnnouncement = _myDbContext.Announcements.SingleOrDefault(a => a.Id == id);

                _myDbContext.Announcements.Remove(delAnnouncement);
                await _myDbContext.SaveChangesAsync();

                return new BaseResponse<string>
                {
                    Response = $"Successful! Announcement with id={id} was deleted!",
                    ErrorMessage = "Request completed!"
                };
            }

            catch (Exception ex)
            {
                return new BaseResponse<string>()
                {
                    IsError = true,
                    ErrorMessage = $"[DeleteAnnouncementAsync] : {ex.Message}"
                };
            }
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

                if (announcementList.Count is 0)
                    return new BaseResponse<List<AnnouncementResponse>> 
                    { 
                        IsError = true,
                        ErrorMessage = "Error! There are not such announcements" 
                    };

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

                var sortList = compareAnnouncementList.OrderByDescending(x => x.WordCount);

                foreach (var item in sortList)
                {
                    returnAnnouncementList.Add(item.First);
                    returnAnnouncementList.Add(item.Second);
                }

                var returnAnnouncement = returnAnnouncementList.Distinct().Take(3).ToList();

                foreach (var item in returnAnnouncement)
                {
                    var announcementResponse = new AnnouncementResponse() { Id = item.Id, Title = item.Title, Description = item.Description, CreatedDate = item.CreatedDate };
                    responceList.Add(announcementResponse);
                }
                return new BaseResponse<List<AnnouncementResponse>>
                {
                    Response = responceList,
                    ErrorMessage = "Request completed!"
                };
            }

            catch (Exception ex)
            {
                return new BaseResponse<List<AnnouncementResponse>>()
                {
                    IsError = true,
                    ErrorMessage = $"[ShowTopThreeAnnouncementsAsync] : {ex.Message}"
                };
            }
        }
    }
}