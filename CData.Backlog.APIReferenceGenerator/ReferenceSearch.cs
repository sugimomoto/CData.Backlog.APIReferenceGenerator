using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CData.Backlog.APIReferenceGenerator
{
    public class ReferenceSearch
    {
		private XPathStatic xPathStati;
		public ReferenceSearch(XPathStatic xPathStati)
		{
			this.xPathStati = xPathStati;
		}

		public List<BacklogAPI> Search(string[] urls)
        {
			var web = new HtmlWeb();
			var backlogAPIs = new List<BacklogAPI>();
			var count = 0;


			foreach (var url in urls)
			{
				count++;

				var api = new BacklogAPI();
				api.ReferenceURL = url;
				api.No = count;

				var document = web.LoadFromWebAsync(url, Encoding.UTF8).Result;

				// Title
				if (document.DocumentNode.SelectSingleNode(xPathStati.APIName) != null)
				{
					api.APIName = document.DocumentNode.SelectSingleNode(xPathStati.APIName).InnerText;
					api.Description = document.DocumentNode.SelectSingleNode(xPathStati.APIName).NextSibling.NextSibling.InnerText.Replace(" \n", ""); ;
				}

				if (url.Contains("get-issue-participant-list"))
				{
					api.APIName = document.DocumentNode.SelectNodes(xPathStati.APINameh2).FirstOrDefault().InnerText;
					api.Description = document.DocumentNode.SelectNodes(xPathStati.APINameh2).FirstOrDefault().NextSibling.NextSibling.InnerText.Replace(" \n", ""); ;
				}

				// Method
				if (document.DocumentNode.SelectSingleNode(xPathStati.Method) != null)
					api.Method = document.DocumentNode.SelectSingleNode(xPathStati.Method).NextSibling.NextSibling.InnerText.Replace(" \n", "");

				// Permission 
				if (document.DocumentNode.SelectSingleNode(xPathStati.Permission) != null)
					api.Permission = document.DocumentNode.SelectSingleNode(xPathStati.Permission).NextSibling.NextSibling.InnerText.Replace(" \n", ""); ;

				// Url 
				if (document.DocumentNode.SelectSingleNode(xPathStati.Url) != null)
					api.Url = document.DocumentNode.SelectSingleNode(xPathStati.Url).NextSibling.NextSibling.InnerText.Replace(" \n", ""); ;

				// ResponseStatus 
				if (document.DocumentNode.SelectSingleNode(xPathStati.ResponseHeader) != null)
					api.ResponseHeader = document.DocumentNode.SelectSingleNode(xPathStati.ResponseHeader).NextSibling.NextSibling.InnerText.Replace(" \n", ""); ;

				// ResponseBody
				if (document.DocumentNode.SelectSingleNode(xPathStati.ResponseBody) != null && document.DocumentNode.SelectSingleNode(xPathStati.ResponseBody).NextSibling.NextSibling != null)
					api.ResponseBody = document.DocumentNode.SelectSingleNode(xPathStati.ResponseBody).NextSibling.NextSibling.InnerText.Replace("&quot;", "\"").Replace(" ", "").Replace("\n", "").Replace(",...", "");

				// URL Parameter
				if (document.DocumentNode.SelectSingleNode(xPathStati.UrlParameters) != null)
				{
					var table = document.DocumentNode.SelectSingleNode(xPathStati.UrlParameters).NextSibling.NextSibling.Descendants("tr").Select(n => n.Elements("td").Select(e => e.InnerText.Replace("&ldquo;", "“").Replace("&rdquo;", "”")).ToArray());
					api.UrlParameters = GetParameters(table);
				}

				// Query Parameter
				if (document.DocumentNode.SelectSingleNode(xPathStati.QueryParameters) != null)
				{
					var table = document.DocumentNode.SelectSingleNode(xPathStati.QueryParameters).NextSibling.NextSibling.Descendants("tr").Select(n => n.Elements("td").Select(e => e.InnerText.Replace("&ldquo;", "“").Replace("&rdquo;", "”")).ToArray());
					api.QueryParameters = GetParameters(table);
				}

				// Request parameter
				if (document.DocumentNode.SelectSingleNode(xPathStati.RequestParameters) != null)
				{

					api.ContentType = document.DocumentNode.SelectSingleNode(xPathStati.RequestParameters).NextSibling.NextSibling.InnerText.Replace("Content-Type:", "").Replace(" \n", "");

					if (document.DocumentNode.SelectSingleNode(xPathStati.RequestParameters).NextSibling.NextSibling.NextSibling.NextSibling.Name == "table")
					{
						var table = document.DocumentNode.SelectSingleNode(xPathStati.RequestParameters).NextSibling.NextSibling.NextSibling.NextSibling.Descendants("tr").Select(n => n.Elements("td").Select(e => e.InnerText.Replace("&ldquo;", "“").Replace("&rdquo;", "”")).ToArray());
						api.RequestParameters = GetParameters(table);
					}
				}

				backlogAPIs.Add(api);
			}

			return backlogAPIs;
		}


		public static List<Parameter> GetParameters(IEnumerable<string[]> table)
		{
			var parameters = new List<Parameter>();

			foreach (var records in table)
			{
				if (records.Length == 0)
					continue;

				parameters.Add(new Parameter()
				{
					ParameterName = records[0],
					ParameterType = records[1],
					ParameterContent = records[2]
				});

			}
			return parameters;
		}
	}
}
