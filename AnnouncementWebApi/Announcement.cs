using System;
using System.Collections.Generic;

namespace AnnouncementWebApi
{
    public class Announcement
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Description { get; set; }
        public DateTime CreatedDate { get; set; }

        public AddedAnnouncement AddAnnouncement()
        {
            return new AddedAnnouncement()
            {
                IdAdded = Id,
                TitleAdded = Title,
                DescriptionAdded = Description,
                CreatedDateAdded = CreatedDate
            };
        }
    }
}
