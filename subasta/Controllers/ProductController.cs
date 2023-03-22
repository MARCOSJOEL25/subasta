using AutoMapper;
using core.Dto;
using core.Interface;
using core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace subasta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IRepoProduct _repoProduct;
        private readonly IMapper _mapper;
        protected DtoResponse response;
        public ProductController(IRepoProduct repoProduct, IMapper mapper)
        {
            _repoProduct = repoProduct;
            _mapper = mapper;
            response = new DtoResponse();
        }
        [HttpGet("search/{SearchWord}")]
        public async Task<List<productDto>> Search(string SearchWord)
        {
            return await _repoProduct.SearchProduct(SearchWord);
        }

        [HttpGet("filterByCategory/{id:int}")]
        public async Task<List<productDto>> filterByCategory(int id)
        {
            return await _repoProduct.filterByCategory(id);
        }

        [HttpGet]
        public async Task<List<productDto>> Get()
        {
            return await _repoProduct.GetProduct();
        }

        [HttpGet("{id:int}")]
        public async Task<productDto> GetbyId(int id)
        {
            return await _repoProduct.GetByIdProduct(id);
        }

        [HttpPost]
        public async Task<ActionResult<DtoResponse>> CreateOrUpdate([FromBody] productDto product)
        {
            try
            {
                //product no found, Eliminado, Error tc 
                var resul = await _repoProduct.createOrUpdate(product);
                if (resul == "201")
                {
                    response.isSuccess = true;
                    response.Message = "Se ha creado correctamente el usuario";
                    return Ok(response);
                }

                response.isSuccess = true;
                response.Message = "Se ha actualizado correctamente el usuario";
                return Ok(response);

            }
            catch (Exception ex)
            { 
                response.ErrorMessages = new List<string> { ex.ToString() };
                response.Message = "Error del try catch";
                response.isSuccess = false;
                return BadRequest(response);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<DtoResponse>> DeleteProduct(int id)
        {
            var resul = await _repoProduct.DeleteProduct(id);
            if (resul == "product no found")
            {
                response.isSuccess = false;
                response.Message = "No se ha encontrado el usuario";
                return BadRequest(response);
            }

            if (resul == "Eliminado")
            {
                response.isSuccess = true;
                response.Message = "eliminado correctamente";
                return Ok(response);
            }

            response.Message = "algo salio mal";
            return BadRequest(response);
        }


    }
}
