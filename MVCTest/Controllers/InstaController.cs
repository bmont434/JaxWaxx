using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.IO;
using System.Text;
using Newtonsoft.Json;


namespace MVCTest.Controllers
{
    public class InstaController : Controller
    {

        [HttpGet]
        public IActionResult InstaIntegration()
        {
            
            Task<MyData> test12 = Postr();
            test12.Wait();
            getr(test12.Result);
            return View();

        }


        public async void getr(MyData ID)
        {
            //var client = new HttpClient();
            //var content = client.GetStringAsync("http://webcode.me");
            ViewBag.AccessToken = ID.access_token;
            ViewBag.UserID = ID.user_id;

            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(new HttpMethod("GET"), "https://graph.instagram.com/" + ID.user_id.ToString() + "?fields=id,username&access_token=" + ID.access_token.ToString());
            var response = await httpClient.SendAsync(request);
                
            

        }


        public async Task<MyData> Postr()
        {
            var httpClient = new HttpClient();

            var request = new HttpRequestMessage(new HttpMethod("POST"), "https://api.instagram.com/oauth/access_token");

            var multipartContent = new MultipartFormDataContent();
            multipartContent.Add(new StringContent(""), "client_id");
            multipartContent.Add(new StringContent(""), "client_secret");
            multipartContent.Add(new StringContent("authorization_code"), "grant_type");
            multipartContent.Add(new StringContent("https://localhost:44344/Insta/InstaIntegration/"), "redirect_uri");
            multipartContent.Add(new StringContent(""), "code");
            request.Content = multipartContent;

            var response = await httpClient.SendAsync(request);
            //ViewBag.test = "TEST TSETE TSETSET";
            var content = await response.Content.ReadAsStringAsync();
            MyData tmp = JsonConvert.DeserializeObject<MyData>(content);

            if (tmp.access_token != null)
            {

                return tmp;
            }
           else
            {
                return null;
            }
            
            

        }

    }

    public class MyData
    {
         public string access_token {get; set ;}
         public string user_id { get; set; }

    }

}
