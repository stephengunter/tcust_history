using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using TzuChiClassLibrary.BO;

namespace TzuChiClassLibrary.DAL
{
    //創校緣起 -> 歷任董事
    public class DirectorsManagementMock : IDirectorsManagement
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        Boolean IDirectorsManagement.ResetDirectorsData(List<DirectorsModel> list)
        {
            throw new NotImplementedException();
        }
        List<DirectorsModel> IDirectorsManagement.GetAll()
        {
            var obj1 = new DirectorsModel()
            {
                SessionNumber = "第一屆董事名單",
                StartYear = "1982",
                EndYear = "1992",
                NameList = new List<string>()
                {
                    "印 順董事",
                    "釋悟見董事 ",
                    "釋明復董事 ",
                    "釋悟性董事 ",
                    "杜詩綿董事 ",
                    "林柏榕董事 ",
                    "陳炯明董事 ",
                    "吳水雲董事 ",
                    "王欲明董事 ",
                    "曾文賓董事 ",
                    "陳燦暉董事 ",
                    "陳紹明董事 "
                }
            };

            List<DirectorsModel> list = new List<DirectorsModel>();
            for (int i = 0; i < 8; i++)
            {
                list.Add(obj1);
            }
            return list;
        }
    }
}
