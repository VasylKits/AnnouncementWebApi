using System;

namespace AnnouncementWebApi
{
    public class Announcement
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? EditDate { get; set; }

        public Announcement()
        {
            CreatedDate = DateTime.Now;
        }
    }
}
