namespace Roblox.Users
{
	/// <summary>
	/// A client for getting user information.
	/// </summary>
	public interface IUsersClient
	{
		/// <summary>
		/// Gets a username by user id.
		/// </summary>
		/// <param name="userId">The user Id.</param>
		/// <returns>The username (or <c>null</c> if the user with the id doesn't exist).</returns>
		string GetUsernameByUserId(long userId);

		/// <summary>
		/// Gets a user Id by username.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <returns>The user Id (or <c>null</c> if the user id doesn't exist).</returns>
		long? GetUserIdByUsername(string username);
	}
}
