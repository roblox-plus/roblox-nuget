using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using TixFactory.Collections;
using TixFactory.Http;
using TixFactory.Http.Client;
using HttpMethod = TixFactory.Http.HttpMethod;

namespace Roblox.Users
{
	/// <inheritdoc cref="IUsersClient"/>
	public class UsersClient : IUsersClient
	{
		private readonly IHttpClient _HttpClient;
		private readonly IDictionary<long, string> _UsernamesByUserId;
		private readonly IDictionary<string, long> _UserIdsByUsername;
		private readonly Uri _GetUsersByIdsUrl = new Uri("https://users.roblox.com/v1/users");
		private readonly Uri _GetUsersByNamesUrl = new Uri("https://users.roblox.com/v1/usernames/users");

		/// <summary>
		/// Initializes a new <see cref="UsersClient"/>.
		/// </summary>
		/// <param name="httpClient">An <see cref="IHttpClient"/>.</param>
		/// <exception cref="ArgumentNullException">
		/// - <paramref name="httpClient"/>
		/// </exception>
		public UsersClient(IHttpClient httpClient)
		{
			_HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

			// Usernames can change, expire them after 5 minutes.
			_UsernamesByUserId = new ExpirableDictionary<long, string>(TimeSpan.FromMinutes(5), ExpirationPolicy.RenewOnWrite);

			// Username -> User Id is basically not going to change.
			_UserIdsByUsername = new ExpirableDictionary<string, long>(TimeSpan.FromHours(1), ExpirationPolicy.RenewOnRead);
		}

		/// <inheritdoc cref="IUsersClient.GetUsernameByUserId"/>
		public string GetUsernameByUserId(long userId)
		{
			if (userId <= 0)
			{
				return null;
			}

			if (_UsernamesByUserId.TryGetValue(userId, out var username))
			{
				return username;
			}

			// TODO: Do the requests in batches if multiple requests come in at the same time.

			var request = new HttpRequest(HttpMethod.Post, _GetUsersByIdsUrl);
			var requestModel = new GetUsersByIdsRequest
			{
				UserIds = new[] { userId }
			};

			request.Body = new StringContent(JsonConvert.SerializeObject(requestModel));
			request.Body.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			request.Headers.Add("Content-Type", "application/json");

			var response = _HttpClient.Send(request);
			var responseModel = JsonConvert.DeserializeObject<ArrayResponse<SkinnyUserResponse>>(response.GetStringBody());

			foreach (var user in responseModel.Data)
			{
				_UserIdsByUsername[user.Name] = user.Id;
				_UsernamesByUserId[user.Id] = user.Name;

				if (user.Id == userId)
				{
					username = user.Name;
				}
			}

			return username;
		}

		/// <inheritdoc cref="IUsersClient.GetUserIdByUsername"/>
		public long? GetUserIdByUsername(string username)
		{
			if (string.IsNullOrWhiteSpace(username))
			{
				return null;
			}

			if (_UserIdsByUsername.TryGetValue(username, out var userId))
			{
				return userId;
			}

			// TODO: Do the requests in batches if multiple requests come in at the same time.

			var request = new HttpRequest(HttpMethod.Post, _GetUsersByNamesUrl);
			var requestModel = new GetUsersByNamesRequest
			{
				Usernames = new[] { username }
			};

			request.Body = new StringContent(JsonConvert.SerializeObject(requestModel));
			request.Headers.Add("Content-Type", "application/json");

			var response = _HttpClient.Send(request);
			var responseModel = JsonConvert.DeserializeObject<ArrayResponse<SkinnyUserResponse>>(response.GetStringBody());

			foreach (var user in responseModel.Data)
			{
				_UserIdsByUsername[user.Name] = user.Id;
				_UsernamesByUserId[user.Id] = user.Name;

				if (user.Name.Equals(username, StringComparison.OrdinalIgnoreCase))
				{
					userId = user.Id;
				}
			}

			return userId;
		}
	}
}
