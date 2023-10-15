using Esfamilo_Core.Enums;
using Esfamilo_Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Esfamilo_Core.Enums.CategoryEnum;

namespace Esfamilo_Core.ModelView
{
    public class GetWordFromJsModelView
    {
        public string Word { get; set; }
        public string Category { get; set; }
        public string Userid { get; set; }
        public string TargetLetter { get; set; }
        public int GetCategoryId()
        {
            CategoryEnums myenum = EnumsTools.ConvertEnum<CategoryEnums>(Category);
            return (int)myenum;
        }
    }
}
