using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TzuChiClassLibrary.BO;

namespace TzuChiClassLibrary.DAL
{
    public interface IImageShowManagement
    {
        Boolean Add(ImageShowModel model);

        Boolean Update(ImageShowModel model);

        Boolean Delete(string imageShowID);

        ImageShowModel GetByImageShowID(string imageShowID);

        List<ImageShowModel> GetAll(PagenationModel model);

        List<ImageShowModel> GetByTypeID(string typeID);
    }
}
