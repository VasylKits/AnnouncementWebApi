//using System;
//using System.Linq;
//using System.Collections.Generic;

//namespace ConsoleApp1
//{
//    internal class Program
//    {
//        static void Main(string[] args)
//        {
//            var announcements = new List<Announcement>()
//            {
//                new Announcement() {Id = 1, Title = "First announcement", Description = "New announcement about car" },
//                new Announcement() {Id = 2, Title = "Announcement second", Description = "This is description" },
//                new Announcement() {Id = 3, Title = "About car", Description = "New build announcement by my" },
//                new Announcement() {Id = 4, Title = "About moto", Description = "Motocycle announcement" }
//            };

//            ShowTop3(announcements);
//            Console.ReadLine();
//        }
//        static void ShowTop3(List<Announcement> announcements)
//        {
//            int count = 0;

//            var compareAnnouncementList = new List<CompareAnnouncement>();
            
//            var returnAnnouncementList = new List<Announcement>();

//            var announcementList = announcements.ToDictionary(x => ++count, x => x);
//            for (int i = 1; i <= announcementList.Count; i++)
//            {
//                var toCompare = $"{announcementList[i].Title} {announcementList[i].Description}".Split('.', ',', ' ', '?');

//                for (int j = i + 1; j <= announcementList.Count; j++)
//                {
//                    var toCompareNext = $"{announcementList[j].Title} {announcementList[j].Description}".Split('.', ',', ' ', '?');
//                    var wordCount = toCompare.Count(x => toCompareNext.Contains(x));
//                    compareAnnouncementList.Add(new CompareAnnouncement { First = announcementList[i], Second = announcementList[j], WordCount = wordCount});
//                }
//            }
//            var sortList = compareAnnouncementList.OrderByDescending(x => x.WordCount).Take(3);
//            foreach (var item in sortList)
//            {
//                returnAnnouncementList.Add(item.First);
//                returnAnnouncementList.Add(item.Second);
//            }
//            var returnAnnnouncement = returnAnnouncementList.Distinct().ToList();
//            foreach(var item in returnAnnnouncement)
//            {
//                Console.WriteLine(item.Id);
//            }
//        }
//    }
//    public class Announcement
//    {
//        public int Id { get; set; }
//        public string Title { get; set; }
//        public string Description { get; set; }
//    }
//    public class CompareAnnouncement
//    {
//        public Announcement First { get; set; }
//        public Announcement Second { get; set; }
//        public int WordCount { get; set; }
//    }
//}