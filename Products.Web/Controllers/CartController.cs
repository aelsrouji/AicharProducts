﻿using Microsoft.AspNetCore.Authentication;
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
        private readonly ICouponService _couponService;
        

        public CartController(IProductService productService, ICartService cartService, ICouponService couponService)
        {
            _productService = productService;
            _cartService = cartService;
            _couponService = couponService;
        }

        public async Task<IActionResult> CartIndex()
        {
            return View(await LoadCartDtoBasedOnLoggedInUser());
        }

        [HttpPost]
        [ActionName("ApplyCoupon")]
        public async Task<IActionResult> ApplyCoupon(CartDto cartDto)
        {
            //todo: after adding authentication - identity server 
            //var userId = User.Claims.Where(_ => _.Type == "sub")?.FirstOrDefault()?.Value;
            //var accessToken = await HttpContext.GetTokenAsync("access_token");
            //var response = _cartService.GetCartByUserIdAsync<ResponseDto>(cartDto, accessToken);
            var response = await _cartService.ApplyCoupon<ResponseDto>(cartDto, null);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(CartIndex));
            }
            return View();

        }

        [HttpPost]
        [ActionName("RemoveCoupon")]
        public async Task<IActionResult> RemoveCoupon(CartDto cartDto)
        {
            //todo: after adding authentication - identity server 
            //var userId = User.Claims.Where(_ => _.Type == "sub")?.FirstOrDefault()?.Value;
            //var accessToken = await HttpContext.GetTokenAsync("access_token");
            //var response = await _cartService.RemoveCoupon<ResponseDto>(cartDto.CartHeader.UserId, accessToken);
            var response = await _cartService.RemoveCoupon<ResponseDto>(cartDto.CartHeader.UserId, null);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(CartIndex));
            }
            return View();

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

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            return View(await LoadCartDtoBasedOnLoggedInUser());

        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CartDto cartDto)
        {
            try
            {
                //var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _cartService.Checkout<ResponseDto>(cartDto.CartHeader, null); //accessToken)

                if (!response.IsSuccess)
                {
                    TempData["Error"] = response.DisplayMessage;
                    return RedirectToAction(nameof(Checkout));
                }

                return RedirectToAction(nameof(Confirmation));

            }
            catch (Exception Ex)
            {
                return View(cartDto);
            }

        }

        
        public async Task<IActionResult> Confirmation()
        {
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
                if (!string.IsNullOrEmpty(cartDto.CartHeader.CouponCode))
                {
                    var coupon = await _couponService.GetCoupon<ResponseDto>(cartDto.CartHeader.CouponCode); //accessToken)
                    if (coupon != null && coupon.IsSuccess)
                    {
                        var couponObj = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(coupon.Result));
                        cartDto.CartHeader.DiscountTotal = couponObj.DiscountAmount;
                    }
                }
                foreach (var detail in cartDto.CartDetails)
                {
                    cartDto.CartHeader.OrderTotal += detail.Product.Price * detail.Count;
                }

                cartDto.CartHeader.OrderTotal -= cartDto.CartHeader.DiscountTotal;

            }
            return cartDto;
        }
    }
}
