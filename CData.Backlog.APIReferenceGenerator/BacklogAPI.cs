using System;
using System.Collections.Generic;
using System.Text;

namespace CData.Backlog.APIReferenceGenerator
{

	// You can also define non-static classes, enums, etc.
	public class BacklogAPI
	{
		public int No { get; set; }

		public string ReferenceURL { get; set; }

		public string APIName { get; set; }

		public string Description { get; set; }

		public string Permission { get; set; }

		public string Url { get; set; }

		public string Method { get; set; }

		public string ContentType { get; set; }

		public List<Parameter> RequestParameters { get; set; }

		public List<Parameter> UrlParameters { get; set; }

		public List<Parameter> QueryParameters { get; set; }

		public string ResponseHeader { get; set; }

		public string ResponseBody { get; set; }
	}

	public class Parameter
	{
		public string ParameterName { get; set; }

		public string ParameterType { get; set; }

		public string ParameterContent { get; set; }
	}

}
