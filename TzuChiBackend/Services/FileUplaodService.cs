using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TzuChiBackend.Context;



namespace TzuChiBackend.Services
{
    public class FileUplaodService : BaseService
    {
        public FileUplaodService(MyContext context)
            : base(context)
        {

        }

        public IEnumerable<FileUplaod> GetByContentId(string contentId)
        {
            return Context.FileUplaods.Where(f => f.ContentID == contentId);
        }

    }
}
