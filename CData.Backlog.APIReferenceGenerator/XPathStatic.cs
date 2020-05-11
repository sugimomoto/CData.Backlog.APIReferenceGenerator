using System;
using System.Collections.Generic;
using System.Text;

namespace CData.Backlog.APIReferenceGenerator
{
    public class XPathStatic
    {
		public XPathStatic(bool isJp)
		{
			if (isJp)
			{
				this.APIName = "//h1";
				this.APINameh2 = "//h2";
				this.Method = "//*[@id=\"メソッド\"]";
				this.Permission = "//*[@id=\"権限\"]";
				this.Url = "//*[@id=\"url\"]";
				this.ResponseHeader = "//*[@id=\"ステータスライン-レスポンスヘッダ\"]";
				this.ResponseBody = "//*[@id=\"レスポンスボディ\"]";
				this.UrlParameters = "//*[@id=\"url-パラメーター\"]";
				this.QueryParameters = "//*[@id=\"クエリパラメーター\"]";
				this.RequestParameters = "//*[@id=\"リクエストパラメーター\"]";
			}
			else
			{
				this.APIName = "//h1";
				this.APINameh2 = "//h2";
				this.Method = "//*[@id=\"method\"]";
				this.Permission = "//*[@id=\"role\"]";
				this.Url = "//*[@id=\"url\"]";
				this.ResponseHeader = "//*[@id=\"status-line-response-header\"]";
				this.ResponseBody = "//*[@id=\"response-body\"]";
				this.UrlParameters = "//*[@id=\"url-parameters\"]";
				this.QueryParameters = "//*[@id=\"query-parameters\"]";
				this.RequestParameters = "//*[@id=\"form-parameters\"]";
			}
		}

		public string ReferenceURL { get; set; }

		public string APIName { get; set; }
		public string APINameh2 { get; set; }

		public string Description { get; set; }

		public string Permission { get; set; }

		public string Url { get; set; }

		public string Method { get; set; }

		public string ContentType { get; set; }

		public string RequestParameters { get; set; }

		public string UrlParameters { get; set; }

		public string QueryParameters { get; set; }

		public string ResponseHeader { get; set; }

		public string ResponseBody { get; set; }


	}
}
