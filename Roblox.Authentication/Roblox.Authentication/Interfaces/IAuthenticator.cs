using Roblox.Users;

namespace Roblox.Authentication
{
	/// <summary>
	/// Responsible for authenticating Roblox users.
	/// </summary>
	public interface IAuthenticator
	{
		/// <summary>
		/// Gets the authenticated user.
		/// </summary>
		/// <returns>The authenticated <see cref="IUserIdentifier"/> (or <c>null</c> if there isn't one.)</returns>
		IUserIdentifier GetAuthenticatedUser();
		
		/// <summary>
		/// Directly sets the authentication cookie assuming one already exists.
		/// </summary>
		/// <remarks>
		/// The .ROBLOSECURITY cookie must already exist in the cookie jar for it to be replaced.
		/// This method does not create new cookies right now.
		/// </remarks>
		/// <param name="robloSecurity">The .ROBLOSECURITY cookie value.</param>
		/// <returns>The <see cref="IUserIdentifier"/> the cookie is associated with (or <c>null</c> if the cookie was not properly replaced or has an invalid value).</returns>
		IUserIdentifier SetAuthenticationCookie(string robloSecurity);
	}
}
