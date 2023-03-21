using core.Dto;
using core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace subasta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IRepoCategory _repoCategory;

        public CategoryController(IRepoCategory repoCategory)
        {
            _repoCategory = repoCategory;
        }

        [HttpGet]
        public Task<List<categoryDto>> getCategories()
        {
            return _repoCategory.GetCategories();
        }
    }
}
