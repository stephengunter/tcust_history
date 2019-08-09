
using System;
using System.Collections.Generic;
using System.Linq;
using TzuChiBackend.Context;
using System.Text.RegularExpressions;

namespace TzuChiBackend.Services
{
    public class NewsService : BaseService
    {
        private readonly string typeId;
        public NewsService(MyContext context)
            : base(context)
        {
            this.typeId = Context.ContentTypes.Where(t => t.TypeName == "大愛新聞").FirstOrDefault().Id;
            
        }

        public void test(string id)
        {
           



        }


        public IEnumerable<Content> GetAll()
        {
            return Context.Contents.Where(c => c.TypeID == this.typeId);
        }

       

        public void ReFormatAll()
        {
           
            var newsList = GetAll().ToList();
            foreach (var item in newsList)
            {
                ReFormat(item);
            }


        }

        void ReFormat(Content content)
        {
            try
            {
                string strTitle = content.ContentName.Replace("（", "(").Replace("）", ")");
                string strContent = content.Description.Replace("（", "(").Replace("）", ")");

                strContent = strContent.Replace("<h3", "<p").Replace("</h3>", "</p>");
                strContent = strContent.Replace("<h4", "<p").Replace("</h4>", "</p>");

                strContent= new Regex("style=\"[^\"]*\"").Replace(strContent, "");

                strContent = strContent.Replace("<span", "<p").Replace("</span>", "</p>");


                content.ContentName = strTitle;
                content.ContentText = strContent;


                Context.SaveChanges();
            }
            catch (Exception ex)
            {

                string msg = ex.Message;
                throw new Exception("     id=" + content.ContentID);
            }



        }

       

    }
}