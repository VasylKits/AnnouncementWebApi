using System;
using System.DateTime;

public class Announcement
{
	public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? EditDate { get; set; }
}
