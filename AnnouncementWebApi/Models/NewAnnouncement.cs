using System;
using System.Collections.Generic;

namespace AnnouncementWebApi
{
    public class NewAnnouncement
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
