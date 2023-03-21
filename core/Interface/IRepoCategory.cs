using core.Dto;
using core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Interface
{
    public interface IRepoCategory
    {
        Task<List<categoryDto>> GetCategories();
    }
}
