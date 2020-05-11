using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace CData.Backlog.APIReferenceGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");

            var isJp = true;

            var referenceSearch = new ReferenceSearch(new XPathStatic(isJp));
            var postmanCollectionGenerator = new PostmanCollectionGenerator(isJp);
            
            var backlogApis = referenceSearch.Search(new APIReferenceList(isJp).urls);
            var postmanCollection = postmanCollectionGenerator.Generate(backlogApis);

            var jsonString = JsonConvert.SerializeObject(postmanCollection);
            File.WriteAllText("backlogPostmanCollection" + (isJp ? "Jp" : "En") + ".json", jsonString);

            Console.WriteLine("End");
            Console.ReadKey();
        }
    }
}
