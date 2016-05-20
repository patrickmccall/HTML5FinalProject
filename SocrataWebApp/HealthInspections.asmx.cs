using System.Web.Configuration;
using System.Web.Script.Services;
using System.Web.Services;
using SODA;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SocrataWebApp
{
  /// <summary>
  /// Summary description for HealthInspections
  /// </summary>
  [WebService(Namespace = "https://healthinspectionmap.azurewebsites.net/")]
  [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
  [System.ComponentModel.ToolboxItem(false)]
  // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
  [System.Web.Script.Services.ScriptService]
  public class HealthInspections : System.Web.Services.WebService
  {

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetBusinesses()
    {
      double lat = 47.60653938;
      double lon = -122.3382229810;
      double boundingDistance = 0.0005 ;

      //get some candidates for searching using the bounding distance and the center point
      double top = lat + boundingDistance;
      double bottom= lat - boundingDistance;

      double left = lon + boundingDistance;
      double right = lon - boundingDistance;


      string appToken = WebConfigurationManager.AppSettings["AppToken"];
      SodaClient client = new SodaClient(@"data.kingcounty.gov", appToken);

      var dataset = client.GetResource<Dictionary<string, object>>("f29f-zza5");

      var soql = new SoqlQuery();
      
      soql.Select("business_id","name", "address", "city", "zip_code", "longitude", "latitude");

      string whereClause = "latitude <" + top
                + " AND latitude > " + bottom
                + " AND longitude <" + left
                + " AND longitude >" + right
      ;
      //string whereClause = "zip_code = '98109'";
      soql.Where(whereClause);
      soql.Group("business_id", "name", "address", "city", "zip_code", "longitude", "latitude");

      var results = dataset.Query<object>(soql);

      return JsonConvert.SerializeObject(results);
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetInspectionsByBusinessId(string businessId)
    {

      string appToken = WebConfigurationManager.AppSettings["AppToken"];
      SodaClient client = new SodaClient(@"data.kingcounty.gov", appToken);

      var dataset = client.GetResource<Dictionary<string, object>>("f29f-zza5");

      var soql = new SoqlQuery();

      soql.Select("business_id", "inspection_serial_num", "inspection_score", "inspection_result","inspection_date");

      string whereClause = "business_id = '" + businessId +"'"; 
      ;

      soql.Where(whereClause);
      soql.Group("business_id", "inspection_serial_num", "inspection_score", "inspection_result", "inspection_date");

      var results = dataset.Query<object>(soql);

      return JsonConvert.SerializeObject(results);
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetViolationsByInspectionId(string InspectionId)
    {

      string appToken = WebConfigurationManager.AppSettings["AppToken"];
      SodaClient client = new SodaClient(@"data.kingcounty.gov", appToken);

      var dataset = client.GetResource<Dictionary<string, object>>("f29f-zza5");

      var soql = new SoqlQuery();

      soql.Select("inspection_serial_num", "violation_record_id", "violation_description", "violation_type", "violation_points");

      string whereClause = "inspection_serial_num = '" + InspectionId + "'";
      ;

      soql.Where(whereClause);
      soql.Group("inspection_serial_num", "violation_record_id", "violation_description", "violation_type", "violation_points");

      var results = dataset.Query<object>(soql);

      return JsonConvert.SerializeObject(results);
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetNearbyBusinesses(double lat = 47.60653938, double lon = -122.3382229810, double boundingDistance = 0.0005)
    {
      //double lat = 47.60653938;
      //double lon = -122.3382229810;
      //double boundingDistance = 0.0005;

      //get some candidates for searching using the bounding distance and the center point
      double top = lat + boundingDistance;
      double bottom = lat - boundingDistance;

      double left = lon + boundingDistance;
      double right = lon - boundingDistance;


      string appToken = WebConfigurationManager.AppSettings["AppToken"];
      SodaClient client = new SodaClient(@"data.kingcounty.gov", appToken);

      var dataset = client.GetResource<Dictionary<string, object>>("f29f-zza5");

      var soql = new SoqlQuery();

      soql.Select("business_id", "name", "address", "city", "zip_code", "longitude", "latitude");

      //string locationEncoded =@'{"type": "Point","coordinates": [' + lon + ', ' + lat + ']}';

      string whereClause = "within_circle(" + "";
      //string whereClause = "latitude <" + top
      //          + " AND latitude > " + bottom
      //          + " AND longitude <" + left
      //          + " AND longitude >" + right
      //;
      //string whereClause = "zip_code = '98109'";
      soql.Where(whereClause);
      soql.Group("business_id", "name", "address", "city", "zip_code", "longitude", "latitude");

      var results = dataset.Query<object>(soql);

      return JsonConvert.SerializeObject(results);
    }


    static bool WithinRadius(int lat, int lon, int centerLat, int centerLon, int radius)
    {
      return true;
    }
  }
}
