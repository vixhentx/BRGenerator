// See https://aka.ms/new-console-template for more information
using BRGenerator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;


Console.WriteLine("This is BRG!");

if(args.Length <=0 )
{
    Functions.showHelp();
    return;
}

#region const
string json =Path.Combine(Directory.GetCurrentDirectory(),"BRconfig.json");
#endregion const
JObject jsonObject = (JObject)JToken.ReadFrom(new JsonTextReader(File.OpenText(json)));

switch (args[0].ToLower())
{
    case "materials":
    {
            Functions.genMaterialItems(jsonObject);
            Console.WriteLine("完成生成materials");
            break;
    }
    default:
    {
            Functions.showHelp();
            break;
    }
}