using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.DTO_s;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    
    public class ProductsController : CustomBaseController
    {
        //Best practice olarak controllerları mümkün olduğunca sade bırakacağız. Business kodu Service katmanında bulunduracağız. Mapleme burada gerçekleşecek. Daha sonra maplemeyi service katmanında gerçekleştireceğiz.

        private readonly IMapper _mapper;
        //private readonly IService<Product> _service;
        private readonly IProductService _service; //IService'i zaten implement ettiği için kaldırdık

        public ProductsController(IMapper mapper, IProductService productService)
        {
            
            _mapper = mapper;
            _service = productService;
        }

        //GET api/products/GetProductWithCategory
        [HttpGet("[action]")]   
        public async Task<IActionResult> GetProductWithCategory()
        {
            return CreateActionResult(await _service.GetProductsWithCategory());
        }



        // GET api/products
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await _service.GetAllAsync();

            var productsDtos = _mapper.Map<List<ProductDto>>(products.ToList());
            //return Ok(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));

            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
        }

        // [ValidateFilter] kullanamayız. 
        [ServiceFilter(typeof(NotFoundFilter<Product>))] 
        //Eğer bir filter constructorunda bir parametre alıyorsa mutlaka servicefilter üzerimden kullanmamamız lazım. Belirtmiş olduğumuz tipi de program.cs'te service'e eklememiz lazım.
        ///GET api/products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            var productsDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productsDto));
        }

           


        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var product = await _service.AddAsync(_mapper.Map<Product>(productDto)); //ProductDto'yu producta dönüştür
            var productsDto = _mapper.Map<ProductDto>(product); //product'ı productDto'ya dönüştür
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(201, productsDto)); //201 creadted durum kodu 
        }

            
        [HttpPut]

        public async Task<IActionResult> Update(ProductUpdateDto productDto)
        {
            await _service.UpdateAsync(_mapper.Map<Product>(productDto)); //geriye bir şey dönmüyor, maplemeye de gerek yok
           
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204)); //geriye bir şey dönmüyor
        }

        // DELETE api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _service.GetByIdAsync(id);
           
            await _service.RemoveAsync(product);
            
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204)); //geriye boş sınıf dönüyor
        }
    }
}

            


