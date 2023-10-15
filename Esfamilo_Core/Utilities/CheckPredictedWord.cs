using Esfamilo_Core.Enums;
using Esfamilo_Core.Ml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Esfamilo_Core.Enums.CategoryEnum;

namespace Esfamilo_Core.Utilities
{
    public class CheckPredictedWord
    {
        public static async Task<bool> CheckPredictedWordIsCorrect(string word, string category)
        {
            return await Task.Run(async () =>
            {
                if (category == CategoryEnums.Names.ToString() || category == CategoryEnums.Lnames.ToString()
    || category == CategoryEnums.Cars.ToString() || category == CategoryEnums.Objects.ToString())
                {
                    return true;
                }
                var persianPredict = CategoryToPersian.GetCategoryPersianName(category);
                var predictedCate = WordPredictML.Predict(new WordPredictML.ModelInput
                {
                    Col0 = word
                }).Result.PredictedLabel;
                if (predictedCate == persianPredict)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            });

        }
    }
}
