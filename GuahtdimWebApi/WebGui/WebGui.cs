using Nancy;

namespace GuahtdimWebApi.WebGui
{
	public class WebGuiModule:NancyModule
	{

		public WebGuiModule():base("/")
		{
			//StaticConfiguration.DisableErrorTraces = false;

			Get["/"]= parameters =>
			{
				var fullInfo = GetFullInfoModel();
				return View["fullInfo.sshtml", fullInfo];
				
				
			};
		}

		private GuahtdimFullInfoModel GetFullInfoModel()
		{
				//Get all needed data from Dapper
			var dapperObj = new DapperFullInfo();
			return dapperObj.GetFullInfo();
			
		}
	}
}