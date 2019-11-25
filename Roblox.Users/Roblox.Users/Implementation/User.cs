namespace Roblox.Users
{
	/// <inheritdoc cref="IUserIdentifier"/>
	public class UserIdentifier : IUserIdentifier
	{
		/// <inheritdoc cref="IUserIdentifier.Id"/>
		public long Id { get; set; }

		/// <inheritdoc cref="IUserIdentifier.Name"/>
		public string Name { get; set; }
	}
}
