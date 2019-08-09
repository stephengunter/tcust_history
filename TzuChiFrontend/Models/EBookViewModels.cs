using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TzuChiFrontend.Models
{
    public class EBookSearchModel
    {
        public int Category { get; set; }
        public int Menu { get; set; }

        public string VideoCategoryId { get; set; }

        public List<EBookCategory> CategoryList;
        public List<EBookFile> EBookFileList;


        public bool Ajax { get; set; }
    }

    public class EBookFile
    {
        public string Id { get; set; }
        public string Title { get; set; }
    }

    public class EBookMenu
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool Active { get; set; }
    }

    public class EBookCategory
    {
        public string Id { get; set; }
        public string Text { get; set; }
    }
}