using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Newtonsoft.Json;

namespace TestNinja.Mocking
{
    public class VideoService
    {
        private IFileReader _fileReader;

        public VideoService(IFileReader fileReader = null) // fileReader is now optional
        {
            _fileReader = fileReader ?? new FileReader(); // if left value (fileReader) is not null, return it, otherwise return value on right (new FileReader())
        }

        public string ReadVideoTitle()
        {
            var str = _fileReader.Read("video.txt");
            var video = JsonConvert.DeserializeObject<Video>(str);
            if (video == null)
                return "Error parsing the video.";
            return video.Title;
        }

        public string GetUnprocessedVideosAsCsv()
        {
            var videoIds = new List<int>();
            
            using (var context = new VideoContext())
            {
                var videos = 
                    (from video in context.Videos
                    where !video.IsProcessed
                    select video).ToList();
                
                foreach (var v in videos)
                    videoIds.Add(v.Id);

                return String.Join(",", videoIds);
            }
        }
    }

    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsProcessed { get; set; }
    }

    public class VideoContext : DbContext
    {
        public DbSet<Video> Videos { get; set; }
    }
}