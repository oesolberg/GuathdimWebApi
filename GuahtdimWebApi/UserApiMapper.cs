using System.Configuration;

namespace GuahtdimWebApi
{
	using Nancy.Security;

	public class UserApiMapper : IUserApiMapper
	{
		public IUserIdentity GetUserFromAccessToken(string accessToken)
		{
			var correctToken = ConfigurationManager.AppSettings["GuahtdimBearerToken"];
			if (accessToken == correctToken)
				return new GuahtdimUserIdentity(){ UserName = "Netduino" };
			return null;
		}
	}
}
