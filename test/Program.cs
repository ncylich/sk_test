#pragma warning disable SKEXP0010
using Microsoft.SemanticKernel;


var endpoint = new Uri("http://localhost:11434");
var modelId = "llama3:8b";

var kernelBuilder = Kernel.CreateBuilder().AddOpenAIChatCompletion( modelId: modelId, apiKey: null, endpoint: endpoint);
var kernel = kernelBuilder.Build();

const string skPrompt = @"
ChatBot that can have a conversation with you about any topic.
It can give explicit instructions or say 'I don't know' if it does not have an answer.

{{$history}}
User: {{$userInput}}
ChatBot:";

var chatFunction = kernel.CreateFunctionFromPrompt(skPrompt);
var history = "";
var arguments = new KernelArguments()
{
    ["history"] = history
};

while (true)
{
    Console.Write("User: ");
    var userInput = Console.ReadLine();
    if (userInput == "exit")
    {
        break;
    }
    arguments["userInput"] = userInput;

    var bot_answer = await chatFunction.InvokeAsync(kernel, arguments);

    Console.WriteLine($"AI: {bot_answer}\n");
    history += $"\nUser: {userInput}\nAI: {bot_answer}\n";
    arguments["history"] = history;
}