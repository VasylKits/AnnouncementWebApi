using System;
using System.Collections.Generic;

namespace AnnouncementWebApi.Models
{
    public class AddedAnnouncement
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<Announcement> AnnouncementList = new();
    }
}
