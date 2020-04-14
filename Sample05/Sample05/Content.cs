using System;
using System.Collections.Generic;
using System.Text;

namespace Sample05
{
    public class Content
    {
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string status { get; set; }
        public DateTime add_time { get; set; }
        public DateTime? modify_time { get; set; }
    }

    public class Comment
    {
        public int id { get; set; }
        public int content_id { get; set; }
        public string content { get; set; }
        public DateTime add_time { get; set; } = DateTime.Now;
    }

    public class ContentWithComment
    {
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string status { get; set; }
        public DateTime add_time { get; set; }
        public DateTime? modify_time { get; set; }
        public IEnumerable<Comment> comments { get; set; }
    }
}
