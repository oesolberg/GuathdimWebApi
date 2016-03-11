using System.Diagnostics;
using GuahtdimWebApi.TempAndHumidity;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;

namespace GuahtdimWebApi.TempAndHumidity
{
	public class TempAndHumidityModule:NancyModule
	{

		public TempAndHumidityModule():base("/tempandhumidity")
		{
			this.RequiresAuthentication();

			
			Post["/", true] = async (x, ctx) =>
			{
				TempAndHumidityData tempAndHumidityData = this.Bind<TempAndHumidityData>();

				Debug.Write(tempAndHumidityData.Temperature);
				//Do saving with dapper
				var tempAndHumiditySaver = new TempAndHumidityDataSaver();
				tempAndHumiditySaver.InsertTempAndHumidityData(tempAndHumidityData);

				return "temp and humidity data entered";
			};

		}
		 
	}
}