using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Roblox.Users
{
	[DataContract]
	internal class ArrayResponse<T>
	{
		[DataMember(Name = "data")]
		public IReadOnlyCollection<T> Data { get; set; }
	}
}
