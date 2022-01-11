using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Services.CouponAPI.DBContexts;
using Services.CouponAPI.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.CouponAPI.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly ApplicationDbContext _db;
        protected IMapper _mapper;

        public CouponRepository(ApplicationDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;

        }
        public async Task<CouponDto> GetCouponByCode(string code)
        {
            var couponFromDb = await _db.Coupons.FirstOrDefaultAsync(x => x.CouponCode == code);
            return _mapper.Map<CouponDto>(couponFromDb);

        }
    }
}
