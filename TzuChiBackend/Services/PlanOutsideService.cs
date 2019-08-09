using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TzuChiBackend.Context;
using System.Web.Mvc;

namespace TzuChiBackend.Services
{
    public class PlanOutsideService : BaseService
    {
       string TYPEID = "9f9670da-f644-47f7-bd4f-312ebaf34963";
       string CATEGORYQUTSIDEID_SISTER = "1ed63661-7538-40df-a5e8-e58c76bbf3a5";  //姊妹校
       string CATEGORYQUTSIDEID_DEGREE = "7ccbdebf-9ac8-4a12-9274-651f299e51aa";  //雙聯學位
       string CATEGORYQUTSIDEID_OVERSEA = "be7892c3-56d1-41e3-aa55-d8b9b764c53c"; //海外交流
       string CATEGORYQUTSIDEID_INDUSTRYCOOPERATION = "d88c0ee7-d872-465c-a2ec-85b2409a8bb2"; //產學合作
       string CATEGORYQUTSIDEID_INTERNSHIP = "609bbb2f-2e6a-4850-97c6-381cb2dfe8f4"; //企業實習

        public PlanOutsideService(MyContext context)
            : base(context)
        {

        }

        public string GetTypeName(string typeId)
        {
            if (typeId == this.CATEGORYQUTSIDEID_SISTER) return "Sister";
            if (typeId == this.CATEGORYQUTSIDEID_DEGREE) return "Degree";
            if (typeId == this.CATEGORYQUTSIDEID_OVERSEA) return "Oversea";
            if (typeId == this.CATEGORYQUTSIDEID_INDUSTRYCOOPERATION) return "cooperation";
            if (typeId == this.CATEGORYQUTSIDEID_INTERNSHIP) return "intern";
            return "";

        }
        public IEnumerable<SelectListItem> TypeSelectList()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Value = CATEGORYQUTSIDEID_SISTER, Text = "姊妹校" });
            list.Add(new SelectListItem() { Value = CATEGORYQUTSIDEID_DEGREE, Text = "雙聯學位" });
            list.Add(new SelectListItem() { Value = CATEGORYQUTSIDEID_OVERSEA, Text = "海外交流" });


            list.Add(new SelectListItem() { Value = CATEGORYQUTSIDEID_INDUSTRYCOOPERATION, Text = "產學合作" });
            list.Add(new SelectListItem() { Value = CATEGORYQUTSIDEID_INTERNSHIP, Text = "企業實習" });

            return list;

        }

        public IEnumerable<PlanOutside> Search(string typeId, string areaId, string keyword)
        {
            IEnumerable<PlanOutside> planList = GetByType(typeId);

            if (!String.IsNullOrEmpty(areaId))
            {
                planList = planList.Where(p => p.CategoryCountryID == areaId);
            }

            if (!String.IsNullOrEmpty(keyword))
            {
                planList = planList.Where(p => p.Content.ContentName.Contains(keyword) );
            }

            return planList;
        }

        public IEnumerable<PlanOutside> GetByType(string typeId)
        {
            return Context.PlanOutsides.Where(c => c.CategoryOutsideID == typeId);
        }


        

        public void UpdateDepartmentName(string oldName, string name)
        {
            //產學合作
            //var internShips = GetInternShips();
            //internShips = internShips.Where(c => c.Department == oldName).ToList();


            //foreach (var item in internShips)
            //{
            //    item.Department = name;
            //}

            Context.SaveChanges();
        }



    }
}
