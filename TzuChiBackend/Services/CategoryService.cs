
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TzuChiBackend.Context;


namespace TzuChiBackend.Services
{
    public class CategoryService : BaseService
    {

      
        public CategoryService(MyContext context)
            : base(context)
        {
            
        }

        public IEnumerable<Category> GetSchoolYears(bool withMax=false)
        {
            string typeId = "Year";
            if (withMax) {
                return Context.Categories.Where(c => c.CategoryTypeID == typeId)
                                        .OrderByDescending(c => c.Sort);
            }
            return Context.Categories.Where(c => c.CategoryTypeID == typeId && c.CategoryID!= "7534f2b6-a020-4195-9c9d-9f0ad6db0ff5")
                                        .OrderByDescending(c => c.Sort);
           
        }

        public Category GetById(string id)
        {
            var category = Context.Categories.Find(id);
            return category;
        }

        public void deleteCategory()
        {
            //var category= Context.Categories.Where(c => c.CategoryName == "產學類型").FirstOrDefault();
            //Context.Categories.Remove(category);
            //Context.SaveChanges();
        }
        public IEnumerable<Category> GetAreas()
        {
            return GetByTypeId("Partition");
        }
        public IEnumerable<Category> GetCountries()
        {
            return Context.Categories.Where(c => c.CategoryTypeID == "Country" && c.CategoryName!="All").OrderBy(c => c.Sort);
        }
        public IEnumerable<Category> GetByTypeId(string typeId)
        {
            return Context.Categories.Where(c => c.CategoryTypeID == typeId).OrderBy(c=>c.Sort);
        }

        public IEnumerable<Category> GetDepartments()
        {
            return GetByTypeId("Department"); 
        }

		public IEnumerable<Category> GetTaiwanAreas()
		{
			return GetByTypeId("Partition");
		}
		public IEnumerable<Category> GetTaiwanCountries()
		{
			return GetByTypeId("Site");
		}

		public void UpdateName(string id, string name)
        {
            var category = Context.Categories.Find(id);


            category.CategoryName = name;

            Context.SaveChanges();
        }


    }
}
