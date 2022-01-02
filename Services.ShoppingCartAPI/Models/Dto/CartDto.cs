using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.ShoppingCartAPI.Models.Dto
{
    public class CartDto
    {
        public CartHeaderDto Cartheader { get; set; }
        public IEnumerable<CartDetailsDto> CartDetails { get; set; }

    }
}
