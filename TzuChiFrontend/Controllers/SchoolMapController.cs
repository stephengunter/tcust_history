using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TzuChiFrontend.Controllers
{
    public class SchoolMapController : Controller
    {
        // 校園地圖
        // GET: SchoolMap
        public ActionResult SchoolMapList()
        {
            return View();
        }

        public ActionResult SchoolMapbyID(int Id)
        {
            //hardCode
            List<string> mapList = new List<string>();


            switch (Id)
            {
                case 1:
                    mapList.Add("~/Scripts/inform/images/picture/1/slide01.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/1/slide02.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/1/slide03.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/1/slide04.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/1/slide05.jpg");
                    break;
                case 2:
                    mapList.Add("~/Scripts/inform/images/picture/2/slide01.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/2/slide02.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/2/slide03.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/2/slide04.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/2/slide05.jpg");
                    break;
                case 3:
                    mapList.Add("~/Scripts/inform/images/picture/3/slide01.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/3/slide02.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/3/slide03.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/3/slide04.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/3/slide05.jpg");
                    break;
                case 4:
                    mapList.Add("~/Scripts/inform/images/picture/4/slide01.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/4/slide02.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/4/slide03.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/4/slide04.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/4/slide05.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/4/slide06.jpg");
                    break;
                case 5:
                    mapList.Add("~/Scripts/inform/images/picture/5/slide01.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/5/slide02.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/5/slide03.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/5/slide04.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/5/slide05.jpg");
                    break;
                case 6:
                    mapList.Add("~/Scripts/inform/images/picture/6/slide01.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/6/slide02.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/6/slide03.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/6/slide04.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/6/slide05.jpg");
                    break;
                case 7:
                    mapList.Add("~/Scripts/inform/images/picture/7/slide01.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/7/slide02.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/7/slide03.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/7/slide04.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/7/slide05.jpg");
                    break;
                case 8:
                    mapList.Add("~/Scripts/inform/images/picture/8/slide01.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/8/slide02.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/8/slide03.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/8/slide04.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/8/slide05.jpg");
                    break;
                case 9:
                    mapList.Add("~/Scripts/inform/images/picture/9/slide01.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/9/slide02.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/9/slide03.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/9/slide04.jpg");
                    break;
                case 10:
                    mapList.Add("~/Scripts/inform/images/picture/10/slide01.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/10/slide02.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/10/slide03.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/10/slide04.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/10/slide05.jpg");
                    break;
                case 11:
                    mapList.Add("~/Scripts/inform/images/picture/11/slide01.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/11/slide02.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/11/slide03.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/11/slide04.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/11/slide05.jpg");
                    break;
                case 12:
                    mapList.Add("~/Scripts/inform/images/picture/12/slide01.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/12/slide02.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/12/slide03.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/12/slide04.jpg");
                    break;
                case 13:
                    mapList.Add("~/Scripts/inform/images/picture/13/slide01.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/13/slide02.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/13/slide03.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/13/slide04.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/13/slide05.jpg");
                    break;
                case 14:
                    mapList.Add("~/Scripts/inform/images/picture/14/slide01.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/14/slide02.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/14/slide03.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/14/slide04.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/14/slide05.jpg");
                    break;
                case 15:
                    mapList.Add("~/Scripts/inform/images/picture/15/slide01.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/15/slide02.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/15/slide03.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/15/slide04.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/15/slide05.jpg");
                    break;
                case 16:
                    mapList.Add("~/Scripts/inform/images/picture/16/slide01.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/16/slide02.jpg");
                    mapList.Add("~/Scripts/inform/images/picture/16/slide03.jpg");
                    break;
            }

            switch (Id)
            {
                case 11:
                case 15:
                case 7:
                case 10:
                case 6:
                case 14:                                
                    ViewBag.MapSlick = @"
                    <div class='slick-el'>
                        <li class='picmenu5'><a href='/SchoolMap/SchoolMapById/11'></a></li>
                        <li class='picmenu6'><a href='/SchoolMap/SchoolMapById/15'></a></li>
                        <li class='picmenu7'><a href='/SchoolMap/SchoolMapById/7'></a></li>
                        <li class='picmenu8'><a href='/SchoolMap/SchoolMapById/10'></a></li>
                        <li class='picmenu9'><a href='/SchoolMap/SchoolMapById/6'></a></li>
                        <li class='picmenu10'><a href='/SchoolMap/SchoolMapById/14'></a></li>
                    </div>
                    <div class='slick-el'>
                        <li class='pic2menu5'><a href='/SchoolMap/SchoolMapById/9'></a></li>
                        <li class='pic2menu6'><a href='/SchoolMap/SchoolMapById/13'></a></li>
                        <li class='pic2menu7'><a href='/SchoolMap/SchoolMapById/5'></a></li>
                        <li class='pic2menu8'><a href='/SchoolMap/SchoolMapById/8'></a></li>
                        <li class='pic2menu9'><a href='/SchoolMap/SchoolMapById/4'></a></li>
                        <li class='pic2menu10'><a href='/SchoolMap/SchoolMapById/3'></a></li>
                    </div>
                    <div class='slick-el'>
                        <li class='pic3menu5'><a href='/SchoolMap/SchoolMapById/2'></a></li>
                        <li class='pic3menu6'><a href='/SchoolMap/SchoolMapById/1'></a></li>
                        <li class='pic3menu7'><a href='/SchoolMap/SchoolMapById/16'></a></li>
                        <li class='pic3menu8'><a href='/SchoolMap/SchoolMapById/12'></a></li>
                        <li class='pic2menu9_none'><a href='javascript:void(0)'></a></li>
                        <li class='pic2menu10_none'><a href='javascript:void(0)'></a></li>
                    </div>";
                    break;
                case 9:
                case 13:
                case 5:
                case 8:
                case 4:
                case 3:
                    ViewBag.MapSlick = @"   
                    <div class='slick-el'>
                        <li class='pic2menu5'><a href='/SchoolMap/SchoolMapById/9'></a></li>
                        <li class='pic2menu6'><a href='/SchoolMap/SchoolMapById/13'></a></li>
                        <li class='pic2menu7'><a href='/SchoolMap/SchoolMapById/5'></a></li>
                        <li class='pic2menu8'><a href='/SchoolMap/SchoolMapById/8'></a></li>
                        <li class='pic2menu9'><a href='/SchoolMap/SchoolMapById/4'></a></li>
                        <li class='pic2menu10'><a href='/SchoolMap/SchoolMapById/3'></a></li>
                    </div>
                    <div class='slick-el'>
                        <li class='pic3menu5'><a href='/SchoolMap/SchoolMapById/2'></a></li>
                        <li class='pic3menu6'><a href='/SchoolMap/SchoolMapById/1'></a></li>
                        <li class='pic3menu7'><a href='/SchoolMap/SchoolMapById/16'></a></li>
                        <li class='pic3menu8'><a href='/SchoolMap/SchoolMapById/12'></a></li>
                        <li class='pic2menu9_none'><a href='javascript:void(0)'></a></li>
                        <li class='pic2menu10_none'><a href='javascript:void(0)'></a></li>
                    </div>
                    <div class='slick-el'>
                        <li class='picmenu5'><a href='/SchoolMap/SchoolMapById/11'></a></li>
                        <li class='picmenu6'><a href='/SchoolMap/SchoolMapById/15'></a></li>
                        <li class='picmenu7'><a href='/SchoolMap/SchoolMapById/7'></a></li>
                        <li class='picmenu8'><a href='/SchoolMap/SchoolMapById/10'></a></li>
                        <li class='picmenu9'><a href='/SchoolMap/SchoolMapById/6'></a></li>
                        <li class='picmenu10'><a href='/SchoolMap/SchoolMapById/14'></a></li>
                    </div>";
                    break;
                case 2:
                case 1:
                case 16:
                case 12:
                    ViewBag.MapSlick = @"                     
                    <div class='slick-el'>
                        <li class='pic3menu5'><a href='/SchoolMap/SchoolMapById/2'></a></li>
                        <li class='pic3menu6'><a href='/SchoolMap/SchoolMapById/1'></a></li>
                        <li class='pic3menu7'><a href='/SchoolMap/SchoolMapById/16'></a></li>
                        <li class='pic3menu8'><a href='/SchoolMap/SchoolMapById/12'></a></li>
                        <li class='pic2menu9_none'><a href='javascript:void(0)'></a></li>
                        <li class='pic2menu10_none'><a href='javascript:void(0)'></a></li>
                    </div>
                    <div class='slick-el'>
                        <li class='picmenu5'><a href='/SchoolMap/SchoolMapById/11'></a></li>
                        <li class='picmenu6'><a href='/SchoolMap/SchoolMapById/15'></a></li>
                        <li class='picmenu7'><a href='/SchoolMap/SchoolMapById/7'></a></li>
                        <li class='picmenu8'><a href='/SchoolMap/SchoolMapById/10'></a></li>
                        <li class='picmenu9'><a href='/SchoolMap/SchoolMapById/6'></a></li>
                        <li class='picmenu10'><a href='/SchoolMap/SchoolMapById/14'></a></li>
                    </div>
                    <div class='slick-el'>
                        <li class='pic2menu5'><a href='/SchoolMap/SchoolMapById/9'></a></li>
                        <li class='pic2menu6'><a href='/SchoolMap/SchoolMapById/13'></a></li>
                        <li class='pic2menu7'><a href='/SchoolMap/SchoolMapById/5'></a></li>
                        <li class='pic2menu8'><a href='/SchoolMap/SchoolMapById/8'></a></li>
                        <li class='pic2menu9'><a href='/SchoolMap/SchoolMapById/4'></a></li>
                        <li class='pic2menu10'><a href='/SchoolMap/SchoolMapById/3'></a></li>
                    </div>";
                    break;
            }



            ViewBag.MapList = mapList;


            return View();
        }
    }
}