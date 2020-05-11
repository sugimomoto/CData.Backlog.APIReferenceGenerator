using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CData.Backlog.APIReferenceGenerator
{
    class PostmanCollectionGenerator
    {
		private PostmanCollection postmanCollection;

		private bool IsJp;
		public PostmanCollectionGenerator(bool isJp)
		{
			this.IsJp = isJp;

			postmanCollection = new PostmanCollection()
			{
				info = new Info()
				{
					_postman_id = new Guid().ToString(),
					name = "Backlog Postman Collection" + (isJp ? " JP" : " EN"),
					description = "## About Backlog\r\n\r\nBacklog is a popular cloud-based project management SaaS in Japan.",
					schema = "https://schema.getpostman.com/json/collection/v2.1.0/collection.json"
				},
				item = new List<Item>(),
				_event = new List<Event>(),
				variable = new List<Variable1>(),
				protocolProfileBehavior = new Protocolprofilebehavior(),
				auth = new Auth()
				{
					type = "apiKey",
					apikey = new List<Apikey>()
				}
			};

			postmanCollection.variable.Add(new Variable1()
			{
				id = new Guid().ToString(),
				key = "ApiKey",
				value = "YOUR_API_KEY",
				type = "string"
			}
			);

			postmanCollection.variable.Add(new Variable1()
			{
				id = new Guid().ToString(),
				key = "BackLogUrl",
				value = "https://cdataa.backlog.com",
				type = "string"
			}
			);

			postmanCollection._event.Add(new Event()
			{
				listen = "prerequest",
				script = new Script()
				{
					id = new Guid().ToString(),
					type = "text/javascript",
					exec = new List<string>()
				}
			});

			postmanCollection._event.Add(new Event()
			{
				listen = "test",
				script = new Script()
				{
					id = new Guid().ToString(),
					type = "text/javascript",
					exec = new List<string>()
				}
			});

			postmanCollection.auth.apikey.Add(new Apikey()
			{
				key = "value",
				value = "{{ApiKey}}",
				type = "string"
			});
			postmanCollection.auth.apikey.Add(new Apikey()
			{
				key = "key",
				value = "apiKey",
				type = "string"
			});
			postmanCollection.auth.apikey.Add(new Apikey()
			{
				key = "in",
				value = "query",
				type = "string"
			});

		}

		public PostmanCollection Generate(List<BacklogAPI> backlogAPIs)
        {
			foreach (var api in backlogAPIs)
			{
				var item = new Item();
				item.name = api.APIName;
				item.request = new Request()
				{
					method = api.Method,
					description = $"# Description\r\n\r\n{api.Description}\r\n\r\n# Reference Url\r\n\r\n{api.ReferenceURL}",
					header = new List<Header>(),
					url = new Url()
					{
						raw = "{{BackLogUrl}}" + api.Url,
						host = new List<string>(),
						path = new List<string>(),
						query = new List<Query>()
					},
				};

				item.request.url.host.Add("{{BackLogUrl}}");
				item.request.url.path = api.Url.Trim('/').Split('/').ToList();
				item.request.url.query.Add(new Query()
				{
					key = "apiKey",
					value = "{{ApiKey}}",
					disabled = false
				});

				if (api.QueryParameters != null)
				{
					api.QueryParameters.ForEach(x =>
					{
						item.request.url.query.Add(new Query()
						{
							key = x.ParameterName,
							value = "<" + x.ParameterType + ">",
							description = x.ParameterContent + " : " + x.ParameterName,
							disabled = false
						});
					});
				}

				if (api.RequestParameters != null)
				{
					item.request.body = new BodyByUrlEncoded()
					{
						mode = "urlencoded",
						urlencoded = new List<Urlencoded>()
					};

					api.RequestParameters.ForEach(x =>
					{
						item.request.body.urlencoded.Add(new Urlencoded()
						{
							key = x.ParameterName.Replace("(必須)", "").Replace("(Required)", "").Trim(' '),
							value = "<" + x.ParameterType + ">",
							description = x.ParameterContent + " : " + x.ParameterName,
							type = "text"
						});
					});

					item.request.header.Add(new Header()
					{
						key = "Content-Type",
						value = api.ContentType
					});
				}

				// 添付ファイル送信リクエストの場合
				if (api.ReferenceURL.Contains("post-attachment-file"))
				{
					item.request.body = new BodyByFormData()
					{
						mode = "formdata",
						formdata = new List<FormData>()
					};

					item.request.body.formdata.Add(new FormData()
					{
						key = "file",
						contentType = "",
						description = "Attachment File",
						type = "file",
						src = ""
					});
				}

				postmanCollection.item.Add(item);
			}

			return postmanCollection;
		}
	}
}
