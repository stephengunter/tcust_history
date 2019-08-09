using TzuChiBackend.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
namespace TzuChiBackend.ViewModels
{
    public class SchoolDiarySearchForm : BaseSearchForm
    {
        public string TypeId { get; set; }
        public IEnumerable<SelectListItem> TypeOptions { get; set; }

        public string TypeName { get; set; }

        public string YearId { get; set; }
        public IEnumerable<SelectListItem> YearOptions { get; set; }




        public IPagedList<Content> PagedRecordResults { get; set; }
        public IEnumerable<SchoolDiaryViewModel> PagedViewResults { get; set; }


    }

   

    public class SchoolDiaryViewModel
    {
        public string Id { get; set; }
        public string Number { get; set; }
        public string Title { get; set; }
        public string ContentText { get; set; }
        public string SchoolYear { get; set; }
        public string Term { get; set; }
        public DateTime DairyDate { get; set; }
        public string Author { get; set; }


        public List<FileUplaod> PicUrlList { get; set; }
        public FileUplaod AttachPicUrl { get; set; }


    }

    


}
