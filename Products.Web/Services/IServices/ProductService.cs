using Products.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Products.Web.Services.IServices
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IHttpClientFactory _clientFactory;

        public ProductService(IHttpClientFactory clientFactory): base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<T> CreateProductAsync<T>(ProductDto productDto)
        {
          return  await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Post,
                Data = productDto,
                Url = SD.ProductAPIBase + "api/products",
                AccessToken =""
            });
        }

        public async Task<T> DeleteProductAsync<T>(int Id)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Delete,
                Url = SD.ProductAPIBase + "api/products/" + Id,
                AccessToken = ""
            });
        }

        public async Task<T> GetAllProductsAsync<T>()
        {
           return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Get,
                Url = SD.ProductAPIBase + "api/products",
                AccessToken = ""
            });
        }

        public async Task<T> GetProductByIdAsync<T>(int Id)
        {
           return  await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Get,
                Url = SD.ProductAPIBase + "api/products/" + Id,
                AccessToken = ""
            });
        }

        public async Task<T> UpdateProductAsync<T>(ProductDto productDto)
        {
          return  await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Put,
                Data = productDto,
                Url = SD.ProductAPIBase + "api/products",
                AccessToken = ""
            });
        }
    }
}
