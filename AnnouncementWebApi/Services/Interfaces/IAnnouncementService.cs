using AnnouncementWebApi.DTOs;
using AnnouncementWebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnnouncementWebApi.Services.Interfaces
{
    public interface IAnnouncementService
    {
        Task<BaseResponse<List<AnnouncementResponse>>> GetAnnouncementsAsync();
        Task<BaseResponse<AnnouncementResponse>> GetByIdAsync(int id);
        Task<BaseResponse<AnnouncementResponse>> AddAnnouncementAsync(NewAnnouncement newAnnouncement);
        Task<BaseResponse<AnnouncementResponse>> EditAnnouncementAsync(EditAnnouncement editAnnouncement);
        Task<BaseResponse<string>> DeleteAnnouncementAsync(int id);
        Task<BaseResponse<List<AnnouncementResponse>>> ShowTopThreeAnnouncementsAsync();
    }
}
