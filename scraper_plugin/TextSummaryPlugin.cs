using Microsoft.SemanticKernel;
using System.ComponentModel;
using System.Globalization;
using System.Threading.Tasks;
using System.IO;

public class TextSummaryPlugin
{
    //[KernelFunction, Description("Summarize the given html text")]
    public static async Task<string> SummarizeTextAsync(Kernel kernel, string website)
    {
        var prompt = $"An objective summarizing tool that first gets a website's parsed text, then outputs a specific summary of it.\n" +
                     $"summarize: {website}";
        var result = await kernel.InvokePromptAsync(prompt);
        return result.ToString();
    }
}
