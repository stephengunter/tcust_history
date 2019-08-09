using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TzuChiBackend.Context;

namespace TzuChiBackend.Services
{
    public static class SelectListExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectListItems(
             this IEnumerable<int> values, int selectedId = 0)
        {
            return values.Select(item =>
                        new SelectListItem
                        {
                            Selected = (item == selectedId),
                            Text = item.ToString(),
                            Value = item.ToString()
                        });

        }

        public static IEnumerable<SelectListItem> ToSelectListItems(
             this IEnumerable<Category> categories, string selected = "", bool hasEmpty = false)
        {
            var list = categories.Select(c =>
                           new SelectListItem
                           {
                               Text = c.CategoryName,
                               Value = c.CategoryID.ToString(),
                               Selected = selected == c.CategoryID
                           }).ToList();

            if (hasEmpty)
            {
                list.Insert(0, new SelectListItem
                {
                    Text = "-------",
                    Value = "",
                    Selected = (selected == "")
                });
            }

            return list;

        }


    }
}