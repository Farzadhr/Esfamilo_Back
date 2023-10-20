using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Esfamilo_Core.Ml
{
    public static class WordPredictMlExtentions
    {
        private static string MLNetModelPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Ml/MLModel1.zip");
        public static void AddPredictionWordEnginePool(this IServiceCollection services)
        {
            services.AddPredictionEnginePool<WordPredictML.ModelInput, WordPredictML.ModelOutput>().FromFile(MLNetModelPath, true);
        }
    }
}
