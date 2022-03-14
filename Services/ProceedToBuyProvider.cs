using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ProceedToBuy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace ProceedToBuy.Services
{
    public class ProceedToBuyProvider:IProceedToBuyProvider
    {
        

            private IConfiguration _configure;
            static private string apiBaseUrl;
            public ProceedToBuyProvider(IConfiguration configure)
            {
                _configure = configure;
                apiBaseUrl = _configure.GetValue<string>("WebAPIBaseUrl");

            }
        public async  Task<Vendor> GetVendors(int productId)
        {
            List<Vendor> vendors=new List<Vendor>() ;
            string apiBaseUrl = $"https://localhost:44354/api/Vendor/{productId}";
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                var response = client.GetAsync(apiBaseUrl);
                response.Wait();
                var result = response.Result;
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync(apiBaseUrl);

                if (Res.IsSuccessStatusCode)
                {
                    var readData = Res.Content.ReadAsAsync<List<Vendor>>().Result;
                   // readData.Wait();
                   
                    vendors = readData;
                   
                   
                }
                
            }
            if (vendors.Count > 0)
            {
                int max = vendors.Max(v => v.Rating);
                Vendor vendor = vendors.FirstOrDefault(v => v.Rating == max);
                return vendor;
            }
            return null;
            


        }



    }
    }

