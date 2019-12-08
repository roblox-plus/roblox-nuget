using System.Runtime.Serialization;

namespace Roblox.Users
{
	[DataContract]
	internal class SkinnyUserResponse
	{
		[DataMember(Name = "id")]
		public long Id { get; set; }

		[DataMember(Name = "name")]
		public string Name { get; set; }
	}
}
