using AnnouncementWebApi.DTOs;
using AnnouncementWebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnnouncementWebApi.Services.Interfaces
{
    public interface IAnnouncementService
    {
        Task<IBaseResponse<List<AnnouncementResponse>>> GetAnnouncementsAsync();
        Task<IBaseResponse<AnnouncementResponse>> GetByIdAsync(int id);
        Task<IBaseResponse<AnnouncementResponse>> AddAnnouncementAsync(NewAnnouncement newAnnouncement);
        Task<IBaseResponse<AnnouncementResponse>> EditAnnouncementAsync(EditAnnouncement editAnnouncement);
        Task<IBaseResponse<string>> DeleteAnnouncementAsync(int id);
        Task<IBaseResponse<List<AnnouncementResponse>>> ShowTopThreeAnnouncementsAsync();
    }
}
