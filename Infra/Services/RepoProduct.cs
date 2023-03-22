using AutoMapper;
using core.Dto;
using core.Interface;
using core.Models;
using Microsoft.EntityFrameworkCore;
using subasta.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Services
{
    public class RepoProduct : IRepoProduct
    {
        private readonly ApplicationContext _Db;
        private IMapper _mapper;
        public RepoProduct(ApplicationContext db, IMapper mapper)
        {
            _Db = db;
            _mapper = mapper;
        }
        public async Task<string> createOrUpdate(productDto product)
        {
            Product productResul = _mapper.Map<Product>(product);
            if (productResul.ProductId > 0)
            {
                _Db.Product.Update(productResul);

                await _Db.SaveChangesAsync();
                return "updated";
            }

            _Db.Product.Add(productResul);
            await _Db.SaveChangesAsync();
            return "201";

        }

        public async Task<string> DeleteProduct(int id)
        {
            try
            {
                Product product = await _Db.Product.FirstOrDefaultAsync(x => x.ProductId == id);
                if (product == null)
                {
                    return "product no found";
                }

                _Db.Product.Remove(product);
                await _Db.SaveChangesAsync();

                return "Eliminado";
            }
            catch (Exception)
            {
                return "Error tc";
            }
            
        }

        public async Task<List<productDto>> filterByCategory(int id)
        {
            var listProduct = await _Db.Product.Where(x => x.CategoryId == id).ToListAsync();
            return _mapper.Map<List<productDto>>(listProduct);
        }

        public async Task<productDto> GetByIdProduct(int id)
        {
            var product = await _Db.Product.FirstOrDefaultAsync(x => x.ProductId == id);
            return _mapper.Map<productDto>(product);
        }

        public async Task<List<productDto>> GetProduct()
        {
            var listProduct = await _Db.Product.ToListAsync();
            return _mapper.Map<List<productDto>>(listProduct);
        }

        public async Task<List<productDto>> SearchProduct(string searchWord)
        {
            var listProduct = await _Db.Product.Where(x => x.ProductName.ToLower().Contains(searchWord)).ToListAsync();
            return _mapper.Map<List<productDto>>(listProduct);
        }

        
    }
}
