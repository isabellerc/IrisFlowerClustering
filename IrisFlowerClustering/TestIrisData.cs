using IrisFlowerClustering;

internal static class TestIrisData
{
    // Exemplo de instância de dados para a classe IrisData
    internal static readonly IrisData Setosa = new IrisData
    {
        SepalLength = 5.1f,
        SepalWidth = 3.5f,
        PetalLength = 1.4f,
        PetalWidth = 0.2f
    };

    internal static readonly IrisData Versicolor = new IrisData
    {
        SepalLength = 5.9f,
        SepalWidth = 2.8f,
        PetalLength = 4.2f,
        PetalWidth = 1.3f
    };

    internal static readonly IrisData Virginica = new IrisData
    {
        SepalLength = 6.3f,
        SepalWidth = 3.3f,
        PetalLength = 6.0f,
        PetalWidth = 2.5f
    };
}
