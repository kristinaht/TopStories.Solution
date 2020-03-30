using System;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq; //this is where JObject comes from. JObject is .NET object that can be treated as JSON
using System.Collections.Generic;
using ConsoleApiCall;

namespace ApiTest
{
  class Program
  {
    static void Main()
    {
      var apiCallTask = ApiHelper.ApiCall("Ws7xotfERIVKDMORGlCVhlmsfcoy8xKB");
      var result = apiCallTask.Result;

      JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(result);
      List<Article> articleList = JsonConvert.DeserializeObject<List<Article>>(jsonResponse["results"].ToString()); //DeserializeObject used to create list of articles. this method grabs JSON keys in the response that match properties in Article class. Properties names need to match JSON keys.

      foreach (Article article in articleList)
      {
        Console.WriteLine($"Section: {article.Section}");
        Console.WriteLine($"Title: {article.Title}");
        Console.WriteLine($"Abstract: {article.Abstract}");
        Console.WriteLine($"Url: {article.Url}");
        Console.WriteLine($"Byline: {article.Byline}");
      }
    }
    //CODE BELOW WAS USED TO SHOW RESULT IN CONSOLE AS API RESULT
    // static void Main(string[] args)
    // {
    //   var apiCallTask = ApiHelper.ApiCall("Ws7xotfERIVKDMORGlCVhlmsfcoy8xKB");//this variable takes the returned Task from the async function in the ApiHelper class below. .We than call the ApiCall method and pass our api key in

    //   //using code below we turn giant string stored as result into JSON data.
    //   var result = apiCallTask.Result;
    //   JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(result); //this is where the conversion happens.
    //   Console.WriteLine(jsonResponse["results"]);
    // }
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