namespace AnnouncementWebApi.DTOs
{
    public class CompareAnnouncement
    {
        public Announcement First { get; set; }
        public Announcement Second { get; set; }
        public int WordCount { get; set; }
    }
}
