using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FundsLibrary.InterviewTest.Web.Repositories
{
	public interface IHttpClientWrapper
	{
		Task<T> GetAndReadFromContentGetAsync<T>(string apiFundmanager);
	}

	public class HttpClientWrapper : IHttpClientWrapper
	{
		private readonly string _serviceAppUrl;

		public HttpClientWrapper(string serviceAppUrl)
		{
			_serviceAppUrl = serviceAppUrl;
		}

		public async Task<T> GetAndReadFromContentGetAsync<T>(string requestUri)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_serviceAppUrl);

				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.GetAsync(requestUri);
				response.EnsureSuccessStatusCode(); //TODO: Handle non success HTTP codes more gracefully.

				return await response.Content.ReadAsAsync<T>();
			}
		}
	}
}
