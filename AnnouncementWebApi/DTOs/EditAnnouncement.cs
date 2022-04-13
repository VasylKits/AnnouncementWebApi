using System;

namespace AnnouncementWebApi.Models
{
    public class EditAnnouncement
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}