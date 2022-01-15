using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.ProductsAPI.Models.Dto;
using Services.ProductsAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.ProductsAPI.Controllers
{
    [Route("api/products")]
    public class ProductAPIController : ControllerBase
    {
        private IProductRepositoty _productRepository;
        protected ResponseDto _response;

        public ProductAPIController(IProductRepositoty productRepositoty)
        {
            _productRepository =  productRepositoty;
            this._response = new ResponseDto(); 
        }

        //[Authorize]
        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                IEnumerable<ProductDto> productsDto = await _productRepository.GetProducts();
                _response.Result = productsDto;
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;
        }

        //[Authorize]
        [HttpGet]
        [Route("{id}")]
        public async Task<object> Get(int id)
        {
            try
            {
                ProductDto productDto = await _productRepository.GetProductById(id);
                _response.Result = productDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;
        }

        //[Authorize]
        [HttpPost]
        public async Task<object> Post([FromBody]ProductDto productDto)
        {
            try
            {
                ProductDto model = await _productRepository.CreateUpdateProduct(productDto);
                _response.Result = model;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;
        }

        [HttpPut]
        //[Authorize]
        public async Task<object> Put([FromBody] ProductDto productDto)
        {
            try
            {
                ProductDto model = await _productRepository.CreateUpdateProduct(productDto);
                _response.Result = model;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{productId}")]
        public async Task<object> Delete(int productId)
        {
            try
            {
                bool isSuccess = await _productRepository.DeleteProduct(productId);
                _response.Result = isSuccess;
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
