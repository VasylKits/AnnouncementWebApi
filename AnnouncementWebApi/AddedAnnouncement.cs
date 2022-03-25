using System;
using System.Collections.Generic;

namespace AnnouncementWebApi
{
    public class AddedAnnouncement
    {
        public int IdAdded { get; set; }
        public string TitleAdded { get; set; }
        public int DescriptionAdded { get; set; }
        public DateTime CreatedDateAdded { get; set; }

        public List<Announcement> AnnouncementList = new List<Announcement>();
    }
}
