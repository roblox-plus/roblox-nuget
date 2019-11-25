using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TixFactory.Http;
using TixFactory.Http.Client;

namespace Roblox.Authentication
{
	/// <summary>
	/// An <see cref="IHttpClientHandler"/> for retrying failed X-CSRF-Tokens.
	/// </summary>
	public class XsrfRetryHandler : HttpClientHandlerBase
	{
		private const string _RobloxDomain = ".roblox.com";
		private const string _XsrfTokenHeaderName = "X-CSRF-TOKEN";
		private string _XsrfToken;

		/// <inheritdoc cref="HttpClientHandlerBase.Invoke"/>
		public override IHttpResponse Invoke(IHttpRequest request)
		{
			AddXsrfToken(request);
			var response = base.Invoke(request);

			if(UpdateXsrfToken(request, response))
			{
				response = base.Invoke(request);
			}

			return response;
		}

		/// <inheritdoc cref="HttpClientHandlerBase.InvokeAsync"/>
		public override async Task<IHttpResponse> InvokeAsync(IHttpRequest request, CancellationToken cancellationToken)
		{
			AddXsrfToken(request);
			var response = await base.InvokeAsync(request, cancellationToken).ConfigureAwait(false);

			if (UpdateXsrfToken(request, response))
			{
				response = await base.InvokeAsync(request, cancellationToken).ConfigureAwait(false);
			}

			return response;
		}

		private void AddXsrfToken(IHttpRequest httpRequest)
		{
			if (httpRequest?.Url.Host == null || !httpRequest.Url.Host.EndsWith(_RobloxDomain))
			{
				return;
			}

			if (!string.IsNullOrWhiteSpace(_XsrfToken) && (httpRequest.Method == HttpMethod.Post || httpRequest.Method == HttpMethod.Patch || httpRequest.Method == HttpMethod.Delete || httpRequest.Method == HttpMethod.Put))
			{
				httpRequest.Headers.AddOrUpdate(_XsrfTokenHeaderName, _XsrfToken);
			}
		}

		private bool UpdateXsrfToken(IHttpRequest httpRequest, IHttpResponse httpResponse)
		{
			if (httpRequest?.Url.Host == null || !httpRequest.Url.Host.EndsWith(_RobloxDomain))
			{
				return false;
			}

			var xsrfTokenValues = httpResponse.Headers.Get(_XsrfTokenHeaderName);
			if (xsrfTokenValues.Any())
			{
				_XsrfToken = xsrfTokenValues.FirstOrDefault();
				AddXsrfToken(httpRequest);
				return true;
			}

			return false;
		}
	}
}
