using Microsoft.ML;
using System.IO;
using IrisFlowerClustering;

var mlContext = new MLContext(seed: 0);  // Cria a instância do MLContext

// Definir os caminhos para os dados e modelo
string _dataPath = @"C:\Users\isabe\OneDrive\Área de Trabalho\IrisFlowerClustering\IrisFlowerClustering\IrisFlowerClustering\Dados\iris.data";

string _modelPath = Path.Combine(Environment.CurrentDirectory, "Dados", "IrisClusteringModel.zip");


// Carregar os dados
IDataView dataView = mlContext.Data.LoadFromTextFile<IrisData>(_dataPath, hasHeader: false, separatorChar: ',');


// Criar o pipeline de aprendizado
string featuresColumnName = "Features";
var pipeline = mlContext.Transforms
    .Concatenate(featuresColumnName, "SepalLength", "SepalWidth", "PetalLength", "PetalWidth")
    .Append(mlContext.Clustering.Trainers.KMeans(featuresColumnName, numberOfClusters: 3));

// Treinar o modelo
var model = pipeline.Fit(dataView);

// Salvar o modeloa
using (var fileStream = new FileStream(_modelPath, FileMode.Create, FileAccess.Write, FileShare.Write))
{
    mlContext.Model.Save(model, dataView.Schema, fileStream);
}

// Criar o PredictionEngine para fazer previsões
var predictor = mlContext.Model.CreatePredictionEngine<IrisData, ClusterPrediction>(model);

// Teste para a flor Setosa
var predictionSetosa = predictor.Predict(TestIrisData.Setosa);
Console.WriteLine($"Setosa - Cluster: {predictionSetosa.PredictedClusterId}");
Console.WriteLine($"Distances: {string.Join(" ", predictionSetosa.Distances ?? Array.Empty<float>())}");

// Teste para a flor Versicolor
var predictionVersicolor = predictor.Predict(TestIrisData.Versicolor);
Console.WriteLine($"Versicolor - Cluster: {predictionVersicolor.PredictedClusterId}");
Console.WriteLine($"Distances: {string.Join(" ", predictionVersicolor.Distances ?? Array.Empty<float>())}");

// Teste para a flor Virginica
var predictionVirginica = predictor.Predict(TestIrisData.Virginica);
Console.WriteLine($"Virginica - Cluster: {predictionVirginica.PredictedClusterId}");
Console.WriteLine($"Distances: {string.Join(" ", predictionVirginica.Distances ?? Array.Empty<float>())}");


