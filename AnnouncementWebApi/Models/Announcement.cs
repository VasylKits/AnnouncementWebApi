using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnnouncementWebApi
{
    public class Announcement
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? EditDate { get; set; }

        public Announcement()
        {
            CreatedDate = DateTime.Now;
            EditDate = null;
        }
    }
}
