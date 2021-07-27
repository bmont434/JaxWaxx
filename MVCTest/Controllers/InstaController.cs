using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Routing;

namespace MVCTest.Controllers
{
    public class InstaController : Controller
    {

        [HttpGet]
        public IActionResult InstaIntegration()
        {

            if (string.IsNullOrEmpty(HttpContext.Request.Query["code"].ToString()) != true)
            {
                string AccessTkn = HttpContext.Request.Query["code"].ToString();
                Task<MyData> Post1 = Postr(AccessTkn);
                Post1.Wait();
                getr(Post1.Result).Wait();
                return View();
            }
            else
            {
                return View();
            }

        }

        public async Task<InstaData> getr(MyData ID)
        {
            ViewBag.AccessToken = ID.access_token;
            ViewBag.UserID = ID.user_id;

            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(new HttpMethod("GET"), "https://graph.instagram.com/" + ID.user_id.ToString() + "?fields=id,media_type,media_url,username&access_token=" + ID.access_token.ToString());
            var response = await httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            InstaData tmp = JsonConvert.DeserializeObject<InstaData>(content);

            if (tmp.id != null)
            {
                ViewBag.AccessToken = tmp.username;
                return tmp;
            }
            else
            {
                return null;
            }

        }


        public async Task<MyData> Postr(string AccessTkn)
        {
            var httpClient = new HttpClient();

            var request = new HttpRequestMessage(new HttpMethod("POST"), "https://api.instagram.com/oauth/access_token");

            var multipartContent = new MultipartFormDataContent();
            multipartContent.Add(new StringContent(""), "client_id");
            multipartContent.Add(new StringContent(""), "client_secret");
            multipartContent.Add(new StringContent("authorization_code"), "grant_type");
            multipartContent.Add(new StringContent("https://localhost:44344/Insta/InstaIntegration/"), "redirect_uri");
            multipartContent.Add(new StringContent(AccessTkn), "code");
            request.Content = multipartContent;

            var response = await httpClient.SendAsync(request);
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
        public string access_token { get; set; }
        public string user_id { get; set; }

    }

    public class InstaData
    {
        public string media_type { get; set; }
        public string id { get; set; }
        public string media_url { get; set; }
        public string username { get; set; }

    }

}
