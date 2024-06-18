#pragma warning disable SKEXP0010
using Microsoft.SemanticKernel;
using System.IO;


var endpoint = new Uri("http://localhost:11434");
var modelId = "llama3:8b";
var kernelBuilder = Kernel.CreateBuilder().AddOpenAIChatCompletion( modelId: modelId, apiKey: null, endpoint: endpoint);
var kernel = kernelBuilder.Build();

const string skPrompt = @"
An objective summarizing tool that reads a parsed website's text and outputs a specific summary of it.

Example: Velocigo.com:
skip to Main Content (918) 921-7500 support@velocigo.com Services Managed IT Services Custom Application Development Cloud Services Advisory and Management Websites & Marketing Security Industries Served Energy Supply Chain Healthcare About Us Our Customers Our Partners Contact Us Resources FAQ Blog Search Open Mobile Menu Search Submit Defending your data, safeguarding your success. Velocigo can make you cyberSECURE LEARN MORE SOC-2 CERTIFIED SECURITY - AVAILABILITY - PROCESSING INTEGRITY - CONFIDENTIALITY - PRIVACY READ MORE Custom IT Services and Solutions IMPROVE BUSINESS EFFICIENCY Learn More Move to the Cloud SPEED - AGILITY - SECURITY READ MORE Oklahoma's leading Managed Service Provider Learn More Managed IT ServicesManaged Service Provider (MSP) for businesses – Complete and Professional IT Solutions provider. We specialize in custom, service oriented solutions to help your business run smoothly and interruption free. Learn More Application DevelopmentFrom custom marketing websites to tailored business applications that integrate seamlessly with your business systems, Velocigo can develop web and mobile applications to make your business processes operate with the highest level of efficiency and uptime. Learn More Cloud ServicesVelocigo proudly partners with Amazon Web Services to provide best-in-class cloud services. We provide on premises to cloud migration, cloud advisory and cloud application development, Microsoft Azure, deployment and monitoring solutions.. Learn More Learn More Learn More Learn More Velocigo Managed Services Velocigo services a number of industries with superior IT solutions. If you’re looking for a Managed Service Provider (MSP) or other IT assistance, such as Amazon Web Services (AWS), contact our team to learn more. While Velocigo possesses the expertise to help just about any business improve, we focus on the following industry verticals: EnergyVelocigo team members have over 40 years of combined experience providing IT for energy companies Professional ServicesProfessional consultation, exceptional customer care, and industry expertise. Supply ChainVelocigo has deep technical and business process expertise in multi-location businesses, distribution, and logistics. HealthcareCompliant and cost-effective solutions for today’s constantly changing healthcare environment. Get a Free Consultation Your IT Company for Business Solutions Velocigo has the experience to help your business succeed. Whether your business is located in Tulsa, Dallas, Kansas City, or anywhere in the USA, Velocigo can help you with our multitude of IT solutions. We provide services to all types of businesses, from online marketing for local shops to custom tracking APIs for large manufacturers and distributors. Efficient and Effective IT Support Get expert support and guidance that helps your business perform. Velocigo provides powerful technical solutions – without the large capital investments required to maintain and scale those offering in house. Why Choose Us: Unparalleled Customer Support User-based Pricing Model Experienced Team Expertise in a Variety of Industries Individualized Solutions & Products Amazon Web Services Partner Velocigo Helps Your Business Increase: Operational Leverage and Efficiency. Lower IT Operational Costs. Return on IT Investment. Efficiency with Data Tracking and Reporting. Data Security. Business process and IT procedural compliance. GET IN TOUCH © Copyright 2024 Velocigo - All Rights Reserved. test 1 × Services Managed IT Services Custom Application Development Cloud Services Advisory and Management Websites & Marketing Security Industries Served Energy Supply Chain Healthcare About Us Our Customers Our Partners Contact Us Resources FAQ Blog Back To Top× 

Output:
Velocigo is a leading managed service provider that offers a range of IT solutions to businesses. Their services include managed IT services, custom application development, cloud services, advisory and management, and websites & marketing security. They cater to various industries such as energy, supply chain, healthcare, and more.
The website highlights Velocigo's expertise in providing custom solutions for their clients, ensuring business efficiency, uptime, and cybersecurity. They partner with Amazon Web Services (AWS) and have expertise in Microsoft Azure. Their services aim to improve operational leverage and efficiency, reduce IT costs, and increase the return on investment.
Velocigo is a SOC-2 certified security company that prioritizes confidentiality, integrity, processing, availability, and privacy. Their website emphasizes the importance of their customer-centric approach and provides resources such as FAQs, blog posts, and contact information for potential clients to get in touch.

{{$history}}
User: {{$userInput}}
ChatBot:";

var chatFunction = kernel.CreateFunctionFromPrompt(skPrompt);
var history = "";
var arguments = new KernelArguments()
{
    ["history"] = history
};

var userInput = File.ReadAllText("sxanpro_scrape.txt");
arguments["userInput"] = userInput;

var bot_answer = await chatFunction.InvokeAsync(kernel, arguments);

string bot_string = $"{bot_answer}";
var lines = bot_string.Split("\n");
bot_string = string.Join("\n", lines[1..]);


history += $"\nUser: {userInput}\nAI: {bot_string}\n";
arguments["history"] = history;

Console.WriteLine(history);