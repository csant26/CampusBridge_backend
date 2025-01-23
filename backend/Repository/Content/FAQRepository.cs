using backend.Data;
using backend.Models.Domain.Content.FAQs;
using backend.Models.DTO.Content.FAQ;
using backend.Repository.Content;
using FuzzySharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;

public class FAQRepository : IFAQRepository
{
    private readonly CampusBridgeDbContext campusBridgeDbContext;
    private readonly MLContext mlContext;
    private readonly PredictionEngine<FAQData, FAQPrediction> predictionEngine;

    public FAQRepository(CampusBridgeDbContext campusBridgeDbContext,
                         MLContext mlContext)
    {
        this.campusBridgeDbContext = campusBridgeDbContext;
        this.mlContext = mlContext;
        this.predictionEngine = LoadTrainedModel(); // Load model on initialization
    }

    // Modified GetAnswer to correctly get answers by matching fuzzy score or prediction
    public async Task<FAQResponseDTO> GetAnswer(FAQRequestDTO fAQRequestDTO)
    {
        if (fAQRequestDTO.Question == null && fAQRequestDTO.Category == null)
            return null;

        var faqs = new List<FAQ>();

        if (fAQRequestDTO.Question != null)
        {
            faqs = await campusBridgeDbContext.FAQs.ToListAsync();
        }

        if (fAQRequestDTO.Category != null)
        {
            faqs = await campusBridgeDbContext.FAQs
                .Where(x => x.Category == fAQRequestDTO.Category)
                .ToListAsync();
        }

        var bestMatch = faqs
            .Select(faq => new
            {
                faq.Question,
                faq.Answer,
                Score = Fuzz.Ratio(faq.Question, fAQRequestDTO.Question) // Fuzzy matching score
            })
            .OrderByDescending(x => x.Score)
            .FirstOrDefault();

        if (bestMatch != null) // Threshold for a good match
        {
            return new FAQResponseDTO { answer = bestMatch.Answer, score = bestMatch.Score };
        }
        else
        {
            return new FAQResponseDTO { answer = "No relevant answer.", score = 0 };
        }
    }

    // Load trained model if exists, otherwise train the model
    public PredictionEngine<FAQData, FAQPrediction> LoadTrainedModel()
    {
        string modelPath = "E:\\chatbot_model.zip";

        if (!File.Exists(modelPath))
        {
            TrainModel(); // Only train if model doesn't exist
        }

        ITransformer loadedModel;
        using (var stream = new FileStream(modelPath, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            loadedModel = mlContext.Model.Load(stream, out var schema);
        }

        return mlContext.Model.CreatePredictionEngine<FAQData, FAQPrediction>(loadedModel);
    }

    // Train the model if it doesn't exist
    private void TrainModel()
    {
        string dataPath = "E:\\faq.csv";
        IDataView dataView = mlContext.Data.LoadFromTextFile<FAQData>(dataPath, separatorChar: ',', hasHeader: true);

        var pipeline = mlContext.Transforms.Text.FeaturizeText("Features", nameof(FAQData.Question))
            .Append(mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(FAQData.Answer)))
            .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy())
            .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedAnswer", "Label"));

        var model = pipeline.Fit(dataView);

        // Save trained model
        mlContext.Model.Save(model, dataView.Schema, "E:\\chatbot_model.zip");
    }

    // Prediction function should use actual question input
    public async Task<string> PredictAnswerAsync(string question)
    {
        var inputData = new FAQData { Question = question };

        // Apply transformation (featurization) to input question
        var transformedData = mlContext.Transforms.Text.FeaturizeText("Features", nameof(FAQData.Question))
            .Fit(mlContext.Data.LoadFromEnumerable(new[] { inputData }))
            .Transform(mlContext.Data.LoadFromEnumerable(new[] { inputData }));

        // Predict answer
        var predictionResult = predictionEngine.Predict(inputData); // Use transformed data

        return predictionResult.PredictedAnswer ?? "No relevant answer.";
    }
}
