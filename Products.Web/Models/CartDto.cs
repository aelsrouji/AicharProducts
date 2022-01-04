using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products.Web.Models
{
    public class CartDto
    {
        public CartHeaderDto Cartheader { get; set; }
        public IEnumerable<CartDetailsDto> CartDetails { get; set; }

    }
}
