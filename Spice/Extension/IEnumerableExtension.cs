using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.Extension
{
    public static class IEnumerableExtension
    {

        public static IEnumerable<SelectListItem> ToSelectListItem<T>(this IEnumerable<T> items, int selectedValue)
        {

            return items.Select(x => new SelectListItem
            {
                Text = x.GetPropertyValue("Name"),
                Value = x.GetPropertyValue("Id"),
                Selected = x.GetPropertyValue("Id").Equals(selectedValue.ToString())

            });

        }
    }
}
