

using System;
using System.Collections.Generic;
using System.Linq;
using TzuChiBackend.Helpers;

namespace TzuChiBackend.ViewModels
{
    public class EBookSearchModel : BaseSearchForm
    {

        public IEnumerable<EBookViewModel> EBookViewList { get; set; }

    }

    public class EBookViewModel:BaseForm
    {
        public string Id { get; set; }
        public string Number { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
        public DateTime CreatedAt { get; set; }

        public IEnumerable<EBookPageModel> EBookPageList { get; set; }


        public int MaxPage
        {
            get
            {
                if (this.EBookPageList.IsNullOrEmpty()) return 0;
                return this.EBookPageList.Max(p=>p.Sort);
            }
        }

    }

    public class EBookPageModel
    {
        public string Id { get; set; }
        public string EBookId { get; set; }
        public int Sort { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
}