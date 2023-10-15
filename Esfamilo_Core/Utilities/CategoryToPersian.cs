using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esfamilo_Core.Utilities
{
    public static class CategoryToPersian
    {
        public static string GetCategoryPersianName(this string category)
        {
            switch(category)
            {
                case "Names":
                    return "نام";
                case "Lnames":
                    return "فامیل";
                case "Cities":
                    return "شهر";
                case "Countries":
                    return "کشور";
                case "Fruits":
                    return "میوه";
                case "Foods":
                    return "غذا";
                case "Colors":
                    return "رنگ";
                case "Animals":
                    return "حیوان";
                case "Objects":
                    return "اشیا";
                case "Cars":
                    return "ماشین";
                default:
                    return "";

            }
        }
    }
}
