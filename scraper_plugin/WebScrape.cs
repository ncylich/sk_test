namespace test;

using Microsoft.SemanticKernel;
using System;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using System.Threading.Tasks;

public class WebScrape
{
    [KernelFunction]
    [Description("Get the parsed text from a website, given a URL")]
    [return: Description("The parsed text from the given website")]
    public static async Task<string> ScrapeWebsite(Kernel kernel, string url)
    {
        return "testing";
        Console.WriteLine("ScrapeWebsite function called with URL: " + url);
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "python",
                Arguments = $"/Users/noahcylich/Documents/MSFT_Proj/Agent_Playground/scraper_plugin/webscraper.py {url}",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };
        process.Start();
        string result = await process.StandardOutput.ReadToEndAsync();
        process.WaitForExit();
        File.Create("/Users/noahcylich/Documents/MSFT_Proj/Agent_Playground/scraper_plugin/WebScrapeRan");
        return result;
    }
}