using Products.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Products.Web.Services.IServices
{
    public class CartService : BaseService, ICartService
    {
        private readonly IHttpClientFactory _clientFactory;

        public CartService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }


        public async Task<T> AddToCartAsync<T>(CartDto cartDto, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Post,
                Data = cartDto,
                Url = SD.ShoppingCartAPIBase + "api/cart/AddCart",
                AccessToken = ""
            });
        }

        public async Task<T> GetCartByUserIdAsync<T>(string userId=null, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Get,
                //todo: update 1 to userId when identity server is implemented
                Url = SD.ShoppingCartAPIBase + "api/cart/GetCart/1" ,//+ userId,
                AccessToken = ""
            });
        }

        public async Task<T> RemoveFromCartAsync<T>(int cartId, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Post,
                Data = cartId,
                Url = SD.ShoppingCartAPIBase + "api/cart/RemoveCart",
                AccessToken = ""
            });
        }

        public async Task<T> UpdateCartAsync<T>(CartDto cartDto, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Post,
                Data = cartDto,
                Url = SD.ShoppingCartAPIBase + "api/cart/UpdateCart",
                AccessToken = ""
            });
        }
    }
}
