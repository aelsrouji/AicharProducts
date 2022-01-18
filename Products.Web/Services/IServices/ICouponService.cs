﻿using Products.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Products.Web.Services.IServices
{
    public interface ICouponService
    {
  
        Task<T> GetCoupon<T>(string couponCode, string token = null);
        
    }
}
