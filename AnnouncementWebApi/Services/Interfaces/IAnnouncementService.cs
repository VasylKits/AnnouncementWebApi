using AnnouncementWebApi.DTOs;
using AnnouncementWebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnnouncementWebApi.Services.Interfaces
{
    public interface IAnnouncementService
    {
        Task<List<AnnouncementResponse>> GetAnnouncementAsync();
        Task<AnnouncementResponse> GetByIdAsync(int id);
        Task<AnnouncementResponse> AddAnnouncementAsync(NewAnnouncement newAnnouncement);
        Task<AnnouncementResponse> EditAnnouncementAsync(EditAnnouncement editAnnouncement);
        Task<int?> DeleteAnnouncementAsync(int id);
        Task<List<AnnouncementResponse>> ShowTopThreeAnnouncementsAsync();
    }
}
