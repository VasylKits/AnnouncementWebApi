using System;

namespace AnnouncementWebApi.DTOs
{
    public class AnnouncementResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? EditDate { get; set; }

        public AnnouncementResponse()
        {
            CreatedDate = DateTime.Now;
        }
    }
}
