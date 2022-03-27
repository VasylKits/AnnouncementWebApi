using System;

namespace AnnouncementWebApi.Models
{
    public class DeleteAnnoucemente
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}