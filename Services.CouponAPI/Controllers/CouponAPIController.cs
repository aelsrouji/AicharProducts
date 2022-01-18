using Microsoft.AspNetCore.Mvc;
using Services.CouponAPI.Models.Dto;
using Services.CouponAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.CouponAPI.Controllers
{
    [ApiController]
    [Route("api/coupon")]

    public class CouponAPIController : Controller
    {
        private readonly ICouponRepository _couponReposiroty;
        protected ResponseDto _response;

        public CouponAPIController(ICouponRepository couponRepository)
        {
            _couponReposiroty = couponRepository;
            this._response = new ResponseDto();
        }

        [HttpGet("{code}")]
        public async Task<object> GetDiscountForCode(string code)
        {
            try
            {
                var coupon = await _couponReposiroty.GetCouponByCode(code);
                _response.Result = coupon;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
