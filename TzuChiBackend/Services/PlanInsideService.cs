using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TzuChiBackend.Context;
using System.Web.Mvc;

namespace TzuChiBackend.Services
{
    public class PlanInsideService:BaseService
    {
        string CATEGORYQUTSIDEID_UNION = "85b5e9b1-f134-4d15-81b0-38e8c81f0f36"; //策略聯盟
        string CATEGORYQUTSIDEID_INTERNSHIP = "baed9b98-d212-481c-bbcf-2183e242217b"; //企業實習


        string CATEGORYQUTSIDEID_Cooperation = "ab004ad5-47c6-4942-baae-f6fe97ca10bc"; //產學合作
        

        public PlanInsideService(MyContext context)
            : base(context)
        {
            
        }

        public string GetTypeName(string typeId)
        {
            if (typeId == this.CATEGORYQUTSIDEID_UNION) return "Union";
            if (typeId == this.CATEGORYQUTSIDEID_INTERNSHIP) return "Intern";
            return "";

        }
        public IEnumerable<SelectListItem> TypeSelectList(bool withCooperation=false)
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Value = CATEGORYQUTSIDEID_UNION, Text = "策略聯盟" });
            list.Add(new SelectListItem() { Value = CATEGORYQUTSIDEID_INTERNSHIP, Text = "企業實習" });

            if(withCooperation) list.Add(new SelectListItem() { Value = CATEGORYQUTSIDEID_Cooperation, Text = "產學合作" });

            return list;

        }

        public IEnumerable<PlanInside> Search(string typeId,string areaId, string keyword)
        {
            IEnumerable<PlanInside> planList = GetByType(typeId);

            if (!String.IsNullOrEmpty(areaId))
            {
                planList = planList.Where(p => p.CategoryPartitionID == areaId);
            }

            if (!String.IsNullOrEmpty(keyword))
            {
                planList = planList.Where(p => p.Department.Contains(keyword) || p.Agencies.Contains(keyword));
            }

            return planList;
        }

        public IEnumerable<PlanInside> GetByType(string typeId)
        {
            return Context.PlanInsides.Where(c => c.CategoryPrepID == typeId);
        }


        public IEnumerable<PlanInside> GetUnions()
        {
            return Context.PlanInsides.Where(c => c.CategoryPrepID == CATEGORYQUTSIDEID_UNION);
        }

        public IEnumerable<PlanInside> GetInternShips()
        {
            return Context.PlanInsides.Where(c => c.CategoryPrepID == CATEGORYQUTSIDEID_INTERNSHIP);
        }

        public void UpdateDepartmentName(string oldName, string name)
        {
            //產學合作
            var internShips = GetInternShips();
            internShips = internShips.Where(c => c.Department == oldName).ToList();
            

            foreach (var item in internShips)
            {
                item.Department = name;
            }

            Context.SaveChanges();
        }

        

    }
}
