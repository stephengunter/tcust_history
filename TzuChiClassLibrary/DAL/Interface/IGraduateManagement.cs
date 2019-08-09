using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TzuChiClassLibrary.BO;

namespace TzuChiClassLibrary.DAL
{
    public interface IGraduateManagement
    {
        //慈濟人文與博雅教育 -> 慈濟人文 -> 慈誠懿德會 -> 緣起不滅
        List<GraduateModel> GetAll();

    }
}
