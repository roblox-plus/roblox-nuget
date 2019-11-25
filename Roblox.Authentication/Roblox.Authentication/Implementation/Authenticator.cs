using System;
using System.Net;
using Roblox.Users;
using TixFactory.CookieJar;
using TixFactory.Http;
using TixFactory.Http.Client;
using HttpClient = TixFactory.Http.Client.HttpClient;
using HttpMethod = TixFactory.Http.HttpMethod;

namespace Roblox.Authentication
{
	/// <inheritdoc cref="IAuthenticator"/>
	public class Authenticator : IAuthenticator
	{
		private const string _GetCurrentUserUrl = "https://assetgame.roblox.com/Game/GetCurrentUser.ashx";
		private const string _RobloSecurityCookieName = ".ROBLOSECURITY";

		private readonly ICookieJar _CookieJar;
		private readonly IHttpClient _HttpClient;

		/// <summary>
		/// Initializes a new <see cref="Authenticator"/>.
		/// </summary>
		/// <param name="cookieJar"></param>
		public Authenticator(ICookieJar cookieJar)
		{
			_CookieJar = cookieJar ?? throw new ArgumentNullException(nameof(cookieJar));

			_HttpClient = new HttpClient(cookieJar);
			_HttpClient.Handlers.Insert(0, new XsrfRetryHandler());
		}

		/// <inheritdoc cref="IAuthenticator.GetAuthenticatedUser"/>
		public IUserIdentifier GetAuthenticatedUser()
		{
			var getUserIdRequest = new HttpRequest(HttpMethod.Get, new Uri(_GetCurrentUserUrl));
			var getUserIdResponse = _HttpClient.Send(getUserIdRequest);

			if (!long.TryParse(getUserIdResponse.GetStringBody(), out var userId))
			{
				return null;
			}

			var getUsernameRequest = new HttpRequest(HttpMethod.Get, new Uri(_GetCurrentUserUrl + "?field=name"));
			var getUsernameResponse = _HttpClient.Send(getUsernameRequest);

			return new UserIdentifier
			{
				Id = userId,
				Name = getUsernameResponse.GetStringBody()
			};
		}

		/// <inheritdoc cref="IAuthenticator.SetAuthenticationCookie"/>
		public IUserIdentifier SetAuthenticationCookie(string robloSecurity)
		{
			var replaced = false;
			foreach (Cookie cookie in _CookieJar.CookieContainer.GetCookies(new Uri("https://www.roblox.com")))
			{
				if (cookie.Name == _RobloSecurityCookieName)
				{
					cookie.Value = robloSecurity;
					replaced = true;
					break;
				}
			}

			if (replaced)
			{
				return GetAuthenticatedUser();
			}

			return null;
		}
	}
}
