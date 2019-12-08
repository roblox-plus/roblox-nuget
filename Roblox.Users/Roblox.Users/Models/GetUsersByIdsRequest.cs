using System.Runtime.Serialization;

namespace Roblox.Users
{
	[DataContract]
	internal class GetUsersByIdsRequest
	{
		[DataMember(Name = "userIds")]
		public long[] UserIds { get; set; }
	}
}
