using AutoMapper;
using core.Dto;
using core.Interface;
using Microsoft.EntityFrameworkCore;
using subasta.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Services
{
    public class RepoCategory : IRepoCategory
    {
        private readonly ApplicationContext _Db;
        private IMapper _mapper;
        public RepoCategory(ApplicationContext db, IMapper mapper)
        {
            _Db = db;
            _mapper = mapper;
        }
        public async Task<List<categoryDto>> GetCategories()
        {
            var listCategory = await _Db.category.Include(x => x.Products).ToListAsync();
            return _mapper.Map<List<categoryDto>>(listCategory);
        }
    }
}
