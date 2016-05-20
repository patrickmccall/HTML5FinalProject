using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SODA;
using System.Data;
using Newtonsoft.Json;


namespace SocrataOpenData
{
  class Program
  {
    static void Main(string[] args)
    {
      //initial a new client
      string token = "bO8Xyyc5Szi9pNl095HzIGkOH";
      SodaClient client = new SodaClient(@"data.kingcounty.gov", token);

      //read metadat of a dataset
      ResourceMetadata metadata = client.GetMetadata("j56h-zgnm");
      Console.WriteLine("{0} has {1} views.", metadata.Name, metadata.ViewsCount);

      var dataset = client.GetResource<Dictionary<string, object>>("j56h-zgnm");

      //Console.WriteLine(dataset.GetType().ToString());
      var allRows = dataset.GetRows();

      var soql = new SoqlQuery();
      soql.Select("last_name");
      soql.Where("last_name='LOGAN'");

      var results = dataset.Query<object>(soql);

      Console.WriteLine( JsonConvert.SerializeObject(results).ToString());
      Console.ReadLine();

      

    }
  }
}
