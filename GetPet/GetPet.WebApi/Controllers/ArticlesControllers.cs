using AutoMapper;
using GetPet.BusinessLogic;
using GetPet.BusinessLogic.Model;
using GetPet.BusinessLogic.Repositories;
using GetPet.Data.Entities;
using GetPet.WebApi.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GetPet.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : BaseController
    {
        private readonly ILogger<ArticlesController> _logger;
        private readonly IMapper _mapper;
        private readonly IArticleRepository _articleRepository;
        private readonly IUnitOfWork _unitOfWork;

        #region Ctor

        public ArticlesController(
            ILogger<ArticlesController> logger,
            IMapper mapper,
            IArticleRepository articleRepository,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _articleRepository = articleRepository;
            _unitOfWork = unitOfWork;
        } 

        #endregion
         
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] BaseFilter filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var articles = await _articleRepository.SearchAsync(filter);
            
            return Ok(_mapper.Map<IEnumerable<ArticleDto>>(articles));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var Article = await _articleRepository.GetByIdAsync(id);
            if (Article == null)
            {
                return NotFound();
            }            
            return Ok(_mapper.Map<ArticleDto>(Article));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(ArticleDto Article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var articleToInsert = _mapper.Map<Article>(Article);

            await _articleRepository.AddAsync(articleToInsert);

            await _unitOfWork.SaveChangesAsync();

            return Ok(articleToInsert);
        }

        //[Authorize]
        //[HttpPost]
        //public async Task<IActionResult> Post(ArticleDto Article)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var articleToInsert = _mapper.Map<Article>(Article);

        //    await _articleRepository.AddAsync(articleToInsert);

        //    await _unitOfWork.SaveChangesAsync();

        //    return Ok(articleToInsert);
        //}
    }
}