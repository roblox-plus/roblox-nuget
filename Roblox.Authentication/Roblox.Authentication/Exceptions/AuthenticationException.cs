using System;
using TixFactory.Http;

namespace Roblox.Authentication
{
	/// <summary>
	/// An exception thrown when the authentication request fails.
	/// </summary>
	public class AuthenticationException : Exception
	{
		/// <summary>
		/// The username attempting to authenticate with.
		/// </summary>
		public string Username { get; set; }

		/// <summary>
		/// The <see cref="IHttpResponse"/> from the login request.
		/// </summary>
		public IHttpResponse HttpResponse { get; set; }

		/// <summary>
		/// Initializes a new <see cref="AuthenticationException"/>.
		/// </summary>
		/// <param name="username">The <see cref="Username"/>.</param>
		/// <param name="httpResponse">The <see cref="HttpResponse"/>.</param>
		public AuthenticationException(string username, IHttpResponse httpResponse)
		{
			Username = username;
			HttpResponse = httpResponse;
		}
	}
}
