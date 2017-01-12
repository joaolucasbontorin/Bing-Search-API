using System;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using System.Web;
using System.Web.UI.WebControls;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Web.Script.Serialization;
//using System.Runtime.Serialization.Json;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using BING_API;
namespace BING_API
{
    static class Program
    {
        static void Main()
        {
            MakeRequest().Wait();
                   
            Console.ReadLine();
        }


        static async Task MakeRequest()
        {
            int contador = 0;
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "YOUR SUBSCRIPTION KEY");       

            // Request parameters
            queryString["q"] = "joao lucas bontorin linkedin";
            queryString["count"] = "1";
            queryString["offset"] = "0";

            var uri = "https://api.cognitive.microsoft.com/bing/v5.0/search?" + queryString;
            var response =  await client.GetAsync(uri);
            var contents = await response.Content.ReadAsStringAsync();
            var request = JObject.Parse(contents);

            Console.WriteLine(request);
            Console.WriteLine("-----------------------------------------------------------------------");

            foreach (var a in request)
            {
                contador++;
                string resultado = (string)a.Value.SelectToken("value[0].displayUrl");           // GET INFORMATION FROM DISPLAYURL - JSON

                if (contador.Equals(2) && resultado != null)
                {
                    Console.WriteLine(resultado);

                }
            }          
            Console.ReadLine();
            
        }
    }

}