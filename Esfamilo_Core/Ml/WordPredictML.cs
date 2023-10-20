using Microsoft.ML.Data;
using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.Extensions.ML;

namespace Esfamilo_Core.Ml
{
    public class WordPredictML
    {
        /// <summary>
        /// model input class for MLModel1.
        /// </summary>
        #region model input class
        public class ModelInput
        {
            [ColumnName(@"col0")]
            public string Col0 { get; set; }

            [ColumnName(@"col1")]
            public string Col1 { get; set; }

        }

        #endregion

        /// <summary>
        /// model output class for MLModel1.
        /// </summary>
        #region model output class
        public class ModelOutput
        {
            [ColumnName(@"col0")]
            public float[] Col0 { get; set; }

            [ColumnName(@"col1")]
            public uint Col1 { get; set; }

            [ColumnName(@"Features")]
            public float[] Features { get; set; }

            [ColumnName(@"PredictedLabel")]
            public string PredictedLabel { get; set; }

            [ColumnName(@"Score")]
            public float[] Score { get; set; }

        }

        #endregion

        private static string MLNetModelPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Ml/MLModel1.zip");

        //public static readonly Lazy<PredictionEngine<ModelInput, ModelOutput>> PredictEngine = new Lazy<PredictionEngine<ModelInput, ModelOutput>>(() => CreatePredictEngine(), true);
        private readonly PredictionEnginePool<ModelInput, ModelOutput> _enginePool;
        public WordPredictML(PredictionEnginePool<ModelInput, ModelOutput> enginePool)
        {
            _enginePool = enginePool;
        }

        public ModelOutput Predict(string input)
        {
            //var predEngine = PredictEngine.Value;
            //return predEngine.Predict(input);
            return _enginePool.Predict(new ModelInput { Col0 = input });
        }

        //private static PredictionEngine<ModelInput, ModelOutput> CreatePredictEngine()
        //{
        //    var mlContext = new MLContext();
        //    ITransformer mlModel = mlContext.Model.Load(MLNetModelPath, out var _);
        //    return mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
        //}
    }
}
