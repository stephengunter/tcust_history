using TzuChiBackend.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace TzuChiBackend.ViewModels
{
    public class PlanInsideSearchForm : BaseSearchForm
    {
        public string  TypeId { get; set; }
        public IEnumerable<SelectListItem> TypeOptions { get; set; }

        public string TypeName { get; set; }

        public string AreaId { get; set; }
        public IEnumerable<SelectListItem> AreaOptions { get; set; }

       

       
        public IPagedList<PlanInside> PagedRecordResults { get; set; }
        public IEnumerable<PlanInsideViewModel> PagedViewResults { get; set; }


    }

    public class PlanOutsideSearchForm : BaseSearchForm
    {
        public string TypeId { get; set; }
        public IEnumerable<SelectListItem> TypeOptions { get; set; }

        public string TypeName { get; set; }

        public string AreaId { get; set; }
        public IEnumerable<SelectListItem> AreaOptions { get; set; }



        
        public IPagedList<PlanOutside> PagedRecordResults { get; set; }
        public IEnumerable<PlanOutsideViewModel> PagedViewResults { get; set; }


    }

    public class PlanInsideViewModel
    {
        public string Id { get; set; }
        public string TypeName { get; set; }
        public string AreaName { get; set; }
        public string CityName { get; set; }
        public string DepartmentName { get; set; }
        public string CompanyName { get; set; }

        public List<FileUplaod> PicUrlList { get; set; }
        public FileUplaod AttachPicUrl { get; set; }


    }

    public class PlanOutsideViewModel
    {
        public string Id { get; set; }
        public string TypeName { get; set; }
        public string AreaName { get; set; }
        public string CityName { get; set; }
        public string DepartmentName { get; set; }
        public string CompanyName { get; set; }

        public List<FileUplaod> PicUrlList { get; set; }
        public FileUplaod AttachPicUrl { get; set; }


    }


}
