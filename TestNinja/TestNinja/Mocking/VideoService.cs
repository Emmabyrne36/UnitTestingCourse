﻿using System.Collections.Generic;
using System.Data.Entity;
using Newtonsoft.Json;

namespace TestNinja.Mocking
{
    public class VideoService
    {
        private IFileReader _fileReader;
        private IVideoRepository _videoRepository;
        

        public VideoService(IFileReader fileReader = null, IVideoRepository videoRepository = null) // fileReader is now optional
        {
            _fileReader = fileReader ?? new FileReader(); // if left value (fileReader) is not null, return it, otherwise return value on right (new FileReader())
            _videoRepository = videoRepository ?? new VideoRepository();
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

            var videos = _videoRepository.GetUnprocessedVideos();
            foreach (var v in videos)
                videoIds.Add(v.Id);

            return string.Join(",", videoIds);


            //using (var context = new VideoContext())
            //{
            //    var videos =
            //        (from video in context.Videos
            //         where !video.IsProcessed
            //         select video).ToList();

            //    foreach (var v in videos)
            //        videoIds.Add(v.Id);

            //    return string.Join(",", videoIds);
            //}
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