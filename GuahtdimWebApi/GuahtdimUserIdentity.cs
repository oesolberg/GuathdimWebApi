namespace GuahtdimWebApi
{

	using System.Collections.Generic;
	using Nancy.Security;

	public class GuahtdimUserIdentity : IUserIdentity
	{
		public string UserName { get; set; }

		public IEnumerable<string> Claims { get; set; }
	}
}
