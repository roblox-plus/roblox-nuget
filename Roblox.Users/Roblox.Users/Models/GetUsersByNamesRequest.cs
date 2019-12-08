using System.Runtime.Serialization;

namespace Roblox.Users
{
	[DataContract]
	internal class GetUsersByNamesRequest
	{
		[DataMember(Name = "usernames")]
		public string[] Usernames { get; set; }
	}
}
