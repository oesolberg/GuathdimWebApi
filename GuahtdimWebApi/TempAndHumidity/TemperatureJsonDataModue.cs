using System;
using System.Collections.Generic;
using System.Text;
using Nancy;
using Nancy.Json;

namespace GuahtdimWebApi.TempAndHumidity
{
    public class TemperatureJsonDataModue : NancyModule
    {

        public TemperatureJsonDataModue() : base("/tempandhumidityjson")
        {
            Get["/"] = parameters =>
            {
                var callback = Request.Query["callback"];
                return GetAllTemeratureDataAsJson(callback);
            };
        }

        private string GetAllTemeratureDataAsJson(string callbackValue)
        {
            return callbackValue + GetTemperatureListFromDapperObject();
            var temperatureList = GetTemperatureListFromDapperObject();
            var jsonConverter = new Nancy.Json.JavaScriptSerializer();
            return jsonConverter.Serialize(temperatureList);
        }

        private string GetTemperatureListFromDapperObject()
        {
            var dapperObj = new TempAndHumidityDataGetter();
            var tempAndHumidityList = dapperObj.GetTempAndHumidityData();
            return CreateStringListFromData(tempAndHumidityList);


            //return "([" +
            //    "[Date.UTC(2016,1,1),19.56],"+ Environment.NewLine +
            //    "[Date.UTC(2016,1,2),19.99]," +Environment.NewLine +
            //    "[Date.UTC(2016,1,3),17.56]," +Environment.NewLine +
            //    "[Date.UTC(2016,1,4),13.56]," +Environment.NewLine +
            //    "[Date.UTC(2016,1,5),21.56]" + Environment.NewLine +
            //"]);";
        }

        private string CreateStringListFromData(List<TempAndCreatedDateTime> tempAndHumidityList)
        {
            if (tempAndHumidityList == null || tempAndHumidityList.Count == 0)
                return "([]);";

            var dataStringBuilder = new StringBuilder();
            var isFirst = true;
            foreach (var tempAndCreatedDateTime in tempAndHumidityList)
            {
                if (!isFirst)
                {
                    dataStringBuilder.Append("," + Environment.NewLine);
                }
                dataStringBuilder.Append(tempAndCreatedDateTime.ToDataString());
                if (isFirst)
                {
                    isFirst = false;
                }

            }
            return "([" + Environment.NewLine + dataStringBuilder.ToString() + Environment.NewLine + "]);";
        }
    }

    internal class TemperatureDataObject

    {
        public DateTime CreatedDateTime { get; set; }
        public Double Temperature { get; set; }
    }
}