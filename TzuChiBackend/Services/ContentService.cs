

using System;
using System.Collections.Generic;
using System.Linq;
using TzuChiBackend.Context;
using System.Text.RegularExpressions;
using TzuChiBackend.Helpers;

namespace TzuChiBackend.Services
{
    public class ContentService : BaseService
    {
        public ContentService(MyContext context)
            : base(context)
        {
          
        }

        public virtual Content GetById(string id)
        {

            return Context.Contents.Find(id);
        }


        public virtual IEnumerable<Content> GetAll()
        {
            return Context.Contents;
        }

        public virtual void ReFormatAll()
        {

            var all = GetAll().ToList();
            foreach (var item in all)
            {
                ReFormat(item);
            }


        }




        public virtual void ReFormat(Content content)
        {
            try
            {

           
                string strContent = content.Description.Replace("（", "(").Replace("）", ")");
                string strTitle = content.ContentName.Replace("（", "(").Replace("）", ")");

                string tag = "";

                List<string> tags = new List<string> { "h3", "h4", "strong" };
                int indexOfTag = 0;
                string match = "";
                foreach (var item in tags)
                {
                    tag = String.Format("<{0}>", item);
                    indexOfTag = strContent.IndexOf(tag);
                    if (indexOfTag >= 0)
                    {
                        match = item;
                        break;
                    }

                }

                int x = 0;
                string title_author = "";

                if (String.IsNullOrEmpty(match))
                {
                    content.ContentName = strTitle;
                    
                    content.ContentText = strContent;

                    Context.SaveChanges();

                    return;
                }
             
                tag = String.Format("</{0}>", match);
                x = strContent.IndexOf(tag);

                if (x < 0)
                {
                    content.ContentName = strTitle;
                    content.Author = "";
                    content.ContentText = strContent;

                    Context.SaveChanges();

                    return;
                }

              
                title_author = strContent.Substring(0, x);
                strContent = strContent.Replace(title_author, "");
                title_author = title_author.Replace(strTitle, "");

                x = title_author.IndexOf("(");
                int y = title_author.IndexOf(")");

                if (y > x)
                {
                    title_author = title_author.Substring(x, y - x);
                }



                title_author = title_author.Replace("(", "").Replace(")", "").Replace("/", "");

                string author = Regex.Replace(title_author, @"[\d-]", string.Empty);
                    

                strContent = new Regex("style=\"[^\"]*\"").Replace(strContent, "");


                strContent = strContent.Replace("<h3", "<p").Replace("</h3>", "</p>");
                strContent = strContent.Replace("<h4", "<p").Replace("</h4>", "</p>");
                strContent = strContent.Replace("<strong", "<p").Replace("</strong>", "</p>");
                strContent = strContent.Replace("<span", "<p").Replace("</span>", "</p>");



                content.ContentName = strTitle;
                content.Author = author.Replace("<h>", "").Trim().RemoveHtmlTags();
                content.ContentText = strContent;

                Context.SaveChanges();
             

               

            }
            catch (Exception ex)
            {

                string msg = ex.Message;
                throw new Exception("     id=" + content.ContentID);
            }

        }

        


        public virtual void xxReFormat(Content content)
        {
            try
            {
                string strTitle = content.ContentName.Replace("（", "(").Replace("）", ")");

                string strContent = content.Description.Replace("（", "(").Replace("）", ")");

                bool h3 = true;

                string[] tags = new string[] { "<h3>","<h4>","<strong>"};

                int x = strContent.IndexOf("<h3>");
                if (x < 0)
                {
                    h3 = false;
                    x = strContent.IndexOf("<h4>");
                    if (x < 0)
                    {
                        return;
                    }
                }


                if (h3) x = strContent.IndexOf("</h3>");
                else x = strContent.IndexOf("</h4>");

                string title_author = strContent.Substring(0, x);

                strContent = strContent.Replace(title_author, "");

                title_author = title_author.Replace(strTitle, "");

                x = title_author.IndexOf("(");
                int y = title_author.IndexOf(")");

                if (y > x)
                {
                    title_author = title_author.Substring(x, y - x);
                }

                title_author = title_author.Replace("(", "").Replace(")", "").Replace("/", "");

                string author = Regex.Replace(title_author, @"[\d-]", string.Empty);

                if (h3) x = strContent.IndexOf("</h3>");
                else x = strContent.IndexOf("</h4>");

                strContent = strContent.Remove(x, 5);

                //strContent = strContent.Replace("<h3>", "<p>").Replace("</h3>", "</p>");
                //strContent = strContent.Replace("<h4>", "<p>").Replace("</h4>", "</p>");

                strContent = strContent.Replace("<h3", "<p").Replace("</h3>", "</p>");
                strContent = strContent.Replace("<h4", "<p").Replace("</h4>", "</p>");

                strContent = new Regex("style=\"[^\"]*\"").Replace(strContent, "");

                strContent = strContent.Replace("<span", "<p").Replace("</span>", "</p>");



                content.ContentName = strTitle;
                content.Author = author.Replace("<h>", "").Trim().RemoveHtmlTags();
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