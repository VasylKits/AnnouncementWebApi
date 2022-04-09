using AnnouncementWebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnnouncementWebApi.Services.Interfaces
{
    public interface IAnnouncementService<AnnouncementResponse>
    {
        Task<List<AnnouncementResponse>> GetAnnouncement();
        Task<AnnouncementResponse> GetById(int id);
        Task<AnnouncementResponse> AddAnnouncement(AnnouncementResponse item);
        Task<AnnouncementResponse> EditAnnouncement(EditAnnouncement editAnnouncement);
        Task<bool> DeleteAnnouncement(int id);
        Task<List<AnnouncementResponse>> ShowTopThreeAnnouncements();
    }
}
