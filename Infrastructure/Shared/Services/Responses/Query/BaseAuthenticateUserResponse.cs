using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services.Responses.Query
{
	[DataContract]
	public class BaseAuthenticateUserResponse : AbstractResponse
	{
		[DataMember]
		public AuthenticationResults SignInResult { get; set; }

		[DataMember]
		public int? UserId { get; set; }

		[DataMember]
		public Guid GlobalUserId { get; set; }

		[DataMember]
		public Guid SessionIdentifier { get; set; }

		[DataMember]
		public string UserName { get; set; }
	}
}
