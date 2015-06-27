using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace CampervibeBooking.UI.ServiceProxies.Vehicle
{
    public class VehicleServiceProxy : IVehicleServiceProxy
    {
        public IList<VehicleServiceModel> GetAll()
        {
            var request = WebRequest.Create(@"http://campervibevehicleinventory.azurewebsites.net/vehicle/restindex");
            request.Method = "GET";

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            using (var responseStream = response.GetResponseStream())
            {
                var reader = new StreamReader(responseStream, Encoding.UTF8);
                var readerString = reader.ReadToEnd();
                var vehicleServiceModels = new JavaScriptSerializer().Deserialize<List<VehicleServiceModel>>(readerString);
                return vehicleServiceModels;
            }
        }
    }
}