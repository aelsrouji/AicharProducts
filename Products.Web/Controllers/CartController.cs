using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Products.Web.Models;
using Products.Web.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        

        public CartController(IProductService productService, ICartService cartService)
        {
            _productService = productService;
            _cartService = cartService;
        }

        public async Task<IActionResult> CartIndex()
        {
            return View(await LoadCartDtoBasedOnLoggedInUser());
        }

        public async Task<IActionResult> Remove(int cartDetailsId)
        {
            //todo: after adding authentication - identity server 
            //var userId = User.Claims.Where(_ => _.Type == "sub")?.FirstOrDefault()?.Value;
            //var accessToken = await HttpContext.GetTokenAsync("access_token");
            //var response = _cartService.GetCartByUserIdAsync<ResponseDto>(userId, accessToken);
            var response = await _cartService.RemoveFromCartAsync<ResponseDto>(cartDetailsId, null);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }

        private async Task<CartDto> LoadCartDtoBasedOnLoggedInUser()
        {
           //todo: after adding authentication - identity server 
          //var userId = User.Claims.Where(_ => _.Type == "sub")?.FirstOrDefault()?.Value;
          //var accessToken = await HttpContext.GetTokenAsync("access_token");
            //var response = _cartService.GetCartByUserIdAsync<ResponseDto>(userId, accessToken);
            var response = await _cartService.GetCartByUserIdAsync<ResponseDto>("1", null);

            CartDto cartDto = new();
            if (response != null && response.IsSuccess)
            {
                cartDto = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(response.Result));
            }

            if (cartDto.CartHeader != null)
            {
                foreach (var detail in cartDto.CartDetails)
                {
                    cartDto.CartHeader.OrderTotal += detail.Product.Price * detail.Count;
                }

            }
            return cartDto;
        }
    }
}
