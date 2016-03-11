using System;

namespace GuahtdimWebApi
{
	using Nancy;
	using Nancy.Security;

	public class MainModule : NancyModule
	{
		public MainModule()
		{
			this.RequiresAuthentication();

			Get["/GetData"] = parameters =>
			{
				var data = new { Name = "John", LastName = "Smith" };

				return Negotiate.WithModel(data);
			};

			Post["/GetData"] = parameters =>
			{
				var data = new { Name = "Post", LastName = "Mann" };

				return Negotiate.WithModel(data);
			};
			
		}
	}
}
