using System.Diagnostics;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;

namespace GuahtdimWebApi.Heater
{
	public class HeaterModule:NancyModule
	{

		public HeaterModule():base("/heater")
		{
			this.RequiresAuthentication();

			
			Post["/", true] = async (x, ctx) =>
			{
				HeaterData heaterData = this.Bind<HeaterData>();

				Debug.Write(heaterData.HeatOn);
				//Do saving with dapper
				var heatSaver = new HeaterDataSaver();
				heatSaver.InsertHeatData(heaterData);

				return "heater data entered";
			};

		}
		 
	}
}