using core.Dto;
using core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Interface
{
    public interface IRepoProduct
    {
        Task<productDto> GetByIdProduct(int id);
        Task<List<productDto>> GetProduct();

        Task<string> createOrUpdate(productDto product);

        Task<string> DeleteProduct(int id);
        Task<List<productDto>> SearchProduct(string searchWord);

        Task<List<productDto>> filterByCategory(int id);
    }
}
