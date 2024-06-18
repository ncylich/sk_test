#pragma warning disable SKEXP0010
using System;
using System.Collections.Immutable;
using System.Threading.Tasks;
using System.IO;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.ChatCompletion;
using test;

class Program
{
    static async Task Main(string[] args)
    {
        File.Delete("/Users/noahcylich/Documents/MSFT_Proj/Agent_Playground/scraper_plugin/WebScrapeRan");
        //string web = "sxanpro.com";
        
        var endpoint = new Uri("http://localhost:11434");
        var modelId = "mistral:v0.3";  // llama3:8b

        var builder = Kernel.CreateBuilder();
        builder.AddOpenAIChatCompletion( modelId: modelId, apiKey: null, endpoint: endpoint);
        builder.Plugins.AddFromType<WebScrape>();
        var kernel = builder.Build();
        
        string website = "www.sxanpro.com";
        string skPrompt = $"An objective website summarizing agent. If you don't have any information about a website," +
                          $"try and use the \"WebScrape.ScrapeWebsite\" function call to get its contents. If you still have no info," +
                          $" please output \"No Data for this Website\"\n" +
                          $"summarize: {website}";

        var history = new ChatHistory();

        // Enable auto function calling
        var executionSettings = new OpenAIPromptExecutionSettings
        {
            ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
        };
        
        history.AddUserMessage(skPrompt);

        var summary = await kernel.GetRequiredService<IChatCompletionService>().GetChatMessageContentAsync(
            history,
            executionSettings: executionSettings
        );

        Console.WriteLine(skPrompt);
        Console.WriteLine(summary.ToString());
    }
}
