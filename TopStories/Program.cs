using System;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq; //this is where JObject comes from. JObject is .NET object that can be treated as JSON

namespace ApiTest
{
  class Program
  {
    static void Main(string[] args)
    {
      var apiCallTask = ApiHelper.ApiCall("Ws7xotfERIVKDMORGlCVhlmsfcoy8xKB");//this variable takes the returned Task from the async function in the ApiHelper class below. .We than call the ApiCall method and pass our api key in

      //using code below we turn giant string stored as result into JSON data.
      var result = apiCallTask.Result;
      JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(result); //this is where the conversion happens.
      Console.WriteLine(jsonResponse["results"]);
    }
  }

  class ApiHelper
  {
    public static async Task<string> ApiCall(string apiKey) //whenever e method is declared as async we need to return a Task type
    {
      RestClient client = new RestClient("https://api.nytimes.com/svc/topstories/v2"); //we instantiate the RestSharp.RestClient object and in it we store the connection. We call this variable client.
      RestRequest request = new RestRequest($"home.json?api-key={apiKey}", Method.GET);
      var response = await client.ExecuteTaskAsync(request); //ExecuteTAskAsync is RestClient's method
      return response.Content; //returns Content of response variable
    }
  }
}