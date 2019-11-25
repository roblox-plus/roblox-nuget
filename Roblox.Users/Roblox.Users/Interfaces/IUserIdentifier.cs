namespace Roblox.Users
{
	/// <summary>
	/// Identifying user information.
	/// </summary>
	public interface IUserIdentifier
	{
		/// <summary>
		/// The user Id.
		/// </summary>
		long Id { get; set; }

		/// <summary>
		/// The username.
		/// </summary>
		string Name { get; set; }
	}
}
