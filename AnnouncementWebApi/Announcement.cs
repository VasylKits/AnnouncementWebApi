using System;
using System.Collections.Generic;

namespace AnnouncementWebApi
{
    public class Announcement
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

        public AddedAnnouncement AddAnnouncement(int id, string title, string description, DateTime createdDate)
        {
            AddedAnnouncement newAnn = new() { Id = id, Title = title, Description = description, CreatedDate = createdDate };
            return newAnn;
        }

        public Announcement EditAnnouncement(Announcement announcement)
        {
            return announcement;
        }
        public Announcement DeleteAnnouncement(Announcement announcement)
        {
            return announcement;
        }
    }
}
