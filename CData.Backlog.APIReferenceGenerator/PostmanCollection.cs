using Newtonsoft.Json;
using System.Collections.Generic;

namespace CData.Backlog.APIReferenceGenerator
{

	// Postman Collection

	public class PostmanCollection
	{
		public Info info { get; set; }
		public List<Item> item { get; set; }

		[JsonProperty("event")]
		public List<Event> _event { get; set; }
		public List<Variable1> variable { get; set; }
		public Auth auth { get; set; }
		public Protocolprofilebehavior protocolProfileBehavior { get; set; }
	}

	public class Info
	{
		// GUID
		public string _postman_id { get; set; }

		// Backlog Postman Collection
		public string name { get; set; }

		// ## About Backlog\r\n\r\nBacklog is a popular cloud-based project management SaaS in Japan.
		public string description { get; set; }

		// https://schema.getpostman.com/json/collection/v2.1.0/collection.json
		public string schema { get; set; }
	}

	public class Protocolprofilebehavior
	{
	}

	// Request Collection
	public class Item
	{
		// スペース情報の取得
		public string name { get; set; }

		// # Description \nスペースの情報を取得します。\n# Reference URL \nhttps://developer.nulab.com/ja/docs/backlog/api/2/get-space/ 
		public string description { get; set; }
		public Request request { get; set; }
		public List<Response> response { get; set; }
	}

	public class Request
	{
		public string method { get; set; }
		public string description { get; set; }
		public List<Header> header { get; set; }
		public Url url { get; set; }
		public dynamic body { get; set; }
	}

	public class Url
	{
		public string raw { get; set; }
		public string protocol { get; set; }
		public List<string> host { get; set; }
		public List<string> path { get; set; }
		public List<Query> query { get; set; }
		public Variable[] variable { get; set; }
	}

	public class Query
	{
		public string key { get; set; }
		public string value { get; set; }
		public string description { get; set; }
		public bool disabled { get; set; }
	}

	public class Variable
	{
		public string key { get; set; }
		public string value { get; set; }
		public string description { get; set; }
	}

	public class BodyByUrlEncoded
	{
		public List<Urlencoded> urlencoded { get; set; }
		public string mode { get; set; }
	}

	public class BodyByFormData
	{
		public List<FormData> formdata { get; set; }
		public string mode { get; set; }
	}

	public class Urlencoded
	{
		public string key { get; set; }
		public string value { get; set; }
		public string type { get; set; }
		public string description { get; set; }
	}

	public class Header
	{
		public string key { get; set; }
		public string value { get; set; }
		public string type { get; set; }
		public string description { get; set; }
	}

	public class Event
	{
		public string listen { get; set; }
		public Script script { get; set; }
	}

	public class Script
	{
		public string id { get; set; }
		public string type { get; set; }
		public List<string> exec { get; set; }
	}

	public class Variable1
	{
		public string id { get; set; }
		public string key { get; set; }
		public string value { get; set; }
		public string type { get; set; }
	}


	public class Response
	{
		public string name { get; set; }
		public Originalrequest originalRequest { get; set; }
		public string status { get; set; }
		public int code { get; set; }
		public string _postman_previewlanguage { get; set; }
		public List<Header> header { get; set; }
		public List<object> cookie { get; set; }
		public string body { get; set; }
	}

	public class Originalrequest
	{
		public string method { get; set; }
		public List<Header> header { get; set; }
		public Url url { get; set; }
	}


	public class Auth
	{
		public string type { get; set; }
		public List<Apikey> apikey { get; set; }
	}

	public class Apikey
	{
		public string key { get; set; }
		public string value { get; set; }
		public string type { get; set; }
	}


	public class FormData
	{
		public string key { get; set; }
		public string contentType { get; set; }
		public string description { get; set; }
		public string type { get; set; }
		public string src { get; set; }
	}

}
