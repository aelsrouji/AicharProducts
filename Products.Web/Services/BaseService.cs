using Newtonsoft.Json;
using Products.Web.Models;
using Products.Web.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Products.Web.Services
{
    public class BaseService : IBaseService
    {
        public ResponseDto responseModel { get ; set; }

        public IHttpClientFactory httpClient { get; set; }

        public BaseService(IHttpClientFactory httpClient)
        {
            this.responseModel = new ResponseDto();
            this.httpClient = httpClient;
        }

        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                var client = httpClient.CreateClient("AicharAPI");
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
                httpRequestMessage.Headers.Add("Accept", "application/json");
                httpRequestMessage.RequestUri = new Uri(apiRequest.Url);
                client.DefaultRequestHeaders.Clear();
                if (apiRequest.Data != null)
                {
                    httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8);
                }

                HttpResponseMessage httpResponseMessage = null;

                switch (apiRequest.ApiType)
                {
                    case SD.ApiType.Get:
                        httpRequestMessage.Method = HttpMethod.Get;
                        break;
                    case SD.ApiType.Post:
                        httpRequestMessage.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.Put:
                        httpRequestMessage.Method = HttpMethod.Put;
                        break;
                    case SD.ApiType.Delete:
                        httpRequestMessage.Method = HttpMethod.Delete;
                        break;
                    default:
                        httpRequestMessage.Method = HttpMethod.Get;
                        break;
                }

                httpResponseMessage = await client.SendAsync(httpRequestMessage);
                var apiContent = await httpResponseMessage.Content.ReadAsStringAsync();
                var apiResponseDto = JsonConvert.DeserializeObject<T>(apiContent);
                return apiResponseDto;
            }
            catch (Exception ex)
            {
                var dto = new ResponseDto
                {
                    DisplayMessage = "Error",
                    ErrorMessages = new List<string> { Convert.ToString(ex.Message) },
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var apiResponseDto = JsonConvert.DeserializeObject<T>(res);
                return apiResponseDto;
         } 
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }

    }
}
