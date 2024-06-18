namespace test;

public class KernelSetup
{
    public static IKernel CreateKernel()
    {
        var endpoint = new Uri("http://localhost:11434");
        var modelId = "llama3:8b";
        var kernelBuilder = Kernel.CreateBuilder().AddOpenAIChatCompletion( modelId: modelId, apiKey: null, endpoint: endpoint);
        var kernel = kernelBuilder.Build();
        return kernel;
    }
}